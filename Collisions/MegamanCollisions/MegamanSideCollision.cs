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
    public class MegamanSideCollision : IResponse
    {
        private MegamanCollisonHandler Handler;

        public MegamanSideCollision(MegamanCollisonHandler handler)
        {
            Handler = handler;
        }

        public void Execute()
        {
            Rectangle adjustedMegamanBox = new Rectangle(
                Handler.megaman.MegamanBox.X, 
                Handler.megaman.MegamanBox.Y, 
                Handler.megaman.MegamanBox.Width, 
                Handler.megaman.MegamanBox.Height - 1 
            );

            CollisionDirection side = CollisionDetector.DetectCollisionType(adjustedMegamanBox, Handler.block.boundingBox);

            if (side == CollisionDirection.Left)
            {
                Handler.megaman.x = Handler.block.boundingBox.Left - Handler.megaman.MegamanBox.Width;
            }
            else if (side == CollisionDirection.Right)
            {
                Handler.megaman.x = Handler.block.boundingBox.Right;
            }
        }
    }

}