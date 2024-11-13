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

        private KeyboardController _keyboardController;
        private MouseController _mouseController;
        private List<ISprite> sprites;  // Keeping this for future use if needed
        List<Pellet> pellets;
        private Megaman megaman;
        private SniperJoe sniperjoe;
        private GenericEnemy displayedEnemy;

        private soundController soundcontroller;
        private LevelLoader levelLoader;
        private LevelParser levelParser;
        private List<IBlocks> levelBlocks;

        float movementSpeed;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int height;
        int width;
        int interval = 0;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            pellets = new List<Pellet>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            camera = new Camera(GraphicsDevice.Viewport);
            soundcontroller = new soundController(Content);

            Texture2D SniperJoeSheet;
            SniperJoeSheet = Content.Load<Texture2D>("enemy");
            movementSpeed = 3;

            width = _graphics.PreferredBackBufferWidth / 2;
            height = _graphics.PreferredBackBufferHeight / 2;
            _mouseController = new MouseController();

            // Load all textures for MegaMan and Enemies
            megaManSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.CreatePellet();

            //load Block Textures
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
     


            // Initialize the displayed enemy
            displayedEnemy = new GenericEnemy();
            displayedEnemy.Initialize(_graphics, 30, 40);

            // Initialize the MegaMan character
            megaman = new Megaman();
            sniperjoe = new SniperJoe(SniperJoeSheet);
            megaman.Initialize(_graphics, movementSpeed, 40, interval);
            sniperjoe.Initialize(_graphics, 30, 40);

            megaman.x = 0;
            megaman.y = 100;

            megaman.reachedCheckpoint();

            _keyboardController = new KeyboardController(this,  megaman, displayedEnemy, pellets);
            _keyboardController.Initialize();
            _mouseController.Initialize(height, width);
            soundcontroller.Initialize();

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

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create the SpriteBatch used for rendering
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            soundcontroller.LoadContent();
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
        }

        protected override void Update(GameTime gameTime)
        {
           Boolean paused = _keyboardController.isPaused();
            if (!_keyboardController.isPaused())
            {
                // Use the keyboard controller to get input and update MegaMan and enemies
                _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);
                List<IBlocks> blockList = new List<IBlocks>();
                List<IEnemySprite> enemies = new List<IEnemySprite>();
                enemies.Add(sniperjoe);
                enemies.AddRange(sniperjoe.projectiles);

                // Update Bombomb directly
                megaman.Update(gameTime, interval);
                sniperjoe.Update(gameTime);
                displayedEnemy.Update(gameTime);
                CollidionHandler.HandleMegamanCollisions(megaman, levelParser.Blocks, enemies);
                CollidionHandler.HandleEnemyCollisions(sniperjoe, blockList, pellets);

                foreach (var pellet in pellets)
                {
                    pellet.Update(gameTime);
                    //CollidionHandler.HandleMegamanPelletCollisions(pellet, sniperjoe);
                }

                camera.Position = new Vector2(megaman.x, camera.Position.Y);

                base.Update(gameTime);
            }


            soundcontroller.Update(megaman, paused);
            _keyboardController.checkExit();

            // Update level blocks if necessary
            foreach (var block in levelBlocks)
            {
                block.Update();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);  // Clear the screen

            
            _spriteBatch.Begin(transformMatrix: camera.GetTransform());

            foreach (var block in levelBlocks)
            {
                block.Draw(_spriteBatch);
            }

            // Draw MegaMan and displayed enemy as before
            megaman.Draw(_spriteBatch, movementSpeed);
            sniperjoe.Draw(_spriteBatch, false, false);
            displayedEnemy.Draw(_spriteBatch);


            foreach (var pellet in pellets)
            {
                pellet.Draw(_spriteBatch, movementSpeed);
            }

            _spriteBatch.End();

            // Draw Bombomb directly
            //bombomb.Draw(_spriteBatch, false, false);  // Draw Bombomb without flipping

            base.Draw(gameTime);
        }
    }
}
