using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameObjects.Tiles;

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

        public void SetGhostDistance(TileList tiles)
        {
            System.Diagnostics.Debug.WriteLine("start");
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
                            trapsList.Add(trap.ghostDistance, trap); 
                        }catch (System.ArgumentException)
                        {
                            trap.ghostDistance += 10.789f;
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
                }
                else //clears all traps that are out of reach
                {
                    trap.Value.AssignedKey = Keys.None;
                }
            }
             return keys;
        }
        //function that gives the keys to the traps
        private void AssignKeys(SortedDictionary<float, Trap> traplist, List<Keys> keys)
        {
            foreach (Keys key in keys)
            {
                //loops thru all the traps till it finds one that doesn't have a key assigned yet
                foreach (KeyValuePair<float, Trap> trap in traplist)
                {
                    if(trap.Value.AssignedKey == Keys.None)
                    {
                        trap.Value.AssignedKey = key;
                        break;
                    }
                }
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
            if (inputHelper.IsKeyDown(Keys.L)  && position.X < GameEnvironment.Screen.X - Sprite.Width)
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
