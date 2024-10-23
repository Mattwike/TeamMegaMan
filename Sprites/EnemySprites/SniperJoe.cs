using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class SniperJoe : IEnemySprite
{
    private int currentFrame;  // Current animation frame
    private int totalFrame;  // Total frames in the animation loop
    private int delayCounter;  // Counter for delaying frame changes
    private int delayMax;  // Maximum delay before advancing to the next frame
    private float x;  // X-coordinate of Sniper Joe's position
    private float y;  // Y-coordinate of Sniper Joe's position
    private float initialY;  // The initial Y-position for Sniper Joe's jump
    private float gravity;  // The gravity effect for jumping
    private Texture2D enemySheet;  // Texture for the Sniper Joe sprite
    private int enemySizeX;  // Width of Sniper Joe's sprite
    private int enemySizeY;  // Height of Sniper Joe's sprite
    private bool isJumping;  // Flag to check if Sniper Joe is jumping
    private bool isFalling;  // Flag to check if Sniper Joe is falling
    private bool justLanded; // Flag to detect landing and advance the frame

    // Constructor to initialize Sniper Joe with texture and position
    public SniperJoe(Texture2D texture, float startX, float startY)
    {
        enemySheet = texture;
        x = startX;  // Set the starting x position of Sniper Joe
        y = startY;  // Set the starting y position of Sniper Joe
        initialY = startY;  // Set the initial Y for jumping reference
        gravity = 4.5f;  // Set the gravity for the jump
        currentFrame = 0;
        totalFrame = 4;  // Total number of animation frames
        delayCounter = 0;
        delayMax = 50;  // Slowed down the animation speed
        enemySizeX = 26;  // Default size based on the first frame
        enemySizeY = 24;  // Default size based on the first frame
        isJumping = false;  // Initially, Sniper Joe is not jumping
        isFalling = false;  // Initially, Sniper Joe is not falling
        justLanded = false; // Track when Sniper Joe just landed
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        // Any other initialization logic if required
    }

    public void Update(GameTime gameTime)
    {
        // Check if Sniper Joe is in the third frame (index 2), and initiate the jump
        if (currentFrame == 3 && !isJumping && !isFalling && !justLanded)
        {
            // Start the jump on the third frame
            isJumping = true;
            gravity = 4.5f;  // Reset gravity for the jump
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
            if (y < initialY)
            {
                y += gravity;  // Move Sniper Joe down
                gravity += 0.25f;  // Increase gravity to simulate speeding up while falling
            }
            else
            {
                y = initialY;  // Reset to the ground level
                isFalling = false;
                justLanded = true;  // Mark that Sniper Joe just landed
                gravity = 4.5f;  // Reset gravity for the next jump
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
    }

    public void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically)
    {
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
                sourceRectangle = new Rectangle(312, 243, 22, 24);  // Frame 3 (jump frame)
                enemySizeX = 22;
                enemySizeY = 24;
                break;
            case 3:
                sourceRectangle = new Rectangle(337, 243, 28, 31);  // Frame 4
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

        _spriteBatch.Begin();
        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();
    }
}
