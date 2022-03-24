using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class Switch : Trap
    {
        public Switch(int x, int y, int length, int x2, int y2, int length2) : base(x, y)
        {
            Add(new SwitchObject(x, y, length, 1));
            Add(new SwitchObject(x2, y2, length2, 2));
        }

        public void Activate(string choise)
        {
            Activated = true;

            //searches chosen trap and activates it
            SwitchObject target = (SwitchObject)Find(choise);
            target.Activate();
        }
    }
}
