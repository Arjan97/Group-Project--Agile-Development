using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects.menuObjects
{
    internal class optionButton : SpriteGameObject
    {
       protected TextGameObject label;//textgameobject of the label on the object
        public optionButton(float x, float y, String labelTxt, float scaleX = 1,  float scaleY = 0.5f) : base("img/menu/spr_optionBackground")
        {
            position = new Vector2(x, y);
            scale = new Vector2(scaleX, scaleY);
            origin = Center;

            //initialises the text label
            label = new TextGameObject("font/Arial12");
            label.Text = labelTxt;
            label.Parent = this;
            label.Position = new Vector2(-Width*1/3, -12);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
            label.Draw(gameTime, spriteBatch);
        }
    }
}
