using Microsoft.Xna.Framework;

namespace BaseProject.GameObjects
{
    public class Cooldown : SpriteGameObject
    {
        public Cooldown(string assetName, GameObject gameEnvironment, Vector2 position ) : base(assetName)
        {
            parent = gameEnvironment;
            this.position = position;
        }

        public void Update(int percentage, GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
