using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using System.Collections.Generic;

public class Gabyoall : IEnemySprite
{
    private Texture2D enemyTexture;  // Texture for Gabyoall
    private float positionX;  // Precise X-coordinate
    private float initialPositionX;  // Initial X position for back-and-forth movement reference
    private float speedX;  // Speed for horizontal movement
    private int screenWidth;  // Screen width to manage boundaries
    private int screenHeight; // Screen height to manage boundaries
    private int width;  // Width of the Gabyoall sprite
    private int height;  // Height of the Gabyoall sprite

    private int currentFrame;  // Current animation frame
    private int frameDelay;  // Delay between frame updates (lower = faster)
    private int frameCounter;  // Counter for controlling frame delay

    private Rectangle[] frames;  // Array of rectangles for animation frames
    private int movementRange;  // Movement range for back-and-forth motion

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

    // Constructor to initialize Gabyoall with its texture
    public Gabyoall(Texture2D texture, Vector2 position)
    {
        enemyTexture = texture;
        SetPosition(position);

        positionX = x;  // Initialize precise position
        initialPositionX = positionX;  // Store initial position for movement range
        width = 16;  // Width of the sprite
        height = 8;  // Height of the sprite

        // Set up frame information for animation
        frames = new Rectangle[2];  // Gabyoall has 2 frames
        frames[0] = new Rectangle(246, 132, 16, 8);  // Frame 1
        frames[1] = new Rectangle(263, 132, 16, 8);  // Frame 2

        currentFrame = 0;  // Start at frame 0
        frameDelay = 5;  // Fast blinking animation (lower value = faster switching)
        frameCounter = 0;

        speedX = 0.5f;  // Movement speed (adjust as necessary)
        movementRange = 50;  // Movement range of 50 pixels back and forth
    }

    // Initialize method to set position, screen boundaries, and other properties
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        // Removed the line that overwrote initialPositionX

        screenWidth = graphics.PreferredBackBufferWidth;  // Set screen boundaries
        screenHeight = graphics.PreferredBackBufferHeight;

        width = 24;  // Set sprite size based on the passed size
        height = 12;  // Height is half of the size to keep the aspect ratio
        speedX = movementSpeed / 10f;  // Adjust horizontal speed based on movement speed parameter

        if (speedX == 0)
        {
            speedX = 0.5f;  // Ensure speedX is not zero
        }
    }

    // Update method to handle movement and animation logic
    public void Update(GameTime gameTime)
    {
        // Update frame counter for animation timing
        frameCounter++;
        if (frameCounter >= frameDelay)
        {
            currentFrame++;  // Move to the next frame
            if (currentFrame >= frames.Length)  // Loop back to the first frame
            {
                currentFrame = 0;
            }
            frameCounter = 0;  // Reset the frame counter
        }

        // Move back and forth within the movementRange
        positionX += speedX;  // Update precise position

        // Update integer x position
        x = (int)positionX;

        // Reverse direction when reaching the bounds of the movement range
        if (positionX > initialPositionX + movementRange || positionX < initialPositionX - movementRange)
        {
            speedX = -speedX;  // Reverse horizontal direction
        }

        // Update hitbox position
        hitbox = new Rectangle(x, y, width, height);
    }

    // Draw method to render Gabyoall on the screen
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
        initialPositionX = positionX;  // Set initial position for movement range
    }
    public void isTouchingFloor()
    {
        //istouchingfloor = false;
    }
}
