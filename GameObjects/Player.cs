using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
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
        Vector2 startPosition;
        public float jumpSpeed;
        public bool isFalling;
        public Vector2 pVelocity;
        public bool isColliding;
        public bool keyPressed;
        public string collidingSide;
        public bool horizontalCollision;
        public bool isGrounded;
        public bool isJumping;
        public int jumpframes;
        public bool jumpKeyPressed;
        public string disabledSide;



        public Player() : base("player/spr_player")
        {
            keyPressed = false;
            pVelocity = velocity;
            isFalling = true;
            isColliding = false;
            hasJumped = false;
            startPosition = GameEnvironment.Screen.ToVector2() / 2;
            jumpSpeed = 100f;
            speed = 5f;
            Origin = Center;
            jumpframes = 0;
            Reset();

        }

        public override void Reset()
        {
            base.Reset();
            position = startPosition;
            Velocity = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            float i = 1;
            //velocity.Y += 2 * i;

            if (isColliding)
            {
                keyPressed = false;
                velocity.Y = 0;
                isColliding = false;

                if (horizontalCollision)
                {
                    switch (collidingSide)
                    {
                        case "left":
                            //System.Diagnostics.Debug.WriteLine("left");
                            velocity.X = 3;
                            horizontalCollision = false;
                            disabledSide = "left";
                            break;
                        case "right":
                            velocity.X = -3;
                            //System.Diagnostics.Debug.WriteLine("right");
                            horizontalCollision = false;
                            disabledSide = "right";
                            break;
                        default:
                            break;

                    }
                    horizontalCollision = false;
                }
     
            } else if(isGrounded){
                disabledSide = "none";
            }

            if (isJumping && jumpframes < 15 && jumpKeyPressed)
            {
                if(jumpframes == 1)
                {
                    velocity.Y += -15;
                }  else if(jumpframes < 5)
                {
                    velocity.Y += -12;
                } else
                {
                    velocity.Y += -9;
                }


                jumpframes++;
            }
            else
            {
                if (!isGrounded)
                {
                    velocity.Y += 2 * i;
                    //System.Diagnostics.Debug.WriteLine("valt");

                }
                jumpframes = 1;
                isJumping = false;
            }
            position += velocity;
            Velocity = Vector2.Zero;

          //  GameEnvironment.cameraPos = new Vector3((position.X - 640) * -1, 0, 0f);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);


        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            System.Diagnostics.Debug.WriteLine("side: " + disabledSide);
            if (inputHelper.IsKeyDown(Keys.Left) && horizontalCollision == false && disabledSide != "left")
            {
                if (disabledSide == "right")
                {
                    disabledSide = "none";
                }
                //System.Diagnostics.Debug.WriteLine(horizontalCollision);
                velocity.X = -speed;
            }

            else if (inputHelper.IsKeyDown(Keys.Right) && disabledSide != "right")
            {
                velocity.X = speed;
                if(disabledSide == "left")
                {
                    disabledSide = "none";
                }
            }
            if (inputHelper.IsKeyDown(Keys.Up) && isGrounded)
            {
                isColliding = false;
                keyPressed = true;
                //position.Y -= jumpSpeed;
                //velocity.Y = -jumpSpeed;
                isJumping = true;
                // hasJumped = true;
                //isFalling = true;
                jumpKeyPressed = true;

            }
            else if (!inputHelper.IsKeyDown(Keys.Up))
            {
                jumpKeyPressed = false;
            }

            // if (inputHelper.IsKeyDown(Keys.Down)) 
            //   velocity.Y = speed;
            //Position += Velocity;

        }

        public void HandleColission(Tile tile)
        {

            Vector2 intersection = Collision.CalculateIntersectionDepth(BoundingBox, tile.BoundingBox);
            //System.Diagnostics.Debug.WriteLine(intersection.X + " " + intersection.Y);


            if (Math.Abs(intersection.X) > Math.Abs(intersection.Y))
            {
                if (intersection.Y < 0)
                {
                    isColliding = true;
                    // System.Diagnostics.Debug.WriteLine("up");
                    isGrounded = true;
                }
                else
                {
                    isColliding = true;
                    // System.Diagnostics.Debug.WriteLine("down");
                }
            }
            else
            {
                if (intersection.X < 0)
                {
                    isColliding = true;
                    collidingSide = "right";
                    horizontalCollision = true;
                }
                else
                {
                    isColliding = true;
                    collidingSide = "left";
                    horizontalCollision = true;
                }
                // System.Diagnostics.Debug.WriteLine(collidingSide);
            }
        }
    }
}
