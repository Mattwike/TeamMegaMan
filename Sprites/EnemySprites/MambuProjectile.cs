using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class MambuProjectile
{
    private Texture2D texture;  // Texture for the projectile
    private float x, y;  // X and Y coordinates of the projectile
    public float speedX, speedY;  // Speed in X and Y directions

    // Change width and height to public so they are accessible outside the class
    public int width { get; set; }  // Width of the projectile
    public int height { get; set; }  // Height of the projectile

    private int screenWidth, screenHeight;  // Screen dimensions for boundary checking

    // Constructor to initialize the projectile with texture, position, and screen dimensions
    public MambuProjectile(Texture2D texture, float startX, float startY, int screenWidth, int screenHeight)
    {
        this.texture = texture;
        this.x = startX;
        this.y = startY;
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
    }

    // Initialize method to set projectile size and speed
    public void Initialize(int projectileWidth, int projectileHeight, float speedX, float speedY)
    {
        this.width = projectileWidth;
        this.height = projectileHeight;
        this.speedX = speedX;
        this.speedY = speedY;
    }

    // Update method to handle projectile movement logic
    public void Update(GameTime gameTime)
    {
        x += speedX;
        y += speedY;
    }

    // Draw method to render the projectile on the screen
    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;

        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        // Define the destination rectangle where the projectile will be drawn on the screen
        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, width, height);

        // Draw the projectile using its texture
        spriteBatch.Draw(texture, destinationRectangle, null, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
    }

    // Method to check if the projectile is off the screen
    public bool IsOffScreen()
    {
        // Return true if the projectile is outside the screen boundaries
        return (x < -width || x > screenWidth + width || y < -height || y > screenHeight + height);
    }

    // Getters for position (if needed for collision or external checks)
    public float GetPositionX()
    {
        return x;
    }

    public float GetPositionY()
    {
        return y;
    }

    public void isTouchingFloor()
    {
        //istouchingfloor = false;
    }
}
