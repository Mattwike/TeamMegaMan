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

        Dictionary<Type, Dictionary<CollisionDirection, IResponse>> collisionDict;

        public MegamanCollisonHandler(Megaman megaman)
        {
            this.megaman = megaman;
            collisionDict = new Dictionary<Type, Dictionary<CollisionDirection, IResponse>>();

            collisionDict.Add(typeof(IBlocks), new Dictionary<CollisionDirection, IResponse>());

            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Top, new MegamanTopCollision(this));
            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Right, new MegamanSideCollision(this));
            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Left, new MegamanSideCollision(this));
            collisionDict[typeof(IBlocks)].Add(CollisionDirection.Bottom, new MegamanBottomCollision(this));
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
                megaman.istouchingfloor = false;
            }
        }

    }
}