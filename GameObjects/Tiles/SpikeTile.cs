using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects.Tiles
{
    internal class SpikeTile  : TrapTile
    {
        public SpriteGameObject indicator;
        public SpikeTile(int x, int y) : base("img/tiles/spr_spikebottom", x, y){
            visible = false;
            indicator = new SpriteGameObject("img/tiles/spr_spikeIndication");
            indicator.Position = new Vector2(0, tileSize / 2+4);
            indicator.Parent = this;
        }

        public override void Activate()
        {
            visible = true;
            base.Activate();
        }
    }
}
