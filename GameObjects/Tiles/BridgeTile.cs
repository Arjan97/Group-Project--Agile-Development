using BaseProject.GameComponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;


namespace BaseProject.GameObjects.Tiles
{
    internal class BridgeTile : TrapTile
    {
        ParticleMachine particles = new ParticleMachine(typeof(Particle));
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
            for(int i = 0; i < 5; i++)
            {
                particles.SpawnParticles(new Vector2(-0.5f*sprite.Width + i*sprite.Width/5, 0.5f*sprite.Width), new Vector2(0, -100));
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
            System.Diagnostics.Debug.WriteLine("hallo");
            base.Draw(gameTime, spriteBatch);
        }
    }
}