using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;

namespace Project1.States.MegamanState
{
    public class RunningShootingLeftMegamanState : IMegamanState
    {
        private Megaman megaman;
        public ISprite Sprite;

        public RunningShootingLeftMegamanState(Megaman Megaman)
        {
            megaman = Megaman;
            megaman.SetDirection(true);
            Sprite = megaManSpriteFactory.Instance.CreateRunningShootingMegaman();
        }

        public void BeClimbingMegamanState()
        {
            megaman.State = new ClimbingMegamanState(megaman);
        }

        public void BeIdleMegamanState()
        {
            megaman.State = new IdleMegamanState(megaman);
        }

        public void BeRunningRightMegamanState()
        {
            megaman.State = new RunningRightMegamanState(megaman);
        }

        public void BeRunningLeftMegamanState()
        {
            megaman.State = new RunningLeftMegamanState(megaman);
        }

        public void BeDamagedMegamanState()
        {
            megaman.State = new DamagedMegamanState(megaman);
        }

        public void BeClimbingShootingLeftMegamanState()
        {
            megaman.State = new ClimbingShootingLeftMegamanState(megaman);
        }

        public void BeClimbingShootingRightMegamanState()
        {
            megaman.State = new ClimbingShootingRightMegamanState(megaman);
        }

        public void BeClimbingReachedTopMegaman()
        {
            megaman.State = new ClimbingReachedTopMegamanState(megaman);
        }

        public void BeRunningShootingRightMegamanState()
        {
            megaman.State = new RunningShootingRightMegamanState(megaman);
        }

        public void BeRunningShootingLeftMegamanState()
        {
            megaman.State = new RunningShootingLeftMegamanState(megaman);
        }

        public void ChangeDirection()
        {
            megaman.State = new RunningShootingRightMegamanState(megaman);
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval)
        {
            Sprite.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval);
        }

        public void Draw(SpriteBatch _spriteBatch, float movementSpeed)
        {
            // Implement draw logic here
            Sprite.Draw(_spriteBatch, movementSpeed, megaman.isfacingLeft, false);
        }
    }
}