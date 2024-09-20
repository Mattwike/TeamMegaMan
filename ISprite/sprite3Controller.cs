using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class sprite3Controller : ISprite
{
    int currentframe;
    int totalframe;
    int delaycounter;
    int delaymax;
    float x;
    float y;
    float i;
    float screenMax;
    float screenMin;

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed)
    {
        currentframe = 0;
        totalframe = 3;
        delaycounter = 0;
        delaymax = 10;
        x = (_graphics.PreferredBackBufferWidth / 2) - 20;
        y = (_graphics.PreferredBackBufferHeight / 2) - 20;
        i = movementSpeed;
        screenMax = y * 2;
        screenMin = 0;
    }

    public void Update(GameTime gameTime)
    {

        if (delaycounter == delaymax)
        {
            currentframe++;
            delaycounter = 0;
        }

        if (currentframe == totalframe)
        {
            currentframe = 0;
        }
        delaycounter++;
    }

    public void Draw(Texture2D spriteTexture, SpriteBatch _spriteBatch, float movementSpeed)
    {
        Rectangle sourceRectangle;
        Rectangle destinationRectangle;

        // TODO: Add your drawing code here

        destinationRectangle = new Rectangle((int)x, (int)y, 40, 40);
        sourceRectangle = new Rectangle(187, 77, 15, 15);

        _spriteBatch.Begin();
        _spriteBatch.Draw(spriteTexture, destinationRectangle, sourceRectangle, Color.White);
        _spriteBatch.End();

        if (y <= screenMin)
        {
            i *= (-1);
        }

        if (y > screenMax)
        {
            i *= (-1);
        }

        y += i;
    }
}