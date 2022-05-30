using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class FinishTile : Tile
    {
        public FinishTile(int x, int y) : base("img/tiles/spr_finishTile", x, y) { }

        public void onCollision()
        {
        }
    }
}
