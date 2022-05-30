using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class Bridge : Trap
    {
        int timer = 0;//timer to count how long the bridge is down
        int downTime = 600;//the time it takes to make the bridge reappear
        public Bridge(int x, int y) : base(x, y)
        {   
            Add(new BridgeTile(x, y));
        }

        /// <summary>
        /// makes the bridge disappear
        /// </summary>
        public override void Activate()
        {
            //when it activates the trap will dissapear
            foreach (BridgeTile tile in Children)
            {
                tile.Visible = false;
            }
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
