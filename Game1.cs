﻿using Microsoft.Xna.Framework;
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
using System.Threading;
using Project1.Level;


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

        private soundController soundcontroller;
        private LevelLoader levelLoader;
        private LevelParser levelParser;
        private List<IBlocks> levelBlocks;
        private List<IEnemySprite> levelEnemies;
        private List<IEnemyProjectile> levelProjectiles;
        //test
        private Texture2D bossSheet;
        private Bombman Bombman;
        private Texture2D TitleScreen;
        private bool start = false;

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
        private GameWorld GameWorld;


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            pellets = new List<Pellet>();
            enemyDropList = new List<EnemyDrop>();
            //_graphics.ToggleFullScreen();
            GameWorld = new GameWorld(_graphics);
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
            Vector2 position = new Vector2(10f, 1113f);
            Bombman = new Bombman(bossSheet, position);
            // Load all textures for MegaMan and Enemies
            megaManSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            BombmanSpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.CreatePellet();
            EnemyDropSpriteFactory.Instance.LoadAllTextures(Content);
            EnemyDropSpriteFactory.Instance.CreateEnemyDrop();
            healthBarSpriteFactory.Instance.LoadAllTextures(Content);
            healthBarSpriteFactory.Instance.CreateHealthBar();
            Bombman.Initialize(_graphics, 12, 10);  
            //load Block Textures
            BlockSpriteFactory.Instance.LoadAllTextures(Content);

            // Initialize the MegaMan character
            megaman = new Megaman();
            megaman.Initialize(_graphics, interval);

            //start

            megaman.reachedCheckpoint();

            _keyboardController = new KeyboardController(this, megaman, pellets);
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
            levelProjectiles = new List<IEnemyProjectile>();
            ypose = (int)megaman.y - 210;
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
            bossSheet = Content.Load<Texture2D>("bossSheet");
            TitleScreen = Content.Load<Texture2D>("MegamanTitleScreen");
        }

        protected override void Update(GameTime gameTime)
        {

            bool paused = _keyboardController.isPaused();
            Bombman.Update(gameTime, camera, (int)megaman.x);
            if (!_keyboardController.isPaused() && _keyboardController.GameStarted())
            {
                // Use the keyboard controller to get input and update MegaMan and enemies
                _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);

                healthBar.Update(gameTime, ypose);

                // Update Bombomb directly
                megaman.Update(gameTime, interval);

                CollidionHandler.HandleMegamanCollisions(megaman, levelParser.Blocks, levelEnemies, enemyDropList, levelProjectiles);

                foreach (var pellet in pellets)
                {
                    pellet.Update(gameTime);
                }
                foreach (var enemyDrop in enemyDropList)
                {
                    enemyDrop.Update(gameTime);
                }
                foreach (var enemy in levelEnemies)
                {
                    enemy.Update(gameTime, camera, (int)megaman.x);
                    if (enemy.hasProjectiles)
                    {
                        if (enemy.GetProjectiles() != null)
                        {
                            levelProjectiles.AddRange(enemy.GetProjectiles());
                        }
                    }
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
            _spriteBatch.Begin(transformMatrix: camera.GetTransform());
            if (!(_keyboardController.GameStarted()))
            {
                
                _spriteBatch.Draw(TitleScreen, new Rectangle(-250, 0, 550, 350), new Rectangle(0, 0, 1000, 1000), Color.White);
                
                
            }
            else if (MegamanDied)
            {
                //int bufferNum = 1000000 / megaman.GetScore();
                pellets.Clear();

                GraphicsDevice.Clear(Color.DodgerBlue);
                
                _spriteBatch.DrawString(GameOverFont, "GAME OVER", new Vector2(scoreX-50, ypose+105), Color.White);
                _spriteBatch.DrawString(GameOverFont, megaman.GetScore().ToString(), new Vector2(scoreX-5, ypose + 135), Color.White);
                _spriteBatch.DrawString(GameOverFont, "PRESS R TO RESTART LEVEL", new Vector2(scoreX - 110, ypose + 210), Color.White);
                
            }
            else
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);  // Clear the screen

            
                

                foreach (var block in levelBlocks)
                {
                    block.Draw(_spriteBatch);
                }

                // Draw MegaMan and displayed enemy as before
                Bombman.Draw(_spriteBatch);
                megaman.Draw(_spriteBatch, movementSpeed);
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
                
                
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}