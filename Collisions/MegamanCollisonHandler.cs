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
    public class MegamanCollisonHandler
    {
        public Megaman megaman;
        private CollisionDetector detector;
        public IBlocks block {get; private set; }
        public IEnemySprite enemy { get; private set; }
        public IEnemyProjectile projectile { get; private set; }
        public EnemyDrop enemyDrop { get; private set; }
        public Rectangle EnemyBox;
        public Rectangle ProjectileBox;
        public Rectangle EnemyDropBox;
        public 

        Dictionary<Type, Dictionary<CollisionDirection, IResponse>> collisionDict;

        public MegamanCollisonHandler(Megaman megaman)
        {
            this.megaman = megaman;
            collisionDict = new Dictionary<Type, Dictionary<CollisionDirection, IResponse>>();

            collisionDict.Add(typeof(IBlocks), new Dictionary<CollisionDirection, IResponse>());
            collisionDict.Add(typeof(IEnemySprite), new Dictionary<CollisionDirection, IResponse>());
            collisionDict.Add(typeof(IEnemyProjectile), new Dictionary<CollisionDirection, IResponse>());

            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Top, new MegamanTopCollision(this));
            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Right, new MegamanSideCollision(this));
            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Left, new MegamanSideCollision(this));
            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Bottom, new MegamanBottomCollision(this));
            collisionDict[typeof(IBlocks)].Add(CollisionDirection.ontop, new MegamanOntopCollision(this));

            collisionDict[typeof(IEnemySprite)].Add(CollisionDirection.Left, new MegamanEnemyCollision(this));
            collisionDict[typeof(IEnemySprite)].Add(CollisionDirection.Right, new MegamanEnemyCollision(this));

            collisionDict[typeof(IEnemyProjectile)].Add(CollisionDirection.Left, new MegamanProjectileCollision(this));
            collisionDict[typeof(IEnemyProjectile)].Add(CollisionDirection.Right, new MegamanProjectileCollision(this));
        }

        public void handleBlockCollision(IBlocks Block)
        {
            block = Block;

            CollisionDirection Direction = CollisionDetector.DetectCollisionType(megaman.MegamanBox, block.boundingBox);

            if (collisionDict[typeof(IBlocks)].ContainsKey(Direction))
            {
                collisionDict[typeof(IBlocks)][Direction].Execute();
            }
            else
            {
                //if (!(block is FloorBlock floorBlock && floorBlock.IsLadder))
                //{
                //    megaman.is_climable = false;
                //}

                megaman.istouchingfloor = false;
            }
        }

        public void handleEnemyCollision(IEnemySprite enemy)
        {
            this.enemy = enemy;
            
            EnemyBox = enemy.getRectangle();

            CollisionDirection Direction = CollisionDetector.DetectCollisionType(megaman.MegamanBox, EnemyBox);
            if (megaman.isVulnerable)
            {
                if (collisionDict[typeof(IEnemySprite)].ContainsKey(Direction))
                {
                    megaman.TakeDamage();
                    collisionDict[typeof(IEnemySprite)][Direction].Execute();
                }
            }
        }

        public void handleEnemyProjectiles(IEnemyProjectile projectile)
        {
            this.projectile = projectile;

            ProjectileBox = projectile.getRectangle();

            CollisionDirection Direction = CollisionDetector.DetectCollisionType(megaman.MegamanBox, ProjectileBox);
            if (megaman.isVulnerable)
            {
                if (collisionDict[typeof(IEnemyProjectile)].ContainsKey(Direction))
                {
                    megaman.TakeDamage();
                    collisionDict[typeof(IEnemyProjectile)][Direction].Execute();
                }
            }
        }

        public void handleEnemyDropCollision(EnemyDrop enemyDrop)
        {
            this.enemyDrop = enemyDrop;

            EnemyDropBox = enemyDrop.getRectangle();

            CollisionDirection Direction = CollisionDetector.DetectCollisionType(megaman.MegamanBox, EnemyDropBox);
            
            if (Direction != CollisionDirection.None)
            {
                enemyDrop.removeEnemyDrop();
                megaman.UpdateScore(500);
                
            }
            
        }

    }
}