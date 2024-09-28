using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BombManThrowBomb : ISprite
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
    public int bombInAir = 0;

    public BombManThrowBomb(Texture2D texture)
    {
        enemySheet = texture;
        x = 450;
        y = 50;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrame = 60;
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
        flipHorizontally = true;

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
        Rectangle sourceRectangleBomb = Rectangle.Empty;
        Rectangle destinationRectangleBomb = Rectangle.Empty;

        if (currentFrame < 10)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(31, 29, 29, 32);
        }
        else if (currentFrame == 10 || currentFrame == 11)
        {
            destinationRectangle = new Rectangle((int)x-2, (int)y-1, enemySizeX+2, enemySizeY+1);
            sourceRectangle = new Rectangle(195, 29, 31, 33);
        }
        else if(currentFrame > 11)
        {
            destinationRectangle = new Rectangle((int)x-4, (int)y+7, enemySizeX+4, enemySizeY-7);
            sourceRectangle = new Rectangle(218, 63, 33, 25);
            bombInAir = 1;
        }
        else
        {
            destinationRectangle = new Rectangle((int)x - 4, (int)y + 7, enemySizeX + 4, enemySizeY - 7);
            sourceRectangle = new Rectangle(218, 63, 33, 25);

            destinationRectangleBomb = new Rectangle((int)x - 4, (int)y + 7, enemySizeX + 4, enemySizeY - 7);
            sourceRectangleBomb = new Rectangle(0, 122, 17, 17);
        }



        _spriteBatch.Begin();
        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();

   
    }
}
