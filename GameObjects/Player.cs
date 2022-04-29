using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using BaseProject.GameObjects.Tiles;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.GameObjects
{
    public class Player : SpriteGameObject
    {
        public bool hasJumped;
        public float speed;
        public float jumpSpeed;
        public bool isFalling;
        public Vector2 pVelocity;
        public bool isColliding;
        public bool keyPressed;
        public string HorizontalCollidingSide, verticalCollidingSide;
        public bool horizontalCollision, verticalCollision;
        public bool isGrounded;
        public bool isJumping;
        public int jumpframes;
        public bool jumpKeyPressed;
        public bool died;
        private float timer;


        public Player() : base("img/players/spr_player")
        {
            keyPressed = false;
            pVelocity = velocity;
            isFalling = true;
            isColliding = false;
            hasJumped = false;
            died = false;
            jumpSpeed = 100f;
            speed = 5f;
            Origin = Center;
            jumpframes = 0;
            timer = 0;
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
            position.X = GameEnvironment.Screen.X / 7;
            position.Y = GameEnvironment.Screen.Y / 2;
            Velocity = Vector2.Zero;
            died=false;
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
            
            float i = 1;
            //velocity.Y += 2 * i;

            if(position.Y > GameEnvironment.Screen.Y)
            {
                death();
            }
            if (isJumping && jumpframes < 30 && jumpKeyPressed && (verticalCollidingSide != "up"))
            {
                if (jumpframes == 1)
                {
                    velocity.Y -= 60;
                }
                else if (jumpframes < 10)
                {
                    velocity.Y -= 34;
                }
                else
                {
                    velocity.Y -= 12;
                }


                jumpframes++;
            }
            else
            {
                jumpframes = 1;
                isJumping = false;
            }




            jumpframes++;

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

            if (inputHelper.IsKeyDown(Keys.Left))
            {
                velocity.X = -speed;
                Player testPlayer = new Player();
                testPlayer.position = testPlayer.position += velocity;
            }

            else if (inputHelper.IsKeyDown(Keys.Right))
            {
                velocity.X = speed;

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
            if (tile is SpikeTile || tile is SpikeRoofTile)
            {
                timer++;
                if(timer == 18)
                {
                    death();
                    timer = 0;
                }
                
            }
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

            if (Math.Abs(intersection.X) > Math.Abs(intersection.Y))
            {
               
                if (intersection.Y < 0)
                {
                    isColliding = true;
                    isGrounded = true;
                    verticalCollidingSide = "down";
                    position.Y -= Math.Abs(intersection.Y) - 1;
                    
                }
                else
                {
                    isColliding = true;
                    //System.Diagnostics.Debug.WriteLine("up");
                    verticalCollidingSide = "up";

                }
            }
            else
            {
                if (intersection.X < 0)
                {
                    isColliding = true;
                    HorizontalCollidingSide = "right";
                    horizontalCollision = true;
                    position.X -= Math.Abs(intersection.X);
                }
                else
                {
                    isColliding = true;
                    HorizontalCollidingSide = "left";
                    horizontalCollision = true;
                    position.X += Math.Abs(intersection.X);
                }
                // System.Diagnostics.Debug.WriteLine(collidingSide);
            }
        }
        void death()
        {
           Reset();            
           died = true;
        }
    }
}
