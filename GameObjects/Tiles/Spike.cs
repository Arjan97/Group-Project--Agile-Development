﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects.Tiles
{
    internal class Spike : Trap
    {
        public Spike(int x, int y) : base(x, y)
        {
                Add(new SpikeTile(x, y));
        }

        public override void CreateButton()
        {
            base.CreateButton();
            button.Position += new Vector2(0,tileSize/2);
        }

        public override void Update(GameTime gameTime)
        {
            //updates the indicator for each spikeTile
            foreach (SpikeTile SpikeTile in Children)
            {
                SpikeTile.indicator.Update(gameTime);
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //displays the indicator for each spikeTile
            foreach (SpikeTile SpikeTile in Children)
            {
                SpikeTile.indicator.Draw(gameTime, spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }
    }
}
