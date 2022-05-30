namespace BaseProject.GameObjects.Tiles
{
    internal class BridgeTile : TrapTile
    {
        public BridgeTile(int x, int y) :  base("img/tiles/spr_bridge", x, y){}

        /// <summary>
        /// function to make the tiles disappear
        /// </summary>
        public override void Activate()
        {
            //when it activates the trap will dissapear
            visible = false;
            base.Activate();
        }
    }
}