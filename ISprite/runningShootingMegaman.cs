using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class runningShootingMegaman : ISprite
{
    int currentframe;
    int totalframe;
    int delaycounter;
    int delaymax;
    float x;
    float y;
    int megamanSizeX;
    int megamanSizeY;
    private Texture2D megaManSheet;

    public runningShootingMegaman(Texture2D texture)
    {
        megaManSheet = texture;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentframe = 0;
        totalframe = 3;
        delaycounter = 0;
        delaymax = 11;
        x = 95;
        y = 15;
        megamanSizeX = megamanSize + 10;
        megamanSizeY = megamanSize;
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

    public void Draw(SpriteBatch _spriteBatch, float movementSpeed, bool flipHorizontally, bool flipVertically)
    {

        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
        {
            spriteEffects |= SpriteEffects.FlipHorizontally;
        }

        if (flipVertically)
        {
            spriteEffects |= SpriteEffects.FlipVertically;
        }

        Rectangle sourceRectangle;
        Rectangle destinationRectangle;

        // TODO: Add your drawing code here
        if (currentframe == 0)
        {
            sourceRectangle = new Rectangle(14, 46, 31, 24);
            destinationRectangle = new Rectangle((int)x, (int)y, megamanSizeX, megamanSizeY);
        }

        else if (currentframe == 1)
        {
            sourceRectangle = new Rectangle(50, 48, 29, 22);
            destinationRectangle = new Rectangle((int)x, (int)y + 2, megamanSizeX - 2, megamanSizeY - 2);
        }

        else if (currentframe == 2)
        {
            sourceRectangle = new Rectangle(84, 46, 26, 24);
            destinationRectangle = new Rectangle((int)x, (int)y, megamanSizeX - 5, megamanSizeY);
        }

        else
        {
            sourceRectangle = new Rectangle(113, 48, 30, 22);
            destinationRectangle = new Rectangle((int)x, (int) + 2, megamanSizeX - 1, megamanSizeY - 2);
        }

        _spriteBatch.Begin();
        _spriteBatch.Draw(megaManSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();
    }
}