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
    public class Player : AnimatedGameObject
    {
        public float speed, jumpSpeed;
        public bool isFalling, isColliding, keyPressed, isGrounded, isJumping, jumpKeyPressed, died, blockMovement;
        public Vector2 pVelocity;
        public string verticalCollidingSide;
        public int jumpframes, blockedframes;
        private float timer;

        public bool isDashing;
        private float dashDuration;
        public int dashPower;
        public bool facingLeft;
        public bool isFacingRight;

        public Player() : base(Game1.Depth_Player)
        {
            LoadAnimation("img/players/spr_player_idle@8", "idle", true, 0.1f);
            LoadAnimation("img/players/spr_player_run@4", "run", true, 0.4f);
            LoadAnimation("img/players/spr_player_jump@2", "jump", true, 0.5f);

            PlayAnimation("idle");
            SetOriginToBottomCenter();

            keyPressed = false;
            pVelocity = velocity;
            isFalling = true;
            isColliding = false;
            died = false;
            jumpSpeed = 100f;
            speed = 5f;
            jumpframes = 0;
            timer = 0;

            //Player dash ability 
            isDashing = false;
            dashDuration = 0;
            dashPower = 30;
            isFacingLeft = false; //Checks if the player is facing left, used for the player dash and animation

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
                PlayAnimation("jump");
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

            if(blockMovement)
            {
                blockedframes++;
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
            //code to block all movement
            if(blockMovement)
            {
                if(blockedframes == 15)
                {
                    blockMovement = false;
                    blockedframes = 0;
                }
                //possible 'stunned' animation
                PlayAnimation("idle");
                return;
            }

            base.HandleInput(inputHelper);

            //Player Dash ability
            if (inputHelper.IsKeyDown(Keys.LeftShift))
            {
                isDashing = true;
                dashDuration++;

                if (dashDuration <= 10 && !facingLeft)
                {
                    velocity.X += dashPower;
                }
                else if (dashDuration <= 10 && facingLeft)
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
                velocity.X = -speed;
                PlayAnimation("run");
                facingLeft = true;
            }

            else if (inputHelper.IsKeyDown(Keys.Right))
            {
                velocity.X = speed;
                facingLeft = false;
                PlayAnimation("run");
                
            } else
            {
                PlayAnimation("idle");
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

            sprite.Mirror = facingLeft;

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
                    position.X -= Math.Abs(intersection.X) -1;
                }
                //collision left side player and right side tile
                else
                {
                    isColliding = true;
                    position.X += Math.Abs(intersection.X) +1;
                }

                //blocking movement when player keeps colliding with wall.
                if(intersection.X > 8 || intersection.X < -8)
                {
                    blockMovement = true;
                }
            }
        }

        //function to handle the death of a player
        void death()
        {
            //TODO death annimation
           Reset();            
           died = true;
            PlayingState play =(PlayingState) GameEnvironment.GameStateManager.GetGameState("playingState");
            play.tileList.nextLevelNr = 0;
        }

        void SetOriginToBottomCenter()
        {
            Origin = new Vector2(sprite.Width / 2, sprite.Height);
        }
    }
}
