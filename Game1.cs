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
using Project1.GameControllers;

namespace Project1
{
    public class Game1 : Game
    {


        Camera camera;
        HealthBar healthBar;

        private KeyboardController _keyboardController;
        private MouseController _mouseController;
        private List<ISprite> sprites; // Keeping this for future use if needed
        private List<Pellet> pellets;
        private List<EnemyDrop> enemyDropList; // Added enemyDropList
        private Megaman megaman;
        private GenericEnemy displayedEnemy;

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
        bool MegamanDied = false;
        int scoreX = 10;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            pellets = new List<Pellet>();
            enemyDropList = new List<EnemyDrop>(); // Initialize enemyDropList
        }

        protected override void Initialize()
        {
            camera = new Camera(GraphicsDevice.Viewport);
            //soundcontroller = new soundController(Content);

            movementSpeed = 3;
            width = _graphics.PreferredBackBufferWidth / 2;
            height = _graphics.PreferredBackBufferHeight / 2;

            _mouseController = new MouseController();

            megaManSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.CreatePellet();
            EnemyDropSpriteFactory.Instance.LoadAllTextures(Content);
            EnemyDropSpriteFactory.Instance.CreateEnemyDrop();
            healthBarSpriteFactory.Instance.LoadAllTextures(Content);
            healthBarSpriteFactory.Instance.CreateHealthBar();

            BlockSpriteFactory.Instance.LoadAllTextures(Content);

            displayedEnemy = new GenericEnemy();
            displayedEnemy.Initialize(_graphics, 30, 40);

            megaman = new Megaman();
            megaman.Initialize(_graphics, movementSpeed, 40, interval);
            megaman.x = 0;
            megaman.y = 1113;

            _keyboardController = new KeyboardController(this, megaman, displayedEnemy, pellets);
            _keyboardController.Initialize();
            _mouseController.Initialize(height, width);

            healthBar = new HealthBar();
            healthBar.Initialize(_graphics, megaman);

            levelLoader = new LevelLoader();
            levelParser = new LevelParser();

            string levelPath = Path.Combine("Levels", "Level1.txt");

            List<string> levelData = levelLoader.LoadLevel(levelPath);

            levelParser.ParseLevel(levelData);

            levelBlocks = levelParser.Blocks;
            levelEnemies = levelParser.Enemies;
            ypose = 920;
            //camera.Zoom(1.85f);

            foreach (var enemy in levelEnemies)
            {
                enemy.Initialize(_graphics, movementSpeed, 40);
            }

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            //soundcontroller.LoadContent();
            font = Content.Load<SpriteFont>("ScoreFont");
            GameOverFont = Content.Load<SpriteFont>("GameOverFont");
            BlockSpriteFactory.Instance.LoadAllTextures(Content);
        }

        protected override void Update(GameTime gameTime)
        {
            _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);

            megaman.Update(gameTime, interval);
            displayedEnemy.Update(gameTime);

            foreach (var enemy in levelEnemies)
            {
                enemy.Update(gameTime);
            }

            CollidionHandler.HandleMegamanCollisions(megaman, levelBlocks, levelEnemies, enemyDropList);

            foreach (var pellet in pellets)
            {
                pellet.Update(gameTime);
            }

            foreach (var enemyDrop in enemyDropList)
            {
                enemyDrop.Update(gameTime);
            }

            camera.Position = new Vector2(megaman.x, ypose);
            scoreX = (int)megaman.x;

            if (megaman.is_climbing && ypose < megaman.y)
            {
                ypose -= 3;
            }

            if (megaman.GetHealth() <= 0 || megaman.y > 1200)
            {
                MegamanDied = true;
            }
            else
            {
                MegamanDied = false;
            }

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


                foreach (var enemy in levelEnemies)
                {
                    enemy.Draw(_spriteBatch, false, false);
                }

                // Draw MegaMan and displayed enemy as before
                megaman.Draw(_spriteBatch, movementSpeed);
                displayedEnemy.Draw(_spriteBatch);
                _spriteBatch.DrawString(font, megaman.GetHealth().ToString(), new Vector2(scoreX - 150, ypose - 50), Color.White);
                _spriteBatch.DrawString(font, megaman.GetScore().ToString(), new Vector2(scoreX, ypose + 30), Color.White);

                healthBar.Draw(_spriteBatch);

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
