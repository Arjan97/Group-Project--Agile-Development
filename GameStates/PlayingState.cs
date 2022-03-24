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
            Add(new Ghost());
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            /*foreach
            foreach (GameObject tile in tileList.Children)
            {
                if(tile is GameObjectList)
                {
                    GameObjectList tileList = (GameObjectList)tile;
                    foreach(SpriteGameObject subTile in tileList.Children)
                    {
                        if (subTile.CollidesWith(player))
                        {
                            System.Diagnostics.Debug.WriteLine("collision");
                        }
                        //collison code
                    }
                } else
                {
                    SpriteGameObject subTile = (SpriteGameObject) tile;
                    if(subTile.CollidesWith(player)){ System.Diagnostics.Debug.WriteLine("collision"); }

                }
            }*/
            ColissionCheck(tileList, player);
        }
    
    public void ColissionCheck(GameObject one, SpriteGameObject other)
        {
            if(one is GameObjectList)
            {
                GameObjectList list = (GameObjectList)one;
                foreach (GameObject sub in list.Children)
                {
                    ColissionCheck(sub, other);
                }
            }
            else
            {
                SpriteGameObject oneSprite = (SpriteGameObject)one;
                if (oneSprite.CollidesWith(other)){
                    System.Diagnostics.Debug.WriteLine("test");           
                }
            }
        }
    
    
    }     
}
