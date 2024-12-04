using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.States.MegamanState;

namespace Project1.GameObjects
{
    public class Bombman
    {
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        private BombmanStateMachine stateMachine;
        public IEnemySprite currentSprite;
        public bool flip;

        public Bombman(Texture2D texture, Vector2 startPosition)
        {
            Position = startPosition;
            Speed = 2.0f;
            stateMachine = new BombmanStateMachine(this);
            currentSprite = BombmanSpriteFactory.Instance.CreateIdleBombMan(startPosition);
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
        {
            currentSprite.Initialize(_graphics, movementSpeed, megamanSize);
        }

        public void Update()
        {
            stateMachine.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
          currentSprite.Draw(spriteBatch, false, false);
            
        }
    }
}


