using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;

namespace Project1.Commands
{
    class ClimbingShootingRightMegamanCommand : ICommand
    {
        Megaman megaman;

        public ClimbingShootingRightMegamanCommand(Megaman Megaman)
        {
            this.megaman = Megaman;
        }
        public void Execute(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval)
        {
            megaman.State.BeClimbingShootingRightMegamanState();
            megaman.State.Initialize(_graphics, movementSpeed, 40, interval);

        }
    }
}