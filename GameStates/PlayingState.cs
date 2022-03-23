using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameStates
{
    public class PlayingState : GameObjectList
    {
        public PlayingState()
        {

            Add(new TileList());
            Add(new Ghost());
        }
    }
}
