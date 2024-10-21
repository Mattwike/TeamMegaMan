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

namespace Project1.Collisions
{
    public class MegamanCollisonHandler
    {
        public Megaman megaman;
        private CollisionDetector detector;

        Dictionary<Type, Dictionary<CollisionDirection, ICommand>> commandDict;

        public MegamanCollisonHandler(Megaman megaman)
        {
            megaman = megaman;
            collisionDict = new Dictionary<Type, Dictionary<CollisionDirection, ICommand>>();

            collisionDict.Add(typeof(IBlock), new Dictionary<CollisionDirection, ICommand>());
        }

    }
}