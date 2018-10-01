using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class Platform : Sprite
    {
        public Rectangle bottom;
        public Rectangle top;
        public int movespeed;
        int Direction = 1;
        public Platform(Texture2D image, Vector2 position, Color color, int movespeed)
            : base(image, position, color)
        {
            this.movespeed = movespeed;
        }
        public void Update ()
        {
            bottom.Y = hitbox.Bottom;
            bottom.X = hitbox.X;
            bottom.Width = hitbox.Width;
            top.Y = hitbox.Top;
            top.X = hitbox.X;
            top.Width = hitbox.Width;
        }
        public void Move(int screensizex)
        {
            if (Direction == 1)
            {
                position.X += movespeed;
            }
            if (Direction == -1)
            {
                position.X += movespeed;
            }
            if (position.X <= 0||position.X + hitbox.Width >= screensizex)
            {
                Direction *= -1;
                movespeed *= -1;
            }
        }

    }
}
