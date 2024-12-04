using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;
using Project1.GameObjects;

public class RedBlaster : IEnemySprite
{
    private int currentFrame;
    private int totalFrames;
    private int delayCounter;
    private int delayMax;
    private float positionX;
    private float positionY;
    private int screenWidth;
    private int screenHeight;
    private Texture2D blasterSheet;
    private int blasterSizeX;
    private int blasterSizeY;
    private bool hasShot;  // Flag to track if a projectile has been shot during the shooting frame
    private bool isVisible;  // Flag to determine if the enemy is visible (alive)
    public bool hasProjectiles { get; set; }

    // List of active projectiles
    private List<IEnemyProjectile> projectiles;

    // Define source frame dimensions for red blaster animation
    private Rectangle[] redBlasterFrames;

    // Health and other properties required by IEnemySprite
    public Rectangle hitbox { get; private set; }
    public int health { get; private set; }

    public int y { get; set; }  // Integer position
    public int x { get; set; }  // Integer position
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public bool hitWall { get; set; }
    public float Gravity
    {
        set { gravity = 4.5f; }
    }

    private float movementSpeed;  // Movement speed (if applicable)
    private Random random;  // Random number generator for projectile angles

    // Constructor
    public RedBlaster(Texture2D texture, Vector2 position)
    {
        blasterSheet = texture;
        SetPosition(position);

        redBlasterFrames = new Rectangle[]
        {
            new Rectangle(319, 11, 9, 16),  // Frame 1
            new Rectangle(329, 11, 8, 16),  // Frame 2
            new Rectangle(345, 11, 9, 16),  // Frame 3
            new Rectangle(356, 11, 15, 16)  // Frame 4 (shooting)
        };

        // Initialize the list of projectiles
        projectiles = new List<IEnemyProjectile>();

        positionX = x;
        positionY = y;

        health = 100;  // Set initial health
        isVisible = true;  // Enemy is visible at the start
        hasShot = false;  // Initially, no projectile has been shot
        hasProjectiles = true;

        // Initialize random number generator
        random = new Random();
    }

    // Initialize method
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        currentFrame = 0;
        totalFrames = redBlasterFrames.Length;
        delayCounter = 0;
        delayMax = 15;  // Adjust as needed for animation speed
        blasterSizeX = 20;
        blasterSizeY = 20;

        screenWidth = graphics.PreferredBackBufferWidth;
        screenHeight = graphics.PreferredBackBufferHeight;

        this.movementSpeed = Math.Abs(movementSpeed);  // Ensure movementSpeed is positive
    }

    // Update method with Camera parameter
    public void Update(GameTime gameTime, Camera camera, int megamanX)
    {
        if (!isVisible)
        {
            return;
        }

        delayCounter++;
        if (delayCounter >= delayMax)
        {
            currentFrame++;
            if (currentFrame >= totalFrames)
            {
                currentFrame = 0;
                hasShot = false;
            }
            delayCounter = 0;
        }

        if (currentFrame == totalFrames - 1 && !hasShot)
        {
            ShootProjectile();
            hasShot = true;
        }

        // Update all projectiles
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update(gameTime, camera, megamanX);

            if (projectiles[i].IsOffScreen(camera))
            {
                projectiles.RemoveAt(i);
            }
        }

        hitbox = new Rectangle(x, y, blasterSizeX, blasterSizeY);
    }

    // Draw method
    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        if (!isVisible)
        {
            return;  // Do not draw if the enemy is not visible (dead)
        }

        // Flip the sprite horizontally to face right
        if (y > 700)
        {
            flipHorizontally = true;
        }

        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;

        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        // Define destination rectangle

        Rectangle destinationRectangle = new Rectangle(x, y, blasterSizeX, blasterSizeY);
        Rectangle sourceRectangle = redBlasterFrames[currentFrame];

        // Draw RedBlaster
        spriteBatch.Draw(blasterSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        // Draw all projectiles
        foreach (var projectile in projectiles)
        {
            // Do not flip the projectile sprite
            projectile.Draw(spriteBatch, false, flipVertically);
        }
    }

    // Method to shoot a projectile
    private void ShootProjectile()
    {
        // Possible speedY values for different angles
        float[] possibleSpeedY = { 0f, -2f, 2f };  // 0 for straight, -2 for upward, 2 for downward

        // Randomly select a speedY value
        int index = random.Next(possibleSpeedY.Length);
        float selectedSpeedY = possibleSpeedY[index];

        float projectileSpeedX = movementSpeed;  // Should be positive to move to the right

        // Create a new projectile at RedBlaster's position
        var projectile = new RedBlasterProjectile(
            blasterSheet,
            x + blasterSizeX,   // Start at the right edge of the RedBlaster
            y + (blasterSizeY / 2),  // Adjust Y position as needed
            screenWidth,
            screenHeight,
            projectileSpeedX,
            selectedSpeedY
        );

        projectile.Initialize(null, projectileSpeedX, 12);  // Adjust size as needed
        projectiles.Add(projectile);
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
            y += 1000;  // Move off-screen

            // Modify the hitbox by creating a new Rectangle
            hitbox = new Rectangle(hitbox.X, hitbox.Y + 1000, hitbox.Width, hitbox.Height);
        }
    }

    // Set the position of the enemy
    public void SetPosition(Vector2 position)
    {
        x = (int)position.X;
        y = (int)position.Y;

        positionX = position.X;
        positionY = position.Y;
    }

    // Handle touching the floor (implement as needed)
    public void isTouchingFloor()
    {
        // Implement if necessary
    }

    // Provide access to the projectiles for collision handling
    public List<IEnemyProjectile> GetProjectiles()
    {
        return projectiles;
    }
}
