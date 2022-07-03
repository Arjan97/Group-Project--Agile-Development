using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects.Tiles
{
    internal class SpikeTile  : TrapTile
    {
        int SpikeTimer = 0;
        int upTime = 800;
        public SpriteGameObject indicator;
        public SpikeTile(int x, int y) : base("img/tiles/spr_spikebottom", x, y){
            visible = false;
            layer = 5;
            //creating indication under the spikes 
            indicator = new SpriteGameObject("img/tiles/spr_spikeIndication",6); 
            indicator.Position = new Vector2(0, tileSize / 2+4); 
            indicator.Parent = this;
        }
        public override void Update(GameTime gameTime)
        {
            //timer for the groundtile spikes
            if (visible)
            {
                if (SpikeTimer > upTime)
                {
                    visible = false;
                    SpikeTimer = 0;
                }
                SpikeTimer++;

            }
            base.Update(gameTime);
        }
        /// <summary>
        /// function that shows the spikes when activated
        /// </summary>
        public override void Activate()
        {
            visible = true;
            base.Activate();
        }
    }
}
