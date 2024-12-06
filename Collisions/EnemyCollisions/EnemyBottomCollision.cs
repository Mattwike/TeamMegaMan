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
	public class EnemyBottomCollision : IResponse
	{

        EnemyCollisonHandler Handler;

		public EnemyBottomCollision(EnemyCollisonHandler handler) 
		{
			Handler = handler;
		}

		public void Execute()
		{

            if (Handler.block is FloorBlock floorBlock && (floorBlock.IsLadder || floorBlock.IsPassable))
            {
                return;
            }

            if (Handler.enemy.IgnoresFloors)
            {
                return;
            }

            Handler.enemy.isFalling = true;
			Handler.enemy.y = Handler.block.boundingBox.Y + Handler.block.boundingBox.Height + 2;
        }
	}
}