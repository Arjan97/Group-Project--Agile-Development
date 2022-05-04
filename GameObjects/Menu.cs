﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using BaseProject.GameObjects.menuObjects;

namespace BaseProject.GameStates
{
    internal class Menu : GameObjectList
    {
        protected Point currentOption = new Point(0,0);
        Point maxOptions = new Point(0,0);
        protected optionButton[,] options;
        SpriteGameObject arrow;

        public Menu(int x, int y)
        {
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

           if( inputHelper.KeyPressed(Keys.Q) || inputHelper.KeyPressed(Keys.NumPad5)){
                GoBack();
           }

           if (inputHelper.KeyPressed(Keys.E) || inputHelper.KeyPressed(Keys.NumPad6)){
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

        //function to place the selection arrow at the right option
        void PositionArrow()
        {
            optionButton currentOption = options[this.currentOption.X, this.currentOption.Y];
            arrow.Position = new Vector2(currentOption.Position.X - (currentOption.Width/2 + arrow.Width), currentOption.Position.Y); 
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x <=maxOptions.X; x++)
            {
                for (int y = 0; y <=maxOptions.Y; y++) { if (options[x, y] != null)  options[x, y].Draw(gameTime, spriteBatch);  }
            }
            base.Draw(gameTime, spriteBatch);
        }
        protected virtual void GoBack() { }
        protected virtual void GoForward(Point choise) { }


        //function that skips selections that dont have options
        void SkipNull(InputHelper inputHelper)
        {
            while (options[currentOption.X, currentOption.Y] == null)
            {
                MoveCursor(inputHelper);
                Wrap();
            }
        }

        //function that moves the cursor from one edge of the options to the other
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
        void MoveCursor(InputHelper inputHelper)
        {
            if (inputHelper.KeyPressed(Keys.W) || inputHelper.KeyPressed(Keys.I)) { currentOption.Y--; }
            if (inputHelper.KeyPressed(Keys.A) || inputHelper.KeyPressed(Keys.J)) { currentOption.X--; }
            if (inputHelper.KeyPressed(Keys.S) || inputHelper.KeyPressed(Keys.K)) { currentOption.Y++; }
            if (inputHelper.KeyPressed(Keys.D) || inputHelper.KeyPressed(Keys.L)) { currentOption.X++; }
        }
    }
}