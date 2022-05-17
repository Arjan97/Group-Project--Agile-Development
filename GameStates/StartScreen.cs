﻿using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    internal class StartScreen : GameObjectList
    {
        public  StartScreen()
        {
            SpriteGameObject title = new SpriteGameObject("img/menu/txt/title");
            title.Origin = title.Center;
            title.Position = new Vector2(GameEnvironment.Screen.X/2, GameEnvironment.Screen.Y*1/3);
            TextGameObject sub = new TextGameObject("font/Arial12");
            sub.Text = "PRESS ANY KEY TO START";
            sub.Position = new Vector2((GameEnvironment.Screen.X - (sub.Text.Length * 12)) / 2, GameEnvironment.Screen.Y*2/3);
            Add(title);
            Add(sub);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.AnyKeyPressed)
            {
                GameEnvironment.GameStateManager.SwitchTo("mainMenuState");
            }
            base.HandleInput(inputHelper);
        }
    }
}
