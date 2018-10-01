using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class Player : Sprite
    {
        public enum PlayerState
        {
            Running,
            Jumping
        }
        public Player(Texture2D image, Vector2 position, Color color)
            :base(image, position, color)
        {

        }

        public void Draw2 (SpriteBatch spritebatch, Texture2D player, Color chromatic)
        {
            spritebatch.Draw(player, position, chromatic);
        }
        public void MoveRight (int screensize)
        {
            if (position.X + hitbox.Width < screensize)
            {
                position.X += 5;
            }
        }
        public void MoveLeft()
        {
            if (position.X > 0)
            {
                position.X -= 5;
            }
        }
        public void MoveUp(int Movespeed)
        {
            position.Y += Movespeed;
        }
        public void MoveDown(int MoveDown, int screensize)
        {
            position.Y += MoveDown;
        }
        public bool NoHit(Rectangle otherhitbox)
        {
            if (hitbox.Intersects(otherhitbox))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Hit(Rectangle otherhitbox)
        {
            if(hitbox.Intersects(otherhitbox))
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
