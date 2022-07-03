using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects
{
    public class Background : SpriteGameObject
    {
        public Background() : base("img/levels/Background@2",-4)
        {
            
            position.X = GameEnvironment.Screen.X * 2 - 290;
            position.Y = GameEnvironment.Screen.Y / 2;
        }
        
        public void loadbackground(int levelNr)
        {
            if(levelNr == 0)
            {
                sprite.SheetIndex = 0;
            }
            if(levelNr !=0)
            {
                sprite.SheetIndex = 1;
            }
        }

    }
}
