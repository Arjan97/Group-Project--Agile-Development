using System;

namespace BaseProject.GameObjects.Tiles
{
    internal class TrapTile : Tile
    {
        public TrapTile(String assetName, int x, int y) : base(assetName, x, y) { }


        /// <summary>
        /// function that handles the activation of the trap
        /// </summary>
        public virtual void Activate() { }
    }
}
