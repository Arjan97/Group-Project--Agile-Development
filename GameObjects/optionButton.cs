using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BaseProject.GameObjects
{
    internal class optionButton : SpriteGameObject
    {
        TextGameObject label;
        public optionButton(float x, float y, float scaleX, float scaleY, String labelTxt) : base("img/menu/spr_optionBackground")
        {
            position = new Vector2(x, y);
            scale = new Vector2(1, 0.5f);
            origin = Center;
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
