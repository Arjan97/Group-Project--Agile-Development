using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameObjects
{
    internal class Ghost : SpriteGameObject
    {
        int speed = 240;
       public Ghost(): base ("img/players/spr_ghost")
        {
            id = "Ghost";
            position.X = GameEnvironment.Screen.X / 2;
            position.Y = GameEnvironment.Screen.Y / 2;
            scale = 1.5f;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            velocity = Vector2.Zero;
            if (inputHelper.IsKeyDown(Keys.J) && position.X > 0)
            {
                velocity.X = -speed;
                sprite.Mirror = false;
            }
            if (inputHelper.IsKeyDown(Keys.L) && position.X < GameEnvironment.Screen.X - Sprite.Width)
            {
                velocity.X = speed;
                sprite.Mirror = true;
            }
            if (inputHelper.IsKeyDown(Keys.K) && position.Y < GameEnvironment.Screen.Y - Sprite.Height)
            {
                velocity.Y = speed;
            }
            if (inputHelper.IsKeyDown(Keys.I) && position.Y > 0)
            {
                velocity.Y = -speed;
            }

            if(velocity.Y != 0 && velocity.X != 0)
            {
                velocity *= 0.75f;
            }
            base.HandleInput(inputHelper);
        }
    }
}
