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
	public class EnemySideCollision : IResponse
	{

        EnemyCollisonHandler Handler;

		public EnemySideCollision(EnemyCollisonHandler handler) 
		{
			Handler = handler;
		}

		public void Execute()
		{
            CollisionDirection side = CollisionDetector.DetectCollisionType(Handler.sniperjoe.SniperJoeBox, Handler.block.boundingBox);

            if (side == CollisionDirection.Left)
			{
                Handler.sniperjoe.x = Handler.block.boundingBox.Left - Handler.sniperjoe.SniperJoeBox.Width;

            }

			else if(side == CollisionDirection.Right)
			{
				Handler.sniperjoe.x = Handler.block.boundingBox.Right;
            }
		}
	}
}