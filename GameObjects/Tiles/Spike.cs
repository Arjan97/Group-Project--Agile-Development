using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects.Tiles
{
    internal class Spike : Trap
    {
        int timer = 0;//the time how long a spike is out
        int upTime = 800;//how long a spike is visible

        public Spike(int x, int y) : base(x, y)
        {
                Add(new SpikeTile(x, y));
        }


        public override void CreateButton()
        {
            base.CreateButton();
            button.Position += new Vector2(0,tileSize/2);//moves button to the ground
        }

        public override void Update(GameTime gameTime)
        {

            //updates the indicator for each spikeTile
            foreach (SpikeTile SpikeTile in Children)
            {
                SpikeTile.indicator.Update(gameTime);
            }

            //timer for the groundtie spikes
            if (visible)
            {
                if (timer > upTime)
                {
                    visible = false;
                    foreach (SpikeTile SpikeTile in Children)
                    {
                        SpikeTile.Visible = false;
                    }
                }
                timer++;
            }
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            //displays the indicator for each spikeTile
            foreach (SpikeTile SpikeTile in Children)
            {
                //draws an indication where a spike can appear
                SpikeTile.indicator.Draw(gameTime, spriteBatch);
            }
            base.Draw(gameTime, spriteBatch);
        }
    }
}
