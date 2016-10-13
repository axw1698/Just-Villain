//Milestone4
// Amy Wang, Kyunghwan Sul
//IGME.105.05
//credits to: https://www.youtube.com/watch?v=ReS1VIUrTnw

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
using System.Threading;

namespace Milestone4_HomingBullets
{
    class MainMenu
    {
                                 /////////////////////////// Attributes  ////////////////////////////////////
        // use enum for all the specific window
        enum GameState { menuWin, helpWin, loadWin,genderWin,characterWin,playWin,gameOverWin };
        GameState gamestate; // create a GameState obj

        // object 
        GameWindow gw;                         


        // One list for one window's assets. 
        List<GuiElement> main = new List<GuiElement>();
        List<GuiElement> gender = new List<GuiElement>();
        List<GuiElement> help = new List<GuiElement>();
        List<GuiElement> load = new List<GuiElement>();
        List<GuiElement> character = new List<GuiElement>();
        List<GuiElement> gameOver = new List<GuiElement>();
        //List<GuiElement> play = new List<GuiElement>();

        private SpriteFont sF;

        // user input
        private Keys[] lastPressedKeys = new Keys[5]; 
        private string myName = ""; // string used for the loadfile 


        /////// Attributes for data info // milestone3
        private int fileNum;                           
        UserData ud = new UserData();
        private bool cheated = false; // check if the user used the external tool
        private string fileName1; 
        private string fileName2;
        private string fileName3;
        private int coinNum;


        // Boolean for click 
        private bool x2AttackClick = false;
        private bool x3AttackClick = false;
        private bool x4AttackClick = false;

        private bool x2CoinClick = false;
        private bool x3CoinClick = false;

        private bool x2JumpClick = false;
        private bool x3JumpClick = false;

        //private bool saveUpGradeClick = false;
        private bool cheatedClick = false;

        private bool saveFile = false;
        private bool newFile = false;      
        private bool gameOn = false;
        private bool fire = false;

        private bool jumpPressed = false;
        //private bool slidePressed = false;

        private bool gameLost = false;

                                     /////////////// Properties
        public bool Fire // check  fire the bullet or not
        {
            get { return fire; }
            set { fire = value; }
        }
        public bool JumpPressed // check  jump  
        {
            get { return jumpPressed; }
            set { jumpPressed = value; }
        }
        //public bool SlidePressed // check  jump  
        //{
        //    get { return slidePressed; }
        //    set { slidePressed = value; }
        //}

                                     /////////////// Constructor 
        public MainMenu()
        {
            // create the obj
            gw = new GameWindow(ud.Health, ud.Attack,ud.JumpUp,ud.AttackDistanceUp,ud.CoinUp, ud.Defense);                                                                                  

            // Add all the assets for the specific List
            main.Add(new GuiElement("menu"));
            main.Add(new GuiElement("logo"));
            main.Add(new GuiElement("New Game"));
            main.Add(new GuiElement("load"));
            main.Add(new GuiElement("help"));
            main.Add(new GuiElement("quit"));

            gender.Add(new GuiElement("otherMenu"));
            gender.Add(new GuiElement("return"));
            gender.Add(new GuiElement("female"));
            gender.Add(new GuiElement("male"));
            gender.Add(new GuiElement("textbox"));
            gender.Add(new GuiElement("Cheat"));

            help.Add(new GuiElement("HelpPage"));                                                                                  
            help.Add(new GuiElement("return"));

            load.Add(new GuiElement("otherMenu"));
            load.Add(new GuiElement("return"));
            // MileStone3
            load.Add(new GuiElement("Character1TextBox"));
            load.Add(new GuiElement("Character2TextBox"));
            load.Add(new GuiElement("Character3TextBox"));


            character.Add(new GuiElement("CharacterPage"));
            //character.Add(new GuiElement("return"));
            character.Add(new GuiElement("play"));
            character.Add(new GuiElement("X2AttackDis"));
            character.Add(new GuiElement("X3AttackDis"));
            character.Add(new GuiElement("X4AttackDis"));
            character.Add(new GuiElement("X2Coin"));
            character.Add(new GuiElement("X3Coin"));
            character.Add(new GuiElement("X2Jump"));
            character.Add(new GuiElement("X3Jump"));
            character.Add(new GuiElement("saveUpGrade"));

            gameOver.Add(new GuiElement("GameOverPage"));                                       
            //play.Add(new GuiElement("bg"));
        }

