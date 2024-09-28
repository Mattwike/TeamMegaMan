using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class Bombomb : ISprite
{
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;
    float x;  // x-coordinate
    float y;  // y-coordinate
    float initialY; // Initial y position to track jumping
    float jumpHeight;  // Height of the jump
    bool isJumping;
    bool hasExploded;

    private Texture2D bombombSheet;
    List<BombombProjectile> projectiles; // List of projectiles spawned after explosion

    Rectangle[] bombombFrames;

    public Bombomb(Texture2D texture, float startX, float startY)
    {
        bombombSheet = texture;
        x = startX;
        y = startY;
        initialY = startY;
        jumpHeight = 20;  // 20px jump height
        isJumping = true; // Start with a jumping behavior
        hasExploded = false;

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

    // Implement the missing Initialize method
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        // Initialization logic, if needed
        currentFrame = 0;
        delayCounter = 0;
        isJumping = true;
        hasExploded = false;
        projectiles.Clear();  // Clear any existing projectiles
    }

    // Implement the missing Draw method matching the interface
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

        // Draw Bombomb
        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, bombombFrames[currentFrame].Width, bombombFrames[currentFrame].Height);
        Rectangle sourceRectangle = bombombFrames[currentFrame];

        _spriteBatch.Begin();
        _spriteBatch.Draw(bombombSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();

        // Draw all projectiles
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
                Explode();  // Spawn projectiles
            }
        }

        // Update all projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Update(gameTime);
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
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX - 20, projectileY, 800, -5, 0));  // Left projectile
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX + 20, projectileY, 800, 5, 0));   // Right projectile
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX - 40, projectileY, 800, -10, 0)); // Far left projectile
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX + 40, projectileY, 800, 10, 0));  // Far right projectile
    }
}
