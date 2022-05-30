using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects.menuObjects
{
    internal class LevelOptionButton : optionButton
    {
        SpriteGameObject icon;//screenshot of the level used as image

        /// <summary>
        /// constructor that makes an option with icon
        /// </summary>
        /// <param name="x">X-position</param>
        /// <param name="y">Y-position</param>
        /// <param name="level">level number</param>
        /// <param name="text">level name</param>
        public LevelOptionButton(int x, int y, int level, string text) : this(x,y, text)
        {
            icon = new SpriteGameObject($"img/levels/icons/spr_iconLevel{level}");
            icon.Position = new Vector2(Position.X, Position.Y -30); 
            icon.Scale = new Vector2(1.5f, 1.5f);
        }

        /// <summary>
        /// consturctor that makes an empty option
        /// </summary>
        /// <param name="x">X-position</param>
        /// <param name="y">Y-position</param>
        /// <param name="text">level label</param>
        public LevelOptionButton(int x, int y, string text = " ") : base(x, y, text, 1.5f, 1.5f)
        {
            position = new Vector2((x + 1) * 250, y * 200 + 200);
            label.Position += new Vector2(0, 50);
        }


        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if(icon != null) icon.Draw(gameTime, spriteBatch);
        }

        public bool HasLevel { get { return icon != null; } }
    }
}
