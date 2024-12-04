// ScrewDriverProjectile.cs

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.GameObjects;  // Adjust this namespace if necessary

public class ScrewDriverProjectile : IEnemySprite
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
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public bool hitWall { get; set; }
    public float Gravity { set { gravity = 4.5f; } }

    // Constructor
    public ScrewDriverProjectile(Texture2D texture, float startX, float startY, int screenWidth, int screenHeight)
    {
        this.texture = texture;
        this.posX = startX;
        this.posY = startY;
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;
        this.health = 1;  // Projectiles typically have low or no health
        this.width = 8;    // Set appropriate width for the projectile
        this.height = 8;   // Set appropriate height for the projectile

        // Initialize hitbox
        this.x = (int)posX;
        this.y = (int)posY;
        hitbox = new Rectangle(x, y, width, height);
    }

    // Initialize method (optional, implement if needed)
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
        x = (int)posX;
        y = (int)posY;
        hitbox = new Rectangle(x, y, width, height);
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
        Rectangle destinationRectangle = new Rectangle(x, y, width, height);

        // Define the source rectangle for the projectile sprite (adjust based on your sprite sheet)
        // Example values; adjust to match your sprite sheet's projectile sprite
        Rectangle sourceRectangle = new Rectangle(338, 182, 6, 6);  // Adjust as needed

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
        Rectangle projectileRectangle = new Rectangle(x, y, width, height);

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
        health -= 1;  // Projectiles typically have minimal health
        if (health <= 0)
        {
            // Handle projectile destruction if needed
            // For example, remove the projectile or trigger an animation
            // This can be managed externally by the parent enemy class
        }
    }

    // Set the position of the projectile
    public void SetPosition(Vector2 position)
    {
        posX = position.X;
        posY = position.Y;
        x = (int)posX;
        y = (int)posY;
        hitbox = new Rectangle(x, y, width, height);
    }

    // Handle touching the floor (implement as needed)
    public void isTouchingFloor()
    {
        // Implement if necessary
    }
}
