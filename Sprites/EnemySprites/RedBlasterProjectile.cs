using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.GameObjects;  // Adjust the namespace as needed

public class RedBlasterProjectile : IEnemyProjectile
{
    private float posX;
    private float posY;
    private float speedX;
    private float speedY;
    private Texture2D projectileSheet;
    private int projectileSizeX;
    private int projectileSizeY;
    private int screenWidth;
    private int screenHeight;

    private Rectangle[] projectileFrames;
    private int currentFrame;
    private int totalFrames;
    private int delayCounter;
    private int delayMax;

    public Rectangle hitbox;
    public int health;

    public int x
    {
        get { return (int)posX; }
        set { posX = value; }
    }

    public int y
    {
        get { return (int)posY; }
        set { posY = value; }
    }

    public RedBlasterProjectile(Texture2D texture, float startX, float startY, int screenWidth, int screenHeight, float speedX, float speedY, bool flipped)
    {
        projectileSheet = texture;
        posX = startX;
        posY = startY;
        this.screenWidth = screenWidth;
        this.screenHeight = screenHeight;

        this.speedX = speedX;
        this.speedY = speedY;

        projectileFrames = new Rectangle[]
        {
            new Rectangle(373, 16, 6, 6),
        };

        currentFrame = 0;
        totalFrames = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 10;
        projectileSizeX = projectileFrames[currentFrame].Width;
        projectileSizeY = projectileFrames[currentFrame].Height;
        if (flipped)
        {
            this.speedX = -speedX;
            this.speedY = -speedY;
        }
        health = 10;
    }

    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        // Implement if necessary
    }

    public void Update(GameTime gameTime, Camera camera, int megamanX)
    {
        posX += speedX;
        posY += speedY;

        hitbox = new Rectangle((int)posX, (int)posY, projectileSizeX, projectileSizeY);

        delayCounter++;
        if (delayCounter >= delayMax)
        {
            currentFrame++;
            if (currentFrame >= totalFrames)
            {
                currentFrame = 0;
            }
            delayCounter = 0;
        }
    }

    public bool IsOffScreen(Camera camera)
    {
        Rectangle visibleArea = camera.GetVisibleArea();
        Rectangle projectileRectangle = new Rectangle((int)posX, (int)posY, projectileSizeX, projectileSizeY);
        return !visibleArea.Intersects(projectileRectangle);
    }

    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;
        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        Rectangle destinationRectangle = new Rectangle((int)posX, (int)posY, projectileSizeX, projectileSizeY);
        Rectangle sourceRectangle = projectileFrames[currentFrame];
        spriteBatch.Draw(projectileSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
    }

    public Rectangle getRectangle()
    {
        return hitbox;
    }

    public void SetPosition(Vector2 position)
    {
        posX = position.X;
        posY = position.Y;
    }
}
