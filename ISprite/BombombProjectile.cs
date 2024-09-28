using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class BombombProjectile : ISprite
{
    float x;
    float y;
    float speedX;
    float speedY;
    private Texture2D projectileSheet;
    int projectileSizeX;
    int projectileSizeY;
    int screenWidth;

    Rectangle[] projectileFrames;
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;

    public BombombProjectile(Texture2D texture, float startX, float startY, int screenWidth, float speedX, float speedY)
    {
        projectileSheet = texture;
        x = startX;
        y = startY;
        this.screenWidth = screenWidth;
        this.speedX = speedX;  // Speed in the X direction
        this.speedY = speedY;  // Speed in the Y direction

        // Define source frame for projectile (adjust based on sprite sheet)
        projectileFrames = new Rectangle[]
        {
            new Rectangle(417, 24, 8, 6),  // Projectile frame
        };

        currentFrame = 0;
        totalFrame = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 10;
        projectileSizeX = projectileFrames[currentFrame].Width;
        projectileSizeY = projectileFrames[currentFrame].Height;
    }

    // Implement the missing Initialize method
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        currentFrame = 0;
        delayCounter = 0;
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

        _spriteBatch.Begin();
        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, projectileSizeX, projectileSizeY);
        Rectangle sourceRectangle = projectileFrames[currentFrame];
        _spriteBatch.Draw(projectileSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();
    }

    public void Update(GameTime gameTime)
    {
        // Move the projectile
        x += speedX;
        y += speedY;

        // Frame delay logic
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
    }

    public bool IsOffScreen()
    {
        // Remove the projectile if it moves off the screen
        return x < 0 || x > screenWidth || y < 0 || y > 600;  // Adjust y-bound if needed
    }
}
