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
        public void Execute(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval)
        {
            enemy.ChangeSprite(true);

        }
    }
}