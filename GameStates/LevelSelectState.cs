using BaseProject.GameObjects.menuObjects;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    internal class LevelSelectState : Menu
    {
        static int maxCollums = 4;//the amount of collums each page
        static int maxRows = 3;//the amount of rows each page
        int currentPage = 0;//int that keeps track on which page the player is on
        int nPages = Game1.maxLevels / maxCollums * maxRows - 1;//number of pages

        public LevelSelectState() : base(maxCollums, maxRows)
        {
            LoadPage(currentPage);
            if (Game1.maxLevels % maxCollums * maxRows - 1 != 0)  nPages++; 
        }

        /// <summary>
        /// loads a page of levels
        /// </summary>
        /// <param name="pageNr">the page number</param>
        void LoadPage(int pageNr)
        {
            int nLevels = maxCollums * maxRows - 1;
           
            //creates the level options
            for (int y = 0; y < maxRows-1; y++)
            {
                for (int x = 0; x < maxCollums; x++)
                {
                    int levelNr = (y + 1) * (x + 1) + pageNr * nLevels-1;

                    //check if the level nr has a level
                    if(levelNr< Game1.maxLevels)
                    {
                    options[x, y] = new LevelOptionButton(x, y, levelNr, $"level {levelNr}");
                    }
                    else
                    {
                        //creates empty level option
                        options[x, y] = new LevelOptionButton(x, y);
                    }
                }
            }

            //shows go back a page button
            if (pageNr > 0)
            {
                 options[0, 2] = new optionButton(250, 3*200, "Previous page");
            }

            //shows next page button
            if(pageNr < nPages)
            {
                options[3, 2] = new optionButton(4 * 250, 3 * 200, "Next Page");
            }



        }

        /// <summary>
        /// function to go back to the main menu
        /// </summary>
        protected override void GoBack()
        {
            GameEnvironment.GameStateManager.SwitchTo("mainMenuState");
        }
        /// <summary>
        /// function that loads the selected level or next page
        /// </summary>
        /// <param name="choise">coördinates of the choise</param>
        protected override void GoForward(Point choise)
        {
            if(choise.X == 3 && choise.Y == 2)
            {
                LoadPage(currentPage++);
                return;
            }
            if(choise.X == 0 && choise.Y == 2)
            {
                LoadPage(currentPage--);
                return;
            }
            //check if the choisen option has a level assigned
            if(options[choise.X, choise.Y] is LevelOptionButton)
            {
                LevelOptionButton levelchoise = (LevelOptionButton)options[choise.X, choise.Y];
                if(!levelchoise.HasLevel) return;
            }

            //loads selected level
            GameEnvironment.GameStateManager.SwitchTo("playingState");
            ((PlayingState)GameEnvironment.GameStateManager.GetGameState("playingState")).LoadLevel((choise.X+1)*(choise.Y+1) -1 +currentPage*8);
        }
    }
}
