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
            // Assign a default position, for example, (0, 0)
            Vector2 defaultPosition = new Vector2(0, 0);

            sprites = new List<IEnemySprite>
            {
                EnemySpriteFactory.Instance.CreateScrewDriver(defaultPosition),
                EnemySpriteFactory.Instance.CreateBombMan(defaultPosition),
                EnemySpriteFactory.Instance.CreateJumpingFlea(defaultPosition),
                EnemySpriteFactory.Instance.CreateBombManThrowing(),
                EnemySpriteFactory.Instance.CreateOctopus(defaultPosition),
                EnemySpriteFactory.Instance.CreateBombManThrowing(),
                EnemySpriteFactory.Instance.CreateMambu(defaultPosition),
                EnemySpriteFactory.Instance.CreateGabyoall(defaultPosition),
                EnemySpriteFactory.Instance.CreateBombomb(defaultPosition),
                EnemySpriteFactory.Instance.CreateSniperJoe(defaultPosition)
            };
            currentSprite = 0;
        }

        public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
        {
            foreach (var sprite in sprites)
            {
                sprite.Initialize(graphics, movementSpeed, size);
            }
        }

        public void ChangeSprite(bool forward)
        {
            if (forward && currentSprite < sprites.Count - 1)
            {
                currentSprite++;
            }
            else if (!forward && currentSprite > 0)
            {
                currentSprite--;
            }
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