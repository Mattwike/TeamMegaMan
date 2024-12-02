using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using System.Collections.Generic;


public class Octopus : IEnemySprite
{
    private Texture2D enemyTexture;  // Texture for Octopus
    //private float x;  // X-coordinate of Octopus's position
    //private float y;  // Y-coordinate of Octopus's position
    private float initialX;  // Initial X position for back-and-forth movement reference
    private float speedX;  // Speed for horizontal movement
    private int screenWidth;  // Screen width to manage boundaries
    private int screenHeight; // Screen height to manage boundaries
    private int width;  // Width of the Octopus sprite
    private int height;  // Height of the Octopus sprite

    private int currentFrame;  // Current animation frame
    private int frameDelay;  // Delay between frame updates (lower = faster)
    private int frameCounter;  // Counter for controlling frame delay

    private float delayTimer;  // Timer for delay at edges
    private float delayInterval;  // Interval time to pause at edges (in seconds)

    private bool isPaused;  // Flag to determine if Octopus is currently pausing at an edge

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

    // Constructor to initialize Octopus with its texture
    public Octopus(Texture2D texture, Vector2 position)
    {
        enemyTexture = texture;
        SetPosition(position);

        initialX = x;  // Store the initial x position to use as reference
        width = 16;  // Width of the sprite
        height = 16;  // Height of the sprite

        // Set up frame information for animation
        frames = new Rectangle[3];  // Octopus has 3 frames
        frames[0] = new Rectangle(1, 19, 16, 16);  // Frame 1: Used when on edges
        frames[1] = new Rectangle(18, 19, 16, 16);  // Frame 2: Used when 10 pixels away
        frames[2] = new Rectangle(35, 19, 16, 16);  // Frame 3: Used when stopping at the side

        currentFrame = 0;  // Start at frame 0
        frameDelay = 5;  // Animation speed (lower value = faster switching)
        frameCounter = 0;

        speedX = 1f;  // Movement speed
        movementRange = 50;  // Movement range of 50 pixels back and forth

        delayTimer = 0;  // Initialize delay timer to 0
        delayInterval = 2f;  // 2-second pause at edges

        isPaused = false;  // Initially not pausing
    }

    // Initialize method to set position, screen boundaries, and other properties
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        //x = 500;  // Example initial X-position
        //y = 200;  // Example initial Y-position
        initialX = x;  // Store the initial x position as reference

        screenWidth = graphics.PreferredBackBufferWidth;  // Set screen boundaries
        screenHeight = graphics.PreferredBackBufferHeight;

        width = size;  // Set sprite size based on the passed size
        height = size;  // Keep height same as width for aspect ratio

        speedX = movementSpeed / 5f;  // Adjust horizontal speed based on movement speed parameter
    }

    // Update method to handle movement and animation logic
    public void Update(GameTime gameTime)
    {
        if (isPaused)
        {
            // If currently paused, update the delay timer
            delayTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (delayTimer >= delayInterval)
            {
                // After 2 seconds, resume movement and reset the timer
                isPaused = false;
                delayTimer = 0;

                // Change frame to frame 1 (resuming movement)
                currentFrame = 1;
            }
        }
        else
        {
            // Move back and forth within the movementRange (e.g., 50 pixels left and right)
            x += (int)speedX;

            // Check if Octopus is 10 pixels away from the side (left or right)
            if (x >= initialX + movementRange - 10 || x <= initialX - movementRange + 10)
            {
                currentFrame = 1;  // Switch to frame 2 when 10 pixels away
            }

            // Check if Octopus reached either side of the range
            if (x >= initialX + movementRange || x <= initialX - movementRange)
            {
                // Reverse horizontal direction and pause at the edge
                speedX = -speedX;
                isPaused = true;  // Trigger the pause at the edge
                currentFrame = 0;  // Switch to frame 3 when reaching the edge
            }

            // Update animation frame only when not paused
            frameCounter++;
            if (frameCounter >= frameDelay)
            {
                if (!isPaused)
                {
                    currentFrame = 2;  // Keep Octopus in the movement frame (Frame 3) when moving
                }
                frameCounter = 0;  // Reset the frame counter
            }
        }
    }

    // Draw method to render Octopus on the screen
    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;

        // Handle flipping if necessary
        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;

        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;


        // Define the destination rectangle where the sprite will be drawn on the screen
        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, width, height);

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
        x = (int)position.X; y = (int)position.Y; initialX = x;
    }
    public void isTouchingFloor()
    {
        //istouchingfloor = false;
    }
}