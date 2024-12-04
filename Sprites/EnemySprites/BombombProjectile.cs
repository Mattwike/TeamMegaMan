// BombombProjectile.cs

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.GameObjects;  // Adjust this namespace if necessary

public class BombombProjectile : IEnemySprite
{
    // Fields
    private Texture2D texture;
    private float posX, posY;
    public float speedX, speedY;
    public int width { get; set; }
    public int height { get; set; }
    private int screenWidth;
    private int screenHeight;

    public Rectangle hitbox;
    public int health;

    public int y { get; set; }
    public int x { get; set; }
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public bool hitWall { get; set; }
    public float Gravity { set { gravity = 4.5f; } }

    private Rectangle[] projectileFrames;
    private int currentFrame;
    private int totalFrame;
    private int delayCounter;
    private int delayMax;

    // Constructor
    public BombombProjectile(Texture2D texture, float startX, float startY, int screenWidth, float speedX)
    {
        this.texture = texture;
        this.posX = startX;
        this.posY = startY;
        this.screenWidth = screenWidth;
        this.screenHeight = 600;  // Adjust as needed

        this.speedX = speedX;
        this.speedY = 2f;

        projectileFrames = new Rectangle[]
        {
            new Rectangle(417, 24, 8, 6),  // Adjust based on your sprite sheet
        };

        currentFrame = 0;
        totalFrame = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 10;
        width = projectileFrames[currentFrame].Width;
        height = projectileFrames[currentFrame].Height;

        health = 10;  // Initial health
    }

    // Initialize method
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        // Implement if necessary
    }

    // Update method with Camera parameter
    public void Update(GameTime gameTime, Camera camera)
    {
        posX += speedX;
        posY += speedY;

        // Update hitbox position
        hitbox = new Rectangle((int)posX, (int)posY, width, height);

        // Frame delay logic (if needed for animation)
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

    // Draw method
    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;
        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        // Define the destination rectangle where the projectile will be drawn on the screen
        Rectangle destinationRectangle = new Rectangle((int)posX, (int)posY, width, height);

        // Define the source rectangle for the projectile sprite (adjust based on your sprite sheet)
        Rectangle sourceRectangle = projectileFrames[currentFrame];

        // Draw the projectile using its texture
        spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
    }

    // Method to check if the projectile is off the screen, accounting for the camera
    public bool IsOffScreen(Camera camera)
    {
        if (camera == null)
        {
            // Default behavior if camera is not provided
            return (posX < -width || posX > screenWidth + width || posY < -height || posY > screenHeight + height);
        }

        Rectangle visibleArea = camera.GetVisibleArea();
        Rectangle projectileRectangle = new Rectangle((int)posX, (int)posY, width, height);
        return !visibleArea.Intersects(projectileRectangle);
    }

    // Get the rectangle for collision detection
    public Rectangle getRectangle()
    {
        return hitbox;
    }

    // Get the health of the projectile
    public int GetHealth()
    {
        return health;
    }

    // Handle taking damage
    public void TakeDamage(List<EnemyDrop> enemyDropList)
    {
        health -= 10;
    }

    // Set the position of the projectile
    public void SetPosition(Vector2 position)
    {
        posX = position.X;
        posY = position.Y;
    }

    // Handle touching the floor (implement as needed)
    public void isTouchingFloor()
    {
        // Implement if necessary
    }
}
