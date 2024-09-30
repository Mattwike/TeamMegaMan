using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BombManThrowBomb : IEnemySprite
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
        Rectangle sourceRectangleBomb;
        Rectangle destinationRectangleBomb;

        if(currentFrame < 20)
        {
            sourceRectangle = new Rectangle(195, 29, 33, 34);
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
        } else
        {
            sourceRectangle = new Rectangle(218, 61, 37, 30);
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);

           
        }
        sourceRectangleBomb = new Rectangle(0, 123, 16, 18);
        destinationRectangleBomb = new Rectangle((int)x + 100, (int)y, enemySizeX, enemySizeY);



        
        _spriteBatch.Begin();
        _spriteBatch.Draw(enemySheet, destinationRectangleBomb, sourceRectangleBomb, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();

   
    }
}
