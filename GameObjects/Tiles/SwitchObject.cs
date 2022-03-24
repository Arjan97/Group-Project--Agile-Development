using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class SwitchObject : GameObjectList
    {
       public SwitchObject(int x, int y, int length, int id)
        {
            this.id = id.ToString();
            for(int i = 0; i < length; i++)
            {
                Add(new SwitchTile(x + i, y));
            }
        }

        public void Activate()
        {
            foreach (SwitchTile tile in Children)
            {
                tile.Activate();
            }
        }
    }
}
