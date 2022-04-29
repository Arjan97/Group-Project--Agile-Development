using Microsoft.Xna.Framework;
using BaseProject.GameObjects.menuObjects;

namespace BaseProject.GameStates
{
    internal class MainMenuState : Menu
    {
        public MainMenuState() : base(3,1)
        {
            options[0, 0] = new optionButton(GameEnvironment.Screen.X / 3, 500, "random level");
            options[1, 0] = new optionButton(GameEnvironment.Screen.X / 2, 500, "level select");
            options[2, 0] = new optionButton(GameEnvironment.Screen.X *2/3, 500, "quit game");
        }

        protected override void GoBack()
        {
            GameEnvironment.GameStateManager.SwitchTo("startScreen");
        }

        protected override void GoForward(Point choise)
        {
            if(choise.X == 0) { 
                GameEnvironment.GameStateManager.SwitchTo("playingState");
                PlayingState playState = (PlayingState)GameEnvironment.GameStateManager.CurrentGameState;
                playState.LoadLevel(GameEnvironment.Random.Next(Game1.maxLevels));
            }
            if(choise.X == 1)
            {
                GameEnvironment.GameStateManager.SwitchTo("levelSelectState");
            }
        }
    }
}
