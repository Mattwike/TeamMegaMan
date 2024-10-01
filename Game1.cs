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
        private Gabyoall gabyoall;

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
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            movementSpeed = 3;

            width = _graphics.PreferredBackBufferWidth / 2;
            height = _graphics.PreferredBackBufferHeight / 2;

            _mouseController = new MouseController();

            megaManSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);

            //sprites = new List<ISprite>
            //{
            //    megaManSpriteFactory.Instance.CreateIdleMegaman(),
            //    megaManSpriteFactory.Instance.CreateRunningMegaman(),
            //    megaManSpriteFactory.Instance.CreateRunningShootingMegaman(),
            //    megaManSpriteFactory.Instance.CreateClimbingShootingMegaman(),
            //    megaManSpriteFactory.Instance.CreateDamagedMegaman(),
            //    megaManSpriteFactory.Instance.CreateClimbingMegaman(),
            //    EnemySpriteFactory.Instance.CreateJumpingFlea(),
            //    EnemySpriteFactory.Instance.CreateBombManIdle(),
            //    EnemySpriteFactory.Instance.CreateBombManThrowing(),
            //    megaManSpriteFactory.Instance.CreateClimbingReachedTopMegaman(),
            //    EnemySpriteFactory.Instance.CreateScrewDriver(),
            //};

            displayedEnemy = new GenericEnemy();
            //arbitrary numbers for movement speed and size
            displayedEnemy.Initialize(_graphics, 30, 40);
         
            megaman = new Megaman();
            megaman.Initialize(_graphics, movementSpeed, 40, interval);

            megaman.x = width;
            megaman.y = height;

            _keyboardController = new KeyboardController(this,  megaman, displayedEnemy);

            _mouseController.Initialize(height, width);
            _keyboardController.Initialize();

            // Create Gabyoall using the factory method
            gabyoall = (Gabyoall)EnemySpriteFactory.Instance.CreateGabyoall();

            // Initialize Gabyoall with position, speed, and size values
            gabyoall.Initialize(_graphics, movementSpeed, 32);  // Example size 32

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void Update(GameTime gameTime)
        {

            // Use the keyboard controller to get input and update the ball position

            //foreach (var obj in sprites)
            //{
            //    obj.Update(gameTime);
            //}

            _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);

            // Use the keyboard controller to get input and update the game objects
            _keyboardController.Update(_graphics, movementSpeed, 40, gameTime);

            // Update Gabyoall's movement and animation
            gabyoall.Update(gameTime);


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //TODO: Add your drawing code here

            //foreach (var obj in sprites)
            //{
            //    obj.Draw(_spriteBatch, movementSpeed, false, false);
            //}

            megaman.Draw(_spriteBatch, movementSpeed);
            displayedEnemy.Draw(_spriteBatch);

            // Existing draw logic for MegaMan and other enemies
            megaman.Draw(_spriteBatch, movementSpeed);
            displayedEnemy.Draw(_spriteBatch);

            // Draw Gabyoall on the screen
            gabyoall.Draw(_spriteBatch, false, false);  // Default flip values

            base.Draw(gameTime);
        }
    }
}
