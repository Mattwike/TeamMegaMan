using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

public class EnemySpriteFactory
{
    private static EnemySpriteFactory instance = new EnemySpriteFactory();
    private Texture2D enemySheet;

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
    }

    public ISprite CreateJumpingFlea()
    {
        return new jumpingFlea(enemySheet);
    }
}