using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class Bars : Sprite
    {

        public Bars (Texture2D image, Vector2 position, Color color)
            :base(image, position, color)
        {

        }

        public void MoveLeft ()
        {
            position.X -= 1;
        }
        public void MoveRight ()
        {
            position.X += 1;
        }

    }
}
