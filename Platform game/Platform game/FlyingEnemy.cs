using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class FlyingEnemy : Sprite
    {
        public bool Left;
        public bool Right;
        public int Health;
        public TimeSpan fireTime;
        public FlyingEnemy(Texture2D image, Vector2 position, Color color, bool Left, bool Right, int Health)
            : base(image, position, color)
        {
            this.Right = Right;
            this.Left = Left;
            this.Health = Health;
        }
        public void MoveRight()
        {
            position.X += 10;
        }
        public void MoveLeft()
        {
            position.X -= 10;
        }
        public bool fire()
        {
            if (fireTime > TimeSpan.FromMilliseconds(50))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool IsOffScreen (int screensize)
        {
            if (hitbox.X + hitbox.Width > screensize+100||hitbox.X<-100)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool hit(Rectangle otherhitbox)
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
