using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameStates
{
    internal class StartScreen : GameObjectList
    {
        public  StartScreen()
        {
            Add(new SpriteGameObject("img/players/spr_player"/*placeholder for background img*/));
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
