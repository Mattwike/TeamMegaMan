using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class EnemySpriteFactory
{
    private static EnemySpriteFactory instance = new EnemySpriteFactory();
    private Texture2D enemySheet;
    private Texture2D bossSheet;

    public static EnemySpriteFactory Instance
    {
        get
        {
            return instance;
        }
    }

    private EnemySpriteFactory()
    {
    }

    public void LoadAllTextures(ContentManager content)
    {
        enemySheet = content.Load<Texture2D>("enemy");
        bossSheet = content.Load<Texture2D>("bossSheet");
    }

    public IEnemySprite CreateJumpingFlea()
    {
        return new jumpingFlea(enemySheet);
    }
  
    public IEnemySprite CreateScrewDriver()
    {
        return new screwDriver(enemySheet);
    }
        
    public IEnemySprite CreateBombManIdle()
    {
        return new bombManIdle(bossSheet);
    }

    public IEnemySprite CreateBombManThrowing()
    {
        return new BombManThrowBomb(bossSheet);
    }

    public IEnemySprite CreateMambu()
    {
        return new Mambu(enemySheet);
    }
}