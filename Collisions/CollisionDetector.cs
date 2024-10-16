using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1.Sprites
{
    public class CollisionDetector
    {

        public static int DetectCollisionType(Rectangle object1, Rectangle object2)
        {
            Rectangle Intersection = Rectangle.Intersect(object1, object2);

            if (!Intersection.IsEmpty)
            {
                if (Intersection.Height > Intersection.Width && object1.X < object2.X)
                {
                    //left
                    return 1;
                }

                if (Intersection.Height > Intersection.Width && object1.X > object2.X)
                {
                    //right 
                    return 3;
                }

                if (Intersection.Width > Intersection.Height && object1.Y < object2.Y)
                {
                    //top
                    return 2;
                }
                
                if (Intersection.Width > Intersection.Height && object1.Y > object2.Y)
                {
                    //bottom
                    return 4;
                }
            }
            return 0;
        }
    }
}