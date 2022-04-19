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

        public TileList()
        {
            LoadLevel(0);
            this.id = "TileList";
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
                    colorCode = fuckzooi[1];
                    colorCode += fuckzooi[3];
                    colorCode += fuckzooi[5];
                    if (x == 75 && y == 5) { System.Diagnostics.Debug.WriteLine(colorCode); }


                    switch (colorCode)
                    {
                        case "195195195":

                            Add(new GroundTile(x, y));
                            break;

                        case "888888":
                            Add(new Bridge(x, y, 4));
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
            int tileSize = new Tile("img/tiles/spr_bridge",0,0).tileSize;
            levelSize = new Vector2(level.Width*tileSize, level.Height*tileSize);
        }

        private Tile FindTile(int x, int y)
        {
            foreach (Tile obj in children)
            {
                if (obj.location.X == x && obj.location.Y == y)
                {
                    return obj;
                }
            }
            return null;
        }

        public Vector2 LevelSize { get { return levelSize; } }

    }
}
