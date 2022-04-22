using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class SwitchObject : Trap
    {
        public SwitchObject(int x, int y, int length, string id) : base(x, y, length)
        {
            this.id = id;
            for (int i = 0; i < length; i++)
            {
                Add(new SwitchTile(x + i, y));
            }

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            // System.Diagnostics.Debug.WriteLine("check");
            base.HandleInput(inputHelper);
        }
        public override void Activate()
        {
            Switch switchtrap = (Switch)parent;
            switchtrap.Activate("1");
            Activated = true;
        }

        public void Arm() { System.Diagnostics.Debug.WriteLine("bang"); }
    }
}
