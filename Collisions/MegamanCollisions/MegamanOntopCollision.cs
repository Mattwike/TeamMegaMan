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
    public class MegamanOntopCollision : IResponse
    {
        private MegamanCollisonHandler Handler;

        public MegamanOntopCollision(MegamanCollisonHandler handler)
        {
            Handler = handler;
        }

        public void Execute()
        {
            if (Handler.block is FloorBlock floorBlock && floorBlock.IsLadder)
            {
                Handler.megaman.is_climable = true;
                Handler.megaman.is_falling = false;
            }
            else
            {
                Handler.megaman.is_climable = false;
                Handler.megaman.is_climbing = false;
                Handler.megaman.is_falling = true;
            }
        }

    }
}
