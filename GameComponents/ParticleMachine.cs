using Microsoft.Xna.Framework;
using System;
using BaseProject.GameObjects;

namespace BaseProject.GameComponents
{
    public class ParticleMachine : GameObjectList
    {
        Type particleType;

        public ParticleMachine(Type type)
        {
            particleType = type;
        }

        public void SpawnParticles(Vector2 position, Vector2 velocity)
        {
            
            Add((GameObject)Activator.CreateInstance(particleType, "img/particle/particle", position, velocity, new Vector2(0,10), 50));
        }

        public override void Update(GameTime gameTime)
        {
            if(children.Count > 0)
            System.Diagnostics.Debug.WriteLine(((Particle)Children[0]).LifeTime);
            base.Update(gameTime);
            checkDead();
        }

        void checkDead()
        {
            bool dead = false;
            foreach (Particle particle in Children)
            {
                if (particle.LifeTime < 0)
                {
                    Remove(particle);
                    dead = true;
                    break;
                }
            }
            if (dead)
            {
                checkDead();
            }
        }
    }
}
