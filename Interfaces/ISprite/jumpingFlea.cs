using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;

public class jumpingFlea : ISprite
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
    private Megaman megaman;

    public jumpingFlea(Texture2D texture)
    {
        enemySheet = texture;
        x = 350;
        y = 30;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman Megaman, int interval, bool isRight)
    {
        currentFrame = 0;
        totalFrame = 20;
        delayCounter = 0;
        delayMax = 10;
        enemySizeX = megamanSize;
        enemySizeY = megamanSize;
        this.megaman = Megaman;
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

        if (currentFrame < 10)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(113, 177, 18, 14);
        }
        else if (currentFrame == 10)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(148, 170, 16, 19);
        }
        else
        {
            destinationRectangle = new Rectangle((int)x, (int)y - 15, enemySizeX - 1, enemySizeY);
            sourceRectangle = new Rectangle(131, 163, 16, 21);
        }

        _spriteBatch.Begin();
        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();
    }
}