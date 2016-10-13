//Milestone4
//Tetyana Kolomiyets
//IGME.105.05
//Draws an obstacle and loops it


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
    class Obstacle:MovingItems
    {
        Texture2D obs, obs2;
        Vector2 pos, pos2;
        const int XDEF = 1100; //EDIT BASED ON GAME WINDOW (should be off screen though)
        const int YDEF = 562; //EDIT ACCORDING TO BG IMAGE
        int height = 0;

        public Obstacle(Texture2D obsImage)
        {
            obs = obsImage;
            obs2 = obsImage; //may be used later
            pos = new Vector2(XDEF, YDEF);
            pos2 = new Vector2(XDEF, YDEF); //may be used later
            height = obs.Bounds.Y;
            
        }
        public override void Draw(int speed, GameTime gameTime, SpriteBatch spriteBatch) //draws and scrolls externally from Game1.cs
        {
            spriteBatch.Draw(obs, pos, Color.White);

            Scroll(speed);
        }
        
        public override void Respawn(int speed, GameTime gameTime, SpriteBatch spriteBatch) 
        {
            if (pos.X <-100) //when the image disappears, respawns it at the other side
            {
                pos = new Vector2(XDEF, YDEF);
                spriteBatch.Draw(obs, pos, Color.White);
                Scroll(speed);
            }
        }

        public override void Scroll(int speed) //moves the obstacle to the left
        {
                pos = new Vector2(pos.X - speed, pos.Y);
        }
        public bool isColliding(Sprite pl)
        {
            if (pos.X <= pl.Position.X + pl.Width - 75 && pos.X >= pl.Position.X)
            {
                while (pos.Y <= pl.Position.Y + pl.Height) //accounts for the height
                    return true;
                return false;
            }
            return false;
        }
    }
}
