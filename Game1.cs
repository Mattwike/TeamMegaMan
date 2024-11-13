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
using Project1.Levels;
using System.IO;


namespace Project1
{
    public class Game1 : Game
    {
        private KeyboardController _keyboardController;
        private MouseController _mouseController;
        private List<ISprite> sprites;  // Keeping this for future use if needed
        List<Pellet> pellets;
        private Megaman megaman;
        private GenericEnemy displayedEnemy;



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
            IsMouseVisible = true;
            pellets = new List<Pellet>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

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
            megaman.Initialize(_graphics, movementSpeed, 40, interval);

            megaman.x = 0;
            megaman.y = 100;

            _keyboardController = new KeyboardController(this,  megaman, displayedEnemy, pellets);
            _keyboardController.Initialize();
            _mouseController.Initialize(height, width);

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
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            // Use the keyboard controller to get input and update MegaMan and enemies
            _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);

            // Update Bombomb directly
            megaman.Update(gameTime);
            displayedEnemy.Update(gameTime);
            CollidionHandler.HandleMegamanCollisions(megaman, levelParser.Blocks);

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

            foreach (var block in levelBlocks)
            {
                block.Draw(_spriteBatch);
            }

            // Draw MegaMan and displayed enemy as before
            megaman.Draw(_spriteBatch, movementSpeed);
            displayedEnemy.Draw(_spriteBatch);


            foreach (var pellet in pellets)
            {
                pellet.Draw(_spriteBatch, movementSpeed);
            }
            // Draw Bombomb directly
            //bombomb.Draw(_spriteBatch, false, false);  // Draw Bombomb without flipping

            base.Draw(gameTime);
        }
    }
}
