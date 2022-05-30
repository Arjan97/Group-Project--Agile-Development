using BaseProject.GameComponents;
using BaseProject.GameObjects.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace BaseProject.GameObjects.Tiles
{
    internal class BridgeTile : TrapTile
    {
        ParticleMachine particles = new ParticleMachine(ParticleType.Particle);
        public BridgeTile(int x, int y) :  base("img/tiles/spr_bridge", x, y)
        {
            particles.Parent = this;
        }
        public override void Activate()
        {
            //when it activates the trap will dissapear
            visible = false;
            base.Activate();
            Random r = GameEnvironment.Random;
            for(int i = 0; i < 15; i++)
            {
                particles.SpawnParticles(new Vector2(-0.5f*sprite.Width + i*sprite.Width/5, 0.5f*sprite.Width -(float)(sprite.Width*r.NextDouble())), new Vector2(0, 0), new Vector2(r.Next(1,20),r.Next(1,20)));
            }
        }

        public override void Update(GameTime gameTime)
        {
            particles.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            particles.Draw(gameTime, spriteBatch);
            base.Draw(gameTime, spriteBatch);
        }
    }
}