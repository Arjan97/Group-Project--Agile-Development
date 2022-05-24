using Microsoft.Xna.Framework;
using System;
using BaseProject.GameObjects;

namespace BaseProject.GameComponents
{
    public class ParticleMachine : GameObjectList
    {
        Type particleType;//saves a type to use this machine for different particle types

        /// <summary>
        /// constructs a particle machine
        /// </summary>
        /// <param name="type">The type of particles it needs to spawn</param>
        public ParticleMachine(Type type)
        {
            particleType = type;
        }

        /// <summary>
        /// function that creates a particle
        /// </summary>
        /// <param name="position">the position relative to the particlemachine parent</param>
        /// <param name="velocity">the velocity of the particle</param>
        /// <returns>void</returns>
        public void SpawnParticles(Vector2 position, Vector2 velocity)
        {
            
            Add((GameObject)Activator.CreateInstance(particleType, "img/particle/particle", position, velocity, new Vector2(0,10), 50));
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
