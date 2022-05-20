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
        private int currentLevel;

        public TileList() : base(-1)
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
            currentLevel = levelNr;
        }

        public int CurrentLevel { get { return currentLevel; } }


        public void LoadLevel(int levelNr)
        {
            //loads the level image
            string levelName = "img/levels/spr_level" + levelNr;
            SpriteSheet level = new SpriteSheet(levelName);
            Texture2D Level = level.Sprite;

            //extracts  the colors from image
            Color[] colors = new Color[Level.Width * Level.Height];
            Level.GetData<Color>(colors);

            //variables used to connect switchobjects with eachoter
            Switch lastSwitch = null;
            int switchCounter = 0;


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
                    
                    
                    Tile neighbour = FindTile(x - 1, y);
                    switch (colorCode)
                    {
                        case "195195195":

                            Add(new GroundTile(x, y));
                            break;

                        case "888888":
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
                            if (neighbour is SpikeRoofTile)
                            {
                                ((SpikeRoof)((SpikeRoofTile)neighbour).Parent).Add(new SpikeRoofTile(x, y));
                            }
                            else
                            {
                                Add(new SpikeRoof(x, y));
                            }
                            break;

                        case "127510":
                            if (neighbour is SpikeTile)
                            {
                                ((Spike)((SpikeTile)neighbour).Parent).Add(new SpikeTile(x, y));
                            }
                            else
                            {
                                Add(new Spike(x, y));
                            }
                            break;

                        case "25512739":
                            if (neighbour is SwitchTile)
                            {
                                ((SwitchObject)((SwitchTile)neighbour).Parent).Add(new SwitchTile(x, y));
                            }
                            else
                            {
                                switchCounter++;
                                if(switchCounter %2 != 0)
                                {
                                    lastSwitch = new Switch(x,y);
                                    Add(lastSwitch);
                                }
                                else
                                {
                                    lastSwitch.Add(new SwitchObject(x, y, "2"));
                                }
                            }
                            break;

                        case "25520224":
                            Add(new FinishTile(x, y));
                            break;
                    }


                }
            }
            //cycles to all the tiles to give buttons to traps
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
            levelSize = new Vector2(level.Width*Tile.tileSize, level.Height*Tile.tileSize);
        }

        void ForceUnassignKeys()
        {
            foreach (GameObject obj in Children)
            {
               if(obj is Trap)
                {
                    ((Trap)obj).AssignedKey = Keys.None;

                    if(obj is Switch)
                    {
                        foreach (SwitchObject switchobj in ((Switch)obj).Children)
                        {
                            switchobj.AssignedKey = Keys.None;
                        }
                    }
                }
            }
        }

        private Tile FindTile(int x, int y)
        {
            foreach (GameObject obj in children)
            {
                if(obj is Switch)
                {
                    foreach (SwitchObject switchObject in ((Switch)obj).Children)
                    {
                        foreach (Tile tile in switchObject.Children)
                        {
                            if (tile.location == new Vector2(x, y))
                            {
                                return tile;
                            }
                        }
                    }
                }
                else if(obj is Trap)
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

        public override void Reset()
        {
            ForceUnassignKeys();
            base.Reset();
        }

    }

    
    
}
