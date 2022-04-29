using BaseProject.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameObjects
{
    internal class Button : SpriteGameObject
    {
        private Keys assignedKey = Keys.None;
        private bool hidden = false;
        public Button(Vector2 position, Trap trap, string id = "button") : base("img/buttons@2x2")
        {
            parent = trap;
            Initialize(position.X, position.Y);
            this.id = id;
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
            scale = new Vector2(0.5f, 0.5f);
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
            get => assignedKey;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(assignedKey))
            {
                if (parent != null)
                {
                    Trap parentTrap = (Trap)parent;
                    //activates the object the button is assigned to
                    parentTrap.Activate();
                    visible = false;
                    assignedKey = Keys.None;
                }
                base.HandleInput(inputHelper);
            }
        }

        public bool Hidden
        {
            get => hidden;
            set => hidden = value;
        }

    }
}
