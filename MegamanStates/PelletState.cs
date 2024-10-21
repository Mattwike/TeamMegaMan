using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;

namespace Project1.States.MegamanState
{
	public class PelletState : IPelletState
	{
		private Megaman megaman;
		private Pellet pellet;
		public ISprite Sprite;

        public PelletState(Pellet pellet)
        {
            Sprite = pelletSpriteFactory.Instance.CreatePellet();
        }

        public void Update(GameTime gameTime)
		{

			Sprite.Update(gameTime);
		}

		public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman megaman, int interval, bool isRight)
		{
			Sprite.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval, isRight);
		}

		public void Draw(SpriteBatch _spriteBatch, float movementSpeed)
		{

			Sprite.Draw(_spriteBatch, movementSpeed, false, false);
		}
	}
}
