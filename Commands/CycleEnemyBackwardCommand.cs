using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;
using Project1.Interfaces.IStates;

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
            if (enemy.state.getID() == 2)
            {
                enemy.state = new BombManIdleState(enemy);
                enemy.Initialize(_graphics, movementSpeed, megamanSize);
            }
            else if (enemy.state.getID() == 1)
            {
                enemy.state = new JumpingFleaState(enemy);
                enemy.Initialize(_graphics, movementSpeed, megamanSize);
            }
        }
    }
}
