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

        private Floor floor;
        private Floor floor2;
        private Floor wall;
        private Floor Ceiling;

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
            Vector2 floorPos = new Vector2(0, 180);
            Vector2 floorPos2 = new Vector2(200, 180);
            Vector2 wallpos = new Vector2(250, 160);
            Vector2 Ceilingpos = new Vector2(200, 100);
            floor = new Floor(10, floorPos);
            floor2 = new Floor(10, floorPos2);
            wall = new Floor(3, wallpos);
            Ceiling = new Floor(10, Ceilingpos);


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

            _keyboardController = new KeyboardController(this, megaman, displayedEnemy, pellets);
            _keyboardController.Initialize();
            _mouseController.Initialize(height, width);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create the SpriteBatch used for rendering
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            font = Content.Load<SpriteFont>("ScoreFont");
            GameOverFont = Content.Load<SpriteFont>("GameOverFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (!_keyboardController.isPaused() && !MegamanDied)
            {
                // Use the keyboard controller to get input and update MegaMan and enemies
                _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);
                List<IBlocks> blockList = new List<IBlocks>();
                List<IEnemySprite> projectiles = new List<IEnemySprite>();
                projectiles.AddRange(sniperjoe.projectiles);
                blockList.Add(floor);
                blockList.Add(floor2);
                blockList.Add(wall);
                blockList.Add(Ceiling);

                // Update Bombomb directly
                megaman.Update(gameTime, interval);
                sniperjoe.Update(gameTime);
                displayedEnemy.Update(gameTime);
                CollidionHandler.HandleMegamanCollisions(megaman, blockList, projectiles, enemyDropList);
                CollidionHandler.HandleEnemyCollisions(sniperjoe, blockList, pellets, enemyDropList);


                _keyboardController.checkExit();


                foreach (var pellet in pellets)
                {
                    pellet.Update(gameTime);
                    //CollidionHandler.HandleMegamanPelletCollisions(pellet, sniperjoe);
                }
                foreach (var enemyDrop in enemyDropList)
                {
                    enemyDrop.Update(gameTime);
                }
                camera.Position = new Vector2(megaman.x, camera.Position.Y);
                scoreX = (int)megaman.x;

                if (megaman.GetHealth() <= 0)
                {
                    MegamanDied = true;
                }

                base.Update(gameTime);
            }
            else
            {
                _keyboardController.checkExit();
            }
            
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

                // Draw MegaMan and displayed enemy as before
                megaman.Draw(_spriteBatch, movementSpeed);
                sniperjoe.Draw(_spriteBatch, false, false);
                displayedEnemy.Draw(_spriteBatch);
                floor.Draw(_spriteBatch);
                floor2.Draw(_spriteBatch);
                wall.Draw(_spriteBatch);
                Ceiling.Draw(_spriteBatch);
                _spriteBatch.DrawString(font, megaman.GetHealth().ToString(), new Vector2(scoreX-370, -200), Color.White);
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
