public class EnemySpriteFactory
{
    private Texture2D enemySheet;
    private static EnemySpriteFactory instance = new EnemySpriteFactory();

    public static EnemySpriteFactory Instance
    {
        get
        {
            return instance;
        }
    }

    private EnemySpriteFactory() { }

    public void LoadEnemyTextures(ContentManager content)
    {
        enemySheet = content.Load<Texture2D>("enemy_sprites"); // Load the enemy sprite sheet
    }

    public ISprite CreateEnemy(string enemyType)
    {

        return new EnemySprite(enemySheet, new Rectangle(32, 32, 32, 32), 4);
        
    }
}
