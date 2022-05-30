using Microsoft.Xna.Framework;
using System;
using BaseProject.GameObjects.Particles;

namespace BaseProject.GameComponents
{
    public enum ParticleType
    {
        Particle,
        WhirlParticle,
    }
    public class ParticleMachine : GameObjectList
    {
        ParticleType particleType;//saves a type to use this machine for different particle types

        /// <summary>
        /// constructs a particle machine
        /// </summary>
        /// <param name="type">The type of particles it needs to spawn</param>
        public ParticleMachine(ParticleType type)
        {
            particleType = type;
        }

        public void SpawnParticles(Vector2 position, Vector2 velocity, Vector2 acceleration, Vector2 Effect, int lifeTime = 50, string texture = "img/particle/particle")
        {
            SpawnParticles(position,velocity,acceleration,lifeTime,texture,Effect.X,Effect.Y);
        }

        /// <summary>
        /// function that creates a particle
        /// </summary>
        /// <param name="position">the position relative to the particlemachine parent</param>
        /// <param name="velocity">the velocity of the particle</param>
        /// <returns>void</returns>
        public void SpawnParticles(Vector2 position, Vector2 velocity, Vector2 acceleration, int lifeTime = 50, string texture = "img/particle/particle", float effectX = 0, float effectY = 0)
        {
            Vector2 Effect = Vector2.Zero;
            if(effectX != 0 || effectY != 0)
            {
                Effect = new Vector2(effectX, effectY);
            }

            switch (particleType)
            {
                case ParticleType.WhirlParticle:
                    Add(new WhirlParticle(texture, position, velocity, acceleration, Effect, lifeTime));
                    break;

                case ParticleType.Particle:
                    Add(new Particle(texture, position, velocity, acceleration, lifeTime));
                    break;
            }
        }


        /// <summary>
        /// Update function that also calls the deathCheck
        /// </summary>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            CheckDead();
        }

        /// <summary>
        /// cycles thru all particles to check if they are expired
        /// </summary>
        /// <returns>void</returns>
        void CheckDead()
        {
            bool dead = false;
            foreach (Particle particle in Children)
            {
                if (particle.LifeTime < 0)
                {
                    Remove(particle);
                    dead = true;
                    break;//breaks the foreach because the list got edited
                }
            }
            //check if the foreach loop was finished or interupted because of dying particle
            if (dead)
            {
                //when interupted restarts the search
                CheckDead();
            }
        }
    }
}
