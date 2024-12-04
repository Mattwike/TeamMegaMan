using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;
using Project1.Interfaces;
using System.Collections.Generic;

public class jumpingFlea : IEnemySprite
{
    // Fields
    private int currentFrame;
    private int totalFrames;
    private int delayCounter;
    private int delayMax;
    //private float x;
    //private float y;
    private Texture2D enemySheet;
    private int enemySizeX;
    private int enemySizeY;
    private float enemySpeed;
    //private float gravity;
    private bool jumping;
    private bool falling;
    private float initialY; // Stores the initial Y position
    public Rectangle hitbox;
    public int health;
    bool isVisible = true;
    GraphicsDeviceManager graphics;
    private bool facingLeft;

    public int y { get; set; }
    public int x { get; set; }
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public bool hitWall { get; set; }
    public float Gravity
    {
        set { gravity = 4.5f; }

    }

    public jumpingFlea(Texture2D texture, Vector2 position)
    {
        enemySheet = texture;
        SetPosition(position);
        initialY = position.Y;
        facingLeft = false;

}

    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrames = 15;
        delayCounter = 0;
        delayMax = 10;
        enemySizeX = 15;
        enemySizeY = 15;
        gravity = 4.5f;
        enemySpeed = 2f;
        jumping = false;
        falling = false;
        health = 10;
        this.graphics = graphics;
    }

    public void Update(GameTime gameTime, Megaman megaman)
    {
        if(x - megaman.x < 0 && !facingLeft)
        {
            enemySpeed = -enemySpeed;
            facingLeft = true;
        }
        else if(x - megaman.x > 0 && facingLeft)
        {
            enemySpeed = -enemySpeed;
            facingLeft = false;
        }

        if (!isVisible)
        {
            return;
        }
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

        if (jumping)
        {
            x -= (int)enemySpeed;
            if (gravity > 0)
            {
                y -= (int)gravity;
                gravity -= 0.25f;
            }
            else
            {
                jumping = false;
                falling = true;
            }
        }
        else if (falling)
        {
            if (!istouchingfloor)
            {
                y += (int)gravity;
                gravity += 0.25f;
            }
            else
            {
                falling = false;
                gravity = 4.5f;
            }
        }
        else
        {
            // Optional: Logic to initiate jumping again after landing
            // For example, start jumping again after some delay
        }

        // Update hitbox position
        hitbox = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
    }

    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        if (!isVisible)
        {
            return;
        }
        SpriteEffects spriteEffects = SpriteEffects.FlipHorizontally;

        if (flipHorizontally)
        {
            spriteEffects |= SpriteEffects.FlipHorizontally;
        }

        if (flipVertically)
        {
            spriteEffects |= SpriteEffects.FlipVertically;
        }

        Rectangle sourceRectangle = Rectangle.Empty;
        Rectangle destinationRectangle = Rectangle.Empty;

        if (currentFrame < 10)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(113, 177, 18, 14);
        }
        else if (currentFrame == 10)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);
            sourceRectangle = new Rectangle(148, 170, 16, 19);
            jumping = true;
        }
        else if (currentFrame > 10 && currentFrame < 13)
        {
            destinationRectangle = new Rectangle((int)x, (int)y - 2, enemySizeX, enemySizeY + 2);
            sourceRectangle = new Rectangle(131, 163, 16, 21);
        }
        else if (currentFrame >= 13)
        {
            destinationRectangle = new Rectangle((int)x, (int)y - 2, enemySizeX, enemySizeY + 2);
            sourceRectangle = new Rectangle(131, 163, 16, 21);
            jumping = false;
            falling = true;
        }

        spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
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
        if (health == 0)
        {
            EnemyDrop enemyDrop = new EnemyDrop();
            enemyDrop.Initialize(graphics, (int)x, (int)y);
            enemyDropList.Add(enemyDrop);
            isVisible = false;
            hitbox.Y += 1000;
        }
    }

    public void SetPosition(Vector2 position)
    {
        x = (int)position.X;
        y = (int)position.Y;
        initialY = position.Y; // Ensure initialY is updated if position changes
    }
    public void isTouchingFloor()
    {
        istouchingfloor = false;
    }
}