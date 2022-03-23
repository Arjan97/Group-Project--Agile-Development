using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects.Tiles;

namespace BaseProject.GameObjects.Tiles
{
    internal class Trap : GameObjectList
    {
        bool Activated = false;
        public Trap(int x, int y)
        {

        }

        public virtual void Activate()
        {
                foreach (TrapTile tile in Children)
                {
                    tile.Activate();
                }
                Activated = true;
        }
    }
}
