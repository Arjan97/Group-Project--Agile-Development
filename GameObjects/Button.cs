using BaseProject.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Input = Microsoft.Xna.Framework.Input;
using BaseProject.GameStates;
using BaseProject.GameComponents;

namespace BaseProject.GameObjects
{
    internal class Button : SpriteGameObject
    {
        private bool hidden = false;
        PlayingState currentPlayingState;

        //variables used for input
        InputHandler input; //InputHandler that handles the different players their input
        private Input.Keys assignedKey = Input.Keys.None;//key which the button listens to

        public Button(Vector2 position, Trap trap, string id = "button") : base("img/spr_buttons@2x2", 5, id)
        {
            parent = trap;
            Initialize(position);
            currentPlayingState = (PlayingState)GameEnvironment.GameStateManager.CurrentGameState;
        }





        /// <summary>
        /// initialises the button
        /// </summary>
        /// <param name="position">the position of the button relative to the parent</param>
        /// <returns>void</returns>
        private void Initialize(Vector2 position)
        {
            this.position = position;
            scale = new Vector2(0.5f, 0.5f);
            input = GameEnvironment.input;
        }

        /// <summary>
        /// changes which key the button has to listen to and updates the sprite
        /// </summary>
        /// <param name="newKey">new key the button has to listen to</param>
        /// <returns>void</returns>
        public void AssignKey(Input.Keys newKey)
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
            //hides the button if the key is invalid or none
            visible = false;
        }

        /// <returns>the assigned key</returns>
        public Input.Keys Key
        {
            get => assignedKey;
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            //disables the activation when the ghost is stunned
            Ghost ghost = currentPlayingState.ghost;
            if(ghost.stunned)
            {
                return;
            }


            if (inputHelper.KeyPressed(assignedKey))
            {
                if (parent != null)
                {
                    //activates the object the button is assigned to
                    Trap parentTrap = (Trap)parent;
                    parentTrap.Activate();

                    visible = false;//hides the button
                    assignedKey = Input.Keys.None;//gives the key that's assigned free to use for other traps
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
