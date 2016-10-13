//Milestone4
//Tetyana Kolomiyets
//IGME.105.05
//Draws a looping background and loops it

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Milestone4_HomingBullets
{
    class Background : MovingItems
    {
        Texture2D img1, img2;
        Vector2 pos1, pos2;

        public Background(Texture2D bg)
        {
            // two textures with the same image will loop back to back
            img1 = bg;
            img2 = bg;
            pos1 = new Vector2(0, 0);
            pos2 = new Vector2(img1.Width, 0); // position of the second background image will be right after the first
        }

        // draws the two images externally from the Game1.cs
        public override void Draw(int speed, GameTime gameTime, SpriteBatch spriteBatch)  
        {
            spriteBatch.Draw(img1, pos1, Color.White);
            spriteBatch.Draw(img2, pos2, Color.White);

            Scroll(speed); // moves the images to the left
        }

        public override void Scroll(int speed) 
        {
            // will put one after the other and when the left side of the image touches the left side of the window
            pos1 = new Vector2(pos1.X - speed, pos1.Y);
            pos2 = new Vector2(pos2.X - speed, pos2.Y);

            if (pos2.X == 0) // will put the picture 1 behind picture 2 when picture 2 starts disappearing
            {
                pos1 = new Vector2(pos2.X + img2.Width, pos1.Y);
            }

            if (pos1.X == 0) // will put picture 2 behind picture 1 when picture 1 starts disappearing
            {
                pos2 = new Vector2(pos1.X + img1.Width, pos2.Y);
            }
        }

        public override void Respawn(int speed, GameTime gameTime, SpriteBatch spriteBatch) {}
    }
}
