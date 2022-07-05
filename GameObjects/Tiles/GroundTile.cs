namespace BaseProject.GameObjects.Tiles
{
    internal class GroundTile : Tile
    {
        public GroundTile(int X, int Y) : base("img/tiles/spr_groundTile", X, Y)
        {
            Layer = 0; 
            
        }
    }
}
