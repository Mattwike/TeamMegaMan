using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;

namespace Project1.States.MegamanState
{
	public class IdleMegamanState : IMegamanState
	{
		private Megaman megaman;
		public ISprite Sprite;

		public IdleMegamanState(Megaman megaman)
		{
			this.megaman = megaman;
            megaman.SetDirection(false);
            Sprite = megaManSpriteFactory.Instance.CreateIdleMegaman(); 
		}

		public void BeRunningLeftState()
		{
			megaman.State = new RunningLeftMegamanState(megaman); 
		}

		public void ChangeDirection()
		{
			// Implementation for changing direction
		}

		public void Update(GameTime gameTime)
		{
			// Implementation for update logic
			Sprite.Update(gameTime);
		}

		public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
		{
			Sprite.Initialize(_graphics, movementSpeed, megamanSize);
		}

		public void Draw(SpriteBatch _spriteBatch, float movementSpeed)
		{
			// Implementation for drawing the sprite
			Sprite.Draw(_spriteBatch, movementSpeed, megaman.isfacingLeft, false);
		}
	}
}
