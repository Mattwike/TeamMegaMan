using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;

public interface ISprite
{

    public Rectangle getRectangle();

    void Update(GameTime gameTime);

    void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically, Color color);

    public void Initialize(GraphicsDeviceManager _graphics, Megaman megaman, int interval, bool isRight);
}