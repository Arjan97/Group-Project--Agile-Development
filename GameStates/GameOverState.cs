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

            options[0, 0] = new optionButton(screen.X / 2, screen.Y * 0.5f, "play again");
            options[0,1] = new optionButton(screen.X / 2, screen.Y * 4/6, "switch");
            options[0, 2] = new optionButton(screen.X / 2, screen.Y * 5 / 6, "main menu");
        }

        protected override void GoForward(Point choise)
        {
            switch (choise.Y)
            {
                case 0:
                    GameEnvironment.GameStateManager.SwitchTo("playingState");
                    PlayingState playState = (PlayingState)GameEnvironment.GameStateManager.CurrentGameState;
                    playState.Reset();
                    break;

                case 1:

                    break;

                case 2:
                    GameEnvironment.GameStateManager.SwitchTo("mainMenuState");
                    break;
            }
        }
    }
}
