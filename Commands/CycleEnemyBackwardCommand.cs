using Microsoft.Xna.Framework;
using Project1.Interfaces;
using Project1.GameObjects;

namespace Project1.Commands
{
    class CycleEnemyBackwardCommand : ICommand
    {
        GenericEnemy enemy;

        public CycleEnemyBackwardCommand(GenericEnemy enemy)
        {
            this.enemy = enemy;
        }
        public void Execute(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
        {
            enemy.changeSprite(false);

        }
    }
}
