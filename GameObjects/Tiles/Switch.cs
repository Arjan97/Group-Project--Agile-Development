using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class Switch : Trap
    {
        public Switch(int x, int y, int length, int x2, int y2, int length2) : base(x, y)
        {
            Add(new SwitchObject(x, y, length));
            Add(new SwitchObject(x2, y2, length2));
        }
    }
}
