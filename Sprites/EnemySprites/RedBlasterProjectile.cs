using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class RedBlasterProjectile : IEnemySprite
{
    protected int currentFrame;  // Marked protected to allow access in derived classes
    protected int totalFrame;    // Marked protected to allow access in derived classes
    protected int delayCounter;
    protected int delayMax;
    protected float x;  // Marked protected to allow access in derived classes
    protected float y;  // Marked protected to allow access in derived classes
    private Texture2D projectileSheet;
    protected int projectileSizeX;  // Marked protected to allow access in derived classes
    protected int projectileSizeY;  // Marked protected to allow access in derived classes
    private float initialX;
    private float initialY;
    private int screenWidth;

    // Define source frame dimensions for the projectile animation
    protected Rectangle[] projectileFrames;  // Marked protected to allow access in derived classes

    public RedBlasterProjectile(Texture2D texture, float startX, float startY, int screenWidth)
    {
        projectileSheet = texture;
        x = startX;
        y = startY;
        initialX = startX;  // Save the initial position
        initialY = startY - 10;  // Adjust starting Y position slightly
        this.screenWidth = screenWidth;  // Set the screen width

        // Setup the animation frames for the projectile
        projectileFrames = new Rectangle[]
        {
            new Rectangle(373, 16, 6, 6),  // Frame 1 (example coordinates, adjust as needed)
        };

        currentFrame = 0;
        totalFrame = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 10;  // You can adjust this to control the frame update speed
        projectileSizeX = 12;  // Default size of the projectile
        projectileSizeY = 12;
    }

    // Implementing the Initialize method as required by the ISprite interface
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int projectileSize)
    {
        // Set initial frame values
        currentFrame = 0;
        totalFrame = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 10;  // Adjust to control the frame update speed

        // Set the projectile size
        projectileSizeX = projectileSize;
        projectileSizeY = projectileSize;

        // Set movement speed (if applicable)
        // Note: In this case, `movementSpeed` is not used because the default behavior moves left
        // If you want to control movement speed, consider using this parameter in derived classes
    }

    // Mark the Update method as virtual to allow overriding in derived classes
    public virtual void Update(GameTime gameTime)
    {
        // Example animation logic for updating frames
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

        // Default behavior: move the projectile to the left
        x -= 5f;  // Move left at a constant speed of 5 units
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

        _spriteBatch.Begin();

        // Destination rectangle on the screen (scaling the projectile if needed)
        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, projectileSizeX, projectileSizeY);
        // Source rectangle from the sprite sheet
        Rectangle sourceRectangle = projectileFrames[currentFrame];

        _spriteBatch.Draw(projectileSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        _spriteBatch.End();
    }

    // Method to check if the projectile is off the screen
    public bool IsOffScreen()
    {
        // Return true if the projectile has moved off the screen to the left or right
        return x < 0 || x > screenWidth || y < 0 || y > screenWidth;  // Adjust boundaries as needed
    }
}
