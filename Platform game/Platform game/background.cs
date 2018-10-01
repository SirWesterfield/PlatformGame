using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class background : Sprite
    {
        Rectangle screensize;
        public background(Texture2D image, Vector2 position, Color color, Rectangle screensize)
            : base(image, position, color)
        {
            this.screensize = screensize;
        }

        public override void Draw(SpriteBatch spritebatch)
        {
            spritebatch.Draw(image, screensize, color);
        }

    }
}
