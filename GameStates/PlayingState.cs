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
           // System.Diagnostics.Debug.WriteLine("start");
            player.isGrounded = false;
            tileList.CheckColission(player);
            ghost.SetGhostDistance(tileList);
            base.Update(gameTime);
            HandleCamera();
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }

        public void LoadLevel(int level)
        {
            tileList.LoadLevel(level);
        }

        //function that moves the camera
        public void HandleCamera()
        {
            //check if player turns around
            if((headingRight && player.GlobalPosition.X < GameEnvironment.Screen.X * 1 / 8) || (!headingRight && player.GlobalPosition.X > GameEnvironment.Screen.X * 7 / 8))
            {
                headingRight = !headingRight;
            }

            //if player moves to the right and passes 3/8 of screen, move camera, unless player is at the edge of the screen
            if (headingRight && player.GlobalPosition.X > GameEnvironment.Screen.X * 3 / 8 && position.X + GameEnvironment.Screen.X < tileList.LevelSize.X)
            {
                position.X -= 5f;
            }
            //if player moves to the left and passes 5/8 of screen, move camera, unless player is at the beginning of the screen
            else if (!headingRight && player.GlobalPosition.X < GameEnvironment.Screen.X * 5 / 8 && position.X <= 0)
            {
                position.X += 5f;
            }
            ghost.StayOnScreen(position);
        }

    }     
}
