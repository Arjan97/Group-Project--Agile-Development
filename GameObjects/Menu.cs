using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseProject.GameObjects.menuObjects;
using BaseProject.GameComponents;

namespace BaseProject.GameStates
{
    internal class Menu : GameObjectList
    {
        //variables used to select options
        protected Point currentOption = new Point(0,0);//a point that stores the coördinates of the option the player currently is on
        Point maxOptions = new Point(0,0);//point that stores how many steps the player can move to the X and Y

        //variables that store the objects
        protected optionButton[,] options;//a double array that stores all the options
        SpriteGameObject arrow;//sprite of the arrow selector

        protected SpriteGameObject backGround;
        protected InputHandler input = GameEnvironment.input;//inputhandler that handles the movement thru menus

        /// <summary>
        /// consturctor that creates a menu grid
        /// </summary>
        /// <param name="x">number of colums</param>
        /// <param name="y">number of rows</param>
        public Menu(int x, int y)
        {
            backGround = new SpriteGameObject("img/levels/Background@2");
            backGround.Position = new Vector2(GameEnvironment.Screen.X * 2 - 290, GameEnvironment.Screen.Y / 2 + 30);
            Add(backGround);

            if (!(x > 0 && y > 0)) return;//check if the menu has options to cycle thru

                maxOptions = new Point(x - 1, y - 1);
                options = new optionButton[x, y];
                arrow = new SpriteGameObject("img/menu/spr_selectIcon");
                arrow.Scale = new Vector2(2, 2);
                Add(arrow);
        }


        public override void HandleInput(InputHelper inputHelper)
        {
            MoveCursor(inputHelper);
            Wrap();

           if( inputHelper.KeyPressed(input.P1(Buttons.B)) || inputHelper.KeyPressed(input.P2(Buttons.B))){
                GoBack();
           }

           if (inputHelper.KeyPressed(input.P1(Buttons.A)) || inputHelper.KeyPressed(input.P2(Buttons.A))){
                GoForward(currentOption);
           }
            SkipNull(inputHelper);
            PositionArrow();
            base.HandleInput(inputHelper);
        }

        public override void Update(GameTime gameTime)
        {
            for(int x = 0; x < maxOptions.X+1; x++)
            {
                for(int y = 0; y < maxOptions.Y+1; y++) { if(options[x,y] != null) options[x,y].Update(gameTime);}
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// function that moves the arrow across the options
        /// </summary>
        void PositionArrow()
        {
            optionButton currentOption = options[this.currentOption.X, this.currentOption.Y];
            arrow.Position = new Vector2(currentOption.Position.X - (currentOption.Width/2 + arrow.Width), currentOption.Position.Y); 
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            for (int x = 0; x <=maxOptions.X; x++)
            {
                for (int y = 0; y <=maxOptions.Y; y++) { if (options[x, y] != null)  options[x, y].Draw(gameTime, spriteBatch);  }
            }
            arrow.Draw(gameTime,spriteBatch);
        }

        /// <summary>
        /// function to go back to the previous screen
        /// </summary>
        protected virtual void GoBack() { }

        /// <summary>
        /// function to activate the selected option
        /// </summary>
        /// <param name="choise">point consisting the x and y of the choise</param>
        protected virtual void GoForward(Point choise) { }


        /// <summary>
        /// function that skips options in the menu that are empty
        /// </summary>
        /// <param name="inputHelper">the player input</param>
        void SkipNull(InputHelper inputHelper)
        {
            while (options[currentOption.X, currentOption.Y] == null)
            {
                MoveCursor(inputHelper);
                Wrap();
            }
        }

        /// <summary>
        /// function that moves the cursor to one side of the screen to the other when the player wants to go off screen
        /// </summary>
        void Wrap()
        {
            if(currentOption.X > maxOptions.X)
            {
                currentOption.X = 0;
            }
            if(currentOption.X < 0)
            {
                currentOption.X = maxOptions.X;
            }
            if (currentOption.Y > maxOptions.Y)
            {
                currentOption.Y = 0;
            }
            if (currentOption.Y < 0)
            {
                currentOption.Y = maxOptions.Y;
            }
        }
        /// <summary>
        /// moves the cursor acros the options
        /// </summary>
        /// <param name="inputHelper">the player input</param>
        void MoveCursor(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(input.P1(Buttons.up)) || inputHelper.KeyPressed(input.P2(Buttons.up))) { currentOption.Y--; }
            if (inputHelper.KeyPressed(input.P1(Buttons.left)) || inputHelper.KeyPressed(input.P2(Buttons.left))) { currentOption.X--; }
            if (inputHelper.KeyPressed(input.P1(Buttons.down)) || inputHelper.KeyPressed(input.P2(Buttons.down))) { currentOption.Y++; }
            if (inputHelper.KeyPressed(input.P1(Buttons.right)) || inputHelper.KeyPressed(input.P2(Buttons.right))) { currentOption.X++; }
        }
    }
}
