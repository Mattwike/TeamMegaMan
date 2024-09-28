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

    public ISprite CreateJumpingFlea()
    {
        return new jumpingFlea(enemySheet);
    }
  
    public ISprite CreateScrewDriver()
    {
        return new screwDriver(enemySheet);
    }
        
    public ISprite CreateBombManIdle()
    {
        return new bombManIdle(bossSheet);
    }

    public ISprite CreateBombManThrowing()
    {
        return new BombManThrowBomb(bossSheet);
    }

    public ISprite CreateBombomb()
    {
        return new Bombomb(enemySheet, 400, 200);
    }
}