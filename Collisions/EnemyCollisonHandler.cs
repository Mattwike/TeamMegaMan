using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Project1;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.Commands;
using Project1.Collision;
using Project1.Enum;
using Project1.CollisionEffects;
using Project1.Sprites;

namespace Project1.Collisions
{
    public class EnemyCollisonHandler
    {
        public IEnemySprite enemy;
        private CollisionDetector detector;
        public IBlocks block {get; private set; }

        Dictionary<Type, Dictionary<CollisionDirection, IResponse>> collisionDict;

        public EnemyCollisonHandler(IEnemySprite enemy)
        {
            this.enemy = enemy;
            collisionDict = new Dictionary<Type, Dictionary<CollisionDirection, IResponse>>();

            collisionDict.Add(typeof(IBlocks), new Dictionary<CollisionDirection, IResponse>());

            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Top, new EnemyTopCollision(this));
            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Right, new EnemySideCollision(this));
            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Left, new EnemySideCollision(this));
            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Bottom, new EnemyBottomCollision(this));

        }

        public void handleBlockCollision(IBlocks Block)
        {
            block = Block;

            CollisionDirection Direction = CollisionDetector.DetectCollisionType(enemy.getRectangle(), block.boundingBox);

            if (collisionDict[typeof(IBlocks)].ContainsKey(Direction))
            {
                collisionDict[typeof(IBlocks)][Direction].Execute();
            }
            else
            {
                enemy.isTouchingFloor();

            }

        }
        public void handlePelletCollision(Pellet pellet, List<EnemyDrop> enemyDropList, Megaman megaman)
        {
            
            CollisionDirection collisionDirection = CollisionDetector.DetectCollisionType(pellet.getRectangle(), enemy.getRectangle());
            CollisionDirection Direction = collisionDirection;
            if (Direction != CollisionDirection.None)
            {
                pellet.removePellet(); 
                enemy.TakeDamage(enemyDropList, megaman);

            }
        }

    }
}