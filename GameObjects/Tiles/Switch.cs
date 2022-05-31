using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects.Tiles;

namespace BaseProject.GameObjects.Tiles
{
    
    internal class Switch : Trap
    {
     
        public Switch(int x, int y) : base(x, y)
        {
            Add(new SwitchObject(x, y, "1"));
        }

        public override void CreateButton()
        {
            base.CreateButton();
            button.Visible = false;
            buttonPosition = ((SwitchObject)Children[0]).ButtonPosition;
        }

        public void Activate(string choice)
        {
            activated = true;
           
            //searches chosen trap and activates it
            SwitchObject target = (SwitchObject)Find(choice);
            target.Arm();
           

            foreach (SwitchObject trap in children)
            {
                trap.button.Visible = false;

                foreach (SwitchTile switchActivation in trap.Children)
                {
                    switchActivation.Sprite.SheetIndex = 1;
                    
                }
            }

        }
        public override void Update(GameTime gameTime)
        {
           
            base.Update(gameTime);
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
