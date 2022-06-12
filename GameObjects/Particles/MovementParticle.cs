using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects.Particles
{
    internal class MovementParticle : Particle
    {
        int timeAlive = 0;
        public MovementParticle(string assetName, Vector2 position, Vector2 velocity, Vector2 acceleration, int lifeTime = 50) : base(assetName, position, velocity, acceleration, lifeTime)
        {
            //made class in case we need it
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
            if (timeAlive < 5)
            {
                visible = false;
            } else
            {
                visible = true;
            }
            timeAlive++;
        }
    }
}
