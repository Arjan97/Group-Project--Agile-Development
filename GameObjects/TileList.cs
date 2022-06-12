using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BaseProject.GameObjects.Tiles;

namespace BaseProject.GameObjects
{
    public class TileList : GameObjectList
    {
        Vector2 levelSize;//stores the size of a  level
        public int nextLevelNr = -1;//value that when changed loads the level of the next value
        public int currentLevel;//number of the current level

        public TileList() : base(-1)
        {
            id = "TileList";
        }

        /// <summary>
        /// function that makes the buttons disappear, used in camera mode
        /// </summary>
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

        /// <summary>
        /// function that makes the buttons reappear, used in camera mode
        /// </summary>
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

        /// <summary>
        /// checks if moving tiles collide with eachother
        /// </summary>
        /// <param name="target">list of tiles that might be moving</param>
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
        /// <summary>
        /// function that loads a level
        /// </summary>
        /// <param name="levelNr">the number of the level that needs to be loaded</param>
        public void NextLevel(int levelNr)
        {
            children.Clear();
            LoadLevel(levelNr);
        }

        public int CurrentLevel { get { return currentLevel; } }

        /// <summary>
        /// function that loads a level in
        /// </summary>
        /// <param name="levelNr">the number/id of the level</param>
        public void LoadLevel(int levelNr)
        {
            if(levelNr >= Game1.maxLevels)
            {
                levelNr = 0;
                System.Diagnostics.Debug.WriteLine("hallo");
            }
            currentLevel = levelNr;
            //loads the level image
            string levelName = "img/levels/spr_level" + levelNr;
            SpriteSheet level = new SpriteSheet(levelName);
            Texture2D Level = level.Sprite;

            //extracts  the colors from image
            Color[] colors = new Color[Level.Width * Level.Height];
            Level.GetData<Color>(colors);

            //variables used to connect switchobjects with eachoter
            Switch lastSwitch = null;//keeps track on which switch was the last so it can be paired
            int switchCounter = 0;//counter to keep track of how many switches there are. If the number is divisable by 2 the switch that is getting created needs to be added to the previous one


            //assings a tile for each pixel
            for (int x = 0; x < Level.Width; x++)
            {
                for (int y = 0; y < Level.Height; y++)
                {
                    //convert color code to string
                    string color = colors[x + (y * Level.Width)].ToString();
                    string[] fuckzooi = color.Split(new Char[] { ' ', ':' }, StringSplitOptions.RemoveEmptyEntries);
                    string colorCode = fuckzooi[1];
                    colorCode += fuckzooi[3];
                    colorCode += fuckzooi[5];
                    
                    
                    Tile neighbour = FindTile(x - 1, y);
                    switch (colorCode)
                    {
                        case "195195195":

                            Add(new GroundTile(x, y));
                            break;

                        case "888888":
                            //check if it is a new bridge or needs to be added to an existing one
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
                            //check if it is a new bridge or needs to be added to an existing one
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
                            //check if it is a new bridge or needs to be added to an existing one
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
                            //check if it is a new bridge or needs to be added to an existing one
                            if (neighbour is SwitchTile)
                            {
                                ((SwitchObject)((SwitchTile)neighbour).Parent).Add(new SwitchTile(x, y));
                            }
                            else
                            {
                                switchCounter++;

                                //checks if the switch needs pairing
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
            //cycles thru all the tiles to give buttons to traps
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

        /// <summary>
        /// removes all assigned keys from traps when a new level is loaded
        /// </summary>
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

        /// <summary>
        /// searches the list for a specific tile
        /// </summary>
        /// <param name="x">X-coördinate of the tile</param>
        /// <param name="y">Y-coördinate of the tile</param>
        /// <returns>the requested tile OR null</returns>
        private Tile FindTile(int x, int y)
        {
            foreach (GameObject obj in children)
            {
                //switches consist of multiple tiles which all have to be checkt independently
                if (obj is Switch)
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
                //traps consist of multiple tiles which all have to be checkt independently
                else if(obj is Trap)
                {
                    foreach (Tile tile in ((Trap)obj).Children)
                    {
                        if (tile.location == new Vector2(x,y))
                        {
                            return tile;
                        }
                    }
                }
                else if (((Tile)obj).location == new Vector2(x,y))
                {
                    return (Tile)obj;
                }
            }
            return null;
        }

        public override void Update(GameTime gameTime)
        {
            //when nextlevelNr is changed it means that a new level needs to be loaded
            if(nextLevelNr > -1)
            {
                NextLevel(nextLevelNr);
                nextLevelNr = -1;
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
