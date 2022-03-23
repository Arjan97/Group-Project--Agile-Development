using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class Spike : Trap
    {
        public Spike(int x, int y, int length) : base(x, y)
        {
            for (int i = 0; i < length; i++)
            {
                Add(new SpikeTile(x + i, y));
            }
        }
    }
}
