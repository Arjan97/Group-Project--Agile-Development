using Microsoft.Xna.Framework;
using System;

namespace BaseProject.GameObjects
{
    internal class Particle : SpriteGameObject
    {
        int lifeTime;//the amount of frames the particle stays alive 
        int maxTime;//the max amount of frames the particle stays alive, used for scale
        Vector2 acceleration;//increasement of the velocity each frame
        public Particle(string assetName, Vector2 position, Vector2 velocity, Vector2 acceleration, int lifeTime = 50) : base(assetName, -2)
        {
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.lifeTime = lifeTime;
            maxTime = lifeTime;
        }

        public override void Update(GameTime gameTime)
        {
            velocity += acceleration;
            base.Update(gameTime);
            lifeTime--;

            //converts the lifetime into opacity
            int opacity = lifeTime / maxTime * 255;
            shade = new Color(shade.R, shade.G, shade.B, opacity);
        }


        public int LifeTime { get { return lifeTime; } }
    }
}
