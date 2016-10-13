// Milestone4
// Isis Melendez
// IGME.105.05
// Sprite Class for characters

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using System.Threading;
using Microsoft.Xna.Framework.GamerServices;


namespace Milestone4_HomingBullets
{
    // represent object in game
    abstract class Sprite
    {
        // Attributes
        protected Texture2D textureImage; // sprite sheet
        protected int frame;
        protected Point frameSize;
        protected int numFrames;
        protected int timeSinceLastFrame;
        protected int millisecondsPerFrame;
        protected Point currentFrame;
        protected Vector2 position;
        bool shot = true;

        // Properties
        public int MillisecondsPerFrame { set { millisecondsPerFrame = value; } }
        public int Ypos { set { Ypos = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public int Width { get { return frameSize.X; } }
        public int Height { get { return frameSize.Y; } }
        public float PosY { set { position.Y = value; } }
        public bool Shot { get { return shot; } set { shot = value; } }

        // Constructor
        public Sprite() { }
        public Sprite(Texture2D img, Point size, int frames, int msPerFrame, Vector2 pos)
        {
            textureImage = img;
            frameSize = size;
            millisecondsPerFrame = msPerFrame;
            numFrames = frames;

            currentFrame.X = 0;
            currentFrame.Y = 0;
            position = pos; // Default position at 16, 368
        }
       
        // called from game1.Update
        public abstract void Update(GameTime gameTime);

        // called by Game1.Draw
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Color color)
        {
            // draw the corect image
            spriteBatch.Draw(
                textureImage, position, new Rectangle(currentFrame.X, currentFrame.Y, frameSize.X, frameSize.Y), color, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
    }
}
