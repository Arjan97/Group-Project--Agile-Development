using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace BaseProject.GameStates
{
    internal class StartScreen : GameObjectList
    {
        public  StartScreen()
        {
            //background of the menu
            SpriteGameObject background = new SpriteGameObject("img/backgrounds/gameBackground");
            background.Origin = background.Center;
            background.Position = new Vector2(GameEnvironment.Screen.X / 2, GameEnvironment.Screen.Y / 2);
            Add(background);

            SpriteGameObject title = new SpriteGameObject("img/menu/txt/title");
            title.Origin = title.Center;
            title.Position = new Vector2(GameEnvironment.Screen.X/2, GameEnvironment.Screen.Y*1/3);
            TextGameObject sub = new TextGameObject("font/Arial12");
            sub.Text = "PRESS ANY KEY TO START";
            sub.Position = new Vector2((GameEnvironment.Screen.X - (sub.Text.Length * 12)) / 2, GameEnvironment.Screen.Y*2/3);
            Add(title);
            Add(sub);

            playMusic();
        }

        //plays music
        public void playMusic()
        {
            GameEnvironment.AssetManager.PlayMusic("sounds/gamesong", true);
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
