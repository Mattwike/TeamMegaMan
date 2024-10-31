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
        private KeyboardController _keyboardController;
        private MouseController _mouseController;
        private List<ISprite> sprites;  // Keeping this for future use if needed
        List<Pellet> pellets;
        private Megaman megaman;
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

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            pellets = new List<Pellet>();
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
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

            _keyboardController = new KeyboardController(this,  megaman, displayedEnemy, pellets);
            _keyboardController.Initialize();
            _mouseController.Initialize(height, width);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Create the SpriteBatch used for rendering
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            // Use the keyboard controller to get input and update MegaMan and enemies
            _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);
            List<IBlocks> blockList = new List<IBlocks>();
            blockList.Add(floor);
            blockList.Add(floor2);
            blockList.Add(wall);
            blockList.Add(Ceiling);

            // Update Bombomb directly
            megaman.Update(gameTime);
            sniperjoe.Update(gameTime);
            displayedEnemy.Update(gameTime);
            CollidionHandler.HandleMegamanCollisions(megaman, blockList);
            CollidionHandler.HandleEnemyCollisions(sniperjoe, blockList);


            foreach (var pellet in pellets)
            {
                pellet.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);  // Clear the screen

            // Draw MegaMan and displayed enemy as before
            megaman.Draw(_spriteBatch, movementSpeed);
            sniperjoe.Draw(_spriteBatch, false, false);
            displayedEnemy.Draw(_spriteBatch);
            floor.Draw(_spriteBatch);
            floor2.Draw(_spriteBatch);
            wall.Draw(_spriteBatch);
            Ceiling.Draw(_spriteBatch);

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
