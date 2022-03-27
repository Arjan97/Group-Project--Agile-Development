using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects.Tiles;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameObjects
{
    internal class Button : SpriteGameObject
    {
        Trap target;
        public Keys assignedKey;
        public Button(float x, float y) : base("") 
        { 
            position.X = x;
            position.Y = y;
        }

        public Button(float x, float y, Trap trap) : base("")
        {
            position.X = x;
            position.Y= y;
            target = trap;
        }

        public void AssignKey(Keys newKey)
        {
            assignedKey = newKey;

            switch (assignedKey)
            {

                case Keys.NumPad4:
                    //green Y
                    break;

                case Keys.NumPad5:
                    //yellow B
                    break;

                case Keys.NumPad6:
                    //red A
                    break;

                case Keys.NumPad8:
                    //blue X
                    break;

            }
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
                }
            base.HandleInput(inputHelper);
        }
    }
}
