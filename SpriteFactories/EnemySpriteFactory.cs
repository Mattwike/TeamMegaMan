using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class EnemySpriteFactory
{
    private static EnemySpriteFactory instance = new EnemySpriteFactory();
    private Texture2D enemySheet;
    private Texture2D bossSheet;

    private Texture2D itemSheet; //Using EnemySpriteFactory for item for now

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

        //Cuttently using the EnemySprite to generate items, change it if you can do that
        itemSheet = content.Load<Texture2D>("items");
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

    public IEnemySprite CreateBombomb()
    {
        return new Bombomb(enemySheet, 400, 200);
    }

    public IEnemySprite CreateOctopus()
    {
        return new Octopus(enemySheet);
    }

    public IEnemySprite CreateGabyoall()
    {
        return new Gabyoall(enemySheet);
    }

    public IEnemySprite CreateMambu()
    {
        return new Mambu(enemySheet);
    }

    //Below are all item sprites
    public IEnemySprite CreateExtraLife()
    {
        return new ExtraLife(itemSheet);
    }

    public IEnemySprite CreateBigYellowDiamond()
    {
        return new BigYellowDiamond(itemSheet);
    }
    
    public IEnemySprite CreateSmallYellowDiamond()
    {
        return new SmallYellowDiamond(itemSheet);
    }

    public IEnemySprite CreateBigBlueRectangle()
    {
        return new BigBlueRectangle(itemSheet);
    }

    public IEnemySprite CreateSmallBlueRectangle()
    {
        return new SmallBlueRectangle(itemSheet);
    }

    public IEnemySprite CreateYellowBall()
    {
        return new YellowBall(itemSheet);
    }
    public IEnemySprite CreateSmallBall()
    {
        return new SmallBall(itemSheet);
    }
}