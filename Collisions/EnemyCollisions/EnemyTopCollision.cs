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
	public class EnemyTopCollision : IResponse
	{

        EnemyCollisonHandler Handler;

		public EnemyTopCollision(EnemyCollisonHandler handler) 
		{
			Handler = handler;
		}

		public void Execute()
		{
			Handler.sniperjoe.istouchingfloor = true;
			Handler.sniperjoe.y = Handler.block.boundingBox.Y - Handler.sniperjoe.SniperJoeBox.Height;
			Handler.sniperjoe.isFalling = false;
            Handler.sniperjoe.gravity = 0;
		}
	}
}