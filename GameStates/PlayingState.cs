using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using BaseProject.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameStates
{
    public class PlayingState : GameObjectList
    {
        Player player = new Player();
        TileList tileList = new TileList();

        public PlayingState()
        {
            Add(player);
            Add(tileList);
            Add(new Ghost());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

           
            tileList.CheckColission(player);
        }
    
    
    
    }     
}
