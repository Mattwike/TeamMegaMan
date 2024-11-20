using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.GameObjects;

namespace Project1.Interfaces
{
    public interface IHealthBarState
    {

        void Update(GameTime gameTime, int ypose);

        public void Initialize(GraphicsDeviceManager _graphics, Megaman megaman);

        void Draw(SpriteBatch _spriteBatch);

    }
}