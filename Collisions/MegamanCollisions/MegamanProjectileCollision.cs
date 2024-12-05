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
	public class MegamanProjectileCollision : IResponse
	{

		MegamanCollisonHandler Handler;

		public MegamanProjectileCollision(MegamanCollisonHandler handler) 
		{
			Handler = handler;
		}

		public void Execute()
		{
            Rectangle ProjectileBox = Handler.ProjectileBox;
            CollisionDirection side = CollisionDetector.DetectCollisionType(Handler.megaman.MegamanBox, ProjectileBox);

            if (side == CollisionDirection.Left)
            {
                Handler.megaman.x -= 1;
                Handler.megaman.TakeDamage();
            }
            else if (side == CollisionDirection.Right)
            {
                Handler.megaman.x += 1;
                Handler.megaman.TakeDamage();
            }
        }
	}
}