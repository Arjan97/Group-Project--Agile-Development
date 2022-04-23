﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameStates;

namespace BaseProject
{
    public class Game1 : GameEnvironment
    {      
        protected override void LoadContent()
        {
            base.LoadContent();

            screen = new Point(1280, 720);
            ApplyResolutionSettings();
            gameStateManager.AddGameState("startScreen", new StartScreen());
            gameStateManager.AddGameState("mainMenuState", new MainMenuState());
            GameStateManager.AddGameState("playingState", new PlayingState());
            GameStateManager.SwitchTo("startScreen");
        }

        protected override void Draw(GameTime gameTime)
        {

            if (inputHelper.KeyPressed(Keys.F11))
            {
                FullScreen = !FullScreen;
            }
            base.Draw(gameTime);
        }

    }
}
