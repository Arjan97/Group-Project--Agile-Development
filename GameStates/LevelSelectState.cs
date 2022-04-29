
using BaseProject.GameObjects.menuObjects;
using Microsoft.Xna.Framework;

namespace BaseProject.GameStates
{
    internal class LevelSelectState : Menu
    {
        static int maxCollums = 4;
        static int maxRows = 3;
        int currentPage = 0;
        int nPages = Game1.maxLevels / maxCollums * maxRows - 1;
        public LevelSelectState() : base(maxCollums, maxRows)
        {
            loadPage(0);
            if (Game1.maxLevels % maxCollums * maxRows - 1 != 0)  nPages++; 
        }

        //function to load the options on the screen
        public void loadPage(int pageNr)
        {
            int nLevels = maxCollums * maxRows - 1;
           
            //adds extra page for left over levels
 

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
            System.Diagnostics.Debug.WriteLine(currentPage);

        }

        //function that sends player back to main menu
        protected override void GoBack()
        {
            GameEnvironment.GameStateManager.SwitchTo("mainMenuState");
        }

        protected override void GoForward(Point choise)
        {
            if(choise.X == 3 && choise.Y == 2)
            {
                loadPage(currentPage++);
                return;
            }
            if(choise.X == 0 && choise.Y == 2)
            {
                loadPage(currentPage--);
                return;
            }
            //check if the choisen option has a level assigned
            if(options[choise.X, choise.Y] is LevelOptionButton)
            {
                LevelOptionButton levelchoise = (LevelOptionButton)options[choise.X, choise.Y];
                if(levelchoise.HasLevel) return;
            }

            //loads selected level
            GameEnvironment.GameStateManager.SwitchTo("playingState");
            ((PlayingState)GameEnvironment.GameStateManager.GetGameState("playingState")).LoadLevel((choise.X+1)*(choise.Y+1) -1 +currentPage*8);
        }
    }
}
