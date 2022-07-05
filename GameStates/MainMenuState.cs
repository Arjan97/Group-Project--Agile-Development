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
            Vector2 Screen = GameEnvironment.Screen.ToVector2();
            Vector2 Middle = Screen / 2;
            this.game = game;
            //loads the menu options
            options[0, 0] = new optionButton(Screen.X / 3, 500, "play");
            options[1, 0] = new optionButton(Screen.X / 2, 500, "level select");
            options[2, 0] = new optionButton(Screen.X *2/3, 500, "quit game");

            SpriteGameObject player = new SpriteGameObject("img/players/spr_player_attack@2");
            SpriteGameObject ghost = new SpriteGameObject("img/players/spr_ghost");
            player.Position = new Vector2(Middle.X - 160, Middle.Y);
            ghost.Position = new Vector2(Middle.X + 160, Middle.Y);
            player.Scale *= 3;
            ghost.Scale *= 5;
            Add(player);
            Add(ghost);

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
                playState.LoadLevel(0);
                
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
