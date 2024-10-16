using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.States.MegamanState;

namespace Project1.GameObjects
{
    public class Pellet
    {

        public IPelletState State;

        public float x {  get; set; }
        public float y { get; set; }

        public bool isfacingLeft { get; set; }

        public Pellet()
        {
            State = new PelletState(this);
        }

        public void SetDirection(bool isFacingLeft)
        {
            isfacingLeft = isFacingLeft;
        }


        public void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman megaman, int interval)
        {
            State.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval);
        }

        public void Draw(SpriteBatch _spriteBatch, float movementSpeed)
        {
            State.Draw(_spriteBatch, movementSpeed);
        }

    }
}