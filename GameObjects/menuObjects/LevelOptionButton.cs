using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects.menuObjects
{
    internal class LevelOptionButton : optionButton
    {
        SpriteGameObject icon;
        public LevelOptionButton(int x, int y, int level, string text) : base(x,y, text, 1.5f, 1.5f)
        {
            Init(x,y);
            icon = new SpriteGameObject($"img/levels/icons/spr_iconLevel{level}");
            icon.Position = new Vector2(Position.X, Position.Y -30); 
            icon.Scale = new Vector2(1.5f, 1.5f);
        }

        //function that gets called when its an empty slot
        public LevelOptionButton(int x, int y) : base(x, y, " ", 1.5f, 1.5f)
        {
            Init(x,y);
        }

        void Init(int x,int y)
        {
            position = new Vector2((x+1)*250, y * 200 + 200);
            label.Position += new Vector2(0, 50);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            if(icon != null) icon.Draw(gameTime, spriteBatch);

        }
    }
}
