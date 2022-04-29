using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects
{
    public class Tile : SpriteGameObject
    {
        public bool moving = false;
       public static int tileSize = 60;

        public Vector2 location;
        public Tile(string assetName, int X, int Y) : base(assetName, -1)
        {
            location = new Vector2(X, Y);
            position.X = X * tileSize;
            position.Y = Y * tileSize + tileSize;
        }
    }
}
