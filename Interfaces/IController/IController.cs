using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public interface IController
{
    void Update(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, GameTime gameTime);
}
