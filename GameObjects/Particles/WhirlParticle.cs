using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects.Particles
{
    internal class WhirlParticle : Particle
    {
        Vector2 whirleffect;//vector that is used to create the whirl effect

        public WhirlParticle(string assetName, Vector2 position, Vector2 velocity, Vector2 acceleration, Vector2 whirleffect, int lifeTime = 50) : base(assetName, position, velocity, acceleration, lifeTime)
        {
            this.whirleffect = whirleffect;
        }

        /// <summary>
        /// function that handles the whirl effect
        /// </summary>
        private void HandleWhirl()
        {
            if ((velocity.X >= whirleffect.X || velocity.X <= -whirleffect.X) && whirleffect.X != 0)
            {
                velocity.X = -velocity.X;
            }

            if ((velocity.Y >= whirleffect.Y || velocity.Y <= -whirleffect.Y) && whirleffect.Y != 0)
            {
                velocity.Y = -velocity.Y;
            }
        }
        public override void Update(GameTime gameTime)
        {
            HandleWhirl();
            base.Update(gameTime);
        }
    }
}
