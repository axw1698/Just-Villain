//Milestone4
//Tetyana Kolomiyets
//IGME.105.05

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
    class Bullet
    {
        Texture2D bul;
        Vector2 pos;
        Rectangle rec;
        int xdef = 50; //EDIT BASED ON GAME WINDOW
        const int YDEF = 400; // default value
        Random rand = new Random();

        //int num=0;
        int width = 0;
        int height = 0;
        int howFar = 500; //how far the bullet will travel
        bool isActive = false;

        public int HowFar { set { howFar = value; } get { return howFar; } }
        public Vector2 Pos { get { return pos; } set { pos = value; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool IsActive { get { return isActive; } set { isActive = value; } }
        public int Xdef { get { return xdef; } set { xdef = value; } }


        public Bullet(Texture2D obstacleImage)
        {
            bul = obstacleImage;
            pos = new Vector2(xdef, YDEF);
            width = bul.Bounds.X;
            height = bul.Bounds.Y;
            rec = new Rectangle((int)pos.X, (int)pos.Y, width, height);

        }
        public void Draw(int speed, GameTime gameTime, SpriteBatch spriteBatch, PlayerSprite pl, int howf) //same as scrolling speed                              // Need to change - Amy ( howfar - howf)
        {
            //pos.X = 250;
            spriteBatch.Draw(bul, pos, Color.White);
            if (isActive == true)
            {
                Scroll(speed, howf, pl);                                                                                                                          // Need to change - Amy ( howfar - howf)
            }
        }

        public void Follow(GameTime gameTime, PlayerSprite pl)
        {
            if (isActive == false)
            {
                pos.Y = pl.Position.Y;
            }
        }

        public void Scroll(int speed, int howFar, PlayerSprite pl) //moves the bullet right
        {
            pos = new Vector2(pos.X + speed, pos.Y);
            if (pos.X >= howFar) //once it reaches the max distance, it stops scrolling
            {
                pl.Shot = false;
                pos.X = xdef; //respawns it at the default x position
                isActive = false;
                
                rec = new Rectangle((int)pos.X, (int)pos.Y, width, height);
            }

        }

        public void Respawn(int speed, GameTime gameTime, SpriteBatch spriteBatch)
        {

        }
        public bool isColliding(EnemySprite en)
        {
            if ((pos.X + width >= en.Pos.X))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
