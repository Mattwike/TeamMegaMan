using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.Interfaces.IStates;
using Project1.States.MegamanState;
using System;
using System.Collections.Generic;


namespace Project1.GameObjects
{
    public class GenericEnemy
    {

        public IEnemyState state;

        public GenericEnemy()
        {
            state = new JumpingFleaState(this);

        }
        public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int Size)
        {
            state.Initialize(graphics, movementSpeed, Size);
        }

        public void Update(GameTime gameTime)
        {
            state.Update(gameTime);        
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            state.Draw(_spriteBatch);
        }


    }
}
