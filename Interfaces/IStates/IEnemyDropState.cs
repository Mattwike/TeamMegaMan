using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.GameObjects;

namespace Project1.Interfaces
{
    public interface IEnemyDropState
    {

        void Update(GameTime gameTime);

        public void Initialize(GraphicsDeviceManager _graphics, int enemyX, int enemyY);

        void Draw(SpriteBatch _spriteBatch);

        public Rectangle getRectangle();

        public void removeEnemyDrop();


    }
}