using BaseProject.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameObjects;
using BaseProject.GameStates;

namespace BaseProject.GameObjects
{
    internal class Button : SpriteGameObject
    {
        private Keys assignedKey = Keys.None;
        private bool hidden = false;
        InputHandler input;
        PlayingState currentPlayingState;

        public Button(Vector2 position, Trap trap, string id = "button") : base("img/buttons@2x2", 0)
        {
            parent = trap;
            Initialize(position.X, position.Y);
            this.id = id;
            currentPlayingState = (PlayingState)GameEnvironment.GameStateManager.CurrentGameState;
        }
        public Button(float x, float y) : base("img/buttons@2x2") 
        {
            Initialize(x, y);
        }





        private void Initialize(float x, float y)
        {
            position.X = x;
            position.Y = y;
            scale = new Vector2(0.5f, 0.5f);
            input = GameEnvironment.input;
        }

        //function to give the button a different key
        public void AssignKey(Keys newKey)
        {
            visible = true;
            assignedKey = newKey;
            if(assignedKey == input.Ghost(Buttons.Y))
            {
                sprite.SheetIndex = 0;
                return;
            }
            if (assignedKey == input.Ghost(Buttons.B)) 
            { 
                sprite.SheetIndex = 1;
                return;
            }
            if (assignedKey == input.Ghost(Buttons.A))
            {
                sprite.SheetIndex = 2;
                return;
            }
            if (assignedKey == input.Ghost(Buttons.X))
            {
                sprite.SheetIndex = 3;
                return;
            }
            visible = false;
        }

        //returns the assigned key
        public Keys Key
        {
            get => assignedKey;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            Ghost ghost = currentPlayingState.ghost;
            if(ghost.stunned)
            {
                return;
            }
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
