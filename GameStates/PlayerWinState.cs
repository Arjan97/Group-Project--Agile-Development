using BaseProject.GameObjects.menuObjects;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    internal class PlayerWinState : GameOverState
    {
        public PlayerWinState()
        {

            Point screen = GameEnvironment.Screen;

            options[0, 0] = new optionButton(screen.X / 2, screen.Y * 0.5f, "next level");
;
        }

        protected override void GoForward(Point choise)
        {
            if(choise.Y == 0)
            {
                NextLevel();
            }
            base.GoForward(choise);
        }



        void NextLevel()
        {
            GameEnvironment.GameStateManager.SwitchTo("playingState");
            PlayingState play = (PlayingState)GameEnvironment.GameStateManager.GetGameState("playingState");
            play.tileList.nextLevelNr = play.tileList.currentLevel + 1;
            //play.ghost.Reset();
        }

        public string text
        {
            set { gameOver.Text = value; }
        }
    }
}
