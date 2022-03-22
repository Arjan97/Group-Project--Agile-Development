using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Player_test
{
    public class Entity : Sprite
    {
        public Entity(Texture2D texture)
            : base(texture)
        {

        }

        public override void Update(GameTime gameTime, List<Sprite> sprites)
        {
            Move();
            Gravity(sprites);

            foreach (var sprite in sprites)
            {
                if (sprite == this)
                    continue;

                if (this.Velocity.X > 0 && this.isTouchingLeft(sprite) ||
                    this.Velocity.X < 0 && this.isTouchingRight(sprite))
                    this.Velocity.X = 0;

                if (this.Velocity.Y > 0 && this.isTouchingTop(sprite) ||
                    this.Velocity.Y < 0 && this.isTouchingBottom(sprite))
                    this.Velocity.Y = 0;
            }
            Position += Velocity;
            Velocity = Vector2.Zero;
        }

        private void Move()
        {
            if (Keyboard.GetState().IsKeyDown(Input.Left))
                Velocity.X = -Speed;
            else if (Keyboard.GetState().IsKeyDown(Input.Right))
                Velocity.X = Speed;
            if (Keyboard.GetState().IsKeyDown(Input.Down))
                Velocity.Y = Speed;

            if (Keyboard.GetState().IsKeyDown(Input.Jump) && hasJumped == false)
            {
                Position.Y -= 100f;
                Velocity.Y = -50f;
                hasJumped = true;
            }
        }

        public void Gravity(List<Sprite> sprites)
        {
            float i = 1;
            Velocity.Y += 4 * i;

            foreach (var sprite in sprites)
                if (this.isTouchingTop(sprite))
                {
                    hasJumped = false;
                }

            if (hasGravity == false)
            {
                Velocity.Y = 0;
            }

            if (Position.Y >= 500)
            {
                Respawn();
            }
        }

        public void Respawn()
        {
            Position = new Vector2(100, 100);
        }
    }
}
