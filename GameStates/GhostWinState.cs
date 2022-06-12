using BaseProject.GameObjects.menuObjects;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    internal class GhostWinState : GameOverState
    {
        
        public GhostWinState()
        {
            Point screen = GameEnvironment.Screen;

            options[0, 0] = new optionButton(screen.X / 2, screen.Y * 0.5f, "play again");

        }

        protected override void GoForward(Point choise)
        {
            if (choise.Y == 0)
            {
                ResetLevel();
            }
            base.GoForward(choise);
        }

        

    }
}
