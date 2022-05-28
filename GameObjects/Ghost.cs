using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameObjects.Tiles;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
    public class Ghost : AnimatedGameObject
    {
        InputHandler input;

        static int speed = 500;
        static float PushSpeed = 300f;
        static int PushTime = 50;
        int PushTimer;
        static int CooldownTime = 300;
        int CooldownTimer;
        static int maxButtons = 4;
        bool onCooldown = false;
        Keys[] trapButtons;
        public bool stunned = false;
        int stunnedTimer = 0;
        int stunnedTime = 90;


       public Ghost()
        {
            input = GameEnvironment.input;


            id = "Ghost";

            scale = new Vector2(1.5f, 1.5f);
            LoadAnimation("img/players/spr_ghostfly@2x1","fly", true, 0.3f);
            LoadAnimation("img/players/spr_ghoststun@2x1", "stunned", true, 0.3f);
            LoadAnimation("img/players/spr_ghost", "idle", false);

            Reset();
        }

        

        public override void Update(GameTime gameTime)
        {
            float bounce = (float)Math.Sin(gameTime.TotalGameTime.Ticks /  10000);
            if (stunned)
            {
                stunnedTimer++;
                if(stunnedTimer >= stunnedTime)
                {
                    stunned = false;
                    stunnedTimer = 0;
                    
                }
                
            }
            position.Y += bounce;
            base.Update(gameTime);
        }

        public override void Reset()
        {
            position = GameEnvironment.Screen.ToVector2() / 2;
            trapButtons = new Keys[4] { input.Ghost(Buttons.X), input.Ghost(Buttons.Y), input.Ghost(Buttons.A), input.Ghost(Buttons.B) };

            base.Reset();
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
                        if (switchTrap.AssignedKey != Keys.None)
                        {
                            keys.Remove(trap.Value.AssignedKey);
                        }

                        if (switchTrap.AssignedSecondKey != Keys.None)
                        {
                            keys.Remove(switchTrap.AssignedSecondKey);
                        }
                    }
                    else
                    {
                        if (trap.Value.AssignedKey != Keys.None)
                        {
                            keys.Remove(trap.Value.AssignedKey);
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
            if(stunned)
            {
                System.Diagnostics.Debug.Write("stunned");
                return;
            }
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
                            if (switchTrap.AssignedKey == Keys.None && switchTrap.AssignedSecondKey == Keys.None)
                            {
                                //if there are enough keys left the keys will be assigned
                                if (keysLeft >= 2)
                                {
                                    switchTrap.AssignedKey = keys[pos];
                                    keys.Remove(keys[pos]);
                                    switchTrap.AssignedSecondKey = keys[0];
                                    keys.Remove(0);
                                }
                                keysLeft -= 2;
                                break;
                            }
                            if (switchTrap.AssignedSecondKey == Keys.None)
                            {
                                switchTrap.AssignedSecondKey = keys[pos];
                                keys.Remove(keys[pos]);
                                keysLeft--;
                                break;
                            }

                            if (switchTrap.AssignedKey == Keys.None)
                            {
                            switchTrap.AssignedKey = keys[pos];
                            keys.Remove(keys[pos]);
                            keysLeft--;
                            break;
                            }
                        }
                    //default function to assign keys 
                    if (trap.Value.AssignedKey == Keys.None)
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

        //function so ghost won't leave screen
        public void StayOnScreen(Vector2 camPos, Boolean playerOnScreen)
        {
            if(playerOnScreen)
            {
                if (GlobalPosition.X < 0)
                {
                    position.X += 5;
                }
                if (GlobalPosition.X > GameEnvironment.Screen.X)
                {
                    position.X -= 5;
                }
            }
            
        }

        public override void HandleInput(InputHelper inputHelper)
        {

            velocity = Vector2.Zero;
            if(stunned)
            {
                return;
            }
            if (inputHelper.IsKeyDown(input.Ghost(Buttons.left)) && position.X > 0)
            {
                velocity.X = -speed;
                sprite.Mirror = false;
            }
            if (inputHelper.IsKeyDown(input.Ghost(Buttons.right))  && GlobalPosition.X < GameEnvironment.Screen.X - Sprite.Width)
            {
                velocity.X = speed;
                sprite.Mirror = true;
            }
            if (inputHelper.IsKeyDown(input.Ghost(Buttons.down)) && position.Y < GameEnvironment.Screen.Y - Sprite.Height)
            {
                velocity.Y = speed;
            }
            if (inputHelper.IsKeyDown(input.Ghost(Buttons.up)) && position.Y > 0)
            {
                velocity.Y = -speed;
            }

            if(!inputHelper.IsKeyDown(input.Ghost(Buttons.up)) && 
               !inputHelper.IsKeyDown(input.Ghost(Buttons.down)) && 
               !inputHelper.IsKeyDown(input.Ghost(Buttons.right)) && 
               !inputHelper.IsKeyDown(input.Ghost(Buttons.left)))
            {
                PlayAnimation("idle");
            } else
            {
                PlayAnimation("fly");
            }

            //check if ghost is traveling diagonally
            if(velocity.Y != 0 && velocity.X != 0)
            {
                velocity *= 0.75f;
            }

            //HandleAnimation(velocity);
            base.HandleInput(inputHelper);
        }

       //despawns the push entity after a certain time
        public void HandlePush(bool activated, SpriteGameObject push)
        {
            
            if (push.Visible)
            {
                PushTimer++;
                if (PushTimer > PushTime)
                {
                    push.Visible = false;
                    PushTimer = 0;

                }
            }
            //checks if the button is pressed and the ability is not on cooldown
            if (activated && !onCooldown)
            {
                onCooldown = true;
                push.Scale = new Vector2(2, 2);
                push.Position = position -new Vector2(0, sprite.Height/2);
                push.Visible = true;

                //check which direction the hunter is facing
                if (sprite.Mirror)
                {
                    push.Velocity = new Vector2(PushSpeed, 0);
                }
                else
                {
                    push.Velocity = new Vector2(-PushSpeed, 0);
                }

            }

            
            //times the cooldown
            if (onCooldown)
            {
                CooldownTimer++;
                if (CooldownTimer > CooldownTime)
                {
                    onCooldown = false;
                    CooldownTimer = 0;
                }
            }

        }
        private void HandleAnimation(Vector2 velocity)
        {
            if(velocity == Vector2.Zero)
            {
                PlayAnimation("idle");
                return;
            }
            PlayAnimation("fly");
        }

        public void getPushed()
        {
            stunned = true;
            PlayAnimation("stunned");
        }

    }

}
