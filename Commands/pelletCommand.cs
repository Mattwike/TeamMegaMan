using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;
using Project1.Sprites;

namespace Project1.Commands
{
    class PelletCommand: ICommand
    {
        Megaman megaman;
        Pellet pellet;

        public PelletCommand(Megaman Megaman)
        {
            this.megaman = Megaman;
        }
        public void Execute(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval)
        {
            megaman.State.BeClimbingMegamanState();
            megaman.State.Initialize(_graphics, movementSpeed, 40, interval);

        }
    }
}