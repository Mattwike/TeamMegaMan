using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;

public interface ISprite
{
    void Update(GameTime gameTime);

    void Draw(SpriteBatch _spriteBatch, float movementSpeed, bool flipHorizontally, bool flipVertically);

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman megaman, int interval, bool isRight);
}