// MambuProjectile.cs

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.GameObjects;  // Adjust this namespace if necessary

public class MambuProjectile : IEnemyProjectile
{
    // Fields
    private Texture2D texture;
    private float posX, posY;
    public float speedX, speedY;
    public int width { get; set; }
    public int height { get; set; }
    private int screenWidth, screenHeight;

    public Rectangle hitbox;
    public int health;

    public int x { get; set; }
    public int y { get; set; }

    // Constructor
    public MambuProjectile(Texture2D texture, float startX, float startY, int screenWidth, int screenHeight)
    {
        this.texture = texture;
        this.posX = startX;
        this.posY = startY;
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        this.health = 10;  // Initial health
    }

    // Initialize method
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        // Implement if necessary
    }

    // Update method with Camera parameter
    public void Update(GameTime gameTime, Camera camera, int megamanX)
    {
        posX += speedX;
        posY += speedY;

        // Update hitbox position
        hitbox = new Rectangle((int)posX, (int)posY, width, height);
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
        Rectangle sourceRectangle = new Rectangle(338, 182, 6, 6);  // Example values

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
