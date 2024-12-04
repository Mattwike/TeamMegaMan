// Mambu.cs

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Project1.GameObjects;  // Adjust this namespace if necessary

public class Mambu : IEnemySprite
{
    // Fields
    private Texture2D enemyTexture;
    private float positionX;
    private float initialPositionX;
    private float speedX;
    private float originalSpeedX;
    private int screenWidth;
    private int screenHeight;
    private int width;
    private int height;

    private int currentFrame;
    private float frameTimer;
    private bool isShooting;
    private bool hasShotProjectiles;
    private Vector2 lockedPosition;

    private Rectangle[] frames;
    private Rectangle projectileFrame;
    private List<MambuProjectile> projectiles;

    private float projectileSpeed = 2f;
    private int movementSpeed = 2;

    public Rectangle hitbox;
    public int health;

    public int y { get; set; }
    public int x { get; set; }
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public bool hitWall { get; set; }
    public float Gravity { set { gravity = 4.5f; } }

    private bool isVisible;

    // Constructor
    public Mambu(Texture2D texture, Vector2 position)
    {
        enemyTexture = texture;
        SetPosition(position);
        projectiles = new List<MambuProjectile>();

        positionX = x;
        initialPositionX = positionX;
        width = 16;  // Initial size; will be set in Initialize
        height = 16;

        frames = new Rectangle[2];
        frames[0] = new Rectangle(301, 181, 16, 16);  // Frame 1
        frames[1] = new Rectangle(319, 176, 17, 21);  // Frame 2

        projectileFrame = new Rectangle(338, 182, 6, 6);  // Projectile frame

        currentFrame = 0;
        frameTimer = 0f;
        isShooting = false;
        hasShotProjectiles = false;

        speedX = 1f;  // Initial movement speed
        originalSpeedX = speedX;

        health = 100;  // Set initial health
        isVisible = true;
    }

    // Initialize method
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        screenWidth = graphics.PreferredBackBufferWidth;
        screenHeight = graphics.PreferredBackBufferHeight;

        // Adjust size based on 'size' parameter if necessary
        width = 20;   // Example: setting width based on size
        height = 20;  // Example: setting height based on size

        speedX = movementSpeed / 4f;  // Adjust speed based on movementSpeed
        originalSpeedX = speedX;

        if (speedX == 0)
        {
            speedX = 1f;  // Prevent zero speed
        }
    }

    // Update method with Camera parameter
    public void Update(GameTime gameTime, Camera camera, int megamanX)
    {
        if (!isVisible)
        {
            return;  // Do not update if the enemy is not visible (dead)
        }

        frameTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Frame switching logic with delay
        if (currentFrame == 0 && frameTimer >= 2f)  // 2 seconds on Frame 1
        {
            currentFrame = 1;
            frameTimer = 0f;
            isShooting = true;
            speedX = 0f;  // Stop movement when shooting
            lockedPosition = new Vector2(x + width / 2, y + height / 2);  // Center position
        }
        else if (currentFrame == 1 && frameTimer >= 2f)  // 2 seconds on Frame 2
        {
            currentFrame = 0;
            frameTimer = 0f;
            speedX = originalSpeedX;  // Resume movement
            hasShotProjectiles = false;  // Reset shooting flag
        }

        // Shoot projectiles if it's time
        if (isShooting && currentFrame == 1 && !hasShotProjectiles)
        {
            ShootProjectiles();
            isShooting = false;
            hasShotProjectiles = true;
        }

        // Movement
        positionX -= speedX;  // Move left by speedX
        x = (int)positionX;

        // Reset position when reaching the left side of the screen
        if (positionX < -width)  // Ensure the entire sprite moves off-screen
        {
            positionX = initialPositionX;
            x = (int)positionX;
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

        // Update hitbox position
        hitbox = new Rectangle(x, y, width, height);
    }

    // Draw method
    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        if (!isVisible)
        {
            return;  // Do not draw if the enemy is not visible (dead)
        }

        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;
        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        // Define destination rectangle
        Rectangle destinationRectangle = new Rectangle(x, y, width, height);
        Rectangle sourceRectangle = frames[currentFrame];

        // Draw Mambu
        spriteBatch.Draw(enemyTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        // Draw projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Draw(spriteBatch, false, false);
        }
    }

    // Method to shoot projectiles in all directions
    private void ShootProjectiles()
    {
        // Create 8 projectiles in a circular pattern
        for (int i = 0; i < 8; i++)
        {
            float angle = MathHelper.ToRadians(i * 45);  // 45 degrees apart
            float projSpeedX = projectileSpeed * (float)Math.Cos(angle);
            float projSpeedY = projectileSpeed * (float)Math.Sin(angle);

            // Create a new projectile at Mambu's center
            var projectile = new MambuProjectile(
                enemyTexture,
                x + width / 2,
                y + height / 2,
                screenWidth,
                screenHeight
            )
            {
                speedX = projSpeedX,
                speedY = projSpeedY,
                width = 6,  // Set projectile size appropriately
                height = 6
            };

            projectiles.Add(projectile);
        }
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
            hitbox = new Rectangle(hitbox.X, hitbox.Y + 1000, hitbox.Width, hitbox.Height);
        }
    }

    // Set the position of the enemy
    public void SetPosition(Vector2 position)
    {
        x = (int)position.X;
        y = (int)position.Y;

        positionX = position.X;
        initialPositionX = positionX;
    }

    // Handle touching the floor (implement as needed)
    public void isTouchingFloor()
    {
        // Implement if necessary
    }
}
