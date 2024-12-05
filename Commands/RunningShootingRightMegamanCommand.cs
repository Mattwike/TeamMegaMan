using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;

namespace Project1.Commands
{
    class RunningShootingRightMegamanCommand : ICommand
    {
        Megaman megaman;

        public RunningShootingRightMegamanCommand(Megaman Megaman)
        {
            this.megaman = Megaman;
        }
        public void Execute(GraphicsDeviceManager _graphics, int interval)
        {
            megaman.State.BeRunningShootingRightMegamanState();
            megaman.State.Initialize(_graphics, interval);

        }
    }
}