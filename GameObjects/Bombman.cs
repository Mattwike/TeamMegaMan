using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

using Project1.States.MegamanState;
using System;
using System.Collections.Generic;


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
        }

        public void Update()
        {
            stateMachine.Update();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //currentSprite.Draw(spriteBatch, false, false);
        }

    }
}