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
	public class MegamanEnemyCollision : IResponse
	{

		MegamanCollisonHandler Handler;

		public MegamanEnemyCollision(MegamanCollisonHandler handler) 
		{
			Handler = handler;
		}

		public void Execute()
		{
            Rectangle enemyBox = Handler.EnemyBox;
            CollisionDirection side = CollisionDetector.DetectCollisionType(Handler.megaman.MegamanBox, enemyBox);

            if (side == CollisionDirection.Left)
            {
                Handler.megaman.x += 10;
                Handler.megaman.TakeDamage();
            }
            else if (side == CollisionDirection.Right)
            {
                Handler.megaman.x -= 10;
                Handler.megaman.TakeDamage();
            }
        }
	}
}