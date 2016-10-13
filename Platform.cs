// Milestone4
// Tetyana Kolomiyets
// IGME.105.05
// Draws a platform and loops it

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
    class Platform : MovingItems
    {
        Texture2D pl;
        Vector2 pos;
        const int XDEF = 1028; //EDIT BASED ON GAME WINDOW
        const int YDEF = 400; //EDIT ACCORDING TO BG IMAGE
        Random rand = new Random();

        int num=0;
        int newY = 0;
        int width = 0;
        int height = 0;

        public int NewY { get { return newY; } }
        public Vector2 Pos { get { return pos; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }


        public Platform(Texture2D obstacleImage)
        {
            pl = obstacleImage;
            pos = new Vector2(XDEF, YDEF);
            width = pl.Bounds.X;
            height = pl.Bounds.Y;
            
        }
        public override void Draw(int speed, GameTime gameTime, SpriteBatch spriteBatch) //same as scrolling speed
        {
            //will make the Y position of next platforms random may not have an effect, keep just in case
            num = rand.Next(0, YDEF);
            newY = YDEF - num;


            spriteBatch.Draw(pl, pos, Color.White);
            Scroll(speed);
        }
        public override void Respawn(int speed, GameTime gameTime, SpriteBatch spriteBatch) //respawns the platform 
        {
            if (pos.X < -400) //if the image is off the left side
            {
                num = rand.Next(0, YDEF);
                newY = YDEF - num; //new Y is a random Y position for the next platform

                pos = new Vector2(XDEF, newY);
                spriteBatch.Draw(pl, pos, Color.White);
                Scroll(speed);
            }
        }

        public override void Scroll(int speed) //moves the platform left
        {
                pos = new Vector2(pos.X - speed, pos.Y);
            
        }
        public bool isColliding(Sprite pl)
        {
            if ((pos.X <= pl.Position.X + pl.Width - 100) && (pos.X >= pl.Position.X - 150)) //for the length of the platform
            {
                while (pos.Y >= (pl.Position.Y+ ((pl.Height/2)+ pl.Height/4)) && pos.Y <= pl.Position.Y + pl.Height) //for the height of the player
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }

        }
    }
}
