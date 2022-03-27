using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using BaseProject.GameObjects.Tiles;

namespace BaseProject.GameObjects.Tiles
{
    internal class Trap : GameObjectList
    {
        public float ghostDistance;
        public Vector2 buttonPosition;
        private Button button;

       internal bool Activated = false;
        public Trap(int x, int y, int length)
        {
            button = new Button(x * (new Tile("img/tiles/spr_groundTile", 0, 0)).tileSize, y, this);
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
