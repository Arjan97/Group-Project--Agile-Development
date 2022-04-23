using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using BaseProject.GameObjects;

namespace BaseProject.GameStates
{
    internal class Menu : GameObjectList
    {
        Point currentOption;
        Point maxOptions = new Point(0,0);
        protected optionButton[,] options;
        SpriteGameObject arrow;

        public Menu(int x, int y)
        {
            if (x > 0 && y > 0)
            {
                maxOptions = new Point(x - 1, y - 1);
                options = new optionButton[x, y];
                arrow = new SpriteGameObject("img/menu/spr_selectIcon");
                arrow.Scale = new Microsoft.Xna.Framework.Vector2(2, 2);
                Add(arrow);
            }
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
            base.HandleInput(inputHelper);
        }

        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            for(int x = 0; x < maxOptions.X+1; x++)
            {
                for(int y = 0; y < maxOptions.Y+1; y++) { options[x,y].Update(gameTime);}
            }
            PositionArrow();
            base.Update(gameTime);
        }

        void PositionArrow()
        {
            optionButton currentOption = options[this.currentOption.X, this.currentOption.Y];
            arrow.Position = new Microsoft.Xna.Framework.Vector2(currentOption.Position.X - (currentOption.Width/2 + arrow.Width), currentOption.Position.Y); }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < maxOptions.X+1; x++)
            {
                for (int y = 0; y < maxOptions.Y + 1; y++) { options[x, y].Draw(gameTime, spriteBatch); }
            }
            base.Draw(gameTime, spriteBatch);
        }
        protected virtual void GoBack() { }
        protected virtual void GoForward(Point choise) { }

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