                                    //////////// Methods ///////////////////////
        public void LoadContent(ContentManager content)
        {
            // load the font
            sF = content.Load<SpriteFont>("SpriteFont1");

            // use foreach to load, center and add in the click event for all the assets in a list
            foreach (GuiElement element in main)
            {
                element.LoadContent(content);
                element.CenterElement(800, 1000);
                element.clickEvent += OnClick;
            }

            //main.Find(x => x.AssetName == "logo").ChangeSize(100);

            // use main.Find to associate the assets name and change that assets' location
            main.Find(x => x.AssetName == "menu").MoveElement(12,-16);
            main.Find(x => x.AssetName == "logo").MoveElement(300, 250);
            main.Find(x => x.AssetName == "New Game").MoveElement(0, -100);
            main.Find(x => x.AssetName == "load").MoveElement(0, 0);
            main.Find(x => x.AssetName == "help").MoveElement(0, 100);
            main.Find(x => x.AssetName == "quit").MoveElement(0, 200);

            // use foreach to load, center and add in the click event for all the assets in a list
            foreach (GuiElement element in gender)
            {
                element.LoadContent(content);
                element.CenterElement(800, 1000);
                element.clickEvent += OnClick;
            }
            
            // use main.Find to associate the assets name and change that assets' location
            gender.Find(x => x.AssetName == "otherMenu").MoveElement(12, -16);
            gender.Find(x => x.AssetName == "return").MoveElement(-300, 300);
            gender.Find(x => x.AssetName == "male").MoveElement(-250,-50);
            gender.Find(x => x.AssetName == "female").MoveElement(250, -50);
            gender.Find(x => x.AssetName == "textbox").MoveElement(0, 300);
            gender.Find(x => x.AssetName == "Cheat").MoveElement(300, 300);// MileStone3



            // use foreach to load, center and add in the click event for all the assets in a list
            foreach (GuiElement element in load)
            {
                element.LoadContent(content);
                element.CenterElement(800, 1000);
                element.clickEvent += OnClick;
            }
            fileName1 = ud.GetFileName(0);
            fileName2 = ud.GetFileName(1);
            fileName3 = ud.GetFileName(2);

            // use main.Find to associate the assets name and change that assets' location
            load.Find(x => x.AssetName == "return").MoveElement(0, 300);
            // MileStone3
            load.Find(x => x.AssetName == "otherMenu").MoveElement(12, -16);
            load.Find(x => x.AssetName == "Character1TextBox").MoveElement(0, -150);
            load.Find(x => x.AssetName == "Character2TextBox").MoveElement(0, 0);
            load.Find(x => x.AssetName == "Character3TextBox").MoveElement(0, 150);


            // use foreach to load, center and add in the click event for all the assets in a list
            foreach (GuiElement element in help)
            {
                element.LoadContent(content);
                element.CenterElement(800, 1000);
                element.clickEvent += OnClick;
            }

            // use main.Find to associate the assets name and change that assets' location
            help.Find(x => x.AssetName == "return").MoveElement(0, 300);
            help.Find(x => x.AssetName == "HelpPage").MoveElement(12, -16);                 


            foreach (GuiElement element in character)
            {
                element.LoadContent(content);
                element.CenterElement(800, 1000);
                element.clickEvent += OnClick;
            }
            // use main.Find to associate the assets name and change that assets' location
            //character.Find(x => x.AssetName == "return").MoveElement(-300, 350);
            character.Find(x => x.AssetName == "CharacterPage").MoveElement(12, -16);
            character.Find(x => x.AssetName == "play").MoveElement(0, 325);
            character.Find(x => x.AssetName == "X2AttackDis").MoveElement(280, 53); //69
            character.Find(x => x.AssetName == "X3AttackDis").MoveElement(380, 53);
            character.Find(x => x.AssetName == "X4AttackDis").MoveElement(380, 149);//165
            character.Find(x => x.AssetName == "X2Coin").MoveElement(280, -76); //60
            character.Find(x => x.AssetName == "X3Coin").MoveElement(380, -76);
            character.Find(x => x.AssetName == "X2Jump").MoveElement(280, -216);
            character.Find(x => x.AssetName == "X3Jump").MoveElement(380, -216);
            character.Find(x => x.AssetName == "saveUpGrade").MoveElement(380, 234);

            //foreach (GuiElement element in play)
            //{
            //    element.LoadContent(content);
            //    element.CenterElement(800, 1000);
            //    element.clickEvent += OnClick;
            //}
            foreach (GuiElement element in gameOver)
            {
                element.LoadContent(content);
                element.CenterElement(800, 1000);
                element.clickEvent += OnClick;
            }
            gameOver.Find(x => x.AssetName == "GameOverPage").MoveElement(12, -16);                                        



            // call method to load content for game window
            gw.LoadContent(content);
        }

        
        public void Update(GameTime gameTime)
        {
            // use a switch statement for updating the click event for each window by using the enum 
            switch (gamestate)
            {
                case GameState.menuWin:
                    foreach (GuiElement element in main)
                    {
                        element.Update();                    
                    }
                    break;
                case GameState.helpWin:
                    foreach (GuiElement element in help)
                    {
                        element.Update();
                    }
                    break;
                case GameState.loadWin:
                    foreach (GuiElement element in load)
                    {
                        element.Update();
                    }             
                    
                    break;
                case GameState.characterWin:
                    foreach (GuiElement element in character)
                    {
                        element.Update();
                    }
                    break;
                case GameState.genderWin:
                    foreach (GuiElement element in gender)
                    {
                        element.Update();
                    }
                    GetKeys(); // for file name
                    break;
                case GameState.playWin:
                    // call the update
                    gw.Update(gameTime);
                    //foreach (GuiElement element in play)
                    //{
                    //    element.Update();
                    //}
                    GetKeys(); // for quit 
                    gameLost = gw.GameOver();
                    if (gameLost)
                    {
                        Thread.Sleep(800);
                        gamestate = GameState.gameOverWin;
                        // save the coin to the character
                        ud.CoinNum = ud.CoinNum + gw.CoinCount;
                        // reset coinCount
                        gw.CoinCount = 0;
                    }
                    break;
                case GameState.gameOverWin:
                    foreach (GuiElement element in gameOver)
                    {
                        element.Update();
                    }         
                    GetKeys(); // for quit                                                                                    

                    break;
                default:
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // use a switch statement for drawing the image in the screen for each window by using the enum 

            switch (gamestate)
            {
                case GameState.menuWin:
                    foreach (GuiElement element in main)
                    {
                        element.Draw(spriteBatch, Color.White);
                        //element.clickEvent += OnClick;                                               
                    }
                    break;

                case GameState.helpWin:
                    foreach (GuiElement element in help)
                    {
                        element.Draw(spriteBatch, Color.White);
                        //element.clickEvent += OnClick;                                                                             
                    }
                    break;

                case GameState.loadWin:
                    foreach (GuiElement element in load)
                    {
                        element.Draw(spriteBatch, Color.White);
                        //element.clickEvent += OnClick;                                                                            
                    }
                    // show the file name on the load page - milestone 3
                    spriteBatch.DrawString(sF, fileName1, new Vector2(580, 230), Color.Black);
                    spriteBatch.DrawString(sF, fileName2, new Vector2(580, 390), Color.Black);
                    spriteBatch.DrawString(sF, fileName3, new Vector2(580, 540), Color.Black);
                    break;

                case GameState.characterWin:
                    foreach (GuiElement element in character)
                    {
                        element.Draw(spriteBatch, Color.White);
                        //element.clickEvent += OnClick;                                                                                

                    }
                    // print the user info - mileStone 3
                    spriteBatch.DrawString(sF, myName, new Vector2(200, 620), Color.Black);
                    spriteBatch.DrawString(sF, ud.CoinNum.ToString(), new Vector2(640, 620), Color.Black);


                    // change the button to red if clicked
                    if(x2AttackClick == true || ud.AttackDistanceUp !=0)
                    character.Find(x => x.AssetName == "X2AttackDis").Draw(spriteBatch, Color.Red);
                    if (x3AttackClick == true || ud.AttackDistanceUp == 2 || ud.AttackDistanceUp == 3)
                    character.Find(x => x.AssetName == "X3AttackDis").Draw(spriteBatch, Color.Red);
                    if (x4AttackClick == true ||ud.AttackDistanceUp ==3)
                    character.Find(x => x.AssetName == "X4AttackDis").Draw(spriteBatch, Color.Red);
                    if (x2CoinClick == true || ud.CoinUp !=0)
                    character.Find(x => x.AssetName == "X2Coin").Draw(spriteBatch, Color.Red);
                    if (x3CoinClick == true || ud.CoinUp ==2)
                    character.Find(x => x.AssetName == "X3Coin").Draw(spriteBatch, Color.Red);
                    if (x2JumpClick == true|| ud.JumpUp !=0)
                    character.Find(x => x.AssetName == "X2Jump").Draw(spriteBatch, Color.Red);
                    if (x3JumpClick == true|| ud.JumpUp ==2)
                    character.Find(x => x.AssetName == "X3Jump").Draw(spriteBatch, Color.Red);
                    //if (saveUpGradeClick == true)
                    //{
                    //    character.Find(x => x.AssetName == "saveUpGrade").Draw(spriteBatch, Color.Red);
                    //    saveUpGradeClick = false;  
                    //}
                    break;
                case GameState.genderWin:
                    foreach (GuiElement element in gender)
                    {
                        element.Draw(spriteBatch, Color.White);
                        //element.clickEvent += OnClick;                                                                          
                    }
                    spriteBatch.DrawString(sF, myName, new Vector2(400, 690), Color.Black);
                    spriteBatch.DrawString(sF, "Need at least 5 letters for Name", new Vector2(390, 720), Color.White);

                    if (cheatedClick == true)
                    {
                        gender.Find(x => x.AssetName == "Cheat").Draw(spriteBatch, Color.Red);
                        //cheatedClick = false;
                        //Thread.Sleep(100);
                        //character.Find(x => x.AssetName == "Cheat").Draw(spriteBatch, Color.White);
                    }
                    break;
                case GameState.playWin:
                    gw.Draw(spriteBatch, gameTime);
                    //foreach (GuiElement element in play)
                    //{
                    //    element.Draw(spriteBatch);
                    //}
                    break;
                case GameState.gameOverWin:
                    foreach (GuiElement element in gameOver)
                    {
                        element.Draw(spriteBatch, Color.White);
                        //element.clickEvent += OnClick;                                                             
                    }
                    //spriteBatch.DrawString(sF, "You lost the game !!", new Vector2(390, 350), Color.White);
                    break;
                default:
                    break;
            }
        }

        // if statements - whenever the different buttons are pressed it will lead to new windows
        public void OnClick(string element) // changed something
        {
            switch (element)
            {
                case "New Game": // play the game
                    gamestate = GameState.genderWin;

                    break;
                case "load":
                    gamestate = GameState.loadWin;
                    Thread.Sleep(100);
                    break;
                case "help":
                    gamestate = GameState.helpWin;
                    break;
                case "quit":
                    Environment.Exit(-1);
                    break;
                case "female":
                    // creare a new character file
                    if (myName.Length >= 5) // MileStone3
                    {
                        ud.SexMale = false; // set the gender
                        saveFile = true;                                                                                    
                        newFile = true;                                                                                    
                        Thread.Sleep(100);                                                                                    
                        gamestate = GameState.loadWin;                                                                             
                    }
                    break;
                case "male":
                    // creare a new character file
                    if (myName.Length >= 5) // MileStone3
                    {
                        ud.SexMale = true; // set the gender
                        saveFile = true;                                                                                    
                        newFile = true;                                                                                    
                        Thread.Sleep(100);                                                                                    
                        gamestate = GameState.loadWin;                                                                              
                    }
                    break;
                case "return": // some issue with this
                    Thread.Sleep(100);                                                                                    
                    if (saveFile == true) // make sure it return to correct window
                    {
                        gamestate = GameState.characterWin;
                    }
                    else
                    {
                        gamestate = GameState.menuWin;
                    }
                    break;
                case "play":
                    gw.LoadUserData(ud.Health, ud.Speed, ud.Attack,ud.JumpUp,ud.AttackDistanceUp,ud.CoinUp);                                                      
                    gamestate = GameState.playWin;
                    gameOn = true;
                    bool gameOver = gw.GameOver();
                    break;
                // MileStone3
                case "Cheat":
                    cheated = true;
                    cheatedClick = true;
                    break;
                    case "Character1TextBox":                                                                               
                    fileNum = 1;
                    if (saveFile == true)
                    {
                        ud.NewFile(myName, 1, cheated);// write a new file
                        fileName1 = ud.GetFileName(0);
                        if (newFile == true)
                        {
                            ud.LoadFile(1);
                            ChangeData();
                            gamestate = GameState.characterWin;
                            gw.LoadUserData(ud.Health, ud.Speed, ud.Attack, ud.JumpUp, ud.AttackDistanceUp, ud.CoinUp);

                        }

                    }
                    else if (fileName1 != "No File")
                    {
                        ud.LoadFile(1);
                        ChangeData();
                        gw.LoadUserData(ud.Health, ud.Speed, ud.Attack, ud.JumpUp, ud.AttackDistanceUp, ud.CoinUp);
                        gamestate = GameState.characterWin;
                    }
                    break;
                case "Character2TextBox":                                                                              
                    fileNum = 2;
                    if (saveFile == true)
                    {
                        ud.NewFile(myName, 2, cheated);// write a new file
                        fileName2 = ud.GetFileName(1);
                        if (newFile == true)
                        {
                            ud.LoadFile(2);
                            ChangeData();
                            gamestate = GameState.characterWin;
                            gw.LoadUserData(ud.Health, ud.Speed, ud.Attack, ud.JumpUp, ud.AttackDistanceUp, ud.CoinUp);
                        }
                    }
                    else if (fileName2 != "No File")
                    {
                        ud.LoadFile(2);
                        ChangeData();
                        gw.LoadUserData(ud.Health, ud.Speed, ud.Attack, ud.JumpUp, ud.AttackDistanceUp, ud.CoinUp);
                        gamestate = GameState.characterWin;
                    }
                    break;
                case "Character3TextBox":                                                                              
                    fileNum = 3;
                    if (saveFile == true)
                    {
                        ud.NewFile(myName, 3, cheated); // write a new file
                        gw.LoadUserData(ud.Health, ud.Speed, ud.Attack, ud.JumpUp, ud.AttackDistanceUp, ud.CoinUp);
                        fileName3 = ud.GetFileName(2);
                        if (newFile == true)
                        {
                            ud.LoadFile(3);
                            ChangeData();
                            gw.LoadUserData(ud.Health, ud.Speed, ud.Attack, ud.JumpUp, ud.AttackDistanceUp, ud.CoinUp);
                            gamestate = GameState.characterWin;
                        }

                    }
                    else if (fileName3 != "No File")
                    {
                        ud.LoadFile(3);
                        ChangeData();
                        gw.LoadUserData(ud.Health, ud.Speed, ud.Attack, ud.JumpUp, ud.AttackDistanceUp, ud.CoinUp);
                        gamestate = GameState.characterWin;
                    }
                    break;
                case "X2AttackDis":
                    if (ud.CoinNum >= 10 && x2AttackClick == false)
                    {
                        ud.AttackDistanceUp = 1;
                        x2AttackClick = true;
                        ud.CoinNum = ud.CoinNum - 10;

                    }
                    break;
                case "X3AttackDis":
                    if (ud.CoinNum >= 50 && x2AttackClick == true && x3AttackClick == false)
                    {
                        ud.AttackDistanceUp = 2;
                        x3AttackClick = true;
                        ud.CoinNum = ud.CoinNum - 50;
                    }
                    break;
                case "X4AttackDis":
                    if (ud.CoinNum >= 100 && x3AttackClick == true && x4AttackClick == false)
                    {
                        ud.AttackDistanceUp = 3;
                        x4AttackClick = true;
                        ud.CoinNum = ud.CoinNum - 100;
                    }
                    break;
                case "X2Coin":
                    if (ud.CoinNum >= 50 && x2CoinClick == false)
                    {
                        ud.CoinUp = 1;
                        x2CoinClick = true;
                        ud.CoinNum = ud.CoinNum - 50;
                    }
                    break;
                case "X3Coin":
                    if (ud.CoinNum >= 100 && x2CoinClick == true && x3CoinClick == false)
                    {
                        ud.CoinUp = 2;
                        x3CoinClick = true;
                        ud.CoinNum = ud.CoinNum - 100;
                    }
                    break;
                case "X2Jump":
                    if (ud.CoinNum >= 10 && x2JumpClick == false)
                    {
                        ud.JumpUp = 1;
                        x2JumpClick = true;
                        ud.CoinNum = ud.CoinNum - 10;
                    }
                    break;
                case "X3Jump":
                    if (ud.CoinNum >= 50 && x2JumpClick == true && x3JumpClick == false)
                    {
                        ud.JumpUp = 2;
                        x3JumpClick = true;
                        //ud.CoinNum = ud.CoinNum - 50; // for testing
                    }
                    break;
                case "saveUpGrade":
                   saveFile = true;
                    gamestate = GameState.loadWin;
                    //saveUpGradeClick = true;
                    //ud.SaveFile(cheated);
                    //ud.CoinNum = ud.CoinNum+50; // used to check update
                    break; 
            }
        }

        // the method that relate to user input
        public void GetKeys() // amy wtf is love - Isis
        {
            //getting keyboard state allowing the kehyboard to be typed
            KeyboardState kbState = Keyboard.GetState();

            //array for keys
            Keys[] pressedKeys = kbState.GetPressedKeys();

            foreach(Keys key in lastPressedKeys)
            {//whenever key is not pressed it will bring in the key up method which will do nothing
                if(!pressedKeys.Contains(key))
                {
                    // key is no longer pressed 
                    OnKeyUp(key);
                }
            }
            foreach(Keys key in pressedKeys)
            {
                if(!lastPressedKeys.Contains(key))
                {//if key is down(pressed)it will bring in the key down method
                    OnKeyDown(key);
                }
            }
            lastPressedKeys = pressedKeys; //it will store the array into the last pressed keys
        }
        public void OnKeyUp(Keys key)
        {//keys are not pressed therefore null
        
        }
        public void OnKeyDown(Keys key)
        {//when keys are pressed
            if(key == Keys.CapsLock || key == Keys.Space || key == Keys.LeftAlt || key == Keys.RightAlt)
            {//when capslock is pressed it won't do anything

            }
            
            else if(key == Keys.Back && myName.Length >0 && gameOn == false)
            {//whenever backspace key is pressed it will delete a letter
                myName = myName.Remove(myName.Length - 1);
            }
            else if (gameOn == false)
            {//whenever a valid key is pressed it will store into the string
                myName += key.ToString();
            }
            if (gameOn == true)
            {
                if (key == Keys.Space)
                {
                    gw.Fire = true;
                    //fire = true;
                }
                if (key == Keys.Q)                                                                                                    
                {
                    if (gamestate == GameState.gameOverWin)
                    {
                    }
                    else
                    {
                        gameOn = false;
                        gamestate = GameState.characterWin;
                        // save the coin to the character
                        ud.CoinNum = ud.CoinNum + gw.CoinCount;
                        // reset coinCount
                        gw.CoinCount = 0;
                    }
                }
                if(key == Keys.S)
                {
                    if (gamestate == GameState.gameOverWin)                                                                    
                    {
                        ud.NewFile(myName, fileNum, cheated);// write a new file
                        Environment.Exit(0);
                    }
                }
                if(key == Keys.Up)
                {
                    jumpPressed = true;
                    gw.WhenJumpPress();
                }
                //if (key == Keys.Down)
                //{
                //    slidePressed = true;
                //    gw.WhenSlidePress();
                //    Console.WriteLine("Slide pressed");
                //}
            }
        }

        // methods that change all the user data (load)
        public void ChangeData()
        {
            // change user data
            coinNum = ud.CoinNum;
            myName = ud.Name;
            // if there is other data;            
        }

    }
}
