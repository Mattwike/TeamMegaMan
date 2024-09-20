using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1
{
    public class Game1 : Game
    {
        private KeyboardController _keyboardController;
        private MouseController _mouseController;
        private sprite1Controller _sprite1Controller;
        private sprite2Controller _sprite2Controller;
        private sprite3Controller _sprite3Controller;
        private sprite4Controller _sprite4Controller;
        private SpriteFont Text;


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

            _keyboardController = new KeyboardController();
            _mouseController = new MouseController();

            _sprite1Controller = new sprite1Controller();
            _sprite2Controller = new sprite2Controller();
            _sprite3Controller = new sprite3Controller();
            _sprite4Controller = new sprite4Controller();

            _sprite1Controller.Initialize(_graphics, movementSpeed);
            _sprite2Controller.Initialize(_graphics, movementSpeed);
            _sprite3Controller.Initialize(_graphics, movementSpeed);
            _sprite4Controller.Initialize(_graphics, movementSpeed);
            
            _mouseController.Initialize(height, width);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            spriteTexture = Content.Load<Texture2D>("link");
            Text = Content.Load<SpriteFont>("Text");
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
            

            _sprite1Controller.Update(gameTime);
            _sprite2Controller.Update(gameTime);
            _sprite3Controller.Update(gameTime);
            _sprite4Controller.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            if (lastInput == 1)
            {
                _sprite1Controller.Draw(spriteTexture, _spriteBatch, movementSpeed);
            }

            if (lastInput == 2)
            {
                _sprite2Controller.Draw(spriteTexture, _spriteBatch, movementSpeed);
            }

            if (lastOutput == 3 || lastMouseQuad == 3)
            {
                lastOutput = 3;
                lastMouseQuad = 3;
                _sprite3Controller.Draw(spriteTexture, _spriteBatch, movementSpeed);
            }

            if (lastOutput == 4 || lastMouseQuad == 4)
            {
                lastOutput = 4;
                lastMouseQuad = 4;
                _sprite4Controller.Draw(spriteTexture, _spriteBatch, movementSpeed);
            }
            
            _spriteBatch.Begin();
            _spriteBatch.DrawString(Text, "Credits: \nProgram Made By: Matthew Weikel\nSprites from: https://www.spriters-resource.com/nes/legendof\nzelda/sheet/8366/\nRipped by: Mister Mike", new Vector2(10, 300), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
