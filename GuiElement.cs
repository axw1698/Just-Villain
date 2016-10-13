// Milestone4
// Amy Wang
// IGME.105.05
// Draws a looping background and loops it
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
    class GuiElement
    {

        // attribute
        private Texture2D guiTexture;
        private Rectangle guiRectangle;
        private string assetName;
        public delegate void ElementClicked(string element); // delegate to make event
        public event ElementClicked clickEvent; // a event for all the click that associate to specific asset name

        // properties
        public string AssetName
        {
            get { return assetName; }
            set { assetName = value; }
        }

        // constructor
        public GuiElement(string assetName)
        {
            this.assetName = assetName;
        }

        // load method
        public void LoadContent(ContentManager content)
        {
            // load the texture
            guiTexture = content.Load<Texture2D>(this.assetName);
            // create the rectangle
            guiRectangle = new Rectangle(0, 0, guiTexture.Width, guiTexture.Height);
        }

        // update method
        public void Update()
        {
            // if statement to check if the mouse is click inside the asset box
            if(guiRectangle.Contains(new Point(Mouse.GetState().X,Mouse.GetState().Y))&& Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                clickEvent(assetName);
            }
        }

        // draw method
        public void Draw(SpriteBatch spriteBatch,Color color)
        {
            spriteBatch.Draw(guiTexture, guiRectangle, color);
        }
        public void ClickDraw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(guiTexture, guiRectangle, Color.Red);
        }
        // center // move method
        public void CenterElement(int height, int width)
        {
            // center the image according to the size of window
            guiRectangle = new Rectangle((width / 2) - (this.guiTexture.Width / 2), (height / 2) - (this.guiTexture.Height / 2), this.guiTexture.Width, this.guiTexture.Height);
        }

        public void MoveElement(int x, int y)
        {
            // move the image according to the center 
            guiRectangle = new Rectangle(guiRectangle.X+=x,guiRectangle.Y+=y, this.guiTexture.Width , this.guiTexture.Height);
        }

        /*
        public void ChangeSize( int wH)
        {
            guiRectangle.Width = guiTexture.Width + wH;
            guiRectangle.Height =guiTexture.Height + wH;

            guiRectangle = new Rectangle(guiRectangle.X, guiRectangle.Y, guiTexture.Width, guiTexture.Height);

        }
        */
    }
}
