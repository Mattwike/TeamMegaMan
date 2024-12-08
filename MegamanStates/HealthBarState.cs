using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.Sprites;

namespace Project1.States.MegamanState
{
    public class HealthBarState : IHealthBarState
    {
        private Megaman megaman;
        private HealthBar healthBar;
        public IHealthBarSprite Sprite;

        public HealthBarState(HealthBar healthBar)
        {
            Sprite = healthBarSpriteFactory.Instance.CreateHealthBar();

        }

        public void Update(GameTime gameTime, int ypose)
        {

            Sprite.Update(gameTime, ypose);
        }

        public void Initialize(GraphicsDeviceManager _graphics, Megaman megaman)
        {
            Sprite.Initialize(_graphics, megaman);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            Sprite.Draw(_spriteBatch);
        }

    }
}