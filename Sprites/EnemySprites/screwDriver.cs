using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class screwDriver : IEnemySprite
{
    int currentFrame;  // Current animation frame
    int totalFrame;  // Total frames in the animation loop
    int delayCounter;  // Counter for delaying frame changes
    int delayMax;  // Maximum delay before advancing to the next frame
    float x;  // X-coordinate of the screwDriver sprite
    float y;  // Y-coordinate of the screwDriver sprite
    private Texture2D enemySheet;  // Texture for the screwDriver sprite
    int enemySizeX;  // Width of the screwDriver sprite
    int enemySizeY;  // Height of the screwDriver sprite

    List<ScrewDriverProjectile> projectiles;  // List to store projectiles
    private bool hasFiredFrame4;  // Track if projectiles have been fired on frame 4
    private bool hasFiredFrame5;  // Track if projectiles have been fired on frame 5
    private int screenWidth = 800;  // Set this to your game's actual screen width
    private int screenHeight = 600;  // Set this to your game's actual screen height

    public screwDriver(Texture2D texture)
    {
        enemySheet = texture;
        x = 400;  // Set the starting x position of the screwDriver
        y = 40;  // Set the starting y position of the screwDriver
        projectiles = new List<ScrewDriverProjectile>();  // Initialize the projectile list
        hasFiredFrame4 = false;  // Reset frame-specific firing flags
        hasFiredFrame5 = false;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrame = 50;  // Total frames in the animation
        delayCounter = 0;
        delayMax = 5;  // Delay for frame update speed
        enemySizeX = megamanSize;
        enemySizeY = megamanSize;
    }

    public void Update(GameTime gameTime)
    {
        // Update animation frame based on delay
        delayCounter++;
        if (delayCounter >= delayMax)
        {
            currentFrame++;
            if (currentFrame >= totalFrame)
            {
                currentFrame = 0;
                hasFiredFrame4 = false;  // Reset firing states when the animation resets
                hasFiredFrame5 = false;
            }
            delayCounter = 0;
        }

        // Fire projectiles on the 4th frame
        if (currentFrame == 4 && !hasFiredFrame4)
        {
            FireProjectiles();  // Call method to fire projectiles
            hasFiredFrame4 = true;  // Set flag to prevent multiple firings on the same frame
        }

        // Fire projectiles on the 5th frame
        if (currentFrame == 5 && !hasFiredFrame5)
        {
            FireProjectiles();  // Call method to fire projectiles
            hasFiredFrame5 = true;  // Set flag to prevent multiple firings on the same frame
        }

        // Update all projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Update(gameTime);
        }

        // Remove projectiles that are off-screen
        projectiles.RemoveAll(p => p.IsOffScreen());
    }

    private void FireProjectiles()
    {
        // Define the starting position for the projectiles relative to the screwDriver position
        float projectileStartX = x + enemySizeX / 2;  // Start in the middle of the enemy horizontally
        float projectileStartY = y;  // Start at the top of the enemy vertically

        // Create and add each projectile with the desired direction and speed
        projectiles.Add(CreateProjectile(projectileStartX, projectileStartY, 0f, -5f));    // Upwards
        projectiles.Add(CreateProjectile(projectileStartX, projectileStartY, -3f, -3f));   // Top-left diagonal
        projectiles.Add(CreateProjectile(projectileStartX, projectileStartY, 3f, -3f));    // Top-right diagonal
        projectiles.Add(CreateProjectile(projectileStartX, projectileStartY, -5f, 0f));    // Leftward
        projectiles.Add(CreateProjectile(projectileStartX, projectileStartY, 5f, 0f));     // Rightward
    }

    private ScrewDriverProjectile CreateProjectile(float startX, float startY, float speedX, float speedY)
    {
        var projectile = new ScrewDriverProjectile(enemySheet, startX, startY, screenWidth, screenHeight);
        projectile.speedX = speedX;
        projectile.speedY = speedY;
        return projectile;
    }

    public void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally) spriteEffects |= SpriteEffects.FlipHorizontally;
        if (flipVertically) spriteEffects |= SpriteEffects.FlipVertically;

        Rectangle sourceRectangle;
        Rectangle destinationRectangle;

        // Ensure the destination rectangle is always set to the same size to prevent scaling issues
        destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);

        // Define animation frames based on `currentFrame`
        if (currentFrame < 10)
            sourceRectangle = new Rectangle(155, 250, 18, 18);  // Initial animation frame
        else if (currentFrame < 20)
            sourceRectangle = new Rectangle(172, 250, 18, 18);  // 2nd animation frame
        else if (currentFrame < 30)
            sourceRectangle = new Rectangle(189, 250, 18, 18);  // 3rd animation frame
        else if (currentFrame < 40)
            sourceRectangle = new Rectangle(206, 250, 18, 18);  // 4th animation frame (fires here)
        else
            sourceRectangle = new Rectangle(224, 251, 18, 18);  // 5th animation frame (fires here)
        // Draw the screwDriver sprite
        _spriteBatch.Begin();  // Begin drawing here to ensure it affects only this section
        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();  // End drawing here to properly manage begin/end blocks

        // Draw all projectiles (they will also have their own Begin/End if needed)
        foreach (var projectile in projectiles)
        {
            projectile.Draw(_spriteBatch, false, false);
        }
//         _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
    }
}
