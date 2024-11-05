using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using Project1.Sprites;

public class jumpingFlea : IEnemySprite
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
    float enemySpeed;
    float gravity;
    bool jumping;
    bool falling;
    public Rectangle hitbox;

    public jumpingFlea(Texture2D texture)
    {
        enemySheet = texture;
        x = 350;
        y = 30;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrame = 15;
        delayCounter = 0;
        delayMax = 10;
        enemySizeX = megamanSize;
        enemySizeY = megamanSize;
        gravity = 4.5f;
        enemySpeed = 2f;
        jumping = false;
        falling = false;
      
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

        if (jumping)
        {
            x += enemySpeed;
            if (gravity > 0)
            {
                y -= gravity;
                gravity -= 0.25f;
            }
            else
            {
                jumping = false;
                falling = true;
            }
        }
        else if (falling)
        {
            if (y < 30)
            {
                y += gravity;
                gravity += 0.25f;
            }
            else
            {
                y = 30;
                falling = false;
                gravity = 4.5f;
            }
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

        Rectangle sourceRectangle = Rectangle.Empty;
        Rectangle destinationRectangle = Rectangle.Empty;

        if (currentFrame < 10)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(113, 177, 18, 14);
        }
        else if (currentFrame == 10)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(148, 170, 16, 19);
            jumping = true;
        }
        else if(currentFrame > 10 && currentFrame < 13)
        {
            destinationRectangle = new Rectangle((int)x, (int)y-2, enemySizeX, enemySizeY+2);
            sourceRectangle = new Rectangle(131, 163, 16, 21);
            
        }else if(currentFrame >= 13)
        {
            destinationRectangle = new Rectangle((int)x, (int)y - 2, enemySizeX, enemySizeY + 2);
            sourceRectangle = new Rectangle(131, 163, 16, 21);
            jumping = false;
            falling = true;
        }

        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
    }
    public Rectangle getRectangle()
    {
        return hitbox;
    }
}