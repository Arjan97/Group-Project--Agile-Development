using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BaseProject.GameComponents;

namespace BaseProject.GameObjects.Tiles
{
    internal class SpikeRoofTile : TrapTile
    {
        public bool PlayerHit = false;//check if player collides with this spike

        //variables used for particles and animation
        int direction;//direction of the animation
        int timer, particleTimer;//timers for particles and animation
        private float radians;//radians used to rotate sprite
        ParticleMachine particles = new ParticleMachine(ParticleType.WhirlParticle);//particlemachine to handle the particles

        public SpikeRoofTile(int x, int y) : base("img/tiles/spr_spikeroof", x, y) 
        {
            timer = GameEnvironment.Random.Next(30, 360);
            particleTimer = GameEnvironment.Random.Next(100, 600);
            direction = GameEnvironment.Random.Next(-1, 1);
            particles.Parent = this;
        }

        public override void Update(GameTime gameTime)
        {
            timer--;
            particleTimer--;

            HandleAnimation();

            particles.Update(gameTime);
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

            if(particleTimer <= 0)
            {
                particles.SpawnParticles(new Vector2(0, sprite.Height/2), new Vector2(0, 0), new Vector2(2, 2), new Vector2(25, 0), 300, "img/particle/spr_particle_spike");
                particleTimer = GameEnvironment.Random.Next(100, 600);
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
            particles.Draw(gameTime, spriteBatch);
            if (!visible || sprite == null)
                return;

            spriteBatch.Draw(sprite.Sprite, GlobalPosition, null, Color.White, radians - MathHelper.ToRadians(0), Origin, scale, SpriteEffects.None, 0);
        }

        /// <summary>
        /// function to activate the trap
        /// </summary>
        public override void Activate()
        {
            //when the trap is activated the tiles will drop
            moving = true;
            velocity.Y += 200;
            base.Activate();
        }

        /// <summary>
        /// function that checks if the spike needs to despawn when it hits the ground
        /// </summary>
        /// <param name="tile"other tiles it collides with></param>
        public override void HandleColission(GameObject tile)
        {   
            //when the spiketile collides with another tile it will turn invisible
            if (tile is Tile)
            {
                 visible = false;
            }
        }
    }
}
