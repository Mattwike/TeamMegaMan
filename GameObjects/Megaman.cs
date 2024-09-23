using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.States.MegamanState;

namespace Project1.GameObjects
{
    public class Megaman
    {

        public IMegamanState State;

        int x {  get; set; }
        int y { get; set; }

        public bool isfacingLeft { get; set; }

        public Megaman()
        {
            State = new IdleMegamanState(this);
        }

        public void SetDirection(bool isFacingLeft)
        {
            isfacingLeft = isFacingLeft;
        }

        public void ChangeDirection()
        {

        }

        public void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
        {
            State.Initialize(_graphics, movementSpeed, megamanSize);
        }

        public void Draw(Texture2D spriteTexture, SpriteBatch _spriteBatch, float movementSpeed)
        {
            State.Draw(spriteTexture, _spriteBatch, movementSpeed);
        }

    }
}