using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using BaseProject.GameObjects.Tiles;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
    public class Player : SpriteGameObject
    {
        public float speed;
        public float jumpSpeed;
        public bool isFalling;
        public Vector2 pVelocity;
        public bool isColliding;
        public bool keyPressed;
        public string verticalCollidingSide;
        public bool isGrounded;
        public bool isJumping;
        public int jumpframes;
        public bool jumpKeyPressed;
        public bool died;
        private float timer;

        public bool isDashing;
        private float dashDuration;
        public int dashPower;
        public bool isFacingLeft;
        public bool isFacingRight;



        public Player() : base("img/players/spr_player")
        {
            keyPressed = false;
            pVelocity = velocity;
            isFalling = true;
            isColliding = false;
            died = false;
            jumpSpeed = 100f;
            speed = 5f;
            Origin = Center;
            jumpframes = 0;
            timer = 0;

            //Player dash ability 
            isDashing = false;
            dashDuration = 0;
            dashPower = 30;
            isFacingLeft = false; //Checks if the player is facing left, used for the player dash

            Reset();
        }

        public override void HandleColission(GameObject obj)
        {
            if(obj is Spike)
            {

            }
            base.HandleColission(obj);
        }

        public override void Reset()
        {
            base.Reset();
            position.X = 1.5f*Tile.tileSize;
            position.Y = GameEnvironment.Screen.Y / 2;
            Velocity = Vector2.Zero;
            died=false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            
            float i = 1;

            //dying when falling of the map
            if(position.Y > GameEnvironment.Screen.Y)
            {
                death();
            }

            //checking different stages of the jump
            if (isJumping && jumpframes < 30 && jumpKeyPressed && (verticalCollidingSide != "up"))
            {
                if (jumpframes == 1)
                {
                    velocity.Y -= 30;
                }
                else if (jumpframes < 10)
                {
                    velocity.Y -= 17;
                }
                else
                {
                    velocity.Y -= 6;
                }

                jumpframes++;
            }
            else
            {
                jumpframes = 1;
                isJumping = false;
            }
            
            //adding gravity
            if (!isGrounded)
            {
                velocity.Y += 4.5f * i;
            }

            position += velocity;
            Velocity = Vector2.Zero;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            //Player Dash ability
            if (inputHelper.IsKeyDown(Keys.LeftShift))
            {
                isDashing = true;
                dashDuration++;

                if (dashDuration <= 10 && !isFacingLeft)
                {
                    velocity.X += dashPower;
                }
                else if (dashDuration <= 10 && isFacingLeft)
                {
                    System.Diagnostics.Debug.WriteLine(2);
                    velocity.X += -dashPower;
                }
            }
            //Checks if the player is dashing, then a cooldown is issued
            if (isDashing){
                timer++;
                if (timer >= 200)
                {
                    dashDuration = 0;
                    timer = 0;
                    isDashing=false;
                }
            }

            if (inputHelper.IsKeyDown(Keys.Left))
            {
                velocity.X += -speed;
                Player testPlayer = new Player();
                testPlayer.position = testPlayer.position += velocity;
                isFacingLeft = true;
            }

            else if (inputHelper.IsKeyDown(Keys.Right))
            {
                velocity.X += speed;
                isFacingLeft = false;
            }


            if (!inputHelper.IsKeyDown(Keys.Left))
            {
                isFacingLeft = false;
            }

            if (inputHelper.IsKeyDown(Keys.Up) && isGrounded)
            {
                isColliding = false;
                keyPressed = true;
                isJumping = true;
                jumpKeyPressed = true;
            }
            else if (!inputHelper.IsKeyDown(Keys.Up))
            {
                jumpKeyPressed = false;
            }

        }

        public void HandleColission(Tile tile)
        {
            //checking and handling collision with SpikeTile
            if (tile is SpikeTile || tile is SpikeRoofTile)
            {
                timer++;
                if(timer == 18)
                {
                    death();
                    timer = 0;
                }
                
            }
            //checking and handling collision with SwitchTile
            if(tile is SwitchTile)
            {
              SwitchObject switchTile = (SwitchObject)tile.Parent;
                if (switchTile.Armed)
                {
                    timer++;
                    if (timer == 20)
                    {
                        death();
                        timer = 0;
                    }

                }
            }
            
            Vector2 intersection = Collision.CalculateIntersectionDepth(BoundingBox, tile.BoundingBox);

            //checking if its a vertical collision
            if (Math.Abs(intersection.X) > Math.Abs(intersection.Y))
            {
               
                //collision bottom side player and top side tile
                if (intersection.Y < 0)
                {
                    isColliding = true;
                    isGrounded = true;
                    verticalCollidingSide = "down";
                    position.Y -= Math.Abs(intersection.Y) - 1;
                    
                }
                //collision top side player and bottom side tile
                else
                {
                    isColliding = true;
                    verticalCollidingSide = "up";

                }
            }
            else
            {
                //collision right side player and left side tile
                if (intersection.X < 0)
                {
                    isColliding = true;
                    position.X -= Math.Abs(intersection.X);
                }
                //collision left side player and right side tile
                else
                {
                    isColliding = true;
                    position.X += Math.Abs(intersection.X);
                }
            }
        }

        //function to handle the death of a player
        void death()
        {
           Reset();            
           died = true;
            PlayingState play =(PlayingState) GameEnvironment.GameStateManager.GetGameState("playingState");
            play.tileList.nextLevelNr = 0;
        }
    }
}
