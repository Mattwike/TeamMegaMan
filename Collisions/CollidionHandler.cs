using System.Collections.Generic;
using System;
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
using Project1.CollisionEffects;


namespace Project1.Collisions
{
	public static class CollidionHandler
	{
		//public static void CollidionHandler(Megaman megaman)
		//{

		//}

		public static void HandleMegamanCollisions(Megaman megaman, List<IBlocks> blocklist)
		{
			MegamanCollisonHandler handler = new MegamanCollisonHandler(megaman);

			foreach (IBlocks block in blocklist)
			{
				handler.handleBlockCollision(block);
			}
		}
	}
}