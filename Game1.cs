using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Project1
{
    public class Game1 : Game
    {
        private KeyboardController _keyboardController;
        private MouseController _mouseController;
        private List<ISprite> sprites;



        Texture2D spriteTexture;
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
            // TODO: Add your initialization logic here

            output = 1;
            mouseQuad = 1;
            lastInput = 1;

            movementSpeed = 3;

            width = _graphics.PreferredBackBufferWidth / 2;
            height = _graphics.PreferredBackBufferHeight / 2;

            _keyboardController = new KeyboardController(this);
            _mouseController = new MouseController();

            sprites = new List<ISprite>
            {
                new idleMegaman(spriteTexture),
                new runningMegaman(spriteTexture),
                new runningShootingMegaman(spriteTexture),
                new damagedMegaman(spriteTexture),
                new climbingMegaman(spriteTexture),
                new climbingShootingMegaman(spriteTexture),
                new climbingReachedTopMegaman(spriteTexture)
            };

            foreach (var obj in sprites)
            {
                obj.Initialize(_graphics, movementSpeed, 40);
            }
            _mouseController.Initialize(height, width);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spriteTexture = Content.Load<Texture2D>("MegaMan");
            megaManSpriteFactory.Instance.LoadAllTextures(Content);
        }

        protected override void Update(GameTime gameTime)
        {

            // Use the keyboard controller to get input and update the ball position
            mouseQuad = _mouseController.Update(lastMouseQuad);
            if(lastMouseQuad != mouseQuad)
            {
                lastMouseQuad = mouseQuad;
                lastInput = mouseQuad;
                lastOutput = lastInput;
            }

            output = _keyboardController.Update(lastOutput);

            if (lastOutput != output)
            {
                lastOutput = output;
                lastInput = lastOutput;
                lastMouseQuad = lastInput;
            }

            foreach(var obj in sprites)
            {
                obj.Update(gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            
            foreach(var obj in sprites)
            {
                obj.Draw(spriteTexture, _spriteBatch, movementSpeed, false, false);
            }

            base.Draw(gameTime);
        }
    }
}
