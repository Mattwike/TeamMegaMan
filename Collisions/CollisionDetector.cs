using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.Enum;

namespace Project1.Collision
{
    public class CollisionDetector
    {

        public static CollisionDirection DetectCollisionType(Rectangle object1, Rectangle object2)
        {
            Rectangle Intersection = Rectangle.Intersect(object1, object2);
            if (!Intersection.IsEmpty)
            {
                if (Intersection.Height > Intersection.Width && object1.X < object2.X)
                {

                    return CollisionDirection.Left;
                }
                if (Intersection.Height > Intersection.Width && object1.X > object2.X)
                {

                    return CollisionDirection.Right;
                }
                if (Intersection.Width > Intersection.Height && object1.Y < object2.Y)
                {

                    return CollisionDirection.Top;
                }
                if (Intersection.Width > Intersection.Height && object1.Y > object2.Y)
                {

                    return CollisionDirection.Bottom;
                }

                if (Intersection.Width == object2.Width)
                {
                    return CollisionDirection.ontop;
                }
            }
            return CollisionDirection.None;
        }
    }
}