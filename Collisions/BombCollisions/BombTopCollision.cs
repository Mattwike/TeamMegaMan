using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Project1;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;
using Project1.Commands;
using Project1.Collision;
using Project1.Enum;
using Project1.Collisions;

namespace Project1.CollisionEffects
{
    public class BombTopCollision : IResponse
    {

        BombCollisionHandler Handler;

        public BombTopCollision(BombCollisionHandler handler)
        {
            Handler = handler;
        }

        public void Execute()
        {
            //Handler.bomb.istouchingfloor = true;
            //Handler.bomb.y -= 10000;

        }
	}
}