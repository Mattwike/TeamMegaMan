﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.GameObjects;

namespace Project1.Interfaces
{
	public interface IMegamanState
	{
		void ChangeDirection();

		void Update(GameTime gameTime);

		public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize);

		void Draw(SpriteBatch _spriteBatch, float movementSpeed);

	}
}