using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class ScrewDriverProjectile : ISprite
{
    private Texture2D projectileTexture;  // The texture for the projectile
    private float x;  // X-coordinate of the projectile
    private float y;  // Y-coordinate of the projectile
    public float speedX { get; set; }  // Horizontal speed of the projectile
    public float speedY { get; set; }  // Vertical speed of the projectile
    private int screenWidth;  // Screen width for boundary checking
    private int screenHeight; // Screen height for boundary checking
    private int projectileWidth;  // Width of the projectile sprite
    private int projectileHeight;  // Height of the projectile sprite
    private Rectangle sourceRectangle;  // Source rectangle for the projectile sprite

    // Constructor to initialize projectile with position, texture, and screen dimensions
    public ScrewDriverProjectile(Texture2D texture, float startX, float startY, int screenWidth, int screenHeight)
    {
        projectileTexture = texture;
        x = startX;
        y = startY;
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        projectileWidth = 6;
        projectileHeight = 6;
        sourceRectangle = new Rectangle(242, 256, 6, 6);  // Coordinates on the sprite sheet

        // Default speeds, which may be adjusted later using Initialize()
        speedX = 0f;
        speedY = 0f;
    }

    // Restored Initialize method to set up projectile size and speed
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int projectileSize)
    {
        // Adjust projectile size based on the input parameter
        projectileWidth = projectileSize;
        projectileHeight = projectileSize;

        // Set up the movement speed
        speedX = movementSpeed;  // Set initial speedX (could be modified by specific logic later)
        speedY = movementSpeed;  // Set initial speedY (could be modified by specific logic later)
    }

    // Update method to handle projectile movement
    public void Update(GameTime gameTime)
    {
        // Move the projectile based on its speed
        x += speedX;
        y += speedY;
    }

    // Draw the projectile at its current position
    public void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;

        // Handle flipping if necessary
        if (flipHorizontally) spriteEffects |= SpriteEffects.FlipHorizontally;
        if (flipVertically) spriteEffects |= SpriteEffects.FlipVertically;

        _spriteBatch.Begin();

        // Destination rectangle where the projectile will be drawn on the screen
        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, projectileWidth, projectileHeight);

        // Draw the projectile using its texture and source rectangle
        _spriteBatch.Draw(projectileTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        _spriteBatch.End();
    }

    // Check if the projectile is off the screen
    public bool IsOffScreen()
    {
        return (x < -projectileWidth || x > screenWidth + projectileWidth || y < -projectileHeight || y > screenHeight + projectileHeight);
    }
}
