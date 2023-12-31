﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameStates;
using BaseProject.GameObjects;

namespace BaseProject
{
    public class Game1 : GameEnvironment
    {
        public const int Depth_Player = 0; // for the player
        public static int maxLevels = 4;
        public bool endGame = false;
        protected override void LoadContent()
        {
            base.LoadContent();

            screen = new Point(1280, 720);
            ApplyResolutionSettings();
            gameStateManager.AddGameState("startScreen", new StartScreen());
            gameStateManager.AddGameState("mainMenuState", new MainMenuState(this));
            gameStateManager.AddGameState("levelSelectState", new LevelSelectState());
            GameStateManager.AddGameState("playingState", new PlayingState());
            GameStateManager.AddGameState("gameOverState", new GhostWinState());
            GameStateManager.AddGameState("playerWinState", new PlayerWinState());
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
