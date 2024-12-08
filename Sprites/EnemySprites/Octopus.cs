using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using System.Collections.Generic;

public class Octopus : IEnemySprite
{
    private Texture2D enemyTexture;  // Texture for Octopus
    private float initialPositionX;  // Initial X position for back-and-forth movement reference
    private float positionX;  // Precise X position (float)
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
    public bool hasProjectiles { get; set; }
    private bool hasChangedDirection = false;

    public int y { get; set; }  // Keep as int to match interface
    public int x { get; set; }  // Keep as int to match interface
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public bool hitWall { get; set; }
    public float gravity { get; set; }
    public bool IgnoresFloors { get; set; }
    public float Gravity
    {
        set { gravity = 4.5f; }
    }

    // Constructor to initialize Octopus with its texture
    public Octopus(Texture2D texture, Vector2 position)
    {
        enemyTexture = texture;
        SetPosition(position);

        positionX = x;  // Initialize positionX to integer x
        initialPositionX = positionX;  // Store the initial x position to use as reference
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
        hasProjectiles = false;
        IgnoresFloors = false;
    }

    // Initialize method to set position, screen boundaries, and other properties
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        // Removed the line that overwrote initialPositionX
        // initialPositionX = positionX;  // This was overwriting the correct initialPositionX

        screenWidth = graphics.PreferredBackBufferWidth;  // Set screen boundaries
        screenHeight = graphics.PreferredBackBufferHeight;

        width = 16;  // Set sprite size based on the passed size
        height = 16;  // Keep height same as width for aspect ratio

        speedX = movementSpeed / 5f;  // Adjust horizontal speed based on movement speed parameter

        if (speedX == 0)
        {
            speedX = 1f;  // Ensure speedX is not zero
        }
    }

    // Update method to handle movement and animation logic
    public void Update(GameTime gameTime, Camera camera, int megamanX)
    {
        if (isPaused)
        {
            // If currently paused, update the delay timer
            delayTimer += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (delayTimer >= delayInterval)
            {
                // After delay interval, resume movement and reset the timer
                isPaused = false;
                delayTimer = 0;

                // Change frame to frame 1 (resuming movement)
                currentFrame = 1;
            }
        }
        else
        {
            // Move back and forth within the movementRange
            positionX += speedX;  // Update positionX with speedX

            // Update x to the integer value of positionX
            x = (int)positionX;

            // Check if Octopus is 10 pixels away from the side (left or right)
            if (positionX >= initialPositionX + movementRange - 10 || positionX <= initialPositionX - movementRange + 10)
            {
                currentFrame = 1;  // Switch to frame 2 when 10 pixels away
            }

            // Check if Octopus reached either side of the range
            if (hitWall && !hasChangedDirection)
            {
                speedX *= -1;           
                positionX += speedX * 2; 
                x = (int)positionX;      
                hitWall = false;    
                isPaused = true;   
                currentFrame = 0; 
                hasChangedDirection = true;
            }
            else if (!hitWall)
            {
                hasChangedDirection = false;  
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

        // Update hitbox position
        hitbox = new Rectangle(x, y, width, height);
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

    public void TakeDamage(List<EnemyDrop> enemyDropList, Megaman megaman)
    {
        health -= 10;
    }

    public void SetPosition(Vector2 position)
    {
        x = (int)position.X;
        y = (int)position.Y;
        positionX = position.X;  // Initialize positionX to the precise X value
        initialPositionX = positionX;  // Set initialPositionX correctly
    }

    public void isTouchingFloor()
    {
        //istouchingfloor = false;
    }

    public List<IEnemyProjectile> GetProjectiles()
    {
        return null;
    }
}
