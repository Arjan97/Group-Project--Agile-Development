using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class SwitchObject : GameObjectList
    {
       public SwitchObject(int x, int y, int length)
        {
            for(int i = 0; i < length; i++)
            {
                Add(new SwitchTile(x + i, y));
            }
        }
    }
}
