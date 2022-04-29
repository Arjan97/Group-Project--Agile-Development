using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class SpikeRoof : Trap
    {
        public SpikeRoof(int x, int y, int length) : base(x, y, length)
        {
            for (int i = 0; i < length; i++)
            {
                Add(new SpikeRoofTile(x + i, y));
            }
        }

        public override void Activate()
        {
            velocity.Y += 200;
            base.Activate();

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
           
        }
    }
}
