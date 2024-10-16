using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.States.MegamanState;
using Microsoft.Xna.Framework.Input;
using System.Linq;



namespace Project1.GameObjects
{

    public class Floor : IBlocks
    {
        public Rectangle FloorBox = new Rectangle(0, 200, 100, 10);
    }

}