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

    float bombX;
    float bombY;
    public int bombInAir;
    int bombSizeX;
    int bombSizeY;
    private float gravity = 1f;
    private float bombSpeed = 3f;

    public BombManThrowBomb(Texture2D texture)
    {
        enemySheet = texture;
        x = 450;
        y = 50;

     


    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrame = 70;
        delayCounter = 0;
        delayMax = 10;
        enemySizeX = 50;
        enemySizeY = 50;

        bombSizeX = 25;
        bombSizeY = 25;

        bombX = 470;
        bombY = 50;

    }

    public void Update(GameTime gameTime)
    {
        delayCounter++;
        if (delayCounter >= delayMax)
        {
            if (bombInAir == 1)
            {
                bombX += bombSpeed;
                bombY += gravity;
            }
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

        Rectangle sourceRectangle = Rectangle.Empty;
        Rectangle destinationRectangle = Rectangle.Empty;
        Rectangle sourceRectangleBomb = Rectangle.Empty;
        Rectangle destinationRectangleBomb = Rectangle.Empty;

        if(currentFrame < 20)
        {
            sourceRectangle = new Rectangle(195, 29, 33, 34);
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
        } else if (currentFrame >= 20 && currentFrame < 22)
        {
            sourceRectangle = new Rectangle(218, 63, 33, 25);
            destinationRectangle = new Rectangle((int)x, (int)y+9, enemySizeX, enemySizeY-9);
            bombInAir = 1;
        } else if(currentFrame >= 22 && currentFrame <= 70)
        {
            //bombMan enemy
            sourceRectangle = new Rectangle(218, 63, 33, 25);
            destinationRectangle = new Rectangle((int)x, (int)y + 9, enemySizeX, enemySizeY - 9);

            //Bomb arc
            sourceRectangleBomb = new Rectangle(0, 123, 16, 18);
            destinationRectangleBomb = new Rectangle((int)bombX, (int)bombY, bombSizeX, bombSizeY);
        } 
       

        
        _spriteBatch.Begin();

        if(bombInAir == 1)
        {
            _spriteBatch.Draw(enemySheet, destinationRectangleBomb, sourceRectangleBomb, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        }

        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();

   
    }
}
