using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using System;
using System.Collections.Generic;

public class Mambu : IEnemySprite
{
    private Texture2D enemyTexture;  // Texture for Mambu
    private float positionX;  // Precise X-coordinate
    private float initialPositionX;  // Initial X position for resetting
    private float speedX;  // Speed for horizontal movement
    private float originalSpeedX;  // Store original speed for resuming movement
    private int screenWidth;  // Screen width to manage boundaries
    private int screenHeight; // Screen height to manage boundaries
    private int width;  // Width of the Mambu sprite
    private int height;  // Height of the Mambu sprite

    private int currentFrame;  // Current animation frame
    private float frameTimer;  // Timer for switching frames
    private bool isShooting;  // Flag to determine if Mambu is shooting
    private bool hasShotProjectiles;  // Flag to ensure projectiles are shot only once per frame change
    private Vector2 lockedPosition;  // Lock the position when shooting starts

    private Rectangle[] frames;  // Array of rectangles for animation frames
    private Rectangle projectileFrame;  // Frame for projectile
    private List<MambuProjectile> projectiles;  // List of projectiles using MambuProjectile class

    private float projectileSpeed = 2f;  // Speed of the projectiles
    private int movementSpeed = 2;  // Movement speed for Mambu

    public Rectangle hitbox;
    public int health;

    public int y { get; set; }
    public int x { get; set; }
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public float Gravity
    {
        set { gravity = 4.5f; }
    }

    // Constructor to initialize Mambu with its texture
    public Mambu(Texture2D texture, Vector2 position)
    {
        enemyTexture = texture;
        SetPosition(position);
        projectiles = new List<MambuProjectile>();  // Initialize projectile list

        positionX = x;  // Initialize precise position
        initialPositionX = positionX;  // Store the initial x position for resetting
        width = 16;  // Width of the first frame
        height = 16;  // Height of the first frame

        // Set up frame information for animation
        frames = new Rectangle[2];  // Mambu has 2 frames
        frames[0] = new Rectangle(301, 181, 16, 16);  // Frame 1
        frames[1] = new Rectangle(319, 176, 17, 21);  // Frame 2

        // Projectile frame
        projectileFrame = new Rectangle(338, 182, 6, 6);

        currentFrame = 0;  // Start at frame 0
        frameTimer = 0f;  // Initialize frame timer
        isShooting = false;  // Initially not shooting
        hasShotProjectiles = false;  // Ensure projectiles are shot only once

        speedX = 1f;  // Horizontal movement speed
        originalSpeedX = speedX;  // Store the original speed for resuming movement
    }

    // Initialize method to set position, screen boundaries, and other properties
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        // Removed the line that overwrote initialPositionX

        screenWidth = graphics.PreferredBackBufferWidth;  // Set screen boundaries
        screenHeight = graphics.PreferredBackBufferHeight;

        width = 24;  // Set sprite size based on the passed size
        height = 24;  // Keep height same as width for aspect ratio

        speedX = movementSpeed / 4f;  // Adjust horizontal speed based on movement speed parameter
        originalSpeedX = speedX;  // Store the original speed for resuming

        if (speedX == 0)
        {
            speedX = 1f;  // Ensure speedX is not zero
        }
    }

    // Update method to handle movement, frame switching, and projectile shooting
    public void Update(GameTime gameTime)
    {
        frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Switch frames and handle frame timing
        if (currentFrame == 0 && frameTimer >= 2f)  // 2 seconds on Frame 1
        {
            currentFrame = 1;  // Switch to Frame 2
            frameTimer = 0;  // Reset frame timer
            isShooting = true;  // Trigger shooting
            speedX = 0f;  // Stop Mambu's movement on Frame 2
            lockedPosition = new Vector2(x, y);  // Lock Mambu's position for centralized shooting
        }
        else if (currentFrame == 1 && frameTimer >= 2f)  // 2 seconds on Frame 2
        {
            currentFrame = 0;  // Switch back to Frame 1
            frameTimer = 0;  // Reset frame timer
            speedX = originalSpeedX;  // Resume Mambu's movement when returning to Frame 1
            hasShotProjectiles = false;  // Reset the projectile shooting flag
        }

        // Shoot projectiles if switching to Frame 2 and not shot already
        if (isShooting && currentFrame == 1 && !hasShotProjectiles)
        {
            ShootProjectiles();  // Call the method to shoot projectiles
            isShooting = false;  // Reset shooting flag after firing once
            hasShotProjectiles = true;  // Ensure projectiles are shot only once
        }

        // Move left continuously unless speedX is zero
        positionX -= speedX;  // Update precise position

        // Update integer x position
        x = (int)positionX;

        // Reset position when reaching the left side of the screen
        if (positionX < 0)
        {
            positionX = initialPositionX;  // Reset to the initial position on the right side of the screen
            x = (int)positionX;
        }

        // Update all projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Update(gameTime);
        }

        // Remove projectiles that are off-screen
        projectiles.RemoveAll(p => p.IsOffScreen());

        // Update hitbox position
        hitbox = new Rectangle(x, y, width, height);
    }

    // Draw method to render Mambu and its projectiles on the screen
    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;

        // Handle flipping if necessary
        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;

        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        // Define the destination rectangle where the sprite will be drawn on the screen
        Rectangle destinationRectangle = new Rectangle(x, y, width, height);

        // Draw the sprite using its texture and current frame rectangle
        spriteBatch.Draw(enemyTexture, destinationRectangle, frames[currentFrame], Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        // Draw all projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Draw(spriteBatch, false, false);
        }
    }

    // Method to shoot 8 projectiles in a circular pattern, using locked position for centralized origin
    private void ShootProjectiles()
    {
        // Use locked position to ensure projectiles are centralized
        float originX = lockedPosition.X;
        float originY = lockedPosition.Y;

        // Calculate positions and directions for 8 projectiles forming a circle
        for (int i = 0; i < 8; i++)
        {
            // Calculate angle in radians for each projectile
            float angle = MathHelper.ToRadians(i * 45);  // 8 projectiles, 360 degrees / 8 = 45 degrees apart

            // Calculate x and y speed components based on the angle
            float projSpeedX = projectileSpeed * (float)Math.Cos(angle);
            float projSpeedY = projectileSpeed * (float)Math.Sin(angle);

            // Create and initialize a new MambuProjectile at locked position
            var projectile = new MambuProjectile(enemyTexture, originX, originY, screenWidth, screenHeight)
            {
                speedX = projSpeedX,
                speedY = projSpeedY,
                width = 6,  // Width of the projectile
                height = 6  // Height of the projectile
            };

            // Add the projectile to the projectiles list
            projectiles.Add(projectile);
        }
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

    public void SetPosition(Vector2 position)
    {
        x = (int)position.X;
        y = (int)position.Y;
        positionX = position.X;  // Initialize precise position
        initialPositionX = positionX;  // Set initial position for resetting
    }
    public void isTouchingFloor()
    {
        //istouchingfloor = false;
    }
}
