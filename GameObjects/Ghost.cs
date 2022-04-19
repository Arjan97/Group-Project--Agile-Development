using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameObjects.Tiles;
using System.Linq;

namespace BaseProject.GameObjects
{
    public class Ghost : SpriteGameObject
    {
        int speed = 240;
        static int maxButtons = 4;
        Keys[] trapButtons = {Keys.NumPad8, Keys.NumPad4, Keys.NumPad5, Keys.NumPad6};

       public Ghost(): base ("img/players/spr_ghost")
        {
            id = "Ghost";
            position.X = GameEnvironment.Screen.X / 2;
            position.Y = GameEnvironment.Screen.Y / 2;
            scale = 1.5f;
        }

        //function to calculate trap distance and assign keys
        public void SetGhostDistance(TileList tiles)
        {
            //dictionary is used to store all the traps together with their distance
            SortedDictionary<float, Trap> trapsList = new SortedDictionary<float, Trap>();
            //searches all the traps from current level
            foreach (GameObject obj in tiles.Children)
            {
                if (obj is Trap)
                {
                    Trap trap = (Trap)obj;
                    if (!trap.Activated)
                    {
                        //calculates the distance between ghost and object System.ArgumentException
                        trap.ghostDistance = Math.Abs(this.Position.X - trap.buttonPosition.X) + Math.Abs(this.Position.Y - trap.buttonPosition.Y);
                        try { 
                            trapsList.Add(trap.ghostDistance + (float)GameEnvironment.Random.NextDouble(), trap); 
                        }catch (ArgumentException)
                        {
                            trap.ghostDistance += (float)GameEnvironment.Random.NextDouble();
                            trapsList.Add(trap.ghostDistance, trap);
                        }
                    }
                }
            }

           List<Keys> freeKeys = UnassignKeys(trapsList);

            if(freeKeys.Count > 0)
            {
                AssignKeys(trapsList, freeKeys);
            }
            System.Diagnostics.Debug.WriteLine(trapsList.Count());
            
        }

        //function to clear the keys of objects too far away, returns a list of keys that are now available
        private List<Keys> UnassignKeys(SortedDictionary<float,Trap> traplist)
        {
            //creates a list of available keys
            List<Keys> keys = new List<Keys>(trapButtons);
            int buttonCounter = 0;

            
             foreach (KeyValuePair<float, Trap> trap in traplist)
            {
                //switch traps use 2 buttons
                if (trap.Value is Switch)
                {
                    buttonCounter++;
                }

                buttonCounter++;

                //check if current trap is one of the closest
                if (buttonCounter <= maxButtons)
                {
                    //deletes already assigned buttons from the list
                    if(trap.Value.AssignedKey != Keys.None)
                    {
                        keys.Remove(trap.Value.AssignedKey);
                    }
                    if(trap.Value is Switch)
                    {
                        Switch switchTrap = (Switch)trap.Value;
                        if (switchTrap.AssignedSecondKey != Keys.None)
                        {
                            keys.Remove(switchTrap.AssignedSecondKey);
                        }
                    }
                }
                else //clears all traps that are out of reach
                {
                    trap.Value.AssignedKey = Keys.None;
                    if(trap.Value is Switch)
                    {
                        Switch switchTrap = (Switch)trap.Value;
                        switchTrap.AssignedSecondKey = Keys.None;
                    }
                }
            }
             return keys;
        }

        //function that gives the keys to the traps
        private void AssignKeys(SortedDictionary<float, Trap> traplist, List<Keys> keys)
        {
            int keysLeft = keys.Count;
            while(keysLeft > 0)
            {
                //loops thru all the traps till it finds one that doesn't have a key assigned yet
                foreach (KeyValuePair<float, Trap> trap in traplist)
                {
                    int pos = GameEnvironment.Random.Next(0, keysLeft - 1);
                    if (trap.Value is Switch)
                    {
                        Switch switchTrap = (Switch)trap.Value;

                        //check if both traps dont have keys
                        if(switchTrap.AssignedKey == Keys.None && switchTrap.AssignedSecondKey == Keys.None)
                        {
                            //if there are enough keys left the keys will be assigned
                            if(keysLeft >= 2)
                            {

                                switchTrap.AssignedKey = keys[pos];
                                keys.Remove(keys[pos]);
                                switchTrap.AssignedSecondKey = keys[0];
                                keys.Remove(0);
                            }
                            keysLeft -= 2;
                            break;
                        }
                        if(switchTrap.AssignedSecondKey == Keys.None)
                        {
                            switchTrap.AssignedSecondKey = keys[pos];
                            keys.Remove(keys[pos]);
                            keysLeft--;
                            break;
                        }
                    }
                    //default function to assign keys 
                    if(trap.Value.AssignedKey == Keys.None)
                    {
                        trap.Value.AssignedKey = keys[pos];
                        keys.Remove(keys[pos]);
                        keysLeft--;
                        break;
                    }
                }
                //breaks the while loop if there are no more traps who need keys
                break;
            }
        }
        public void StayOnScreen(Vector2 camPos)
        {
            if(GlobalPosition.X < 0)
            {
                position.X+=5;
            }
            if(GlobalPosition.X > GameEnvironment.Screen.X)
            {
                position.X -=5;
            }
        }
        public override void HandleInput(InputHelper inputHelper)
        {
            velocity = Vector2.Zero;
            if (inputHelper.IsKeyDown(Keys.J) && position.X > 0)
            {
                velocity.X = -speed;
                sprite.Mirror = false;
            }
            if (inputHelper.IsKeyDown(Keys.L)  && GlobalPosition.X < GameEnvironment.Screen.X - Sprite.Width)
            {
                velocity.X = speed;
                sprite.Mirror = true;
            }
            if (inputHelper.IsKeyDown(Keys.K) && position.Y < GameEnvironment.Screen.Y - Sprite.Height)
            {
                velocity.Y = speed;
            }
            if (inputHelper.IsKeyDown(Keys.I) && position.Y > 0)
            {
                velocity.Y = -speed;
            }

            //check if ghost is traveling diagonally
            if(velocity.Y != 0 && velocity.X != 0)
            {
                velocity *= 0.75f;
            }
            base.HandleInput(inputHelper);
        }
    }
}
