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

<<<<<<< HEAD
            foreach (var obj in sprites)
            {
                obj.Initialize(_graphics, movementSpeed, 40, megaman, interval);
            }
            _keyboardController = new KeyboardController(this,  megaman);
=======
            //foreach (var obj in sprites)
            //{
            //    obj.Initialize(_graphics, movementSpeed, 40);
            //}
            _keyboardController = new KeyboardController(this,  megaman, displayedEnemy);
>>>>>>> 4ca635ec8bdddcec745316d0361920af3eb3494c

            _mouseController.Initialize(height, width);
            _keyboardController.Initialize();
            
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
            base.Draw(gameTime);
        }
    }
}
