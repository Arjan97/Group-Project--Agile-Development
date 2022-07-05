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
                System.Diagnostics.Debug.WriteLine(percentage);
            if(percentage == 0)
            {
                percentage = 100;
            }
            Shade = new Color(255 * percentage /100, 255 * percentage / 100, 255 * percentage / 100);
            
            base.Update(gameTime);
        }
    }
}
