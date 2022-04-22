using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameStates
{
    internal class MenuState : GameObjectList
    {
        Point currentOption;
        Point maxOptions = new Point(0,0);
        protected SpriteGameObject[,] options;
        public MenuState(int x, int y)
        {
            maxOptions = new Point(x-1, y-1);
            options = new SpriteGameObject[x,y];
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
            base.Update(gameTime);
        }
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int x = 0; x < maxOptions.X + 1; x++)
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
