using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Project1.SpriteFactories;
using Project1.Sprites;
using Project1.States.MegamanState;
using Project1.GameObjects;

namespace Project1
{
    public class Game1 : Game
    {
        private KeyboardController _keyboardController;
        private MouseController _mouseController;
        private List<ISprite> sprites;
        private Megaman megaman;
        private GenericEnemy displayedEnemy;

        private SniperJoe sniperJoe;  // Step 1: Declare a Sniper Joe instance

        float movementSpeed;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        int height;
        int width;
        int output;
        int lastOutput;
        int lastMouseQuad;
        int mouseQuad;
        int lastInput;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // Existing initialization logic

            output = 1;
            mouseQuad = 1;
            lastInput = 1;

            movementSpeed = 3;

            width = _graphics.PreferredBackBufferWidth / 2;
            height = _graphics.PreferredBackBufferHeight / 2;

            _mouseController = new MouseController();

            megaManSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            displayedEnemy = new GenericEnemy();
            displayedEnemy.Initialize(_graphics, 30, 40);

            megaman = new Megaman();
            megaman.Initialize(_graphics, movementSpeed, 40);

            _keyboardController = new KeyboardController(this, megaman, displayedEnemy);

            _mouseController.Initialize(height, width);
            _keyboardController.Initialize();

            // Step 2: Initialize Sniper Joe with default values (position and size can be adjusted)
            sniperJoe = (SniperJoe)EnemySpriteFactory.Instance.CreateSniperJoe();


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Step 3: Load the Sniper Joe content (texture)
            sniperJoe.Initialize(_graphics, movementSpeed, 40);  // Example initialization values
        }

        protected override void Update(GameTime gameTime)
        {
            // Existing update logic

            _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);

            // Step 4: Update Sniper Joe animation
            sniperJoe.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Existing draw logic

            megaman.Draw(_spriteBatch, movementSpeed);
            displayedEnemy.Draw(_spriteBatch);

            // Step 5: Draw Sniper Joe
            sniperJoe.Draw(_spriteBatch, false, false);  // Default flip values

            base.Draw(gameTime);
        }
    }
}
