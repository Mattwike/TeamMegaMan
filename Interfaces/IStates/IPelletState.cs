using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.GameObjects;

namespace Project1.Interfaces
{
    public interface IPelletState
    {

        void Update(GameTime gameTime);

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman megaman, int interval, bool isRight);

        void Draw(SpriteBatch _spriteBatch, float movementSpeed);

        public Rectangle getRectangle();

        public void removePellet();


    }
}