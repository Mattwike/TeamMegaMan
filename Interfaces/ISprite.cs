using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

public interface ISprite
{
    void Update(GameTime gameTime);

    void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically);

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize);

}