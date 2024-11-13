using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.States.MegamanState;
using System.Collections.Generic;

namespace Project1.GameObjects
{
    public class EnemyDrop
    {

        public IEnemyDropState State;

        public float x { get; set; }
        public float y { get; set; }


        public EnemyDrop()
        {
            State = new EnemyDropState(this);
        }


        public void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }

        public void Initialize(GraphicsDeviceManager _graphics, int enemyX, int enemyY)
        {
            State.Initialize(_graphics, enemyX, enemyY);
        }

        public void Draw(SpriteBatch _spriteBatch, float movementSpeed)
        {
            State.Draw(_spriteBatch);
        }

        public Rectangle getRectangle()
        {
            return State.getRectangle();
        }
        public void removeEnemyDrop()
        {
            State.removeEnemyDrop();
        }

    }
}