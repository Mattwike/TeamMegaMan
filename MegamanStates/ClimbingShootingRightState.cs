using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;

namespace Project1.States.MegamanState
{
    public class ClimbingShootingRightMegamanState : IMegamanState
    {
        private Megaman megaman;
        public ISprite Sprite;

        public ClimbingShootingRightMegamanState(Megaman Megaman)
        {
            megaman = Megaman;
            megaman.SetDirection(false);
            Sprite = megaManSpriteFactory.Instance.CreateClimbingShootingMegaman();
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

        public void BeFallingMegamanState()
        {
            megaman.State = new FallingMegamanState(megaman);
        }

        public void BeFallingShootingMegamanState()
        {
            megaman.State = new FallingShootingMegamanState(megaman);
        }

        public void BeIdleShootingMegamanState()
        {
            megaman.State = new IdleShootingMegamanState(megaman);
        }

        public void ChangeDirection()
        {
            megaman.State = new ClimbingShootingLeftMegamanState(megaman);
        }

        public void Update(GameTime gameTime)
        {
            Sprite.Update(gameTime);
        }

        public void Initialize(GraphicsDeviceManager _graphics, int interval)
        {
            Sprite.Initialize(_graphics, megaman, interval, false);
        }

        public void Draw(SpriteBatch _spriteBatch, Color currentColor)
        {
            // Implement draw logic here
            Sprite.Draw(_spriteBatch, megaman.isfacingLeft, false, currentColor);
        }

        public Rectangle getRectangle()
        {
            return Sprite.getRectangle();
        }
    }
}
