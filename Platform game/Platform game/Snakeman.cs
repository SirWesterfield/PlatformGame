using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class Snakeman : Sprite
    {
        public bool Left;
        public bool Right;
        public bool ground;
        public bool jumping;
        public int upspeed;
        public int downspeed;
        public bool move;
        public bool attack;
        public TimeSpan fireTime;
        Rectangle bigHitbox;
        public Snakeman(Texture2D image, Vector2 position, Color color, bool Left, bool Right, int upspeed, int downspeed, bool move, bool attack)
            :base(image,position,color)
        {
            this.Left = Left;
            this.Right = Right;
            this.upspeed = upspeed;
            this.downspeed = downspeed;
            this.move = move;
            this.attack = attack;
        }
        public void Sides(int screensize)
        {
            if (position.X <= 0&&Right == false)
            {
                Right = true;
                Left = false;
            }
            if (position.X + hitbox.Width >= screensize&&Left == false)
            {
                Left = true;
                Right = false;
            }
        }
        public bool Fire()
        {
            if (fireTime > TimeSpan.FromMilliseconds(1000))
            {
                return true;
            }
            else
            { 
                return false;
            }
        }
        public void Draw (SpriteBatch spritebatch, Texture2D looks, Color chromatics)
        {
            spritebatch.Draw(looks, position, chromatics);
        }
        public void MoveRight()
        {           
                position.X += 5;
        }
        public void MoveLeft()
        {
                position.X -= 5;
        }
        public void MoveUp()
        {
                position.Y += upspeed;
        }
        public void MoveDown()
        {
            position.Y += downspeed;
        }
        public bool DownyDown ()
        {
            if (ground == false &&jumping == false)
            {
                return true;
            }
            return false;
        }
        public void Ground (int screensize)
        {
            if (hitbox.Y +hitbox.Height > screensize&&jumping == false)
            {
                position.Y = screensize - hitbox.Height;
                ground = true;
            }
        }
        public bool hit (Rectangle otherhitbox)
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
        public bool otherHit(Rectangle otherhitbox)
        {
            bigHitbox = hitbox;
            bigHitbox.Width += 250;
            bigHitbox.X = hitbox.X - 90;
            if (bigHitbox.Intersects(otherhitbox))
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
