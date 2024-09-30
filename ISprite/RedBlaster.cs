using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class RedBlaster : ISprite
{
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;
    float x;  // x-coordinate
    float y;  // y-coordinate
    private Texture2D blasterSheet;
    int blasterSizeX;
    int blasterSizeY;

    // List of active projectiles
    List<RedBlasterProjectile> projectiles;

    // Define source frame dimensions for red blaster animation
    Rectangle[] redBlasterFrames;

    // Cooldown-related variables
    float shotCooldownMax = 0.5f;  // Cooldown time between shots (in seconds)
    float shotCooldownTimer = 0f;  // Timer to track when the next shot can be fired

    public RedBlaster(Texture2D texture)
    {
        blasterSheet = texture;
        x = 650;
        y = 100;

        redBlasterFrames = new Rectangle[]
        {
            new Rectangle(319, 11, 9, 16),  // frame 1
            new Rectangle(329, 11, 8, 16),
            new Rectangle(345, 11, 9, 16),
            new Rectangle(356, 11, 15, 16)
        };

        // Initialize the list of projectiles
        projectiles = new List<RedBlasterProjectile>();
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamansize)
    {
        currentFrame = 0;
        totalFrame = redBlasterFrames.Length;
        delayCounter = 0;
        delayMax = 15;
        blasterSizeX = megamansize;
        blasterSizeY = megamansize;
    }

    public void Update(GameTime gameTime)
    {
        delayCounter++;
        if (delayCounter >= delayMax)
        {
            currentFrame++;
            if (currentFrame >= totalFrame)
            {
                currentFrame = 0;
            }
            delayCounter = 0;
        }

        // Update the shot cooldown timer
        shotCooldownTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;

        // Only add a new projectile if enough time has passed (cooldown) and we are in the last frame
        if (currentFrame == totalFrame - 1 && shotCooldownTimer >= shotCooldownMax)
        {
            // Decrease the y position by 10 pixels (or whatever value you prefer)
            float adjustedY = y + 10;

            // Instantiate a new projectile with the adjusted y value
            projectiles.Add(new RedBlasterProjectile(blasterSheet, x, adjustedY, 800)); // Example screen width

            shotCooldownTimer = 0f;  // Reset the cooldown timer after firing
        }

        // Update all projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Update(gameTime);
        }

        // Remove projectiles that go off-screen
        projectiles.RemoveAll(p => p.IsOffScreen());
    }


    public void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
        {
            spriteEffects |= SpriteEffects.FlipHorizontally;
        }

        if (flipVertically)
        {
            spriteEffects |= SpriteEffects.FlipVertically;
        }

        // Begin the SpriteBatch for drawing RedBlaster
        _spriteBatch.Begin();

        // Draw RedBlaster itself
        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, blasterSizeX, blasterSizeY);
        Rectangle sourceRectangle = redBlasterFrames[currentFrame];

        _spriteBatch.Draw(blasterSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        _spriteBatch.End();

        // Now handle drawing the projectiles
        foreach (var projectile in projectiles)
        {
            // Each projectile will handle its own Begin/End
            projectile.Draw(_spriteBatch, flipHorizontally, flipVertically);
        }
    }
}
