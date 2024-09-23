using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class runningMegaman : ISprite
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

    public runningMegaman(Texture2D texture) 
    {
        megaManSheet = texture;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentframe = 0;
        totalframe = 3;
        delaycounter = 0;
        delaymax = 10;
        x = 55;
        y = 15;
        megamanSizeX = megamanSize + 3;
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


        if (currentframe == 0)
        {
            sourceRectangle = new Rectangle(188, 12, 24, 22);
            destinationRectangle = new Rectangle((int)x, (int)y, megamanSizeX, megamanSizeY);
        }

        else if (currentframe == 1)
        {
            sourceRectangle = new Rectangle(218, 10, 16, 24);
            destinationRectangle = new Rectangle((int)x, (int)y - 2, megamanSizeX -8, megamanSizeY + 2);
        }

        else
        {
            sourceRectangle = new Rectangle(239, 12, 21, 22);
            destinationRectangle = new Rectangle((int)x, (int)y, megamanSizeX - 3, megamanSizeY);
        }

        _spriteBatch.Begin();
        _spriteBatch.Draw(megaManSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();

    }
}