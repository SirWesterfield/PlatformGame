using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class Moveythingy : Sprite
    {
        
        public Moveythingy(Texture2D image, Vector2 position, Color color)
            : base(image, position, color)
        {
        
        }

        public void MoveRight (int movespeed)
        {
            position.X += movespeed;
        }
        
        public void MoveLeft(int movespeed)
        {
            position.X -= movespeed;
        }

        public void MoveUp(int movespeed)
        {
            position.Y -= movespeed;
        }

        public void MoveDown(int movespeed)
        {
            position.Y += movespeed;
        }




    }
}
