using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;
using System;
using System.Collections.Generic;

public interface IEnemyBomb
{
    public int y {  get; set; }

    public int x { get; set; }

    public bool istouchingfloor { get; set; }

    public float gravity { get; set; }

    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size);

    public void Update(GameTime gameTime, Camera camera, int megamanX);

    public Rectangle getRectangle();

    public bool IsOffScreen(Camera camera);

    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically);

    public void SetPosition(Vector2 position);
}
