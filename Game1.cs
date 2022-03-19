using Microsoft.Xna.Framework;
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

            screen = new Point(800, 600);
            ApplyResolutionSettings();

            GameStateManager.AddGameState("playState", new PlayingState());
            GameStateManager.SwitchTo("playState");
        }

    }
}
