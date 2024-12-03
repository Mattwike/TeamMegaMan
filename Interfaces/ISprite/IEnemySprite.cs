using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;
using System;
using System.Collections.Generic;

public interface IEnemySprite
{
    int y { get; set; }
    int x { get; set; }
    bool isFalling {  get; set; }
    bool istouchingfloor { get; set; }
    float gravity { get; set; }
    public bool hitWall { get; set; }
    void Update(GameTime gameTime);

    void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically);

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize);

    public Rectangle getRectangle();

    public void TakeDamage(List<EnemyDrop> enemyDropList);

    public int GetHealth();

    public void isTouchingFloor();
}
