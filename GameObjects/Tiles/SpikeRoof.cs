using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class SpikeRoof : Trap
    {
        public SpikeRoof(int x, int y) : base(x, y)
        {

                Add(new SpikeRoofTile(x, y));
        }

        public override void Activate()
        {
            //when the trap is activated, it will fall down
            velocity.Y += 200;
            base.Activate();

        }


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);


        }

    }
}
