using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class SwitchObject : Trap
    {
       public SwitchObject(int x, int y, int length, int id) : base(x, y, length)
        {
            this.id = id.ToString();
            for(int i = 0; i < length; i++)
            {
                Add(new SwitchTile(x + i, y));
            }
        }
    }
}
