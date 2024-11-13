using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;

public interface IHealthBarSprite
{


    void Update(GameTime gameTime);

    void Draw(SpriteBatch _spriteBatch);

    public void Initialize(GraphicsDeviceManager _graphics, Megaman megaman);


}