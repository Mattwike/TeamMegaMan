using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SmallBlueRectangle : IEnemySprite
{
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;
    float x;
    float y;
    private Texture2D itemSheet;
    int itemSizeX;
    int itemSizeY;
    public SmallBlueRectangle(Texture2D texture)
    {
        itemSheet = texture;
        x = 400;
        y = 90;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrame = 10;
        delayCounter = 0;
        delayMax = 5;
        itemSizeX = megamanSize;
        itemSizeY = megamanSize;
    }

    public void Update(GameTime gameTime)
    {
        delayCounter++;
        if (delayCounter >= delayMax)
        {
            currentFrame++;
            if (currentFrame >= totalFrame)
            {
                currentFrame = 0;
            }
            delayCounter = 0;
        }
    }

    public void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically)
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

        if (currentFrame < 5)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, itemSizeX, itemSizeY);
            sourceRectangle = new Rectangle(110, 8, 12, 8);
        }
        else
        {
            destinationRectangle = new Rectangle((int)x, (int)y, itemSizeX, itemSizeY);
            sourceRectangle = new Rectangle(126, 8, 12, 8);
        }

        _spriteBatch.Begin();
        _spriteBatch.Draw(itemSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();
    }
}
