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
    public class BombCollisionHandler
    {
        //private CollisionDetector detector;
        //public IEnemyBomb bomb {get; private set; }
        //public IBlocks block { get; private set; }

        //Dictionary<Type, Dictionary<CollisionDirection, IResponse>> collisionDict;

        public BombCollisionHandler(IEnemyBomb Bomb)
        {
            //this.bomb = Bomb;
            //collisionDict.Add(typeof(IBlocks), new Dictionary<CollisionDirection, IResponse>());
            //collisionDict[typeof(IBlocks)].Add(CollisionDirection.Top, new BombTopCollision(this));
        }

        public void handleFloorCollision(IBlocks Block)
        {
            //block = Block;

            //CollisionDirection Direction = CollisionDetector.DetectCollisionType(bomb.getRectangle(), block.boundingBox);
           
            //if (Direction != CollisionDirection.None)
            //{
            //    collisionDict[typeof(IBlocks)][Direction].Execute();
            //}
        }

    }
}