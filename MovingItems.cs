//Milestone4
//Tetyana Kolomiyets
//Kyunghwan Sul
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
    public abstract class MovingItems //just a parent class
    {
        public abstract void Scroll(int speed); //speed will determine how quickly the items will scroll

        public abstract void Draw(int speed, GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void Respawn(int speed, GameTime gameTime, SpriteBatch spriteBatch);
    }
}
