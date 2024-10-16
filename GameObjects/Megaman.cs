using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.States.MegamanState;
using Microsoft.Xna.Framework.Input;
using System.Linq;



namespace Project1.GameObjects
{
    public class Megaman
    {
        public bool is_running = false;
        public bool is_shooting = false;
        public bool is_damaged = false;
        public bool is_jumping = false;
        public bool is_falling = false;
        public bool is_climbing = false;
        public bool reached_top = false;
        //private bool is_damaged = false;
        public float initialY;
        public float gravity = 4.5f;
        public int MegamanSize;
        public Rectangle MegamanBox;
        //test
        private Floor floor;

        public IMegamanState State;

        public float x {  get; set; }
        public float y { get; set; }

        public bool isfacingLeft { get; set; }

        public Megaman()
        {
            State = new IdleMegamanState(this);
        }

        public void SetDirection(bool isFacingLeft)
        {
            isfacingLeft = isFacingLeft;
        }

        public void ChangeDirection()
        {
            State.ChangeDirection();
        }

        public void Update(GameTime gameTime)
        {
            MegamanBox = new Rectangle((int)x, (int)y, MegamanSize, MegamanSize);
            State.Update(gameTime);
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval)
        {
            floor = new Floor();
            MegamanSize = megamanSize;
            State.Initialize(_graphics, movementSpeed, megamanSize, interval);
        }

        public void Draw(SpriteBatch _spriteBatch, float movementSpeed)
        {
            State.Draw(_spriteBatch, movementSpeed);
        }
        
        public void Jump(Keys[] pressedKeys)
        {

            if (!is_jumping && !is_falling && pressedKeys.Contains(Keys.Space))
            {
                is_jumping = true;
                initialY = y;
                gravity = 4.5f;

            }

            if (is_jumping)
            {
                if (gravity > 0)
                {
                    y -= gravity;
                    gravity -= .25f;
                }
                else
                {
                    is_jumping = false;
                    is_falling = true;
                }
            }
            else if (is_falling)
            {
                if (y < floor.FloorBox.Location.Y)
                {
                    y += gravity;
                    gravity += .25f;
                }
                else
                {
                    is_falling = false;
                    gravity = 4.5f;
                }
            }
        }
    }
}