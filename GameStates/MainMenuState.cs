using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;

namespace BaseProject.GameStates
{
    internal class MainMenuState : Menu
    {
        public MainMenuState() : base(3,1)
        {
            options[0, 0] = new optionButton(GameEnvironment.Screen.X / 3, 500, 0.5f, 0.25f, "random level");
            options[1, 0] = new optionButton(GameEnvironment.Screen.X / 2, 500, 0.5f, 0.25f, "level select");
            options[2, 0] = new optionButton(GameEnvironment.Screen.X *2/3, 500, 0.5f, 0.25f, "quit game");
        }
    }
}
