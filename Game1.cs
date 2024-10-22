using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.SpriteFactories;
using Project1.Sprites;
using Project1.GameObjects;
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
        private GenericEnemy displayedEnemy;

        private Floor floor;


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
            Vector2 floorPos = new Vector2(0, 180);
            floor = new Floor(10, floorPos);
            

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

            // Update Bombomb directly
            megaman.Update(gameTime);
            displayedEnemy.Update(gameTime);

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
            displayedEnemy.Draw(_spriteBatch);
            floor.Draw(_spriteBatch);

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
