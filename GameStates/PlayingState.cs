using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects;
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
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //foreach
            foreach (GameObject tile in tileList.Children)
            {
                if(player.CollidesWith((SpriteGameObject)tile) && player.isFalling)
                {
                    //colission code
                    //velocity.Y = 0;
                    // player.speed = 0;
                    //player.position = 0;
                    //player.touchGrass = true;
                    
                    player.isFalling = false;
                    player.hasJumped = false;
                        //player.Collide();
                        System.Diagnostics.Debug.WriteLine("collision");
                }
            }
        }
    }     
}
