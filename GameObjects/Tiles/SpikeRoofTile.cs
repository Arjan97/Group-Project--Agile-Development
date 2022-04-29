using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class SpikeRoofTile : TrapTile
    {
        public bool PlayerHit = false;
        public SpikeRoofTile(int x, int y) : base("img/tiles/spr_spikeroof", x, y) 
        {
    }

        public override void Activate()
        {
            moving = true;
            velocity.Y += 200;
            base.Activate();
        }
        public override void HandleColission(GameObject tile)
        {
            if (tile is Tile)
            {
                 visible = false;
            }
        }

        public override void CheckColission(SpriteGameObject obj)
        {   
            base.CheckColission(obj);
        }

    }
}
