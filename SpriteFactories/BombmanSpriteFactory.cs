using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class BombmanSpriteFactory
{
    private static BombmanSpriteFactory instance = new BombmanSpriteFactory();
    private Texture2D bossSheet;

    public static BombmanSpriteFactory Instance
    {
        get
        {
            return instance;
        }
    }

    private BombmanSpriteFactory()
    {
    }

    public void LoadAllTextures(ContentManager content)
    {
        bossSheet = content.Load<Texture2D>("bossSheet");
    }

    public IEnemySprite CreateIdleBombMan(Vector2 position)
    {
        return new bombManIdle(bossSheet, position);
    }

    public IEnemySprite CreateBombManThrowBomb(Vector2 position)
    {
        return new BombManThrowBomb(bossSheet, position);
    }

    public IEnemySprite CreateBombmanThrownBomb(Vector2 position)
    {
        return new BombmanThrownBomb(bossSheet, position);
    }

    public IEnemySprite CreateBomb(Vector2 position)
    {
        return new Bomb(bossSheet, position);
    }

    public IEnemySprite CreateBombmanJump(Vector2 position)
    {
        return new BombmanJump(bossSheet, position);
    }
}