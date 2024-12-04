using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;

public class KillerBomb : IEnemySprite
{
    private Texture2D enemyTexture;  // Texture for Killer Bomb
    private float positionX;  // X-coordinate of the enemy's position
    private float positionY;  // Y-coordinate of the enemy's position
    private float speedX;  // Speed for horizontal movement (negative for moving left)
    private float oscillationFrequency;  // Controls the speed of up and down movement
    private float originalY;  // Initial y-position to keep track of oscillation
    private int screenWidth;  // Screen width to manage boundaries
    private int screenHeight;  // Screen height to manage boundaries
    private int width;  // Width of the enemy sprite
    private int height;  // Height of the enemy sprite
    private int origonalX;

    private Rectangle sourceRectangle;  // Source rectangle for the sprite from the sprite sheet
    private Rectangle hitbox;  // Hitbox for collision detection
    private bool isVisible;  // Visibility flag

    // Health and other properties required by IEnemySprite
    public int health { get; private set; }
    public bool hasProjectiles { get; set; }
    public bool hitWall { get; set; }

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

    // Constructor to initialize the texture
    public KillerBomb(Texture2D texture, Vector2 position)
    {
        enemyTexture = texture;
        SetPosition(position);
        originalY = positionY;  // Store the initial y-coordinate for oscillation

        // Set default values for speed and oscillation
        width = 16;
        height = 16;
        speedX = -1f;  // Moves left at a basic speed of 1
        oscillationFrequency = 1.5f;  // Frequency of the vertical movement

        // Define the source rectangle for the single frame
        sourceRectangle = new Rectangle(114, 212, 16, 16);

        health = 100;
        isVisible = true;
        hasProjectiles = false;
        origonalX = x;
    }

    // Initialize method to set screen boundaries and speed
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        screenWidth = graphics.PreferredBackBufferWidth;  // Set screen boundaries
        screenHeight = graphics.PreferredBackBufferHeight;

        // Adjust movement speeds and sprite size
        speedX = -movementSpeed;  // Move left
        oscillationFrequency = 1.5f;  // Set the frequency for oscillation
        width = 20;
        height = 20;

        // Update hitbox
        hitbox = new Rectangle((int)positionX, (int)positionY, width, height);
    }

    // Update method to handle movement logic
    public void Update(GameTime gameTime, Camera camera, int megamanX)
    {

        // Move left at a constant speed
        positionX += speedX;

        // Oscillate up and down using a sine function to simulate smooth movement
        positionY = originalY + (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * oscillationFrequency) * 50);

        // Update hitbox
        hitbox = new Rectangle((int)positionX, (int)positionY, width, height);

        // Remove or deactivate the enemy if it goes off-screen to the left
        if (positionX + width < 0)
        {
            isVisible = false;
        }
    }

    // Draw method to render the sprite on the screen
    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        if (!isVisible)
        {
            positionX = origonalX;  // Do not draw if the enemy is not visible (dead)
            positionY = originalY;
            isVisible = true;
        }

        SpriteEffects spriteEffects = SpriteEffects.None;

        // Handle flipping if necessary
        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;

        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        // Define the destination rectangle where the sprite will be drawn on the screen
        Rectangle destinationRectangle = new Rectangle((int)positionX, (int)positionY, width, height);

        // Draw the sprite using its texture and source rectangle
        spriteBatch.Draw(enemyTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
    }

    // Get the rectangle for collision detection
    public Rectangle getRectangle()
    {
        return hitbox;
    }

    // Get the health of the enemy
    public int GetHealth()
    {
        return health;
    }

    // Handle taking damage
    public void TakeDamage(List<EnemyDrop> enemyDropList)
    {
        health -= 10;
        if (health <= 0)
        {
            // Handle enemy death and drops
            EnemyDrop enemyDrop = new EnemyDrop();
            enemyDrop.Initialize(null, x, y);  // Adjust parameters as needed
            enemyDropList.Add(enemyDrop);

            // Remove or deactivate the enemy
            isVisible = false;

            // Modify the hitbox by creating a new Rectangle
            hitbox = new Rectangle(hitbox.X, hitbox.Y + 1000, hitbox.Width, hitbox.Height);
        }
    }


    // Set the position of the enemy
    public void SetPosition(Vector2 position)
    {
        positionX = position.X;
        positionY = position.Y;
    }

    // Handle touching the floor (implement as needed)
    public void isTouchingFloor()
    {
        // Implement if necessary
    }

    public List<IEnemyProjectile> GetProjectiles()
    {
        return null;
    }
}
