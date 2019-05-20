using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform_game
{
    class OtherPlatforms : Platform
    {
        
        public OtherPlatforms (Texture2D image, Vector2 position, Color color, bool OnSide)
            :base(image, position, color, OnSide)
        {

        }


    }
}
