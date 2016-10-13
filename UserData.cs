// Milestone4
// Amy Wang
// IGME.105.05

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace Milestone4_HomingBullets
{
    //MileStone3 - Amy Wang
    // this class include all the code that relate to the data of the game's value
    class UserData
    {

        
        ///////// attributes
        // For external tools
        //private int speedEx, attackEx, defenseEx, healthEx;
        // for the basic info
        private string name;
        private int coinNum;
        private int jumpUp;
        private int coinUp;
        private int attackDisUp;
        private double speed;
        private int attack, defense, health;
        private bool sexMale;

        // Constructor
        public UserData()
        {
            // set the default value;
            speed = 0;  // not sure, need to talk with Kio
            attack = 10;                                                                                             // Need to change
            defense = 3; // 100%
            health = 40;

            name = "";
            coinNum = 0;
            jumpUp = 0;
            coinUp = 0;
            attackDisUp = 0;
            
            //sexMale = true;
        }

        // Properties
        #region
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int CoinNum
        {
            get { return coinNum; }
            set { coinNum = value; }

        }
                    // Update
        public int JumpUp
        {
            get { return jumpUp; }
            set { jumpUp = value; }

        }
        public int CoinUp
        {
            get { return coinUp; }
            set { coinUp = value; }

        }
        public int AttackDistanceUp
        {
            get { return attackDisUp; }
            set { attackDisUp = value; }

        }
                // External data
        public double Speed
        {
            get { return speed; }
        }
        public int Attack
        {
            get { return attack; }
        }
        public int Defense
        {
            get { return defense; }
        }
        public int Health
        {
            get { return health; }
        }
        public bool SexMale
        {
            get { return sexMale; }
            set { sexMale = value; }
        }
        #endregion

        // methods
        public void ReadData()
        {
            try
            {
                // open binaryReader
                BinaryReader input = new BinaryReader(File.OpenRead("config.dat"));

                // read and save the data in to the attributes
                
                speed = input.ReadDouble();

                // read out the data
                attack = input.ReadInt32();
                defense = input.ReadInt32();
                health = input.ReadInt32();
                if(health == 0) //least health = 1
                {
                    health = 1;
                }

                // done with the files
                input.Close();
            }
            catch(IOException ioe)
            {
                // nothing is going to change // data is going to be default set in the constructor
            }
        }
        public void NewFile(string nm,int saveFile, bool cheated)
        {
            // write data in to a new save file
            try
            {
                string fileName = saveFile+ "Save.dat";
                // create a stream
                Stream str = File.OpenWrite(fileName);

                // create a bindary writer
                BinaryWriter save = new BinaryWriter(str);

                // write data to stream
                save.Write(nm);
                save.Write(coinNum); // coinNum
                save.Write(jumpUp); // jumpUp
                save.Write(coinUp); // coinUp
                save.Write(attackDisUp); // attackDistanceUp
                // if cheated - change the default value
                if (cheated == true)
                {
                    ReadData(); // change the default
                }
                save.Write(speed);
                save.Write(attack);
                save.Write(defense);
                save.Write(health);

                save.Write(sexMale);

                // done with file
                save.Close();
            }
            catch (IOException ioe)
            {
                // error
            }
        }
        public string GetFileName(int saveFile) // use to show the name in character page
        {
            // write data in to a new save file
            try
            {
                // get the name of the file of save
                string[] files = Directory.GetFiles(".", "*Save.dat");

                // read in the correct file 
                BinaryReader input1 = new BinaryReader(File.OpenRead(files[saveFile]));

                // save the first line (name) and return it
                string fileName = input1.ReadString();

                return fileName;

            }
            catch (IOException ioe)
            {
                return "No File";
            }
            catch (Exception ex)
            {
                return "No File"; // show no file when no save file is found
            }
        }
        public void LoadFile(int fileNum)
        {
            try
            {
                // open binaryReader
                BinaryReader input = new BinaryReader(File.OpenRead(fileNum + "Save.dat"));

                // read and save the data in to the attributes
                name = input.ReadString();
                coinNum = input.ReadInt32();
                jumpUp = input.ReadInt32();
                coinUp = input.ReadInt32();
                attackDisUp = input.ReadInt32();

                speed = input.ReadDouble();                                                                                 // Need to change - Amy
                attack = input.ReadInt32();
                defense = input.ReadInt32();
                health = input.ReadInt32();

                sexMale = input.ReadBoolean();

                // done with the files
                input.Close();
            }
            catch (IOException ioe)
            {
                // nothing is going to change // data is going to be default set in the constructor
            }
        }

    }
}
