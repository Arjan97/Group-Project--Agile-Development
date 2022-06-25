using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects
{
    public class Background : SpriteGameObject
    {
        public Background() : base("Background", -4)
        {

            position.X = GameEnvironment.Screen.X * 2 - 290;
            position.Y = (GameEnvironment.Screen.Y / 2) + 30;
        }


    }
}
