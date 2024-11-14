using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;

namespace Project1.States.MegamanState
{
    public class EnemyDropState : IEnemyDropState
    {
        private Megaman megaman;
        private EnemyDrop enemyDrop;
        public IEnemyDropSprite Sprite;

        public EnemyDropState(EnemyDrop enemyDrop)
        {
            Sprite = EnemyDropSpriteFactory.Instance.CreateEnemyDrop();
        }

        public void Update(GameTime gameTime)
        {

            Sprite.Update(gameTime);
        }

        public void Initialize(GraphicsDeviceManager _graphics, int enemyX, int enemyY)
        {
            Sprite.Initialize(_graphics, enemyX, enemyY);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            Sprite.Draw(_spriteBatch);
        }

        public Rectangle getRectangle()
        {
            return Sprite.getRectangle();
        }

        public void removeEnemyDrop()
        {
            Sprite.removeEnemyDrop();
        }
    }
}