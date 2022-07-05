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

            player = new SpriteGameObject("img/players/spr_player_dead");
            ghost = new SpriteGameObject("img/players/spr_ghost_victory");
            SetSpritesPos();
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
