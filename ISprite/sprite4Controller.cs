using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class sprite4Controller : ISprite
{
    int currentframe;
    int totalframe;
    int delaycounter;
    int delaymax;
    float x;
    float screenMax;
    float screenMin;
    float y;
    float i;

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed)
    {
        currentframe = 0;
        totalframe = 3;
        delaycounter = 0;
        delaymax = 10;
        x = (_graphics.PreferredBackBufferWidth / 2) - 20;
        y = (_graphics.PreferredBackBufferHeight / 2) - 20;
        screenMax = x;
        screenMin = 0;
        i = movementSpeed;
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

        if (currentframe == 0)
        {
            sourceRectangle = new Rectangle(187, 77, 15, 15);
        }

        else if (currentframe == 1)
        {
            sourceRectangle = new Rectangle(203, 77, 27, 15);
            destinationRectangle = new Rectangle((int)x, (int)y, 52, 40);
        }

        else if (currentframe == 2)
        {
            sourceRectangle = new Rectangle(232, 77, 22, 15);
            destinationRectangle = new Rectangle((int)x, (int)y, 47, 40);
        }

        else if (currentframe == 3)
        {
            sourceRectangle = new Rectangle(256, 77, 18, 15);
            destinationRectangle = new Rectangle((int)x, (int)y, 42, 40);
        }

        _spriteBatch.Begin();
        _spriteBatch.Draw(spriteTexture, destinationRectangle, sourceRectangle, Color.White);
        _spriteBatch.End();

        if (x <= screenMin)
        {
            i *= (-1);
        }

        if (x > screenMax * 2)
        {
            i *= (-1);
        }

        x += i;
    }
}