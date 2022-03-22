using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Player_test
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private List<Sprite> _sprites;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var player = Content.Load<Texture2D>("wizard_2");
            var floorTile = Content.Load<Texture2D>("tiles_003");

            _sprites = new List<Sprite>()
            {
                new Entity(player)
                {
                    Input = new Input()
                    {
                        Left = Keys.A,
                        Down = Keys.S,
                        Right = Keys.D,
                        Jump = Keys.W,
                    },
                    Position = new Vector2(100,100),
                    Speed = 5f,
                    hasGravity = true,
                },

                new Entity(floorTile)
                {
                    Input = new Input(){},
                    Position = new Vector2(0,96*4),
                    hasGravity = false,
                },
                new Entity(floorTile)
                {
                    Input = new Input(){},
                    Position = new Vector2(96,96*4),
                    hasGravity = false,
                },
                new Entity(floorTile)
                {
                    Input = new Input(){},
                    Position = new Vector2(96*3,96*4),
                    hasGravity = false,
                },
                new Entity(floorTile)
                {
                    Input = new Input(){},
                    Position = new Vector2(96*4,96*4),
                    hasGravity = false,
                },
            };
        }

        protected override void Update(GameTime gameTime)
        {
            foreach (var sprite in _sprites)
                sprite.Update(gameTime, _sprites);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var sprite in _sprites)
                sprite.Draw(_spriteBatch);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
