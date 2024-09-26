using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class RedBlasterProjectile : ISprite
{
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;
    float x;
    float y;
    private Texture2D projectileSheet;
    int projectileSizeX;
    int projectileSizeY;
    float speed;
    float initialX;  // Store the initial X position for resetting if needed
    float initialY;  // Store the initial Y position for resetting if needed
    int screenWidth;  // The width of the screen to determine when the projectile goes off-screen

    // Define source frame dimensions for the projectile animation
    Rectangle[] projectileFrames;

    // Constructor
    public RedBlasterProjectile(Texture2D texture, float startX, float startY, int screenWidth)
    {
        projectileSheet = texture;
        x = startX;
        y = startY;
        initialX = startX;  // Save the initial position for resetting if needed
        initialY = startY-10;
        this.screenWidth = screenWidth;  // Set the screen width
        speed = 5f;  // Default speed of the projectile

        // Setup the animation frames for the projectile
        projectileFrames = new Rectangle[]
        {
            new Rectangle(373, 16, 6, 6),  // Frame 1
        };

        currentFrame = 0;
        totalFrame = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 10;  // You can adjust this to control the frame update speed
        projectileSizeX = 12;  // Default size of the projectile
        projectileSizeY = 12;
    }

    // Add the Initialize method to match the ISprite interface
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int projectileSize)
    {
        currentFrame = 0;
        totalFrame = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 10;  // Adjust to control the frame update speed

        // Set the projectile size
        projectileSizeX = projectileSize;
        projectileSizeY = projectileSize;

        // Set the movement speed
        speed = movementSpeed;
    }

    // Update method to handle movement and frame updates
    public void Update(GameTime gameTime)
    {
        // Move the projectile to the left at the set speed
        x -= speed;

        // Frame delay logic
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

    // Draw method (each projectile manages its own SpriteBatch.Begin/End)
    public void Draw(SpriteBatch _spriteBatch, float movementSpeed, bool flipHorizontally, bool flipVertically)
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

        // Begin SpriteBatch for this specific projectile
        _spriteBatch.Begin();

        // Destination rectangle on the screen (scaling the projectile if needed)
        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, projectileSizeX, projectileSizeY);
        // Source rectangle from the sprite sheet
        Rectangle sourceRectangle = projectileFrames[currentFrame];

        _spriteBatch.Draw(projectileSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        // End SpriteBatch for this specific projectile
        _spriteBatch.End();
    }

    // Method to check if the projectile is off the screen
    public bool IsOffScreen()
    {
        // Return true if the projectile has moved off the screen to the left
        return x < 0;
    }
}
