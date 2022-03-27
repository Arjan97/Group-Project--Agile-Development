using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class Bridge : Trap
    {
        int timer = 0;
        int downTime = 600;
        public Bridge(int x, int y, int length) : base(x, y, length)
        {
            for (int i = 0; i < length; i++)
            {
                Add(new BridgeTile(x + i, y));
            }
        }

            public override void Activate()
        {
            visible = false;
            base.Activate();
        }

        public override void Update(GameTime gameTime)
        {
            //cooldown for the bridge to rebuild itself
            if (!visible) {
                if(timer > downTime) {
                    visible = true;
                    foreach (BridgeTile tile in Children)
                    {
                        tile.Visible = true;
                    }
                }
                timer++;
            }
            base.Update(gameTime);
        }
    }
}
