using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using System.Collections.Generic;

public class BombombProjectile : IEnemySprite
{
    float x;
    float y;
    float speedX;  // Horizontal speed (constant left or right)
    float speedY;  // Vertical speed (constant falling)
    private Texture2D projectileSheet;
    int projectileSizeX;
    int projectileSizeY;
    int screenWidth;
    int screenHeight;  // Added: screenHeight variable to check boundaries

    Rectangle[] projectileFrames;
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;

    public Rectangle hitbox;
    public int health;

    public BombombProjectile(Texture2D texture, float startX, float startY, int screenWidth, float speedX)
    {
        projectileSheet = texture;
        x = startX;
        y = startY;
        this.screenWidth = screenWidth;
        this.screenHeight = 600;  // Default screen height (set as 600), modify this based on your screen size

        this.speedX = speedX;  // Set horizontal speed (left or right)
        this.speedY = 2f;    // Set constant falling speed

        // Define source frame for projectile (adjust based on sprite sheet)
        projectileFrames = new Rectangle[]
        {
            new Rectangle(417, 24, 8, 6),  // Projectile frame from sprite sheet
        };

        currentFrame = 0;
        totalFrame = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 10;
        projectileSizeX = projectileFrames[currentFrame].Width;
        projectileSizeY = projectileFrames[currentFrame].Height;
    }

    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        currentFrame = 0;
        delayCounter = 0;
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

        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, projectileSizeX, projectileSizeY);
        Rectangle sourceRectangle = projectileFrames[currentFrame];
        _spriteBatch.Draw(projectileSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
    }

    public void Update(GameTime gameTime)
    {
        // Move the projectile horizontally and vertically at constant speeds
        x += speedX;  // Move left or right based on speedX
        y += speedY;  // Move down at a constant speed

        // No increase in horizontal or vertical speed, maintain constant motion

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

    public bool IsOffScreen()
    {
        // Check if the projectile is off-screen based on screen width and height
        return x < 0 || x > screenWidth || y > 300;  // Make sure y > screenHeight to detect bottom screen boundary
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
