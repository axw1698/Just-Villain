// Milestone4
// Kyunghwan Sul, Isis Melendez
// IGME.105.05

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

// Enemy Sprite Class takes care of the animated enemies that appear on screen
namespace Milestone4_HomingBullets
{
    class EnemySprite
    {
        // Attributes
        private Texture2D textureImage; // sprite sheet
        private Point frameSize;
        private int frame;
        private int numFrames;
        private int millisecondsPerFrame;
        private int timeSinceLastFrame;
        private Point currentFrame;
        private Vector2 pos;
        const int XDEF = 1100; //  based on game window (should be off screen though)
        const int YDEF = 365;

        private int health = 100;                                                                                           
        private bool active = false;
        private bool gotHit = false;           

        // Health attributes
        private int maxHP;
        private int currentHP;

        // Properties
        public int MillisecondsPerFrame { set { millisecondsPerFrame = value; } }
        public double MaxHP { get { return maxHP; } }
        public double CurrentHP { get { return currentHP; } }
        public Vector2 Pos { get { return pos; } }
        public int Health { get { return health; } set { health = value; } }
        public bool Active { get { return active; } set { active = value; } }

        public EnemySprite(Texture2D img, Point size, int frames, int msPerFrame, int mxHP)
        {
            textureImage = img;
            frameSize = size;
            millisecondsPerFrame = msPerFrame;
            numFrames = frames;

            currentFrame.X = 0;
            currentFrame.Y = 0;

            maxHP = mxHP;
            currentHP = maxHP;                                                                              

            pos = new Vector2(XDEF, YDEF);
        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                // time for a new frame
                timeSinceLastFrame = 0;
                frame++;
                if (frame >= numFrames)
                {
                    // wrap around
                    frame = 0;
                }

                // set the upper left corner of new frame
                currentFrame.X = frameSize.X * frame;
            }
        }
       
        public void Scroll(int speed) //moves the obstacle to the left
        {
                pos = new Vector2(pos.X - speed, pos.Y);
        }

        public void Draw(int speed, GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            // draw the frame
            spriteBatch.Draw(textureImage, pos, new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), color, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            Scroll(speed);
        }

        public void Respawn(int speed, GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (pos.X < -100 || active == false) //when the image disappears, respawns it at the other side
            {
                Console.WriteLine("Enemy Health: " + currentHP + "/" + maxHP);
                currentHP = maxHP; // reset health
                pos = new Vector2(XDEF, YDEF);
                spriteBatch.Draw(textureImage, pos, Color.White);
                Scroll(speed);
                active = true;
            }            
        }

        public bool isColliding(Bullet bullet, int damage) 
        {                                                         
            //bullet = new Bullet();

            if (bullet.Pos.X + bullet.Width >= this.pos.X && bullet.Pos.X <= this.pos.X + frameSize.X)
            {
                if (pos.Y <= bullet.Pos.Y + bullet.Height) //accounts for the height
                {
                    gotHit = true;
                    if (currentHP <= 0)
                        currentHP = 0;
                }
                return true;
            }
            else
            {
                if (gotHit == true)
                {
                    gotHit = false;
                    currentHP -= damage;
                    Console.WriteLine("Enemy Health: " + currentHP + "/" + maxHP);
                }
                return false;
            }
        }

        public bool isColliding(Sprite pl)
        {
            if (pos.X <= pl.Position.X + pl.Width - 75 && pos.X >= pl.Position.X/2)
            {
                while (pos.Y <= pl.Position.Y + pl.Height)
                    return true;
                return false;
            }
            return false;
        }
    }
}
