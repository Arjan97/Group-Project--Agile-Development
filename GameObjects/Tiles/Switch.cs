using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class Switch : Trap
    {
        public Switch(int x, int y, int length, int x2, int y2, int length2) : base(x, y, length)
        {
            Add(new SwitchObject(x, y, length, "1"));
            Add(new SwitchObject(x2, y2, length2, "2"));
            button.Visible = false;
        }

        public void Activate(string choice)
        {
            Activated = true;

            //searches chosen trap and activates it
            SwitchObject target = (SwitchObject)Find(choice);
            target.Arm();
            foreach (SwitchObject trap in children)
            {
                trap.button.Visible = false;
            }

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            
            base.HandleInput(inputHelper);
        }


        public override Keys AssignedKey { 
            get {
                SwitchObject switchobject = (SwitchObject)Find("1");
                return switchobject.AssignedKey;
                }
            set
            {
                SwitchObject switchobject = (SwitchObject)Find("1");
                switchobject.AssignedKey = value;
            }
        }

        public Keys AssignedSecondKey
        {
            get
            {
                SwitchObject switchobject = (SwitchObject)Find("2");
                return switchobject.AssignedKey;
            }
            set
            {
                SwitchObject switchobject = (SwitchObject)Find("2");
                switchobject.AssignedKey = value;
            }
        }
    }
}
