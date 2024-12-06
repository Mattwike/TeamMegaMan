// BombombProjectile.cs

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.GameObjects;
using System.Runtime.CompilerServices;
using System;


public class BombombProjectile : IEnemyBomb
{
    // Fields
    private Texture2D texture;
    private Texture2D Bomb;
    private float posX, posY;
    public float speedX, speedY;
    public int width { get; set; }
    public int height { get; set; }
    private int screenWidth;
    private int screenHeight;

    public Rectangle hitbox;
    public int health;

    public int y { get; set; }
    public int x { get; set; }
    //public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public float Gravity { set { gravity = 4.5f; } }

    private Rectangle[] projectileFrames;
    private int currentFrame;
    private int totalFrame;
    private int delayCounter;
    private int delayMax;
    private float fallheight;
    private float interval;
    private Megaman megaman;

    // Constructor
    public BombombProjectile(Texture2D texture, Texture2D Bomb, float startX, float startY, int screenWidth, float speedX, int number, Megaman megaman)
    {
        this.texture = texture;
        this.Bomb = Bomb;
        this.megaman = megaman;
        this.posX = startX;
        this.posY = startY;
        this.screenWidth = screenWidth;
        this.screenHeight = 600;  // Adjust as needed

        this.speedX = speedX;
        this.speedY = 2f;
        interval = 0;

        projectileFrames = new Rectangle[]
        {
            new Rectangle(417, 24, 8, 6),
        };

        currentFrame = 0;
        totalFrame = projectileFrames.Length;
        delayCounter = 0;
        delayMax = 20;
        width = projectileFrames[currentFrame].Width;
        height = projectileFrames[currentFrame].Height;
        health = 10;  // Initial health
        switch (number)
        {
            case 1:
                fallheight = posY + 30;
                break;
            case 2:
                fallheight = posY + 90;
                break;
            case 3:
                fallheight = posY + 30;
                break;
            case 4:
                fallheight = posY + 90;
                break;
        }
    }

    // Initialize method
    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        // Implement if necessary
    }

    // Update method with Camera parameter
    public void Update(GameTime gameTime, Camera camera, int megamanX)
    {
        if (posY < fallheight)
        {
            posX += speedX;
            posY += speedY;
        }

        // Update hitbox position
        hitbox = new Rectangle((int)posX, (int)posY, width, height);

        // Frame delay logic (if needed for animation)
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

    // Draw method
    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        Rectangle explosionRectangle;
        Rectangle destinationRectangle;
        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;
        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        // Define the destination rectangle where the projectile will be drawn on the screen
        destinationRectangle = new Rectangle((int)posX, (int)posY, width, height);

        // Define the source rectangle for the projectile sprite (adjust based on your sprite sheet)
        Rectangle sourceRectangle = projectileFrames[currentFrame];

        // Draw the projectile using its texture
        if (posY + 1 > fallheight)
        {
            if (interval <= 10)
            {
                explosionRectangle = new Rectangle(77, 129, 4, 4);
                destinationRectangle = new Rectangle((int)posX, (int)posY, 4, 4);
                interval++;
                if (Math.Sqrt(Math.Pow(megaman.x - posX, 2) + Math.Pow(megaman.y - posY, 2)) <= 8)
                {
                    megaman.TakeDamage();
                }
            }
            else if (interval >= 10 && interval <= 20)
            {
                explosionRectangle = new Rectangle(84, 123, 16, 16);
                destinationRectangle = new Rectangle((int)posX, (int)posY, 16, 16);
                interval++;
                if(Math.Sqrt(Math.Pow(megaman.x - posX, 2) + Math.Pow(megaman.y - posY, 2)) <= 32)
                {
                    megaman.TakeDamage();
                }
            }
            else if (interval >= 20 && interval <= 30)
            {
                explosionRectangle = new Rectangle(103, 125, 12, 12);
                destinationRectangle = new Rectangle((int)posX, (int)posY, 12, 12);
                interval++;
                if (Math.Sqrt(Math.Pow(megaman.x - posX, 2) + Math.Pow(megaman.y - posY, 2)) <= 24)
                {
                    megaman.TakeDamage();
                }
            }

            else if (interval >= 30 && interval <= 40)
            {
                explosionRectangle = new Rectangle(121, 126, 10, 10);
                destinationRectangle = new Rectangle((int)posX, (int)posY, 10, 10);
                interval++;
                if (Math.Sqrt(Math.Pow(megaman.x - posX, 2) + Math.Pow(megaman.y - posY, 2)) <= 20)
                {
                    megaman.TakeDamage();
                }
            }
            else
            {
                explosionRectangle = sourceRectangle;
                posX -= 1000;
                //interval = 0;
            }

            //Rectangle explosiondestinationRectangle = new Rectangle(pausex, pausex, width, height);
            spriteBatch.Draw(Bomb, destinationRectangle, explosionRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        }
        else
        {
            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        }
    }

    // Method to check if the projectile is off the screen, accounting for the camera
    public bool IsOffScreen(Camera camera)
    {
        if (camera == null)
        {
            // Default behavior if camera is not provided
            return (posX < -width || posX > screenWidth + width || posY < -height || posY > screenHeight + height);
        }

        Rectangle visibleArea = camera.GetVisibleArea();
        Rectangle projectileRectangle = new Rectangle((int)posX, (int)posY, width, height);
        return !visibleArea.Intersects(projectileRectangle);
    }

    // Get the rectangle for collision detection
    public Rectangle getRectangle()
    {
        return hitbox;
    }

    // Set the position of the projectile
    public void SetPosition(Vector2 position)
    {
        posX = position.X;
        posY = position.Y;
    }

    public List<IEnemyProjectile> GetProjectiles()
    {
        return null;
    }
}
