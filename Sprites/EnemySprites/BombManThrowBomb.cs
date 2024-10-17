using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Data;

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
    float initialBombX;
    float initialBombY;

    bool bombInAir;
    bool bombExploding;
    float explosionX;
    float explosionY;
    int bombSizeX;
    int bombSizeY;
    private float gravity = 4.5f;
    private float bombSpeed = 5f;

    public BombManThrowBomb(Texture2D texture)
    {
        enemySheet = texture;
        x = 100;
        y = 200;

     


    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrame = 26;
        delayCounter = 0;
        delayMax = 10;
        enemySizeX = 50;
        enemySizeY = 50;

        bombSizeX = 25;
        bombSizeY = 25;

        bombX = 25;
        bombY = 225;
        initialBombX = bombX;
        initialBombY = bombY;

        bombInAir = false;
        bombExploding = false;

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

        if (bombInAir)
        {
            if(bombY < 300)
            {
                bombY -= gravity;
                bombX += bombSpeed;
                gravity -= 0.25f;
            }
        }else if (!bombInAir)
        {
            gravity = 4.5f;
            bombY = initialBombY;
            bombX = initialBombX;
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

        Rectangle sourceRectangleExplosion = Rectangle.Empty;
        Rectangle destinationRectangleExplosion = Rectangle.Empty;

        if(currentFrame < 10)
        {

            sourceRectangle = new Rectangle(195, 29, 33, 34);
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
        } else if (currentFrame >= 10 && currentFrame < 13)
        {
            bombInAir = true;
            sourceRectangle = new Rectangle(218, 63, 33, 25);
            destinationRectangle = new Rectangle((int)x, (int)y+9, enemySizeX, enemySizeY-9);
        } else if(currentFrame >= 13 && currentFrame <= 20)
        {
            //bombMan enemy
            sourceRectangle = new Rectangle(218, 63, 33, 25);
            destinationRectangle = new Rectangle((int)x, (int)y + 9, enemySizeX, enemySizeY - 9);

            //Bomb arc
            sourceRectangleBomb = new Rectangle(0, 123, 16, 18);
            destinationRectangleBomb = new Rectangle((int)bombX, (int)bombY + 9, bombSizeX, bombSizeY);
        } else if(currentFrame == 21)
        {
            bombExploding = true;
            sourceRectangle = new Rectangle(218, 63, 33, 25);
            destinationRectangle = new Rectangle((int)x, (int)y + 9, enemySizeX, enemySizeY - 9);

            sourceRectangleExplosion = new Rectangle(74, 126, 8, 8);
            destinationRectangleExplosion = new Rectangle((int)bombX, (int)bombY + 9, 10, 10);
        } else if (currentFrame == 22)
        {
            sourceRectangle = new Rectangle(218, 63, 33, 25);
            destinationRectangle = new Rectangle((int)x, (int)y + 9, enemySizeX, enemySizeY - 9);

            sourceRectangleExplosion = new Rectangle(117, 123, 16, 16);
            destinationRectangleExplosion = new Rectangle((int)bombX, (int)bombY + 9, 20, 20);

        }else if (currentFrame == 23)
        {
            sourceRectangle = new Rectangle(218, 63, 33, 25);
            destinationRectangle = new Rectangle((int)x, (int)y + 9, enemySizeX, enemySizeY - 9);

            sourceRectangleExplosion = new Rectangle(101, 123, 14, 14);
            destinationRectangleExplosion = new Rectangle((int)bombX, (int)bombY + 9, 35, 35);

        }else if (currentFrame == 24)
        {
            sourceRectangle = new Rectangle(218, 63, 33, 25);
            destinationRectangle = new Rectangle((int)x, (int)y + 9, enemySizeX, enemySizeY - 9);

            sourceRectangleExplosion = new Rectangle(83, 123, 18, 18);
            destinationRectangleExplosion = new Rectangle((int)bombX, (int)bombY + 9, 45, 45);

        }
        else
        {
            sourceRectangle = new Rectangle(218, 63, 33, 25);
            destinationRectangle = new Rectangle((int)x, (int)y + 9, enemySizeX, enemySizeY - 9);
            bombInAir = false;
            bombExploding = false;
        }



        _spriteBatch.Begin();

        if(bombInAir)
        {
            _spriteBatch.Draw(enemySheet, destinationRectangleBomb, sourceRectangleBomb, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        }
        if (bombExploding)
        {
            _spriteBatch.Draw(enemySheet, destinationRectangleExplosion, sourceRectangleExplosion, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        }

        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();

   
    }
}
