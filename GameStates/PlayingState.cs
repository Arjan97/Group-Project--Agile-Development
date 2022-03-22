using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameStates
{
    public class PlayingState : GameObjectList
    {
        Player player;
        public PlayingState()
        {
            player = new Player();

            this.Add(player);
            Add(new TileList());
        }
    }     
}
