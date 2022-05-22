using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class SwitchObject : Trap
    {
        bool armed = false;
        public SwitchObject(int x, int y, string id) : base(x, y)
        {
            this.id = id;

         Add(new SwitchTile(x, y));
        }

        public override void CreateButton()
        {
            base.CreateButton();
            
        }

        public override void Activate()
        {
            Switch switchtrap = (Switch)parent;
            switchtrap.Activate(id);
            Activated = true;
        }
        

        public void Arm() { armed = true; }

        public bool Armed { get => armed; }

        public Vector2 ButtonPosition { get => buttonPosition;}
    }
}
