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

        bool headingRight = true;

        public PlayingState()
        {
            Add(player);
            Add(tileList);
            Add(ghost);
        }

        public override void Update(GameTime gameTime)
        {
            System.Diagnostics.Debug.WriteLine("start");
            player.isGrounded = false;
            tileList.CheckColission(player);
            ghost.SetGhostDistance(tileList);
            base.Update(gameTime);
            HandleCamera();
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

        public void HandleCamera()
        {
            if((headingRight && player.GlobalPosition.X < GameEnvironment.Screen.X * 1 / 8) || (!headingRight && player.GlobalPosition.X > GameEnvironment.Screen.X * 7 / 8))
            {
                headingRight = !headingRight;
            }
            if (headingRight && player.GlobalPosition.X > GameEnvironment.Screen.X * 3 / 8 && position.X + GameEnvironment.Screen.X < tileList.LevelSize.X)
            {
                position.X -= 5f;
            }
            else if (!headingRight && player.GlobalPosition.X < GameEnvironment.Screen.X * 5 / 8 && position.X <= 0)
            {
                position.X += 5f;
            }

            System.Diagnostics.Debug.WriteLine(headingRight);
        }

    }     
}
