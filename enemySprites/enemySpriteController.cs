public class EnemyController
{
    private ISprite enemy;

    public EnemyController()
    {
        enemy = megaManSpriteFactory.Instance.CreateEnemy("enemy_sprite");
    }

    public void Update(GameTime gameTime)
    {
        enemy.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        enemy.Draw(spriteBatch, new Vector2(100, 100)); // Adjust position accordingly
    }
}