using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using System.Collections.Generic;

public class bombManIdle : IEnemySprite
{
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;
    //float x;
    //float y;
    private Texture2D enemySheet;
    int enemySizeX;
    int enemySizeY;
    public Rectangle hitbox;
    public int health;

    public int y { get; set; }
    public int x { get; set; }
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public bool hitWall { get; set; }
    public float Gravity
    {
        set { gravity = 4.5f; }

    }
    public bombManIdle(Texture2D texture, Vector2 position)
    {
        enemySheet = texture;
        x = (int)position.X;
        y = (int)position.Y;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrame = 20;
        delayCounter = 0;
        delayMax = 10;
        enemySizeX = 50;
        enemySizeY = 50;
        hitbox.X = 400;
        hitbox.Y = 40;
        hitbox.Width = 50;
        hitbox.Height = 50;
    }

    public void Update(GameTime gameTime, Camera camera, int megamanX)
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
            destinationRectangle = new Rectangle((int)x, (int)y, 24, 24);
            sourceRectangle = new Rectangle(60, 36, 25, 25);
        }
        else if (currentFrame == 10)
        {
            destinationRectangle = new Rectangle((int)x-3, (int)y-5, 27, 29);
            sourceRectangle = new Rectangle(31, 30, 29, 31);
        }
        else
        {
            destinationRectangle = new Rectangle((int)x-1, (int)y-14, 25, 38);
            sourceRectangle = new Rectangle(0, 21, 28, 41);
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
    public void isTouchingFloor()
    {
        //istouchingfloor = false;
    }
}
