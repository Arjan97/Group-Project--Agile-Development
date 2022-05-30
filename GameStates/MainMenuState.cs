using Microsoft.Xna.Framework;
using BaseProject.GameObjects.menuObjects;
using System;
namespace BaseProject.GameStates
{
    internal class MainMenuState : Menu
    {
        Game1 game;
        public MainMenuState(Game1 game) : base(3,1)
        {
            this.game = game;
            //loads the menu options
            options[0, 0] = new optionButton(GameEnvironment.Screen.X / 3, 500, "random level");
            options[1, 0] = new optionButton(GameEnvironment.Screen.X / 2, 500, "level select");
            options[2, 0] = new optionButton(GameEnvironment.Screen.X *2/3, 500, "quit game");
        }
        /// <summary>
        /// function to go back to startscreen
        /// </summary>
        protected override void GoBack()
        {
            GameEnvironment.GameStateManager.SwitchTo("startScreen");
        }

        /// <summary>
        /// function that handles the selected option
        /// </summary>
        /// <param name="choise">chosen option</param>
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
            if(choise.X == 2)
            {
                game.Exit();
            }
        }
    }
}
