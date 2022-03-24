using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class SpikeTile  : TrapTile
    {
        public SpikeTile(int x, int y) : base("img/tiles/spr_spikebottom", x, y){
            visible = false;
        }

        public override void Activate()
        {
            visible = true;
            base.Activate();
        }
    }
}
