//Milestone4
//Tetyana Kolomiyets, Isis Melendez, Amy Wang, Kyungwhan Sul
//IGME.105.05
// The main gameplay window's class 0 which loads all the content for this window and draws and updates them

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
    class GameWindow
    {
        // Attributes    
        private const int DEFAULT_DAMAGE = 5;
        private int obsSpeed;
        private int plspeed;
        private int bgSpeed;
        private int eneSpeed;
        private int bulSpeed;
        private int spawn;
        private int attack;
        private int coinUp;
        private int howfar;
        private int jumpUp;
        Random rand = new Random();

        //Point playerPoint = new Point(256, 256);
        Vector2 SpritePosition = new Vector2(16, 368);

        // Background Attributes
        Texture2D obs, bg, pl, c1, c2,c3, c4, bul;
        Background background;
        Obstacle obstacle;
        Coin coin1, coin2, coin3, coin4;
        Platform platform;
        Bullet bullet;
        SpriteFont sF;

        // Sprite Attributes
        PlayerSprite player;
        EnemySprite enemy;
        int msPerFrame = 20;
        int maxHP;
        double damageMultiplier; // based on the defense by tool

        // Status Bar Attributes
        StatusBar healthBar;
        StatusBar evilMeter;
        double evilMultiplier = 1;

        private int coinCount = 0;
        private bool jump;
        private bool obstacleCollision;
        private bool enemyCollision;
        private bool bulletCollision;

        // TEST Bool
        private bool fire = false;
        public bool Fire
        {
            get { return fire; }
            set { fire = value; }
        }
        public int CoinCount
        {
            get { return coinCount; }
            set { coinCount = value; }
        }

        // Constructor
        public GameWindow(int maxHealth, int atk,int jUp,int dUp, int cUp, int def)
        {
            // Attributes
            obsSpeed = 5;
            plspeed = 4;
            bgSpeed = 4; 
            eneSpeed = 7;
            bulSpeed = 17;
            maxHP = maxHealth;
            jumpUp = jUp + 1;
            howfar = dUp + 1;
            coinUp = cUp + 1;
            damageMultiplier = (double)(def) / 3.0; // get decimal %

            // used to change distance
            switch(dUp)
            {
                case 0: howfar = 500;
                    break;
                case 1: howfar = 650;
                    break;
                case 2: howfar = 800;
                    break;
                case 3: howfar = 1000;
                    break;
            }
        }

        public void LoadUserData(int changedHp, double changedSp, int changedAtk, int jUp,int dUp, int cUp)     
        {           
            //Console.WriteLine("Loading user data.");
            double change = (double)changedHp;
            healthBar.CurrentValue = change;
            healthBar.MaxValue = change;
            attack = changedAtk;
        
            if (changedSp >= 1)
            {
                int speed = (int)changedSp;
                obsSpeed = obsSpeed * speed;
                //plspeed = plspeed * speed;
                //bgSpeed = bgSpeed + speed;
                eneSpeed = eneSpeed * speed;
                bulSpeed = bulSpeed * speed;
            }
            else if (changedSp == 0.5)
            {
                obsSpeed = obsSpeed / 2;
                //plspeed = plspeed / 2;
                //bgSpeed = bgSpeed / 2;
                eneSpeed = eneSpeed / 2;
                bulSpeed = bulSpeed / 2;
            }

            jumpUp = jUp+1;

            player.CanJump = jumpUp;
            coinUp = cUp + 1;
            // use to change the attack distance
            switch (dUp)
            {
                case 0: howfar = 500;
                    break;
                case 1: howfar = 650;
                    break;
                case 2: howfar = 800;
                    break;
                case 3: howfar = 1000;
                    break;
            }
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager content)
        {

            // Load Player Sprite assets
            Texture2D plImage = content.Load<Texture2D>("plAvatar_spritesheet.png");
            player = new PlayerSprite(plImage, new Point(256, 256), 14, msPerFrame, new Vector2(16, 368));

            // Load Enemy Sprite assets
            Texture2D enImage = content.Load<Texture2D>("enAvatar_spritesheet.png");
            enemy = new EnemySprite(enImage, new Point(256, 256), 2, 300, 20);    

            // Load Health Bar assets
            Texture2D hpMeterRed = content.Load<Texture2D>("hp_meter_red.png");
            Texture2D hpBase = content.Load<Texture2D>("hp_base.png");
            Texture2D hpTransp = content.Load<Texture2D>("hp_transparency.png");
            Rectangle hpMeterRec = new Rectangle(0, 0, hpBase.Width, hpBase.Height);
            Rectangle hpRedRec = new Rectangle(0, 0, hpMeterRed.Width, hpMeterRed.Height);
            healthBar = new StatusBar(hpBase, hpTransp, hpMeterRed, hpMeterRec, hpRedRec, maxHP);

            // Load Evil Meter assets
            Texture2D evMeter = content.Load<Texture2D>("em_meter.png");
            Texture2D evBase = content.Load<Texture2D>("em_base.png");
            Texture2D evTransp = content.Load<Texture2D>("em_transparency.png");
            Rectangle evRec = new Rectangle(0, 0, evBase.Width, evBase.Height);
            Rectangle evMeterRec = new Rectangle(0, 0, evMeter.Width, evMeter.Height);
            evilMeter = new StatusBar(evBase, evTransp, evMeter, evRec, evMeterRec, 6);

            // Load background objects
            bg = content.Load<Texture2D>("background.png");
            obs = content.Load<Texture2D>("obstacle.png");
            pl = content.Load<Texture2D>("platform.png");
            bul = content.Load<Texture2D>("player_bullet.png");
            c1 = content.Load<Texture2D>("coin.png");
            c2 = content.Load<Texture2D>("coin.png");
            c3 = content.Load<Texture2D>("coin.png");
            c4 = content.Load<Texture2D>("coin.png");

            background = new Background(bg);
            obstacle = new Obstacle(obs);
            platform = new Platform(pl);
            bullet = new Bullet(bul);

            coin1 = new Coin(c1, 0, new Point(32, 32), 4, 60, 0);
            coin2 = new Coin(c2, 200, new Point(32, 32), 4, 60, 50);
            coin3 = new Coin(c3, 410, new Point(32, 32), 4, 60, -50);
            coin4 = new Coin(c4, 650, new Point(32, 32), 4, 60, -100);

            sF = content.Load<SpriteFont>("SpriteFont1");
        }

        // Update method
        public void Update(GameTime gameTime)
        {
            // Update player
            player.Update(gameTime);
            enemy.Update(gameTime);
            bullet.Follow(gameTime, player);

            coin1.Update(gameTime);
            coin2.Update(gameTime);
            coin3.Update(gameTime);
            coin4.Update(gameTime);

            // Check if dead
            if (healthBar.CurrentValue == 0)
                healthBar.IsZero = true;

            // Change damage multiplier according to value of evil bar
            if (evilMeter.CurrentValue > (evilMeter.MaxValue / 2))
                evilMultiplier = 1;
            else if (evilMeter.CurrentValue <= (evilMeter.MaxValue / 3)) // if less than a third
                evilMultiplier = 2.0;
            else if (evilMeter.CurrentValue <= (evilMeter.MaxValue / 2)) // if less than half
                evilMultiplier = 1.5; // take 1.5 damage

            spawn = rand.Next(100);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            // Draw scrolling game elements
            background.Draw(bgSpeed, gameTime, spriteBatch);
            obstacle.Draw(obsSpeed, gameTime, spriteBatch);
            platform.Draw(plspeed, gameTime, spriteBatch);
            coin1.Draw(plspeed, gameTime, spriteBatch, player);
            coin2.Draw(plspeed, gameTime, spriteBatch, player);
            coin3.Draw(plspeed, gameTime, spriteBatch, player);
            coin4.Draw(plspeed, gameTime, spriteBatch, player);

            // Draw Meters
            healthBar.Draw(spriteBatch, gameTime, new Vector2(16, 16), Color.White);
            evilMeter.Draw(spriteBatch, gameTime, new Vector2(16, 64), Color.White);
            string hpdisplay = healthBar.CurrentValue + "/" + healthBar.MaxValue;
            string emDisplay = evilMeter.CurrentValue + "/" + evilMeter.MaxValue;
            spriteBatch.DrawString(sF, hpdisplay, new Vector2(240, 20), Color.White);
            spriteBatch.DrawString(sF, emDisplay, new Vector2(190, 63), Color.White);
            spriteBatch.DrawString(sF, coinCount.ToString(), new Vector2(500, 20), Color.Black);
            
            // Control bullet fire
            if (fire == true)
            {
                bullet.IsActive = true;
                fire = false;
            }
            //bullet.Pos = new Vector2 (250, bullet.Pos.Y);
            bullet.Draw(bulSpeed, gameTime, spriteBatch, player,howfar); //will need to get a keybress from the player and pass it through an IF statement
            //menu.Fire = false;
            
            // OBSTACLE COLLISION
            if (obstacle.isColliding(player)) 
            {
                player.Draw(gameTime, spriteBatch, Color.Red);
                obstacleCollision = true;

            }

            else if (enemy.isColliding(player)) //ENEMY COLLISION
            {
                player.Draw(gameTime, spriteBatch, Color.Red);
                enemyCollision = true;
            }

            // COIN COLLISION
            else if (coin1.isColliding(player))
            {
                coin1.Respawn(plspeed, gameTime, spriteBatch, 100);
                coinCount = coinCount + coinUp;     
            }  
         
            else if (coin2.isColliding(player))
            {
                coin2.Respawn(plspeed, gameTime, spriteBatch, 100);
                coinCount = coinCount + coinUp; 
            }

            else if (coin3.isColliding(player))
            {
                coin3.Respawn(plspeed, gameTime, spriteBatch, 100);
                coinCount = coinCount + coinUp;    
            }

            else if (coin4.isColliding(player))
            {
                coin4.Respawn(plspeed, gameTime, spriteBatch, 100);
                coinCount = coinCount + coinUp;   
            }

            else if (platform.isColliding(player))
            {
                player.Draw(gameTime, spriteBatch, Color.White);
                player.OnPlatform = true;
                if (player.OnPlatform)
                {
                    SpritePosition = new Vector2(16, (int)platform.Pos.Y - player.Height);
                    player.Position = SpritePosition;
                }
            }

            else
            {
                player.OnPlatform = false;
                player.Draw(gameTime, spriteBatch, Color.White);
            }

            // Subtract Health when done colliding 
            // Avoids constant subtracting for duration of collision
            if (obstacle.isColliding(player) == false )
            {
                if (obstacleCollision)
                {
                    player.Draw(gameTime, spriteBatch, Color.Red);

                    // take Damage to HP & Evil because tripping over rocks is lame
                    healthBar.Subtract((int)(DEFAULT_DAMAGE * damageMultiplier * evilMultiplier)); // to be changed according to value of evil meter and damageMultiplier ranging from .01-2.0
                    evilMeter.Subtract(1);
                    obstacleCollision = false;
                }
            }
            // Enemy Collides with player
            if (enemy.isColliding(player) == false)
            {
                if (enemyCollision)
                {
                    player.Draw(gameTime, spriteBatch, Color.Red);
                    healthBar.Subtract((int)(DEFAULT_DAMAGE * damageMultiplier * evilMultiplier));
                    evilMeter.Subtract(1);
                    enemyCollision = false;
                }
                else if(enemy.Pos.X < -128)
                {
                    enemy.Respawn(eneSpeed, gameTime, spriteBatch);
                }
            }

            // Enemy is hit by bullet
            // check if bullet is being fired
            if (bullet.IsActive && enemy.isColliding(bullet, attack))
            {
                bulletCollision = true;

                enemy.Health = enemy.Health - attack;                                                     
                enemy.Draw(eneSpeed, gameTime, spriteBatch, Color.Red);
                if (enemy.Health == 0)
                {
                    enemy.Active = false;
                }             
            }
            else if(bulletCollision)
            {
                evilMeter.Add(1);
                bulletCollision = false;
            }
            else
                enemy.Draw(eneSpeed, gameTime, spriteBatch, Color.White);

            if (enemy.CurrentHP <= 0)
            {
                enemy.Respawn(eneSpeed, gameTime, spriteBatch);
                enemy.Active = false;
            }

            // GET ENEMY TO RESPAWN ONCE GOES OFF SCREEN LEFT
            if (spawn > 0 && spawn < 100)
                obstacle.Respawn(obsSpeed, gameTime, spriteBatch);
            if (spawn > 45)
                platform.Respawn(plspeed, gameTime, spriteBatch);
            //if (spawn > 45)
                //enemy.Respawn(eneSpeed, gameTime, spriteBatch);

            // coins respawn after disappearing off screen
            if (coin1.Pos.X < -10)
                coin1.Respawn(obsSpeed, gameTime, spriteBatch, 100);
            if (coin2.Pos.X < -10)
                coin2.Respawn(obsSpeed, gameTime, spriteBatch, 100);
            if (coin3.Pos.X < -10)
                coin3.Respawn(obsSpeed, gameTime, spriteBatch, 100);
            if (coin4.Pos.X < -10)
                coin4.Respawn(obsSpeed, gameTime, spriteBatch, 100);

        }
        
        // Method to check for jump presses
        public void WhenJumpPress()
        {
            player.Jump = true;
        }

        //public void WhenSlidePress()
        //{
        //    player.Slide = true;
        //}

        public bool GameOver()
        {
            if (healthBar.IsZero)
            {
                return true;
            }
            else return false;
        }
    }
}
