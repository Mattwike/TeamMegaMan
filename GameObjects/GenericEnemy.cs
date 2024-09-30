using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

using Project1.States.MegamanState;
using System;
using System.Collections.Generic;


namespace Project1.GameObjects
{
    public class GenericEnemy
    {

        private List<IEnemySprite> sprites;
        public int currentSprite;

        public GenericEnemy()
        {
            sprites = new List<IEnemySprite>
            {
                EnemySpriteFactory.Instance.CreateScrewDriver(),
                EnemySpriteFactory.Instance.CreateBombManIdle(),
                EnemySpriteFactory.Instance.CreateJumpingFlea()

            };
            currentSprite = 0;

        }
        public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int Size)
        {
            foreach (var sprite in sprites)
            {
                sprite.Initialize(graphics, movementSpeed, Size);
            }
        }

        public void changeSprite(int newIndex)
        {
            currentSprite = newIndex;
        }

        public void Update(GameTime gameTime)
        {
            sprites[currentSprite].Update(gameTime);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            sprites[currentSprite].Draw(_spriteBatch, false, false);
        }


    }
}
