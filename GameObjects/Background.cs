using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects
{
    internal class Background : SpriteGameObject
    {
        protected float _layer { get; set; }
        protected Texture2D _texture;

        public float Layer
        {
            get { return _layer; }
            set { _layer = value; }
        }

        public Vector2 Position;
        public Rectangle Rectangle
        {
            get { return new Rectangle((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height); }
        }
        public Background(Texture2D texture) : base("img/backgrounds/gameBackground")
        {
            //position = new Vector2(X, Y);
            //scale = new Vector2(scaleX, scaleY);
            origin = Center;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, Position, null, Color.White, 0, new Vector2(0, 0), 1f, SpriteEffects.None, Layer);
           // base.Draw(gameTime, spriteBatch);
        }
    }
}
