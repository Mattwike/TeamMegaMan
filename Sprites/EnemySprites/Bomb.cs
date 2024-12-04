using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using System.Collections.Generic;

public class Bomb : IEnemySprite
{
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;
    //float x;
    //float y;
    private Texture2D BossSheet;
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
    public bool hasProjectiles { get; set; }
    public float Gravity
    {
        set { gravity = 4.5f; }

    }
    public Bomb(Texture2D texture, Vector2 position)
    {
        BossSheet = texture;
        x = (int)position.X;
        y = (int)position.Y;
        hasProjectiles = false;
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
            destinationRectangle = new Rectangle((int)x, (int)y, 14, 14);
            sourceRectangle = new Rectangle(2, 125, 14, 14);
        }
        else if (currentFrame == 10)
        {
            destinationRectangle = new Rectangle((int)x, (int)y-2, 14, 12);
            sourceRectangle = new Rectangle(19, 127, 14, 12);
        }
        else
        {
            destinationRectangle = new Rectangle((int)x, (int)y, 14, 14);
            sourceRectangle = new Rectangle(2, 125, 14, 14);
        }

        _spriteBatch.Draw(BossSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
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

    public List<IEnemyProjectile> GetProjectiles()
    {
        return null;
    }
}
