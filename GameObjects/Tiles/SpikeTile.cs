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
            indicator = new SpriteGameObject("img/tiles/spr_spikeIndication"); //indication under the spikes 
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
        public override void Activate()
        {
            visible = true;
            base.Activate();
        }
    }
}
