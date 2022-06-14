using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects
{
    public class Background : SpriteGameObject
    {
        public Background() : base("img/levels/Background",-4)
        {
            
            position.X = GameEnvironment.Screen.X * 2 - 290;
            position.Y = GameEnvironment.Screen.Y / 2;
        }
        

    }
}
