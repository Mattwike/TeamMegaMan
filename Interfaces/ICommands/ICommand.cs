using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Project1.Interfaces
{
    interface ICommand
    {
        void Execute(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval);
    }
}