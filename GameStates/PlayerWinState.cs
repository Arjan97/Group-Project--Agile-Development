using BaseProject.GameObjects.menuObjects;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    internal class PlayerWinState : Menu
    {
        public PlayerWinState() : base(1,3)
        {
            Point screen = GameEnvironment.Screen;

            //creates the game over text
            TextGameObject gameOver = new TextGameObject("font/Arial40");
            gameOver.Text = "Player won";
            
            gameOver.Position = new Vector2((screen.X / 2) - (gameOver.Size.X / 2), screen.Y * 1 / 5);
            Add(gameOver);

            options[0, 0] = new optionButton(screen.X / 2, screen.Y * 0.5f, "next level");
            options[0,1] = new optionButton(screen.X / 2, screen.Y * 4/6, "switch");
            options[0, 2] = new optionButton(screen.X / 2, screen.Y * 5 / 6, "main menu");
        }

        protected override void GoForward(Point choise)
        {
            switch (choise.Y)
            {
                case 0:
                    NextLevel();
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

        void ResetLevel()
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
            PlayingState playState = (PlayingState)GameEnvironment.GameStateManager.CurrentGameState;
            playState.Reset();
        }

        void NextLevel()
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
            PlayingState play = (PlayingState)GameEnvironment.GameStateManager.GetGameState("playingState");
            play.tileList.nextLevelNr = play.tileList.currentLevel + 1;
            //play.ghost.Reset();
        }
    }
}
