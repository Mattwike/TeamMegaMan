using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;


public class SniperJoe : IEnemySprite
{
    private int currentFrame;  // Current animation frame
    private int totalFrame;  // Total frames in the animation loop
    private int delayCounter;  // Counter for delaying frame changes
    private int delayMax;  // Maximum delay before advancing to the next frame
    public float x;  // X-coordinate of Sniper Joe's position
    public float y;  // Y-coordinate of Sniper Joe's position
    private float initialY;  // The initial Y-position for Sniper Joe's jump
    public float gravity;  // The gravity effect for jumping
    private Texture2D enemySheet;  // Texture for the Sniper Joe sprite
    private Texture2D projectileTexture;  // Texture for the projectile
    private int enemySizeX;  // Width of Sniper Joe's sprite
    private int enemySizeY;  // Height of Sniper Joe's sprite
    public bool isJumping;  // Flag to check if Sniper Joe is jumping
    public bool isFalling;  // Flag to check if Sniper Joe is falling
    private bool hasShot;  // Flag to track if a projectile has been shot during frame 3
    private bool justLanded; // Flag to detect landing and advance the frame
    private List<IEnemySprite> projectiles;  // List to store projectiles
    public Rectangle SniperJoeBox;
    public bool istouchingfloor;

    public Rectangle hitbox;
    public int health;
    bool isVisible = true;

    private int screenWidth;  // Screen width to manage projectile boundaries

    // Constructor with only the texture parameter, like the other enemy classes
    public SniperJoe(Texture2D texture)
    {
        enemySheet = texture;
        x = 0;  // Set the starting x position of Sniper Joe
        y = 130;  // Set the starting y position of Sniper Joe
        initialY = y;  // Set the initial Y for jumping reference
        gravity = 4.5f;  // Set the gravity for the jump
        currentFrame = 0;
        totalFrame = 4;  // Total number of animation frames
        delayCounter = 0;
        delayMax = 50;  // Slowed down the animation speed
        enemySizeX = 26;  // Default size based on the first frame
        enemySizeY = 24;  // Default size based on the first frame
        isJumping = false;  // Initially, Sniper Joe is not jumping
        isFalling = false;  // Initially, Sniper Joe is not falling
        justLanded = false;  // Track when Sniper Joe just landed
        hasShot = false;  // Track if Sniper Joe has already shot during frame 3
        health = 100;

        // Load default textures and settings
        projectileTexture = texture;  // Here you would load a default projectile texture if available
        screenWidth = 800;  // Assume a default screen width; adjust if necessary

        projectiles = new List<IEnemySprite>();
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        // Any other initialization logic if required
        screenWidth = _graphics.PreferredBackBufferWidth;  // Use the actual screen width if available
    }

    public void Update(GameTime gameTime)
    {
        if (health <= 0)
        {
            isVisible = false;
        }
        if (!isVisible)
        {
            return;
        }
        SniperJoeBox = new Rectangle((int)x, (int)y, 26, 24);
        hitbox.X = (int)x;
        hitbox.Y = (int)y;
        hitbox.Width = 26;
        hitbox.Height = 24;
        // Check if Sniper Joe is in the third frame (index 2), and initiate the jump
        if (currentFrame == 3 && !isJumping && !isFalling && !justLanded)
        {
            // Start the jump on the third frame
            isJumping = true;
            gravity = 4.5f;  // Reset gravity for the jump
        }

        if (!isJumping && !istouchingfloor)
        {
            y += gravity;
        }

        // Handle jumping
        if (isJumping)
        {
            if (gravity > 0)
            {
                y -= gravity;  // Move Sniper Joe up
                gravity -= 0.25f;  // Reduce gravity to simulate slowing down at the peak
            }
            else
            {
                isJumping = false;
                isFalling = true;  // Start falling after reaching the peak
            }
        }

        // Handle falling back down
        else if (isFalling)
        {
            if (!istouchingfloor)
            {
                gravity += 0.25f;  // Increase gravity to simulate speeding up while falling
            }
            else
            {
                isFalling = false;
                justLanded = true;  // Mark that Sniper Joe just landed
                istouchingfloor = false;
            }
        }

        // If Sniper Joe just landed, advance the frame
        if (justLanded)
        {
            currentFrame++;  // Move to the next frame after landing
            if (currentFrame >= totalFrame)
            {
                currentFrame = 0;  // Reset animation to the first frame
            }
            justLanded = false;  // Reset the just landed flag
            delayCounter = 0;  // Reset the delay counter so Sniper Joe doesn't switch frames too quickly
            hasShot = false;  // Reset shooting flag when moving to frame 0 (after jump)
        }

        // If not jumping, falling, or just landed, continue normal frame update based on delay
        if (!isJumping && !isFalling && !justLanded)
        {
            delayCounter++;
            if (delayCounter >= delayMax)
            {
                currentFrame++;
                if (currentFrame >= totalFrame)
                {
                    currentFrame = 0;  // Reset animation to the first frame
                }
                delayCounter = 0;
            }
        }

        // Check if Sniper Joe is in Frame 3 and shoot a projectile once
        if (currentFrame == 2 && !hasShot)
        {
            ShootProjectile();
            hasShot = true;  // Ensure the projectile is only shot once per frame 3
        }

        // Update each projectile
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update(gameTime);

            // Remove the projectile if it's off the screen
            if (((SniperJoeProjectile)projectiles[i]).IsOffScreen())
            {
                projectiles.RemoveAt(i);
            }
        }
    }

    private void ShootProjectile()
    {
        // Create a new projectile at Sniper Joe's gun position
        SniperJoeProjectile projectile = new SniperJoeProjectile(projectileTexture, x, y + 10, screenWidth);
        projectiles.Add(projectile);
    }

    public void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        if (!isVisible)
        {
            return;
        }
        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;

        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        Rectangle sourceRectangle;
        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);

        // Define animation frames based on `currentFrame`
        switch (currentFrame)
        {
            case 0:
                sourceRectangle = new Rectangle(254, 243, 26, 24);  // Frame 1
                enemySizeX = 26;
                enemySizeY = 24;
                break;
            case 1:
                sourceRectangle = new Rectangle(287, 243, 22, 24);  // Frame 2
                enemySizeX = 22;
                enemySizeY = 24;
                break;
            case 2:
                sourceRectangle = new Rectangle(312, 243, 22, 24);  // Frame 3 (shoot frame)
                enemySizeX = 22;
                enemySizeY = 24;
                break;
            case 3:
                sourceRectangle = new Rectangle(337, 243, 28, 31);  // Frame 4 (jump frame)
                enemySizeX = 28;
                enemySizeY = 31;
                break;
            default:
                sourceRectangle = new Rectangle(254, 243, 26, 24);  // Default to Frame 1
                enemySizeX = 26;
                enemySizeY = 24;
                break;
        }

        // Set the destination rectangle size based on the current frame's size
        destinationRectangle.Width = enemySizeX;
        destinationRectangle.Height = enemySizeY;

        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        // Draw each projectile
        foreach (var projectile in projectiles)
        {
            projectile.Draw(_spriteBatch, false, false);  // No flipping required for projectiles
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
    public void TakeDamage()
    {
        health -= 10;
    }
}