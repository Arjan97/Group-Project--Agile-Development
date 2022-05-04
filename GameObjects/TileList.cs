using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameObjects.Tiles;
using System.Linq;

namespace BaseProject.GameObjects
{
    public class TileList : GameObjectList
    {
        Vector2 levelSize;
        string colorCode;
        public int nextLevelNr = -1;

        public TileList()
        {
            id = "TileList";
        }

        public void HideButtons()
        {
            foreach (GameObject tile in Children)
            {
                if(tile is Trap)
                {
                    Trap traptile = (Trap)tile;
                    traptile.buttonVisibility = false;
                }
            }
        }
        public void ShowButtons()
        {
            foreach (GameObject tile in Children)
            {
                if (tile is Trap)
                {
                    Trap traptile = (Trap)tile;
                    traptile.buttonVisibility = true;
                }
            }
        }

        private void CheckMovingTilesColission(GameObjectList target)
        {
            foreach (GameObject tile in target.Children)
            {
                if (tile is GameObjectList)
                {
                    CheckMovingTilesColission((GameObjectList)tile);

                }

                else if (((Tile)tile).moving)
                {
                    tile.CheckColission(this);
                }
            }

        }

        public void nextLevel(int levelNr)
        {
            children.Clear();
            nextLevelNr = -1;
            LoadLevel(levelNr);
        }


        public void LoadLevel(int levelNr)
        {
            //loads the level image
            string levelName = "img/levels/spr_level" + levelNr;
            SpriteSheet level = new SpriteSheet(levelName);
            Texture2D Level = level.Sprite;

            //extracts  the colors from image
            Color[] colors = new Color[Level.Width * Level.Height];
            Level.GetData<Color>(colors);

            //assings a tile for each pixel
            for (int x = 0; x < Level.Width; x++)
            {
                for (int y = 0; y < Level.Height; y++)
                {
                    //convert color code to string
                    string color = colors[x + (y * Level.Width)].ToString();
                    string[] fuckzooi = color.Split(new Char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                    if(x == 5 && y == 3) System.Diagnostics.Debug.WriteLine(color);
                    colorCode = fuckzooi[1];
                    colorCode += fuckzooi[3];
                    colorCode += fuckzooi[5];
                    int length = 255 - int.Parse(fuckzooi[7].Substring(0, fuckzooi[7].Length - 1));
                    


                    switch (colorCode)
                    {
                        case "195195195":

                            Add(new GroundTile(x, y));
                            break;

                        case "888888":
                            Tile neighbour = FindTile(x - 1, y);
                            if (neighbour is BridgeTile)
                            {
                                ((Bridge)((BridgeTile)neighbour).Parent).Add(new BridgeTile(x,y));
                            }
                            else
                            {
                            Add(new Bridge(x, y));
                            }
                                
                            break;

                        case "2362836":
                            Add(new SpikeRoof(x, y, 3));
                            break;

                        case "127510":
                            Add(new Spike(x, y, 3));
                            break;

                        case "25512739":
                            Add(new Switch(x, y, 4, 33, 11, 10));
                            break;

                        case "25520224":
                            Add(new FinishTile(x, y));
                            break;
                    }


                }
            }
            foreach (GameObject obj in Children)
            {
                if (obj is Trap)
                    {
                    if(obj is Switch){
                        foreach (GameObject obj2 in ((Switch)obj).Children)
                        {
                            ((Trap)obj2).CreateButton();
                        }
                    }
                    ((Trap)obj).CreateButton();
                    }
            }
            int tileSize = Tile.tileSize;
            levelSize = new Vector2(level.Width*tileSize, level.Height*tileSize);
        }

        private Tile FindTile(int x, int y)
        {
            foreach (GameObject obj in children)
            {
                if(obj is Trap)
                {
                    foreach (Tile tile in ((Trap)obj).Children)
                    {
                        if (tile.location == new Vector2(x,y))
                        {
                            return tile;
                        }
                    }
                }else if (((Tile)obj).location == new Vector2(x,y))
                {
                    return (Tile)obj;
                }
            }
            return null;
        }

        public override void Update(GameTime gameTime)
        {
            if(nextLevelNr > -1)
            {
                nextLevel(nextLevelNr);
            }
            CheckMovingTilesColission(this);
            base.Update(gameTime);
        }

        public Vector2 LevelSize { get { return levelSize; } }

    }
    
}
