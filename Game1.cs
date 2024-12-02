using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.SpriteFactories;
using Project1.Sprites;
using Project1.GameObjects;
using Project1.Interfaces;
using Project1.Collisions;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Media;
using Project1.GameControllers;
using Project1.Levels;
using System.IO;
using System;



namespace Project1
{
    public class Game1 : Game
    {
        Camera camera;
        HealthBar healthBar;

        private KeyboardController _keyboardController;
        private MouseController _mouseController;
        private List<ISprite> sprites;  // Keeping this for future use if needed
        List<Pellet> pellets;
        List<EnemyDrop> enemyDropList;
        private Megaman megaman;
        private int megamanHealth = 100;
        private GenericEnemy displayedEnemy;

        private soundController soundcontroller;
        private LevelLoader levelLoader;
        private LevelParser levelParser;
        private List<IBlocks> levelBlocks;
        private List<IEnemySprite> levelEnemies;

        float movementSpeed;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        int ypose;
        int height;
        int width;
        int interval = 0;

        private SpriteFont font;
        private SpriteFont GameOverFont;
        int scoreX = 10;
        bool MegamanDied = false;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            pellets = new List<Pellet>();
            enemyDropList = new List<EnemyDrop>();
            
        }
        

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(GraphicsDevice.Viewport);
            soundcontroller = new soundController(Content);

            movementSpeed = 3;

            width = _graphics.PreferredBackBufferWidth / 2;
            height = _graphics.PreferredBackBufferHeight / 2;
            _mouseController = new MouseController();

            // Load all textures for MegaMan and Enemies
            megaManSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.CreatePellet();
            EnemyDropSpriteFactory.Instance.LoadAllTextures(Content);
            EnemyDropSpriteFactory.Instance.CreateEnemyDrop();
            healthBarSpriteFactory.Instance.LoadAllTextures(Content);
            healthBarSpriteFactory.Instance.CreateHealthBar();

            //load Block Textures
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
     


            // Initialize the displayed enemy
            displayedEnemy = new GenericEnemy();
            displayedEnemy.Initialize(_graphics, 30, 40);

            // Initialize the MegaMan character
            megaman = new Megaman();
            megaman.Initialize(_graphics, movementSpeed, 40, interval);

            //start
            megaman.x = 0;
            megaman.y = 1113;

            //megaman.x = 4700;
            //megaman.y = 169;

            megaman.reachedCheckpoint();

            _keyboardController = new KeyboardController(this, megaman, displayedEnemy, pellets);
            _keyboardController.Initialize();
            _mouseController.Initialize(height, width);
            soundcontroller.Initialize();

            healthBar = new HealthBar();
            healthBar.Initialize(_graphics, megaman);

            // Initialize the level loader and parser
            levelLoader = new LevelLoader();
            levelParser = new LevelParser();

            string levelPath = Path.Combine("Levels", "Level1.txt");

            // Load the level data
            List<string> levelData = levelLoader.LoadLevel(levelPath);

            // Parse the level data to create blocks
            levelParser.ParseLevel(levelData);

            // Retrieve the blocks created by the parser
            levelBlocks = levelParser.Blocks;
            levelEnemies = levelParser.Enemies;
            ypose = (int) megaman.y - 210;
            camera.Zoom(1.85f);

            foreach (var enemy in levelEnemies)
            {
                enemy.Initialize(_graphics, movementSpeed, 40);  // Adjust parameters as needed
                
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create the SpriteBatch used for rendering
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            soundcontroller.LoadContent();
            font = Content.Load<SpriteFont>("ScoreFont");
            GameOverFont = Content.Load<SpriteFont>("GameOverFont");
            BlockSpriteFactory.Instance.LoadAllTextures(Content);

        }

        protected override void Update(GameTime gameTime)
        {

            bool paused = _keyboardController.isPaused();

            if (!_keyboardController.isPaused())
            {
                // Use the keyboard controller to get input and update MegaMan and enemies
                _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);

                healthBar.Update(gameTime, ypose);

                // Update Bombomb directly
                megaman.Update(gameTime, interval);
                displayedEnemy.Update(gameTime);

                CollidionHandler.HandleMegamanCollisions(megaman, levelParser.Blocks, levelEnemies, enemyDropList);

                foreach (var pellet in pellets)
                {
                    pellet.Update(gameTime);
                    //CollidionHandler.HandleMegamanPelletCollisions(pellet, sniperjoe);
                }
                foreach (var enemyDrop in enemyDropList)
                {
                    enemyDrop.Update(gameTime);
                }
                foreach (var enemy in levelEnemies)
                {
                    enemy.Update(gameTime);
                    CollidionHandler.HandleEnemyCollisions(enemy, levelBlocks, pellets, enemyDropList);
                }
                // Update level blocks if necessary
                foreach (var block in levelBlocks)
                {
                    block.Update();
                }
                camera.Position = new Vector2(megaman.x, ypose);
                //camera.Position = new Vector2(megaman.x, ypose);
                scoreX = (int)megaman.x;

                if (!megaman.is_jumping && !megaman.is_falling)
                {
                    ypose = (int) megaman.y - 210;
                }

                if (megaman.GetHealth() <= 0 || megaman.y > 1200)
                {
                    MegamanDied = true;
                }
                else
                {
                    MegamanDied = false;
                }

                base.Update(gameTime);
            }


            soundcontroller.Update(megaman, paused, pellets, MegamanDied);
            _keyboardController.checkExit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            if (MegamanDied)
            {
                //int bufferNum = 1000000 / megaman.GetScore();
                pellets.Clear();

                GraphicsDevice.Clear(Color.DodgerBlue);
                _spriteBatch.Begin(transformMatrix: camera.GetTransform());
                _spriteBatch.DrawString(GameOverFont, "GAME OVER", new Vector2(scoreX-50, ypose+105), Color.White);
                _spriteBatch.DrawString(GameOverFont, megaman.GetScore().ToString(), new Vector2(scoreX-5, ypose + 135), Color.White);
                _spriteBatch.DrawString(GameOverFont, "PRESS R TO RESTART LEVEL", new Vector2(scoreX - 110, ypose + 210), Color.White);
                _spriteBatch.End();
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);  // Clear the screen

            
                _spriteBatch.Begin(transformMatrix: camera.GetTransform());

                foreach (var block in levelBlocks)
                {
                    block.Draw(_spriteBatch);
                }

                // Draw MegaMan and displayed enemy as before
                megaman.Draw(_spriteBatch, movementSpeed);
                displayedEnemy.Draw(_spriteBatch);
                _spriteBatch.DrawString(font, megaman.GetHealth().ToString(), new Vector2(scoreX-150, ypose-50), Color.White);
                _spriteBatch.DrawString(font, megaman.GetScore().ToString(), new Vector2(scoreX, ypose+30), Color.White);

                healthBar.Draw(_spriteBatch);

                foreach (var pellet in pellets)
                {
                    pellet.Draw(_spriteBatch, movementSpeed);
                }
                foreach (var enemyDrop in enemyDropList)
                {
                    enemyDrop.Draw(_spriteBatch, movementSpeed);
                }
                foreach (var enemy in levelEnemies)
                {
                    enemy.Draw(_spriteBatch, false, false);
                }
                _spriteBatch.End();
            }

            base.Draw(gameTime);
        }
    }
}
