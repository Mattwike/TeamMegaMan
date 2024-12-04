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

    public IEnemySprite CreateJumpingFlea(Vector2 position)
    {
        return new jumpingFlea(enemySheet, position);
    }

    public IEnemySprite CreateScrewDriver(Vector2 position)
    {
        return new screwDriver(enemySheet, position);
    }

    public IEnemySprite CreateBombomb(Vector2 position)
    {
        return new Bombomb(enemySheet, position.X, position.Y, position);
    }

    public IEnemySprite CreateOctopus(Vector2 position)
    {
        return new Octopus(enemySheet, position);
    }

    public IEnemySprite CreateGabyoall(Vector2 position)
    {
        return new Gabyoall(enemySheet, position);
    }

    public IEnemySprite CreateMambu(Vector2 position)
    {
        return new Mambu(enemySheet, position);
    }

    public IEnemySprite CreateSniperJoe(Vector2 position)
    {
        return new SniperJoe(enemySheet, position);
    }

    public IEnemySprite CreateRedBlaster(Vector2 position)
    {
        return new RedBlaster(enemySheet, position);
    }

    public IEnemySprite CreateKillerBomb(Vector2 position)
    {
        return new KillerBomb(enemySheet, position);
    }
}