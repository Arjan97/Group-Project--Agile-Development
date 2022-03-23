using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class Bridge : Trap
    {
        public Bridge(int x, int y, int length) : base(x, y)
        {
            for (int i = 0; i < length; i++)
            {
                Add(new BridgeTile(x + i, y));
            }
        }
    }
}
