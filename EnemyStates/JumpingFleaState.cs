using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using Project1.Interfaces.IStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JumpingFleaState : IEnemyState
{
    private GenericEnemy enemy;
    private ISprite sprite;

    public JumpingFleaState(GenericEnemy enemy)
    {
        this.enemy = enemy;
        sprite = EnemySpriteFactory.Instance.CreateJumpingFlea();

    }

    public void beBombManIdle()
    {
        enemy.state = new BombManIdleState(enemy);
    }

    public void beJumpingFlea()
    {
        enemy.state = new JumpingFleaState(enemy);
    }

    public void beScrewDriver()
    {
        enemy.state = new ScrewDriverState(enemy);
    
    }

    public void Draw(SpriteBatch _spriteBatch)
    {
        sprite.Draw(_spriteBatch, false, false);
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        sprite.Initialize(_graphics, movementSpeed, megamanSize);
    }

    public void Update(GameTime gameTime)
    {
        sprite.Update(gameTime);
    }

    public int getID()
    {
        return 0;
    }
}