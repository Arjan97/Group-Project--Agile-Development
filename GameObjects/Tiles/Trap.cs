using System;
using System.Collections.Generic;
using System.Text;
using BaseProject.GameObjects.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameObjects.Tiles
{
    internal class Trap : GameObjectList
    {
        public int tileSize = (new Tile("", 0, 0)).tileSize;
        public float ghostDistance;
        public Vector2 buttonPosition;
        public Button button;

       internal bool Activated = false;
        public Trap(int x, int y, int length)
        {
            buttonPosition = new Vector2(x * tileSize + tileSize * length / 2, y * tileSize + tileSize);
            button = new Button(buttonPosition, this);
        }

        
        public virtual void Activate()
        {
            foreach (TrapTile tile in Children)
            {
                tile.Activate();
            }
            Activated = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            base.Draw(gameTime, spriteBatch);
            if (button.Visible && !button.Hidden)
            {
                button.Draw(gameTime, spriteBatch);
            }
        }

        //function to get or change the key for the button
        public virtual Keys AssignedKey
        {
            get { return button.Key; }
            set { button.AssignKey(value); }
        }

        public override void Update(GameTime gameTime)
        {
            button.Update(gameTime);
            base.Update(gameTime);
        }

        public override void HandleInput(InputHelper inputHelper)
        {
            button.HandleInput(inputHelper);
            base.HandleInput(inputHelper);
        }

        public bool buttonVisibility
        {
            set { button.Hidden = !value; }
        }
    }
}
