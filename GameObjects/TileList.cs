using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using BaseProject.GameObjects.Tiles;

namespace BaseProject.GameObjects
{
    public  class TileList : GameObjectList
    {
        string colorCode;
 

        public TileList()
        {
            LoadLevel(0);
        }

        public void LoadLevel(int levelNr)
        {
            string levelName = "img/levels/spr_level" + levelNr;
            SpriteSheet level = new SpriteSheet(levelName);
            Texture2D Level = level.Sprite;


            Color[] colors = new Color[Level.Width * Level.Height];
            Level.GetData<Color>(colors);
            for (int x = 0; x< Level.Width; x++)
            {
                for (int y = 0; y< Level.Height; y++)
                {
                    string color = colors[x + (y * Level.Width)].ToString();
                    string[] fuckzooi = color.Split(new Char[] { ' ', ':'}, StringSplitOptions.RemoveEmptyEntries);
                    colorCode = fuckzooi[1];
                    colorCode += fuckzooi[3];
                    colorCode += fuckzooi[5];
                    
                    System.Diagnostics.Debug.WriteLine(colorCode);
          
                    switch (colorCode)
                    {
                        case "195195195":

                            Add(new GroundTile(x, y));
                            break;
                    }
                }
            }
        }

    }
}
