using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class Sprite
    {
        public Texture2D image;
        public Vector2 position;
        public Color color;
        public Rectangle hitbox
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, image.Width, image.Height);
            }
        }
        public Sprite(Texture2D image, Vector2 position, Color color)
        {
            this.image = image;
            this.position = position;
            this.color = color;
        }

        public virtual void Draw (SpriteBatch spritebatch)
        {
            spritebatch.Draw(image, position, color);
        }
        

    }
}
