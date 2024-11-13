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

		public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval);

		void Draw(SpriteBatch _spriteBatch, float movementSpeed, Color currentColor);

		public void BeClimbingMegamanState();

        public void BeIdleMegamanState();

		public void BeRunningRightMegamanState();

		public void BeRunningLeftMegamanState();

		public void BeDamagedMegamanState();

		public void BeClimbingShootingLeftMegamanState();

		public void BeClimbingShootingRightMegamanState();

		public void BeClimbingReachedTopMegaman();

		public void BeRunningShootingLeftMegamanState();

        public void BeRunningShootingRightMegamanState();

		public void BeFallingMegamanState();

        public void BeFallingShootingMegamanState();

		public Rectangle getRectangle();

    }
}