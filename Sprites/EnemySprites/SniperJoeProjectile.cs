// SniperJoeProjectile.cs

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.GameObjects;  // Adjust the namespace as needed

public class SniperJoeProjectile : IEnemySprite
{
    private float posX;
    private float posY;
    private float speedX;
    private Texture2D projectileSheet;
    private int projectileSizeX;
    private int projectileSizeY;
    private int screenWidth;

    private Rectangle[] projectileFrames;
    private int currentFrame;
    private int totalFrame;
    private int delayCounter;
    private int delayMax;

    public Rectangle hitbox;
    public int health;

    public int x
    {
        get { return (int)posX; }
        set { posX = value; }
    }

    public int y
    {
        get { return (int)posY; }
        set { posY = value; }
    }

    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public bool hitWall { get; set; }
    public float gravity { get; set; }
    public float Gravity { set { gravity = 4.5f; } }

    public SniperJoeProjectile(Texture2D texture, float startX, float startY, int screenWidth)
    {
        projectileSheet = texture;
        posX = startX;
        posY = startY;
        this.screenWidth = screenWidth;

        speedX = -5f;

        projectileFrames = new Rectangle[]
        {
            new Rectangle(371, 244, 6, 6),
        };

        currentFrame = 0;
        totalFrame = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 10;
        projectileSizeX = projectileFrames[currentFrame].Width;
        projectileSizeY = projectileFrames[currentFrame].Height;

        health = 10;
    }

    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        // Implement if necessary
    }

    public void Update(GameTime gameTime, Camera camera)
    {
        posX += speedX;

        hitbox = new Rectangle((int)posX, (int)posY, projectileSizeX, projectileSizeY);

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

    public bool IsOffScreen(Camera camera)
    {
        Rectangle visibleArea = camera.GetVisibleArea();
        Rectangle projectileRectangle = new Rectangle((int)posX, (int)posY, projectileSizeX, projectileSizeY);
        return !visibleArea.Intersects(projectileRectangle);
    }

    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;
        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        Rectangle destinationRectangle = new Rectangle((int)posX, (int)posY, projectileSizeX, projectileSizeY);
        Rectangle sourceRectangle = projectileFrames[currentFrame];
        spriteBatch.Draw(projectileSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
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

    public void SetPosition(Vector2 position)
    {
        posX = position.X;
        posY = position.Y;
    }

    public void isTouchingFloor()
    {
        // Implement if necessary
    }
}
