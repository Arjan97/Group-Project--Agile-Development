using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects.Tiles
{
    internal class SpikeRoofTile : TrapTile
    {
        public bool PlayerHit = false;

        private float radians;
        int timer;
        int direction;
        public SpikeRoofTile(int x, int y) : base("img/tiles/spr_spikeroof", x, y) 
        {
            timer = GameEnvironment.Random.Next(30, 360);
            direction = GameEnvironment.Random.Next(-1,1);
        }

        public override void Update(GameTime gameTime)
        {
            timer--;
            HandleAnimation();
            base.Update(gameTime);
        }

        /// <summary>
        /// fucntion that gives the rotation to the spike
        /// </summary>
        void HandleAnimation()
        {
            //when the trap is activated the rotation gets reset
            if (((Trap)parent).Activated)
            {
                radians = 0;
                return;
            }
            //timer to start rotating
            if(timer <= 0)
            {

                if(timer <= -20)
                {
                    timer = GameEnvironment.Random.Next(30, 360);
                    direction = GameEnvironment.Random.Next(-1, 1);
                    radians = 0;
                    return;
                }

                if(timer >= -15 && timer <= -5)
                {
                    radians += 0.05f * direction;
                    return;
                }

                radians -= 0.05f* direction;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!visible || sprite == null)
                return;

            spriteBatch.Draw(sprite.Sprite, GlobalPosition, null, Color.White, radians - MathHelper.ToRadians(0), Origin, scale, SpriteEffects.None, 0);
        }

        public override void Activate()
        {
            //when the trap is activated the tiles will drop
            moving = true;
            velocity.Y += 200;
            base.Activate();
        }
        public override void HandleColission(GameObject tile)
        {   
            //when the spiketile collides with another tile it will turn invisible
            if (tile is Tile)
            {
                 visible = false;
            }
        }

        public override void CheckColission(SpriteGameObject obj)
        {   
            base.CheckColission(obj);
        }

    }
}
