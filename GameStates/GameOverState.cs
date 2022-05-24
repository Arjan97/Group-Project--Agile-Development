using BaseProject.GameObjects.menuObjects;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    internal class GameOverState : Menu
    {
        public GameOverState() : base(1,3)
        {
            Point screen = GameEnvironment.Screen;

            //creates the game over text
            TextGameObject gameOver = new TextGameObject("font/Arial12");
            gameOver.Text = "Game Over";
            gameOver.Position = new Vector2(screen.X / 2, screen.Y * 1 / 3);
            Add(gameOver);

            //assigns buttons to the grid
            options[0, 0] = new optionButton(screen.X / 2, screen.Y * 0.5f, "play again");
            options[0,1] = new optionButton(screen.X / 2, screen.Y * 4/6, "switch");
            options[0, 2] = new optionButton(screen.X / 2, screen.Y * 5 / 6, "main menu");
        }

        /// <summary>
        /// handles the selected option
        /// </summary>
        /// <param name="choise">selected option</param>
        protected override void GoForward(Point choise)
        {
            switch (choise.Y)
            {
                case 0:
                    ResetLevel();
                    break;

                case 1:
                    input.AssignKeys(!input.IsPlayer1Ghost);
                    ResetLevel();
                    break;

                case 2:
                    GameEnvironment.GameStateManager.SwitchTo("mainMenuState");
                    break;
            }
        }

        /// <summary>
        /// switches to and resets the playstate
        /// </summary>
        void ResetLevel()
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
            PlayingState playState = (PlayingState)GameEnvironment.GameStateManager.CurrentGameState;
            playState.Reset();
        }
    }
}
