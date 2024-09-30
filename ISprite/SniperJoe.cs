using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

public class SniperJoe : ISprite
{
    private int currentFrame;  // Current animation frame
    private int totalFrame;  // Total frames in the animation loop
    private int delayCounter;  // Counter for delaying frame changes
    private int delayMax;  // Maximum delay before advancing to the next frame
    private float x;  // X-coordinate of Sniper Joe's position
    private float y;  // Y-coordinate of Sniper Joe's position
    private Texture2D enemySheet;  // Texture for the Sniper Joe sprite
    private int enemySizeX;  // Width of Sniper Joe's sprite
    private int enemySizeY;  // Height of Sniper Joe's sprite

    // Constructor to initialize Sniper Joe with texture and position
    public SniperJoe(Texture2D texture, float startX, float startY)
    {
        enemySheet = texture;
        x = startX;  // Set the starting x position of Sniper Joe
        y = startY;  // Set the starting y position of Sniper Joe
        currentFrame = 0;
        totalFrame = 4;  // Total number of animation frames
        delayCounter = 0;
        delayMax = 10;  // Delay for frame update speed
        enemySizeX = 26;  // Default size based on the first frame
        enemySizeY = 24;  // Default size based on the first frame
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        // Any other initialization logic if required
    }

    public void Update(GameTime gameTime)
    {
        // Update frame based on delay
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

        // Optionally, update Sniper Joe's position or behavior here if needed
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
                sourceRectangle = new Rectangle(312, 243, 22, 24);  // Frame 3
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
