using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Bombomb : ISprite
{
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;
    float x;  // x-coordinate of Bombomb
    float y;  // y-coordinate of Bombomb
    float initialX;  // Store the initial x position
    float initialY;  // Store the initial y position
    float jumpHeight;  // Height of the jump
    bool isJumping;
    bool hasExploded;
    bool isVisible;  // Added: Visibility flag to hide the Bombomb sprite

    private int screenHeight;  // Screen height for boundary detection

    private Texture2D bombombSheet;
    List<BombombProjectile> projectiles; // List of projectiles spawned after explosion

    Rectangle[] bombombFrames;

    public Bombomb(Texture2D texture, float startX, float startY)
    {
        bombombSheet = texture;
        x = startX;
        y = startY;
        initialX = startX;  // Save the initial position for resetting
        initialY = startY;
        jumpHeight = 100;  // 20px jump height
        isJumping = true; // Start with a jumping behavior
        hasExploded = false;
        isVisible = true; // Start by making Bombomb visible

        screenHeight = 600;  // Default screen height (set to match your game screen size)

        // Define animation frames for Bombomb (adjust according to your sprite sheet)
        bombombFrames = new Rectangle[]
        {
            new Rectangle(400, 21, 16, 12), // Bombomb sprite frame
        };

        currentFrame = 0;
        totalFrame = bombombFrames.Length;
        delayCounter = 0;
        delayMax = 10;  // Adjust frame rate as needed
        projectiles = new List<BombombProjectile>();
    }

    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        currentFrame = 0;
        delayCounter = 0;
        isJumping = true;
        hasExploded = false;
        isVisible = true; // Make Bombomb visible when initialized
        projectiles.Clear();  // Clear any existing projectiles
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

        // Only draw Bombomb if it is visible
        if (isVisible)
        {
            Rectangle destinationRectangle = new Rectangle((int)x, (int)y, bombombFrames[currentFrame].Width, bombombFrames[currentFrame].Height);
            Rectangle sourceRectangle = bombombFrames[currentFrame];

            _spriteBatch.Begin();
            _spriteBatch.Draw(bombombSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
            _spriteBatch.End();
        }

        // Draw all projectiles regardless of Bombomb's visibility
        foreach (var projectile in projectiles)
        {
            projectile.Draw(_spriteBatch, flipHorizontally, flipVertically);
        }
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

        // Jumping logic
        if (isJumping && !hasExploded)
        {
            y -= 1;  // Move up by 1px per frame

            // Check if Bombomb has reached its jump height
            if (initialY - y >= jumpHeight)
            {
                // Stop jumping and explode
                isJumping = false;
                hasExploded = true;
                isVisible = false;  // Hide Bombomb when it explodes
                Explode();  // Spawn projectiles
            }
        }

        // Update all projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Update(gameTime);
        }

        // Check if all projectiles are off the screen
        if (projectiles.Count > 0 && projectiles.TrueForAll(p => p.IsOffScreen()))
        {
            ResetBombomb();  // Reset Bombomb position and restart animation
        }

        // Remove projectiles that go off-screen or disappear after some time
        projectiles.RemoveAll(p => p.IsOffScreen());
    }

    private void Explode()
    {
        // Define explosion positions
        float projectileX = x;
        float projectileY = y - bombombFrames[currentFrame].Height / 2;  // Center of explosion

        // Create projectiles (4 in total)
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX - 10, projectileY, 800, -0.25f));  // Left projectile
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX + 10, projectileY, 800, 0.25f));   // Right projectile
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX - 50, projectileY, 800, -0.25f));  // Another left projectile
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX + 50, projectileY, 800, 0.25f));   // Another right projectile
    }

    private void ResetBombomb()
    {
        // Reset Bombomb to its initial position
        x = initialX;
        y = initialY;

        // Clear all projectiles
        projectiles.Clear();

        // Reset the jump and explosion state
        isJumping = true;
        hasExploded = false;
        isVisible = true;  // Make Bombomb visible again
    }
}
