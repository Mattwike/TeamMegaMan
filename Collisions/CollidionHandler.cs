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

		public static void HandleMegamanCollisions(Megaman megaman, List<IBlocks> blocklist, List<IEnemySprite> enemies, List<EnemyDrop> enemyDropList, List<IEnemyProjectile> projectiles)
		{
			MegamanCollisonHandler handler = new MegamanCollisonHandler(megaman);

			foreach (IBlocks block in blocklist)
			{
				handler.handleBlockCollision(block);
			}
            
            foreach (IEnemySprite enemy in enemies)
            {
                handler.handleEnemyCollision(enemy);
            }
            foreach (EnemyDrop enemyDrop in enemyDropList)
            {
                handler.handleEnemyDropCollision(enemyDrop);
            }

            foreach(IEnemyProjectile projectile in projectiles)
            {
                handler.handleEnemyProjectiles(projectile);
            }
        }

        public static void HandleEnemyCollisions(IEnemySprite enemy, List<IBlocks> blocklist, List<Pellet> pellets, List<EnemyDrop> enemyDropList, Megaman megaman)
        {
            EnemyCollisonHandler handler = new EnemyCollisonHandler(enemy);

            foreach (IBlocks block in blocklist)
            {
                handler.handleBlockCollision(block);
            }
            foreach (Pellet pellet in pellets)
            {
                handler.handlePelletCollision(pellet, enemyDropList, megaman);
            }
        }
        public static void HandleMegamanPelletCollisions(Pellet pellet, IEnemySprite enemy, List<EnemyDrop> enemyDropList)
        {
            PelletCollisionHandler handler = new PelletCollisionHandler(pellet);

            handler.handleEnemyCollision(enemy);
            
        }
    }
}