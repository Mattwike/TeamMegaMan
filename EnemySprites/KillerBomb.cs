using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;  // Include System for Math

namespace Project1.GameObjects
{
    public class KillerBomb : IEnemySprite
    {
        private Texture2D enemyTexture;  // Texture for Killer Bomb
        private float x;  // X-coordinate of the enemy's position
        private float y;  // Y-coordinate of the enemy's position
        private float speedX;  // Speed for horizontal movement (negative for moving left)
        private float oscillationFrequency;  // Controls the speed of up and down movement
        private float originalY;  // Initial y-position to keep track of oscillation
        private int screenWidth;  // Screen width to manage boundaries
        private int screenHeight;  // Screen height to manage boundaries
        private int width;  // Width of the enemy sprite
        private int height;  // Height of the enemy sprite

        private Rectangle sourceRectangle;  // Source rectangle for the sprite from the sprite sheet

        // Constructor to initialize the position, texture, and screen boundaries
        public KillerBomb(Texture2D texture)
        {
            enemyTexture = texture;
            x = 500;  // Set the starting x position (example)
            y = 200;  // Set the starting y position (example)
            originalY = y;  // Store the initial y-coordinate for oscillation
            screenWidth = 800;  // Set to default screen width, can be changed
            screenHeight = 600;  // Set to default screen height, can be changed

            // Set default values for speed and oscillation
            width = 16;
            height = 16;
            speedX = -1f;  // Moves left at a basic speed of 1
            oscillationFrequency = 1.5f;  // Frequency of the vertical movement (adjust this value as needed)

            // Define the source rectangle for the single frame
            sourceRectangle = new Rectangle(114, 212, 16, 16);
        }

        // Initialize method to set position, screen boundaries, and speed
        public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
        {
            x = 500;  // Initial X-position (example)
            y = 200;  // Initial Y-position (example)
            originalY = y;  // Store original Y position

            screenWidth = graphics.PreferredBackBufferWidth;  // Set screen boundaries
            screenHeight = graphics.PreferredBackBufferHeight;

            // Adjust movement speeds and sprite size
            speedX = -movementSpeed;  // Move left
            oscillationFrequency = 1.5f;  // Set the frequency for oscillation
            width = size;
            height = size;
        }

        // Update method to handle movement logic
        public void Update(GameTime gameTime)
        {
            // Move left at a constant speed
            x += speedX;

            // Oscillate up and down using a sine function to simulate smooth movement
            // Adjust the amplitude (20 in this case) for how far up and down it moves
            y = originalY + (float)(Math.Sin(gameTime.TotalGameTime.TotalSeconds * oscillationFrequency) * 50);

            // Reset the position to the starting point if it goes off-screen to the left
            if (x < 0)
            {
                x = screenWidth + width;  // Reset to the right side of the screen
            }
        }

        // Draw method to render the sprite on the screen
        public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;

            // Handle flipping if necessary
            if (flipHorizontally)
                spriteEffects |= SpriteEffects.FlipHorizontally;

            if (flipVertically)
                spriteEffects |= SpriteEffects.FlipVertically;

            spriteBatch.Begin();

            // Define the destination rectangle where the sprite will be drawn on the screen
            Rectangle destinationRectangle = new Rectangle((int)x, (int)y, width, height);

            // Draw the sprite using its texture and source rectangle
            spriteBatch.Draw(enemyTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

            spriteBatch.End();
        }
    }
}
