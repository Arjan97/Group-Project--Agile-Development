using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class TrapTile : Tile
    {
        bool Activated = false;
        public TrapTile(string asset, int x, int y) : base(asset, x, y)
        {

        }

        public virtual void Activate()
        {
          Activated = true;
        }
    }
}
