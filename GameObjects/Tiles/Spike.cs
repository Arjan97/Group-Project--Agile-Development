using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects.Tiles
{
    internal class Spike : Trap
    {
        public Spike(int x, int y, int length) : base(x, y, length)
        {
            //turns spiketiles that are connected into a spike
            for (int i = 0; i < length; i++)
            {
                Add(new SpikeTile(x + i, y));
            }
            button.Position += new Vector2(0,tileSize/2);
        }

        public override void Update(GameTime gameTime)
        {
            //updates the indicator for each spikeTile
            foreach (SpikeTile SpikeTile in Children)
            {
                SpikeTile.indicator.Update(gameTime);
                //System.Diagnostics.Debug.WriteLine(SpikeTile.indicator.GlobalPosition);
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //displays the indicator for each spikeTile
            foreach (SpikeTile SpikeTile in Children)
            {
                SpikeTile.indicator.Draw(gameTime, spriteBatch);
                //System.Diagnostics.Debug.WriteLine(SpikeTile.indicator.Position);
            }
            base.Draw(gameTime, spriteBatch);
        }
    }
}
