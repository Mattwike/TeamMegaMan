using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
<<<<<<< HEAD:Interfaces/ISprite/ISprite.cs
using Project1.GameObjects;
=======
using System;
>>>>>>> 4ca635ec8bdddcec745316d0361920af3eb3494c:Interfaces/ISprite.cs

public interface ISprite
{
    void Update(GameTime gameTime);

    void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically);

<<<<<<< HEAD:Interfaces/ISprite/ISprite.cs
    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman megaman, int interval);
=======
    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize);

>>>>>>> 4ca635ec8bdddcec745316d0361920af3eb3494c:Interfaces/ISprite.cs
}