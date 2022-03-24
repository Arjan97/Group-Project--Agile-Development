using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class TrapTile : Tile
    {
        public TrapTile(String assetName, int x, int y) : base(assetName, x, y) { }

        public virtual void Activate() { }
    }
}
