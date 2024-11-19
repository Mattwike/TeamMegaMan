using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.SpriteFactories;
using Project1.Sprites;
using Project1.GameObjects;
using Project1.Interfaces;
using Project1.Collisions;
using System.Collections.Generic;
using Project1.Levels;
using System.IO;

namespace Project1
{
    public class Game1 : Game
    {
        private KeyboardController _keyboardController;
        private MouseController _mouseController;
        private List<ISprite> sprites;  // Keeping this for future use if needed
        private List<Pellet> pellets;
        private List<EnemyDrop> enemyDropList;  // Added enemyDropList
        private Megaman megaman;
        private GenericEnemy displayedEnemy;

        private LevelLoader levelLoader;
        private LevelParser levelParser;
        private List<IBlocks> levelBlocks;
        private List<IEnemySprite> levelEnemies;

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
            IsMouseVisible = true;
            pellets = new List<Pellet>();
            enemyDropList = new List<EnemyDrop>();  // Initialize enemyDropList
        }

        protected override void Initialize()
        {
            // Initialize movement speed and screen dimensions
            movementSpeed = 3;
            width = _graphics.PreferredBackBufferWidth / 2;
            height = _graphics.PreferredBackBufferHeight / 2;

            _mouseController = new MouseController();

            // Load all textures for MegaMan and Enemies
            megaManSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.CreatePellet();

            // Load Block Textures
            BlockSpriteFactory.Instance.LoadAllTextures(Content);

            // Initialize the displayed enemy
            displayedEnemy = new GenericEnemy();
            displayedEnemy.Initialize(_graphics, 30, 40);

            // Initialize the MegaMan character
            megaman = new Megaman();
            megaman.Initialize(_graphics, movementSpeed, 40, interval);
            megaman.x = 0;
            megaman.y = 100;

            _keyboardController = new KeyboardController(this, megaman, displayedEnemy, pellets);
            _keyboardController.Initialize();
            _mouseController.Initialize(height, width);

            // Initialize the level loader and parser
            levelLoader = new LevelLoader();
            levelParser = new LevelParser();

            string levelPath = Path.Combine("Levels", "Level1.txt");

            // Load the level data
            List<string> levelData = levelLoader.LoadLevel(levelPath);

            // Parse the level data to create blocks and enemies
            levelParser.ParseLevel(levelData);

            // Retrieve the blocks and enemies created by the parser
            levelBlocks = levelParser.Blocks;
            levelEnemies = levelParser.Enemies;

            // Initialize enemies
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
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            // Use the keyboard controller to get input and update MegaMan and enemies
            _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);

            // Update MegaMan
            megaman.Update(gameTime, interval);  // Added 'interval' parameter

            displayedEnemy.Update(gameTime);

            // Update level enemies
            foreach (var enemy in levelEnemies)
            {
                enemy.Update(gameTime);
            }

            // Handle collisions
            CollidionHandler.HandleMegamanCollisions(megaman, levelBlocks, levelEnemies, enemyDropList);  // Added 'enemyDropList' parameter

            foreach (var pellet in pellets)
            {
                pellet.Update(gameTime);
            }

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

            _spriteBatch.Begin();

            // Draw level blocks
            foreach (var block in levelBlocks)
            {
                block.Draw(_spriteBatch);
            }

            // Draw enemies
            foreach (var enemy in levelEnemies)
            {
                enemy.Draw(_spriteBatch, false, false);
            }

            // Draw MegaMan and displayed enemy
            megaman.Draw(_spriteBatch, movementSpeed);
            displayedEnemy.Draw(_spriteBatch);

            // Draw pellets
            foreach (var pellet in pellets)
            {
                pellet.Draw(_spriteBatch, movementSpeed);
            }

            // Draw enemy drops if any
            foreach (var enemyDrop in enemyDropList)
            {
                enemyDrop.Draw(_spriteBatch, movementSpeed);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
