using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class PlayerSprite:Sprite
    {

        public PlayerSprite(Texture2D image, Vector2 position, Color color)
            :base(image,position, color)
        {

        }


        public void OtherDraw(SpriteBatch spritebatch, Color color)
        {
            spritebatch.Draw(image, position, color);
        }

    }
}
