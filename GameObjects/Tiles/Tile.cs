using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects
{
    internal class Tile : SpriteGameObject
    {
        int tileSize = 60;

        Vector2 location;
        public Tile(string assetName, int X, int Y) : base(assetName)
        {
            location = new Vector2(X, Y);
            position.X = X * tileSize;
            position.Y = Y * tileSize + 2*tileSize;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
