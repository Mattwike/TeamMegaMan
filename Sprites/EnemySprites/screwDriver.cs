using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using System.Collections.Generic;

public class screwDriver : IEnemySprite
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
    public Rectangle hitbox;
    public int health;

    public screwDriver(Texture2D texture)
    {
        enemySheet = texture;
        x = 400;
        y = 30;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrame = 50;
        delayCounter = 0;
        delayMax = 5;
        enemySizeX = megamanSize;
        enemySizeY = megamanSize;
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
            sourceRectangle = new Rectangle(155, 250, 18, 18);
        }
        else if (currentFrame < 15)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(172, 250, 18, 18);
        }
        else if (currentFrame < 20)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(189, 250, 18, 18);
        }
        else if (currentFrame < 30)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(206, 250, 18, 18);
        }
        else if(currentFrame < 40)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(223, 250, 18, 18);
        }
        else
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(189, 250, 18, 18);
        }

        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
    }
    public Rectangle getRectangle()
    {
        return hitbox;
    }
    public int GetHealth()
    {
        return health;
    }
    public void TakeDamage(List<EnemyDrop> enemyDropList)
    {
        health -= 10;
    }
}
