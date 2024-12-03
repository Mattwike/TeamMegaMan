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

            if (Handler.block is FloorBlock floorBlock && (floorBlock.IsLadder || floorBlock.IsPassable))
            {
                return;
            }

            CollisionDirection side = CollisionDetector.DetectCollisionType(Handler.enemy.getRectangle(), Handler.block.boundingBox);

            if (side == CollisionDirection.Left)
			{
                Handler.enemy.x = Handler.block.boundingBox.Left - Handler.enemy.getRectangle().Width;
                Handler.enemy.hitWall = true;

            }

			else if(side == CollisionDirection.Right)
			{
				Handler.enemy.x = Handler.block.boundingBox.Right;
                Handler.enemy.hitWall = true;
            }
		}
	}
}