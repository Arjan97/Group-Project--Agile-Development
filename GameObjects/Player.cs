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
            velocity.Y += 2 * i;

            if (isColliding)
            {
                keyPressed = false;
                //hasJumped = false;
                velocity.Y = 0;

                isColliding = false;
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
                velocity.X = -speed;
            else if (inputHelper.IsKeyDown(Keys.Right)) 
                velocity.X = speed;
            if (inputHelper.IsKeyDown(Keys.Up) && isColliding)
            {
                isColliding = false;
                keyPressed = true;
                //position.Y -= jumpSpeed;
                velocity.Y = -jumpSpeed;
               // hasJumped = true;
                //isFalling = true;
            }

            // if (inputHelper.IsKeyDown(Keys.Down)) 
            //   velocity.Y = speed;
            //Position += Velocity;

        }

        public void HandleColission(Tile tile)
        {
            Vector2 intersection = Collision.CalculateIntersectionDepth(BoundingBox,  tile.BoundingBox);
            System.Diagnostics.Debug.WriteLine(intersection.X + " " + intersection.Y);


            if(Math.Abs(intersection.X)> Math.Abs(intersection.Y))
            {
                if(intersection.Y < 0)
                {
                    isColliding = true;
                    System.Diagnostics.Debug.WriteLine("up");
                }
                else
                {
                    isColliding = true;
                    System.Diagnostics.Debug.WriteLine("down");
                }
            }
            else
            {
                if(intersection.X < 0)
                {
                    isColliding = true;
                    System.Diagnostics.Debug.WriteLine("left");
                }
                else
                {
                    isColliding = true;
                    System.Diagnostics.Debug.WriteLine("right");
                }
            }
        }
    }
}
