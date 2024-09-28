using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;

namespace Project1.Commands
{
    class CycleEnemyForwardCommand : ICommand
    {
        GenericEnemy enemy;

        public CycleEnemyForwardCommand(GenericEnemy enemy)
        {
            this.enemy = enemy;
        }
        public void Execute(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
        {
            if (enemy.state.getID() == 0)
            {
                enemy.state = new BombManIdleState(enemy);
                enemy.Initialize(_graphics, movementSpeed, megamanSize);
            }
            else if (enemy.state.getID() == 1)
            {
                enemy.state = new ScrewDriverState(enemy);
                enemy.Initialize(_graphics, movementSpeed, megamanSize);
            }
        }
    }
}
