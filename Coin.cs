//Milestone4
//Tetyana Kolomiyets, Isis Melendez
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
    class Coin
    {
        Texture2D coin;
        Vector2 pos;
        const int XDEF = 1028; // BASED ON GAME WINDOW
        const int YDEF = 350; // ACCORDING TO BG IMAGE
        Random rand = new Random();

        int num = 0;
        int newY = 0;
        int width = 0;
        int height = 0;
        bool readyToRespawn = false;

        // Animation attributes
        private int frame;
        private Point frameSize;
        private int numFrames;
        private int timeSinceLastFrame;
        private int millisecondsPerFrame;
        private Point currentFrame;

        public int NewY { get { return newY; } }
        public Vector2 Pos { get { return pos; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool ReadyToRespawn { get { return readyToRespawn; } set { readyToRespawn = value; } }


        public Coin(Texture2D coinImage, int spaceBetween, Point size, int frames, int msPerFrame, int yPlace)
        {
            frameSize = size;
            millisecondsPerFrame = msPerFrame;
            numFrames = frames;

            currentFrame.X = 0;
            currentFrame.Y = 0;

            coin = coinImage;
            pos = new Vector2(XDEF + spaceBetween, YDEF + yPlace);
            width = coin.Bounds.X;
            height = coin.Bounds.Y;

        }

        // coin animation update
        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame = 0;
                frame++;
                if (frame >= numFrames)
                {
                    frame = 0;
                }
                currentFrame.X = frameSize.X * frame;
            }
        }

        public void Draw(int speed, GameTime gameTime, SpriteBatch spriteBatch, Sprite pl) //same as scrolling speed
        {
            //will make the Y position of next platforms random may not have an effect, keep just in case
            num = rand.Next(250, YDEF);
            newY = YDEF - num;
            //pos.X = pos.X + spaceBetween;

            spriteBatch.Draw(coin, new Vector2(pos.X, pos.Y), new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            Scroll(speed);
        }

        public void Respawn(int speed, GameTime gameTime, SpriteBatch spriteBatch, int extraX) //respawns the platform
        {
            num = rand.Next(0, YDEF - 100);
            newY = YDEF - num; //new Y is a random Y position for the next platform

            pos = new Vector2(XDEF + extraX, newY);
            spriteBatch.Draw(coin, pos, Color.White);
            Scroll(speed);

        }

        public void Scroll(int speed) //moves the platform left
        {
            pos = new Vector2(pos.X - speed, pos.Y);
            if (pos.X <= -400)
                readyToRespawn = true;
        }
        public bool isColliding(Sprite pl)
        {
            if ((pos.X <= pl.Position.X + pl.Width - 100) && (pos.X >= pl.Position.X - 150))
            {
                while (pos.Y >= pl.Position.Y && pos.Y <= pl.Position.Y + pl.Height)
                {
                    Console.WriteLine(pl.Position.Y + pl.Height);
                    return true;
                }
                return false;
            }
            else
                return false;

        }

    }
}
