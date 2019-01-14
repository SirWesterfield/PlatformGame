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
        public Rectangle LeftSide;
        public Rectangle RightSide;
        public int movespeed;
        public bool OnSide;
        public Platform(Texture2D image, Vector2 position, Color color, int movespeed, bool OnSide)
            : base(image, position, color)
        {
            this.movespeed = movespeed;
            this.OnSide = OnSide;
        }
        public void Update ()
        {
            bottom.Y = hitbox.Bottom;
            bottom.X = hitbox.X;
            bottom.Width = hitbox.Width-3;
            bottom.X = hitbox.X + 3;

            top.Y = hitbox.Top;
            top.X = hitbox.X;
            top.Width = hitbox.Width-12;
            top.X = hitbox.X + 3;

            LeftSide = hitbox;
            LeftSide.X = hitbox.X + hitbox.Width;
            LeftSide.Width = 1;
            LeftSide.Y = hitbox.Y + 5;
            LeftSide.Height = hitbox.Height-5;
            
            RightSide = hitbox;            
            RightSide.Width = 1;           
            RightSide.Y = hitbox.Y + 5;
            RightSide.Height = hitbox.Height - 5;
        }

        public void Draw2(SpriteBatch spritebatch, Texture2D image)
        {
            spritebatch.Draw(image, position, color);
        }

    }
}
