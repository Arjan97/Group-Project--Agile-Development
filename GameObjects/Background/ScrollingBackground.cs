using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.GameObjects.Background
{
    public class ScrollingBackground : GameObjectList
    {
        private bool _constantSpeed; //bool for letting the layer move even when player does not

        private float _scrollingSpeed; //speed of the moving images 

        private List<Background> _sprites; //list for the sprites

        private readonly Player _player; //usage for player location

        private float _speed; //speed of the moving images

        //public for creating new list
        public ScrollingBackground(Texture2D texture, Player player, float scrollingSpeed, bool constantSpeed = false)
          : this(new List<Texture2D>() { texture, texture }, player, scrollingSpeed, constantSpeed)
        {

        }

        //public for usage outside class and placing the images
        public ScrollingBackground(List<Texture2D> textures, Player player, float scrollingSpeed, bool constantSpeed = false)
        {
            _player = player;

            _sprites = new List<Background>();

            for (int i = 0; i < textures.Count; i++)
            {
                var texture = textures[i];

                _sprites.Add(new Background(texture)
                {
                    Position = new Vector2(i * texture.Width - Math.Min(i, i + 1), GameEnvironment.Screen.Y - texture.Height),
                });
            }
            _scrollingSpeed = scrollingSpeed;
            _constantSpeed = constantSpeed;
            System.Diagnostics.Debug.WriteLine("lijstje gemaakt?");
        }

        public override void Update(GameTime gameTime)
        {
            ApplySpeed(gameTime);
            CheckPosition();
        }

        //speed of the scrolling images
        private void ApplySpeed(GameTime gameTime)
        {
            _speed = (float)(_scrollingSpeed * gameTime.ElapsedGameTime.TotalSeconds);

            if (!_constantSpeed || _player.Velocity.X > 0)
                _speed *= _player.Velocity.X;

            foreach (var sprite in _sprites)
            {
                sprite.Position.X -= _speed;
            }
        }

        //checks position for the images
        private void CheckPosition()
        {
            for (int i = 0; i < _sprites.Count; i++)
            {
                var sprite = _sprites[i];

                if (sprite.Rectangle.Right <= 0)
                {
                    var index = i - 1;

                    if (index < 0)
                        index = _sprites.Count - 1;

                    sprite.Position.X = _sprites[index].Rectangle.Right - (_speed * 2);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach (var sprite in _sprites)
                sprite.Draw(gameTime, spriteBatch);
        }
    }
}

