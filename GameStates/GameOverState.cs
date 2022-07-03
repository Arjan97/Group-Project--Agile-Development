using BaseProject.GameObjects.menuObjects;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    internal class GameOverState : Menu
    {
        protected TextGameObject gameOver;
        protected SpriteGameObject player;
        protected SpriteGameObject ghost;
        public GameOverState() : base(1,3)
        {
            Point screen = GameEnvironment.Screen;
            //creates the game over text
            gameOver = new TextGameObject("font/Arial40");
            gameOver.Text = "Player 1 Wins!";
            gameOver.Position = new Vector2(screen.X / 2 - 8*12, screen.Y * 1 / 3);
            Add(gameOver);

            //assigns buttons to the grid
            options[0,1] = new optionButton(screen.X / 2, screen.Y * 4/6, "switch");
            options[0, 2] = new optionButton(screen.X / 2, screen.Y * 5 / 6, "main menu");
        }

        protected void SetSpritesPos()
        {
            Vector2 middle = GameEnvironment.Screen.ToVector2() / 2;
            player.Origin = player.Center;
            ghost.Origin = ghost.Center;

            player.Position = new Vector2((float)(middle.X * 0.5), middle.Y);
            player.Scale *= 3;

            ghost.Position = new Vector2((float)(middle.X * 1.5), middle.Y);
            ghost.Scale *= 5;
            Add(player);
            Add(ghost);

        }
        /// <summary>
        /// handles the selected option
        /// </summary>
        /// <param name="choise">selected option</param>
        protected override void GoForward(Point choise)
        {
            switch (choise.Y)
            {

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
       protected void ResetLevel()
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
            PlayingState playState = (PlayingState)GameEnvironment.GameStateManager.CurrentGameState;
            playState.Reset();

            if (input.IsPlayer1Ghost)
            {
                gameOver.Text = "player 1 wins!";
            }
            else
            {
                gameOver.Text = "player 2 wins!";
            }
        }
    }
}
