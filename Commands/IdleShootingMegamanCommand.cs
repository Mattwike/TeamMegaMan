using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;

namespace Project1.Commands
{
    class IdleShootingMegamanCommand : ICommand
    {
        Megaman megaman;

        public IdleShootingMegamanCommand(Megaman Megaman)
        {
            this.megaman = Megaman;
        }
        public void Execute(GraphicsDeviceManager _graphics, int interval)
        {
            megaman.State.BeIdleShootingMegamanState();
            megaman.State.Initialize(_graphics, interval);
        }
    }
}