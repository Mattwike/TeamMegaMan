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
        List<EnemyDrop> enemyDropList;
        private Megaman megaman;
        private int megamanHealth = 100;
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
            EnemyDropSpriteFactory.Instance.LoadAllTextures(Content);
            EnemyDropSpriteFactory.Instance.CreateEnemyDrop();

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
            megaman.y = 1113;

            megaman.reachedCheckpoint();

            _keyboardController = new KeyboardController(this, megaman, displayedEnemy, pellets);
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
                List<IBlocks> blockList = new List<IBlocks>();
                List<IEnemySprite> enemies = new List<IEnemySprite>();
                enemies.Add(sniperjoe);
                enemies.AddRange(sniperjoe.projectiles);

                // Update Bombomb directly
                megaman.Update(gameTime, interval);
                sniperjoe.Update(gameTime);
                displayedEnemy.Update(gameTime);

                CollidionHandler.HandleMegamanCollisions(megaman, levelParser.Blocks, enemies, enemyDropList);
                CollidionHandler.HandleEnemyCollisions(sniperjoe, blockList, pellets, enemyDropList);

                foreach (var pellet in pellets)
                {
                    pellet.Update(gameTime);
                    //CollidionHandler.HandleMegamanPelletCollisions(pellet, sniperjoe);
                }
                foreach (var enemyDrop in enemyDropList)
                {
                    enemyDrop.Update(gameTime);
                }
                //camera.Position = new Vector2(megaman.x, camera.Position.Y);
                camera.Position = new Vector2(megaman.x, 1200);
                scoreX = (int)megaman.x;

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
            if (MegamanDied)
            {


                GraphicsDevice.Clear(Color.Black);
                _spriteBatch.Begin(transformMatrix: camera.GetTransform());
                _spriteBatch.DrawString(GameOverFont, "GAME OVER", new Vector2(scoreX - 370, -50), Color.Red);
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
                sniperjoe.Draw(_spriteBatch, false, false);
                displayedEnemy.Draw(_spriteBatch);
                _spriteBatch.DrawString(font, megaman.GetHealth().ToString(), new Vector2(scoreX - 370, -200), Color.White);
                _spriteBatch.DrawString(font, megaman.GetScore().ToString(), new Vector2(scoreX, -200), Color.White);

                foreach (var pellet in pellets)
                {
                    pellet.Draw(_spriteBatch, movementSpeed);
                }
                foreach (var enemyDrop in enemyDropList)
                {
                    enemyDrop.Draw(_spriteBatch, movementSpeed);
                }
                _spriteBatch.End();
            }
            

            

            

            // Draw Bombomb directly
            //bombomb.Draw(_spriteBatch, false, false);  // Draw Bombomb without flipping

            base.Draw(gameTime);
        }
    }
}
