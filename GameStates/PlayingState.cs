using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
using BaseProject.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameStates
{
    public class PlayingState : GameObjectList
    {
        Player player = new Player();
        TileList tileList = new TileList();
        Ghost ghost = new Ghost();

        public PlayingState()
        {
            Add(player);
            Add(tileList);
            Add(ghost);
        }

        public override void Update(GameTime gameTime)
        {
            player.isGrounded = false;
            tileList.CheckColission(player);
            ghost.SetGhostDistance(tileList);
            base.Update(gameTime);

        }

        public override void HandleInput(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.NumPad0))
            {

            }
            base.HandleInput(inputHelper);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

    }     
}
