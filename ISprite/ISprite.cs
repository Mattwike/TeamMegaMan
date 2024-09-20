using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public interface ISprite
{
    void Update(GameTime gameTime);

    void Draw(Texture2D spriteTexture, SpriteBatch _spriteBatch, float movementSpeed);

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed);
}