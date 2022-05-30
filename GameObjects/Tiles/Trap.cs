using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BaseProject.GameObjects.Tiles
{
    internal class Trap : GameObjectList
    {
        public int tileSize = Tile.tileSize;//the height/width of a tile

        //variables used for trap activation
        public float ghostDistance;//the distance between the ghost and this trap, used to keep track which trap is closest
        public Vector2 buttonPosition;//the position of a button
        public Button button;//the buttton that activates the trap

       internal bool activated = false;//bool that keeps track if the trap is already activated
        public Trap(int x, int y)
        {
        }


        /// <summary>
        /// function that creates a button for the trap
        /// </summary>
        public virtual void CreateButton()
        {
            int length = children.Count;
            buttonPosition = new Vector2(Children[0].Position.X + tileSize * length / 2, Children[0].Position.Y);
            button = new Button(buttonPosition, this);
        }

        
        /// <summary>
        /// function that activates each tile in the trap
        /// </summary>
        public virtual void Activate()
        {
            foreach (TrapTile tile in Children)
            {
                tile.Activate();
            }
            activated = true;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            base.Draw(gameTime, spriteBatch);
            if (button.Visible && !button.Hidden)
            {
                button.Draw(gameTime, spriteBatch);
            }
        }

        /// <summary>
        /// function to get or change the key for the button
        /// </summary>
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

        public bool Activated
        {
            get { return activated; }
        }
    }
}
