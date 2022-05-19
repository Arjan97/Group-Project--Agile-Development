using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class SwitchObject : Trap
    {
        bool armed = false;
        public bool collidedWithPlayer = false;
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
            if (!collidedWithPlayer)
            {
                Switch switchtrap = (Switch)parent;
                switchtrap.Activate(id);
                Activated = true;
            } else
            {
                
            }
                            

            
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(collidedWithPlayer)
            {
                System.Diagnostics.Debug.WriteLine("true");
                this.button.Hidden = true;
            } else
            {
                this.button.Hidden = false;
            }

            //collidedWithPlayer = false;
        }

        public void Arm() { armed = true; }

        public bool Armed { get => armed; }

        public Vector2 ButtonPosition { get => buttonPosition;}


    }
}
