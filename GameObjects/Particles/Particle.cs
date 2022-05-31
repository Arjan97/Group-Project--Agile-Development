using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BaseProject.GameObjects.Particles
{
    internal class Particle : SpriteGameObject
    {
        int lifeTime;//the amount of frames the particle stays alive 
        int maxTime;//the max amount of frames the particle stays alive, used for scale
        Vector2 acceleration;//increasement of the velocity each frame
        public Particle(string assetName, Vector2 position, Vector2 velocity, Vector2 acceleration, int lifeTime = 50) : base(assetName, -3)
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
            int opacity = lifeTime * 255 / maxTime;
            //System.Diagnostics.Debug.WriteLine(opacity);
            Color newColor = new Color(Shade, opacity);
            Shade = newColor;

        }


        public int LifeTime { get { return lifeTime; } }
    }
}
