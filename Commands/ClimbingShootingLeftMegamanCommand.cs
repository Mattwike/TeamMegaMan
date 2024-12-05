using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;

namespace Project1.Commands
{
    class ClimbingShootingLeftMegamanCommand : ICommand
    {
        Megaman megaman;

        public ClimbingShootingLeftMegamanCommand(Megaman Megaman)
        {
            this.megaman = Megaman;
        }
        public void Execute(GraphicsDeviceManager _graphics, int interval)
        {
            megaman.State.BeClimbingShootingLeftMegamanState();
            megaman.State.Initialize(_graphics, interval);

        }
    }
}