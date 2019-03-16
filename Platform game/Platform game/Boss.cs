using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class Boss : Sprite
    {
        public bool Right;
        public bool Left;
       
        int downspeed;
        int upspeed;
        public int health;
        public Rectangle smallHitbox;
        Rectangle smallTopHitbox;
        public Boss(Texture2D image, Vector2 position, Color color, bool Left, bool Right, int downspeed, int upspeed, int health)
            : base(image, position, color)
        {
            this.Left = Left;
            this.Right = Right;
            this.downspeed = downspeed;
            this.upspeed = upspeed;
            this.health = health;
        }
        public void Draw(SpriteBatch spritebatch, Texture2D looks, Color chromatics)
        {
            spritebatch.Draw(looks, position, chromatics);
        }
        public void MoveRight()
        {
            position.X += 2;
        }
        public void MoveLeft()
        {
            position.X -= 2;
        }
        public void Sides(int screensize)
        {
            if (position.X <= 0 )
            {
                position.X = 0;
            }
            if (position.X + hitbox.Width >= screensize )
            {
                position.X = screensize - hitbox.Width;
            }
        }
        public void SmallHitbox()
        {
            smallHitbox = hitbox;
            smallHitbox.Y = hitbox.Y + 10;
            if (Right)
            {
                smallHitbox.Width = 100;
                smallHitbox.X = hitbox.X + 90;
            }
            if (Left)
            {
                smallHitbox.Width = 100;
                smallHitbox.X = hitbox.X+10;
            }
        }
        public bool otherHit(Rectangle otherhitbox)
        {
            
            if (smallHitbox.Intersects(otherhitbox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool topHit (Rectangle otherhitbox)
        {
            smallTopHitbox = hitbox;
            if (Right)
            {
                smallTopHitbox.Width = 100;
                smallTopHitbox.X = hitbox.X + 90;
                smallTopHitbox.Height = 1;
            }
            if (Left)
            {
                smallTopHitbox.Width = 100;
                smallTopHitbox.X = hitbox.X + 117;
                smallTopHitbox.Height = 1;
            }
            if (smallTopHitbox.Intersects(otherhitbox))
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
