using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class Enemy : Sprite
    {
        public enum State
        {
            Follow,
            Run,
            Shoot
            
        }

        public State state = State.Follow;

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
        public Rectangle otherHitbox;
        public bool OnPlatform;
        public bool InPlayerArea;
        public Enemy(Texture2D image, Vector2 position, Color color, bool Left, bool Right, int upspeed, int downspeed, bool move, bool attack, bool OnPlatform, bool InPlayerArea)
            :base(image,position,color)
        {
            this.Left = Left;
            this.Right = Right;
            this.upspeed = upspeed;
            this.downspeed = downspeed;
            this.move = move;
            this.attack = attack;
            this.OnPlatform = OnPlatform;
            this.InPlayerArea = InPlayerArea;
        }
        /*public void Sides(int screensize)
        {
            if (position.X <= 0&&Right == false)
            {
                
                Left = false;
            }
            if (position.X + hitbox.Width >= screensize&&Left == false)
            {
                
                Right = false;
            }
        }*/
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
        public void Ground (int screensize, Rectangle otherHitbox)
        {
            if (hitbox.Intersects(otherHitbox)&&jumping == false)
            {
                position.Y = otherHitbox.Y - hitbox.Height;
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
        public bool BigHit(Rectangle otherhitbox)
        {
            
            if (bigHitbox.Intersects(otherhitbox))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public bool otherHit(Rectangle hitboxother)
        {
            if (otherHitbox.Intersects(hitboxother))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void othermoveLeft (int MoveSpeed)
        {
            position.X -= MoveSpeed;
        }
        public void othermoveRight (int MoveSpeed)
        {
            position.X += MoveSpeed;
        }
        public void UpdateHitbox ()
        {
            bigHitbox = hitbox;
            bigHitbox.Width += 250;
            bigHitbox.X = hitbox.X - 125;
            bigHitbox.Height = hitbox.Height + 1000;
            bigHitbox.Y = hitbox.Y - 500;

            otherHitbox = bigHitbox;
            otherHitbox.Width += 150;
            otherHitbox.X -= 75;
            
            
        }

    }
}
