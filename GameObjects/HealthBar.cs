using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.States.MegamanState;
using System.Collections.Generic;

namespace Project1.GameObjects
{
    public class HealthBar
    {

        public IHealthBarState State;

        public float x { get; set; }
        public float y { get; set; }


        public HealthBar()
        {
            State = new HealthBarState(this);
        }


        public void Update(GameTime gameTime)
        {
            State.Update(gameTime);
        }

        public void Initialize(GraphicsDeviceManager _graphics, Megaman megaman)
        {
            State.Initialize(_graphics, megaman);
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            State.Draw(_spriteBatch);
        }

    }
}