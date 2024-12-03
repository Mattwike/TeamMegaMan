using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using System.Collections.Generic;

public class RedBlasterProjectile : IEnemySprite
{
    private float positionX;
    private float positionY;
    private float speedX;  // Horizontal speed for the projectile
    private float speedY;  // Vertical speed for the projectile
    private Texture2D projectileSheet;
    private int projectileSizeX;
    private int projectileSizeY;
    private int screenWidth;
    private int screenHeight;

    private Rectangle[] projectileFrames;
    private int currentFrame;
    private int totalFrames;
    private int delayCounter;
    private int delayMax;

    public Rectangle hitbox;
    public int health;

    // Position properties to match IEnemySprite
    public int x
    {
        get => (int)positionX;
        set => positionX = value;
    }

    public int y
    {
        get => (int)positionY;
        set => positionY = value;
    }

    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public float Gravity
    {
        set { gravity = 4.5f; }
    }

    // Constructor
    public RedBlasterProjectile(Texture2D texture, float startX, float startY, int screenWidth, int screenHeight, float speedX, float speedY)
    {
        projectileSheet = texture;
        positionX = startX;
        positionY = startY;
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;

        this.speedX = speedX;  // Projectile moves to the right
        this.speedY = speedY;  // Vertical speed (can be positive or negative)

        // Define source frame for the projectile
        projectileFrames = new Rectangle[]
        {
            new Rectangle(373, 16, 6, 6),  // Adjust these coordinates based on your sprite sheet
        };

        currentFrame = 0;
        totalFrames = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 10;  // Adjust delayMax to control animation speed
        projectileSizeX = projectileFrames[currentFrame].Width;
        projectileSizeY = projectileFrames[currentFrame].Height;

        health = 10;  // Set health if needed
    }

    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        currentFrame = 0;
        delayCounter = 0;

        // Set the projectile size
        projectileSizeX = size;
        projectileSizeY = size;

        // Movement speed is already set in the constructor
    }

    public void Update(GameTime gameTime)
    {
        // Move the projectile horizontally and vertically
        positionX += speedX;
        positionY += speedY;

        // Update hitbox position
        hitbox = new Rectangle((int)positionX, (int)positionY, projectileSizeX, projectileSizeY);

        // Frame delay logic (if needed for animation)
        delayCounter++;
        if (delayCounter >= delayMax)
        {
            currentFrame++;
            if (currentFrame >= totalFrames)
            {
                currentFrame = 0;
            }
            delayCounter = 0;
        }
    }

    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;

        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        // Define the destination rectangle where the sprite will be drawn on the screen
        Rectangle destinationRectangle = new Rectangle((int)positionX, (int)positionY, projectileSizeX, projectileSizeY);
        Rectangle sourceRectangle = projectileFrames[currentFrame];

        // Draw the projectile using its texture and current frame rectangle
        spriteBatch.Draw(projectileSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
    }

    public bool IsOffScreen()
    {
        // Return true if the projectile has moved off the screen
        return positionX < 0 || positionX > screenWidth || positionY < 0 || positionY > screenHeight;
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
        if (health <= 0)
        {
            // Handle projectile destruction if needed
            // For example, move it off-screen or mark it as inactive
            positionY += 1000;  // Move off-screen
            hitbox.Y += 1000;
        }
    }

    public void SetPosition(Vector2 position)
    {
        positionX = position.X;
        positionY = position.Y;
    }

    public void isTouchingFloor()
    {
        // Implement if necessary
    }
}
