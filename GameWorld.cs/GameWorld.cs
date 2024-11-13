//public class GameWorld
//{
//    public List<IBlock> Blocks { get; private set; }
//    public List<IEnemy> Enemies { get; private set; }
//    public Megaman Player { get; set; }

//    public GameWorld()
//    {
//        Blocks = new List<IBlock>();
//        Enemies = new List<IEnemy>();
//    }

//    public void Update(GameTime gameTime)
//    {
//        foreach (var block in Blocks)
//            block.Update();

//        foreach (var enemy in Enemies)
//            enemy.Update(gameTime);

//        Player.Update(gameTime);
//    }

//    public void Draw(SpriteBatch spriteBatch)
//    {
//        foreach (var block in Blocks)
//            block.Draw(spriteBatch);

//        foreach (var enemy in Enemies)
//            enemy.Draw(spriteBatch);

//        Player.Draw(spriteBatch, movementSpeed);
//    }
//}
