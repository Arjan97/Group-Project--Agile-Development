﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.GameObjects.Tiles
{
    internal class BridgeTile : TrapTile
    {
        public BridgeTile(int x, int y) :  base("img/tiles/spr_bridge", x, y){}
        public override void Activate()
        {
            //when it activates the trap will dissapear
            visible = false;
            base.Activate();
        }
    }
}