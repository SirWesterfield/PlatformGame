using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class Crates : Sprite
    {


        
        public Crates (Texture2D image, Vector2 position, Color color)
            :base(image, position, color)
        {

        }


        public bool Intersect (Rectangle otherhitbox)
        {
            if (otherhitbox.Intersects(hitbox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void MoveLeft(int MoveSpeed)
        {
            position.X -= MoveSpeed;
        }
        public void MoveRight(int MoveSpeed)
        {
            position.X += MoveSpeed;
        }

        public void MoveDown (int screensize)
        {
            if (position.Y+hitbox.Height < screensize)
            {
                position.Y += 30;
            }
            if (position.Y+hitbox.Height>screensize)
            {
                position.Y = screensize - hitbox.Height;
            }
        }
    }
}
