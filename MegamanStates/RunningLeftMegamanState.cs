using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;

namespace Project1.States.MegamanState
{
    public class RunningLeftMegamanState : IMegamanState
    {
        private Megaman megaman;
        public ISprite Sprite;

        public RunningLeftMegamanState(Megaman Megaman)
        {
            megaman = Megaman;
            megaman.SetDirection(true);
            Sprite = megaManSpriteFactory.Instance.CreateRunningMegaman();
        }

        public void BeIdleMegamanState()
        {
            megaman.State = new IdleMegamanState(megaman);
        }

        public void ChangeDirection()
        {
            megaman.State = new RunningRightMegamanState(megaman);
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
        {
            Sprite.Initialize(_graphics, movementSpeed, megamanSize);
        }

        public void Draw(SpriteBatch _spriteBatch, float movementSpeed)
        {
            // Implement draw logic here
            Sprite.Draw(_spriteBatch, movementSpeed, megaman.isfacingLeft, false);
        }
    }
}
