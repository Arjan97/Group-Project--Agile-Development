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
      

        public Player() : base("player/spr_player")
        {
            pVelocity = velocity;
            isFalling = true;
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

            if (!isFalling)
            {
                velocity.Y = 0;
            }
           
            position += velocity;
            Velocity = Vector2.Zero;
        }

        public void Draw()
        {

        }
        public override void HandleInput(InputHelper inputHelper)
        {
            base.HandleInput(inputHelper);

            if (inputHelper.IsKeyDown(Keys.Left)) 
                velocity.X = -speed;
            else if (inputHelper.IsKeyDown(Keys.Right)) 
                velocity.X = speed;
            if (inputHelper.IsKeyDown(Keys.Up) && hasJumped == false)
            {
                position.Y -= jumpSpeed;
                velocity.Y = -jumpSpeed/2;
                hasJumped = true;
                isFalling = true;
            } 
                
           // if (inputHelper.IsKeyDown(Keys.Down)) 
             //   velocity.Y = speed;
             //Position += Velocity;
           
        }

        
    }
}
