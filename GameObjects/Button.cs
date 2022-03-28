using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameObjects
{
    internal class Button : SpriteGameObject
    {
        Trap target;
        private Keys assignedKey = Keys.None;

        public Button(Vector2 position, Trap trap) : base("img/buttons@2x2")
        {
            target = trap;
            Initialize(position.X, position.Y);
        }
        public Button(float x, float y) : base("img/buttons@2x2") 
        {
            Initialize(x, y);
        }



       /* public Button(float x, float y, Trap trap) : base("img/buttons@2x2")
        {

            target = trap;
            Initialize(x,y);

        }*/

        private void Initialize(float x, float y)
        {
            position.X = x;
            position.Y = y;
            scale = 0.5f;
            id = "button";
        }

        //function to give the button a different key
        public void AssignKey(Keys newKey)
        {
            visible = true;
            assignedKey = newKey;
            //switch to update the texture
            switch (assignedKey)
            {
                case Keys.NumPad4:
                    sprite.SheetIndex = 0;
                    break;

                case Keys.NumPad5:
                    sprite.SheetIndex = 1;
                    break;

                case Keys.NumPad6:
                    sprite.SheetIndex = 2;
                    break;

                case Keys.NumPad8:
                    sprite.SheetIndex = 3;
                    break;

                case Keys.None:
                    visible = false;
                    break;

            }
        }

        //returns the assigned key
        public Keys Key
        {
            get { return assignedKey; }
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(assignedKey))
            {
                if (target != null)
                {
                    //activates the object the button is assigned to
                    target.Activate();
                    visible = false;
                    assignedKey = Keys.None;
                }
                base.HandleInput(inputHelper);
            }
        }
    }
}
