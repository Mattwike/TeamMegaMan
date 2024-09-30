using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class bombManIdle : IEnemySprite
{
    int currentFrame;    // Make sure to use camelCase consistently
    int totalFrame;
    int delayCounter;
    int delayMax;
    float x;
    float y;
    private Texture2D enemySheet;
    int enemySizeX;
    int enemySizeY;
    public bombManIdle(Texture2D texture)
    {
        enemySheet = texture;
        x = 400;
        y = 40;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrame = 20;
        delayCounter = 0;
        delayMax = 10;
        enemySizeX = 50;
        enemySizeY = 50;
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

        if (currentFrame < 10)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(60, 36, 25, 25);
        }
        else if (currentFrame == 10)
        {
            destinationRectangle = new Rectangle((int)x-8, (int)y-10, enemySizeX+8, enemySizeY+10);
            sourceRectangle = new Rectangle(31, 30, 29, 31);
        }
        else
        {
            destinationRectangle = new Rectangle((int)x-6, (int)y-30, enemySizeX+6, enemySizeY+30);
            sourceRectangle = new Rectangle(0, 21, 28, 41);
        }

        _spriteBatch.Begin();
        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();
    }
}
