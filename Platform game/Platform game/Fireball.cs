using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class Fireball : Sprite
    {
        public bool Left;
        public bool Right;
        public bool IsPLayer;
        public bool down;
        public Fireball(Texture2D image, Vector2 position, Color color, bool Left, bool Right,bool down, bool IsPLayer)
            :base(image, position, color)
        {
            this.Left = Left;
            this.Right = Right;
            this.IsPLayer = IsPLayer;
            this.down = down;
        }

        public void MoveLeft ()
        {
            position.X -= 10;
        }
        public void MoveRight ()
        {
            position.X += 10;
        }
        public void MoveDown()
        {
            position.Y += 10;
        }
        public bool IsOffScreen (int screensizeX, int screensizeY)
        {
            if (hitbox.X + hitbox.Width > screensizeX || hitbox.X < 0 || hitbox.Y > screensizeY-100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Hit(Rectangle otherhitbox)
        {
            if (hitbox.Intersects(otherhitbox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
