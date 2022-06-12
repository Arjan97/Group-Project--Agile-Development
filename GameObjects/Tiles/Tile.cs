using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class Tile : SpriteGameObject
    {
       public bool moving = false;//bool that keeps track if this object is moving (used for colission)
       public static int tileSize = 60;//static int for the number of pixels of a tile

        public Vector2 location;//vector2 that stores the X and Y of the grid
        public Tile(string assetName, int X, int Y) : base(assetName,-1)
        {
            location = new Vector2(X, Y);
            position.X = X * tileSize;
            position.Y = Y * tileSize + tileSize;
        }
    }
}
