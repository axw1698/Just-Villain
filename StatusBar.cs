// Milestone4
// Isis Melendez
// IGME.105.05
// Status Bar Class - handles all bars of value according to player stats

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
    // Class defines status bars for player character
    class StatusBar
    {
        // Attributes
        Texture2D baseImage, overTransparency, mainMeter;
        Rectangle baseRec, mainMeterRec; // transparency overlay is same as base meter image because neither change size
        int initialWidth;

        // numerical values
        double maxValue;
        double currentValue;

        private bool isZero;

        // Properties
        public double MaxValue { get { return maxValue; } set { maxValue = value; } }
        public double CurrentValue { get { return currentValue; } set { currentValue = value; } }
        public bool IsZero { get { return isZero; } set { isZero = value; } }
        
        // Constructor
        public StatusBar(Texture2D baseImg, Texture2D transp, Texture2D mainMtr, Rectangle baseR, Rectangle barRec, int max)
        { 
            baseImage = baseImg;
            overTransparency = transp;
            mainMeter = mainMtr;
            baseRec = baseR;
            mainMeterRec = barRec;
            initialWidth = mainMeterRec.Width;

            // Set initial values
            maxValue = max;
            currentValue = maxValue;
        }

        // value of bar is depleted (health loss, etc.)
        // this method should deplete the length of the visible value bar in proportion to the value parameter
        // should make one method that will handle positive and negative values to change? or a Subtract and Add??
        public void Subtract(int damage) // will always be >= 0
        {
            // deplete current value
            currentValue -= damage;

            // check for zero or negative value
            if (currentValue <= 0)
            {
                currentValue = 0;
                mainMeterRec.Width = 0;
            }
            else
            {
                // deplete visual meter
                mainMeterRec.Width = (int)((currentValue / maxValue) * initialWidth);
            }
        }

        // Method to add to the current value of the status bar
        public void Add(int amount)
        {
            // cannot exceed max Value
            if (currentValue >= maxValue)
            {
                currentValue = maxValue;
                mainMeterRec.Width += initialWidth;
            }
            // add amount to current value
            else
            {
                currentValue += amount;
                mainMeterRec.Width = (int)((currentValue / maxValue) * initialWidth);
            }
        }

        // draw method
        // called in Game1.cs
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Vector2 pos, Color barColor)
        {
            // Draw parts of status bar
            spriteBatch.Draw(baseImage, pos, baseRec, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(mainMeter, pos, mainMeterRec, barColor, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
            spriteBatch.Draw(overTransparency, pos, baseRec, Color.White, 0, Vector2.Zero, 1f, SpriteEffects.None, 0);
        }
    }
}
