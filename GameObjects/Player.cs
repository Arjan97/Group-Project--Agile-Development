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
        public string HorizontalCollidingSide, verticalCollidingSide;
        public bool horizontalCollision, verticalCollision;
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
            if (isJumping && jumpframes < 30 && jumpKeyPressed && (verticalCollidingSide != "up"))
            {
                if(jumpframes == 1)
                {
                    velocity.Y -= 30;
                }  else if(jumpframes < 10)
                {
                    velocity.Y -= 15;
                } else
                {
                    velocity.Y -= 5;
                }


                jumpframes++;
            }
            else
            {
                jumpframes = 1;
                isJumping = false;
            }

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
    }
}
