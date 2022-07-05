using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Input = Microsoft.Xna.Framework.Input;
using BaseProject.GameObjects.Tiles;
using BaseProject.GameComponents;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects
{
    public class Ghost : AnimatedGameObject
    {
        InputHandler input;//this makes sure the ghost listens to the right input

        static int speed = 500;//int that sets the ghost its speed

        //variables used for the push ability
        static int PushTime = 100;//int used to set the time the push is active
        int PushTimer;//timer that is used to time the push active time
        static float PushSpeed = 300f;//float that sets the pushObjects speed

        //variables for the push cooldown
        static int CooldownTime = 300;//int used to set the time between pushes
        int CooldownTimer;//timer that is used to disable the push for a certain time
        bool onCooldown = false;//bool that says if the ghost is on cooldown
        Cooldown pushCooldown;

        //variables that are used for the input to activate the traps
        static int maxButtons = 4;//number of the amount of traps a ghost can activate at once
        Input.Keys[] trapButtons;//array that stores the keys the ghost can press to activate a trap

        //variables used for the ghost stun
        public bool stunned = false;//bool that keeps track if the ghost is stunned
        int stunnedTimer = 0;//timer that tracks how long the ghost is stunned
        int stunnedTime = 90;//max stun stime


       public Ghost()
        {
            input = GameEnvironment.input;

            pushCooldown = new Cooldown("img/icon/spr_push", parent, new Vector2(GameEnvironment.Screen.X - 100, 80));
            id = "Ghost";

            scale = new Vector2(1.5f, 1.5f);
            LoadAnimation("img/players/spr_ghostfly@2x1","fly", true, 0.3f);
            LoadAnimation("img/players/spr_ghoststun@2x1", "stunned", true, 0.3f);
            LoadAnimation("img/players/spr_ghost", "idle", false);

            Reset();
        }

        

        public override void Update(GameTime gameTime)
        {
            if (stunned)
            {
                stunnedTimer++;
                if(stunnedTimer >= stunnedTime)
                {
                    stunned = false;
                    stunnedTimer = 0;
                    
                }
                
            }

            pushCooldown.Update((CooldownTimer*100/CooldownTime), gameTime);
            //adds a small bounce effect for the ghost
            float bounce = (float)Math.Sin(gameTime.TotalGameTime.Ticks /  10000);
            position.Y += bounce;

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            pushCooldown.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }

        /// <summary>
        /// repositions the ghost and updates which keys the ghost has to listen to
        /// </summary>
        /// <returns>void</returns>
        public override void Reset()
        {
            position = GameEnvironment.Screen.ToVector2() / 2;
            trapButtons = new Input.Keys[4] { input.Ghost(Buttons.X), input.Ghost(Buttons.Y), input.Ghost(Buttons.A), input.Ghost(Buttons.B) };
            base.Reset();
        }
        

        /// <summary>
        /// function that assings the traps to the keys
        /// </summary>
        /// <param name="tiles">list of tiles where the traps will be searched in</param>
        /// <returns>void</returns>
        public void SetGhostTraps(TileList tiles)
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

           List<Input.Keys> freeKeys = UnassignKeys(trapsList);

            if(freeKeys.Count > 0)
            {
                AssignKeys(trapsList, freeKeys);
            }
            
        }

        /// <summary>
        /// function to clear the keys of objects too far away, returns a list of keys that are now available
        /// </summary>
        /// <param name="traplist">dictionary of all the traps and their distance sorted by distance</param>
        /// <returns>list of keys that have no trap assigned</returns>
        private List<Input.Keys> UnassignKeys(SortedDictionary<float,Trap> traplist)
        {
            //creates a list of available keys
            List<Input.Keys> keys = new List<Input.Keys>(trapButtons);
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
                    if(trap.Value.AssignedKey != Input.Keys.None)
                    {
                        keys.Remove(trap.Value.AssignedKey);
                    }
                    if(trap.Value is Switch)
                    {
                        Switch switchTrap = (Switch)trap.Value;
                        if (switchTrap.AssignedKey != Input.Keys.None)
                        {
                            keys.Remove(trap.Value.AssignedKey);
                        }

                        if (switchTrap.AssignedSecondKey != Input.Keys.None)
                        {
                            keys.Remove(switchTrap.AssignedSecondKey);
                        }
                    }
                    else
                    {
                        if (trap.Value.AssignedKey != Input.Keys.None)
                        {
                            keys.Remove(trap.Value.AssignedKey);
                        }
                    }
                }
                else //clears all traps that are out of reach
                {
                    trap.Value.AssignedKey = Input.Keys.None;
                    if(trap.Value is Switch)
                    {
                        Switch switchTrap = (Switch)trap.Value;
                        switchTrap.AssignedSecondKey = Input.Keys.None;
                    }
                }
            }
             return keys;
        }

        /// <summary>
        /// function that gives the left over keys to the closest traps
        /// </summary>
        /// <param name="traplist">sorted dictionary of traps with their distance, sorted by distance</param>
        /// <param name="keys">list of keys that don't have a trap assingned</param>
        /// <returns>void</returns>
        private void AssignKeys(SortedDictionary<float, Trap> traplist, List<Input.Keys> keys)
        {
            if(stunned)
            {
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
                            if (switchTrap.AssignedKey == Input.Keys.None && switchTrap.AssignedSecondKey == Input.Keys.None)
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
                            if (switchTrap.AssignedSecondKey == Input.Keys.None)
                            {
                                switchTrap.AssignedSecondKey = keys[pos];
                                keys.Remove(keys[pos]);
                                keysLeft--;
                                break;
                            }

                            if (switchTrap.AssignedKey == Input.Keys.None)
                            {
                            switchTrap.AssignedKey = keys[pos];
                            keys.Remove(keys[pos]);
                            keysLeft--;
                            break;
                            }
                        }
                    //default function to assign keys 
                    if (trap.Value.AssignedKey == Input.Keys.None)
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

        /// <summary>
        /// function that makes sure the ghost is not of screen
        /// </summary>
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

            //disables movement when stunned
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

            //plays idle animation when no keys are pressed
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

            base.HandleInput(inputHelper);
        }

       /// <summary>
       /// function that spawns the push when button is pressed and despawns the push after it is expired
       /// </summary>
       /// <param name="activated">bool if the button is pressed</param>
       /// <param name="push">the push object</param>
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

        /// <summary>
        /// function that gets called when colliding with the player their push projectile and handles the stun
        /// </summary>
        public void GetStunned()
        {
            stunned = true;
            PlayAnimation("stunned");
        }

    }

}
