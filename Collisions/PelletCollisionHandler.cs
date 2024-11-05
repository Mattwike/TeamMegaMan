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
    public class PelletCollisionHandler
    {
        public Pellet pellet;
        private CollisionDetector detector;
        public IEnemySprite Enemy {get; private set; }

        Dictionary<Type, Dictionary<CollisionDirection, IResponse>> collisionDict;

        public PelletCollisionHandler(Pellet pellet)
        {
            this.pellet = pellet;
            //collisionDict = new Dictionary<Type, Dictionary<CollisionDirection, IResponse>>();

            //collisionDict.Add(typeof(IBlocks), new Dictionary<CollisionDirection, IResponse>());

            //collisionDict[typeof(IBlocks)].Add(CollisionDirection.Top, new MegamanTopCollision(this));
            //collisionDict[typeof(IBlocks)].Add(CollisionDirection.Right, new MegamanSideCollision(this));
            //collisionDict[typeof(IBlocks)].Add(CollisionDirection.Left, new MegamanSideCollision(this));
            //collisionDict[typeof(IBlocks)].Add(CollisionDirection.Bottom, new MegamanBottomCollision(this));
        }

        public void handleEnemyCollision(IEnemySprite enemy)
        {
            enemy = Enemy;

            CollisionDirection collisionDirection = CollisionDetector.DetectCollisionType(pellet.getRectangle(), enemy.getRectangle());
            CollisionDirection Direction = collisionDirection;
            if (Direction != CollisionDirection.None)
            {
                pellet.x = -1000;
                while (true)
                {

                }
                
            }
        }

    }
}