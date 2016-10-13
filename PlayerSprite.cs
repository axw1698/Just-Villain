// Milestone4
// Isis Melendez, Amy Wang
// IGME.105.05
// Player Sprite Class
// **SLIDING NOT IN USE

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
    // Subclass representing the playable sprite avatar
    // Inherits from Sprite class
    class PlayerSprite : Sprite
    { 
        // Player specific attributes
        private Vector2 velocity;
        private bool hasJumped;
        //private bool hasSlid;
        private bool onPlatform = false;
        private bool falling = false;   // new bool 
        private bool jump; // used for calling back to MainMenu
        //private bool slide;
        private int canJump; // How many times you can jump   
        private int jumpNum; // How many times you've jumped 

        // Properties
        public bool OnPlatform { get { return onPlatform; } set { onPlatform = value; } }
        public bool Jump { get { return jump; } set { jump = value; } }
        //public bool Slide { get { return slide; } set { slide = value; } }
        public int CanJump { get { return canJump; } set { canJump = value; } }

        public PlayerSprite(Texture2D img, Point size, int frames, int msPerFrame, Vector2 pos)
            : base(img, size, frames, msPerFrame, pos)
        {
            // Indicate that player begins not jumping 
            hasJumped = true;
            //hasSlid = true;
            jump = false;
            jumpNum = 0;   
        }

        // Update method called in Update of GameWindow.cs
        public override void Update(GameTime gameTime)
        {
            // Handle Constant Running Animation
            // check to see if new frame is needed AND sprite is not Jumping or sliding
            if (hasJumped == false && jump == false) // therefore, freezes frame if jumping or sliding and resumes animation otherwise
            {
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    // get new frame
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

            ////// SLIDE HANDLING
            //if (slide == true && hasSlid == false)
            //{
            //    Console.WriteLine("Is sliding.");
            //    // set the upper left corner of SLIDING frame
            //    currentFrame.Y = 256;
            //    currentFrame.X = 0;

            //    position.X += 3f;
            //    velocity.X = 7f;

            //    hasSlid = true;
            //    slide = false;               
            //}

            //if (hasSlid == true)  // return
            //{
            //    //float i = 5; 
            //    //velocity.X += 0.08f * i;
            //    currentFrame.Y = 256;
            //    currentFrame.X = 0;
            //}

            //if (position.X >= 0) // prevent going back off edge of screen
            //{
            //    hasSlid = false;
            //}                

            // Handle Movement and keypresses
            position += velocity;

            //// JUMP HANDLING
            if (jump == true && jumpNum < canJump) // jump    // extra jump 
            {
                jumpNum++;
                position.Y -= 8f;
                velocity.Y = -14f;
                hasJumped = true;
                jump = false;
            }

            if (jump == true && hasJumped == false) // jump
            {
                position.Y -= 8f;
                velocity.Y = -14f;
                hasJumped = true;
                jump = false;
            }

            if (hasJumped == true)  // fall
            {
                float i = 5; // Pseudo-physics
                velocity.Y += 0.08f * i; // Gravity
                if (jumpNum < canJump && jump) // extra jump when falling
                    hasJumped = false;
            }

            if (position.Y >= 368) // prevent fall off the floor
                hasJumped = false;

            // fall handler
            if (onPlatform == false && falling == true && position.Y < 368) // falling of the platform 
                hasJumped = true;

            if (hasJumped == false)
            {
                jumpNum = 0;
                velocity.Y = 0f; // stop moving down
            }

            //jumping on platform   - it need to be after falling, otherwize whenever you jump, you actavite the fall, so jumping fail......
            if (onPlatform)
            {
                hasJumped = false;
                falling = true;
                if (jump)
                {
                    falling = false;
                }
            }
        }                
    }
}
