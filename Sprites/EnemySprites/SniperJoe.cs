// SniperJoe.cs

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.GameObjects;  // Adjust the namespace as needed

public class SniperJoe : IEnemySprite
{
    private int currentFrame;
    private int totalFrame;
    private int delayCounter;
    private int delayMax;
    private float initialY;
    private Texture2D enemySheet;
    private Texture2D projectileTexture;
    private int enemySizeX;
    private int enemySizeY;
    private bool isJumping;
    private bool justLanded;
    private bool hasShot;
    public List<SniperJoeProjectile> projectiles;

    public Rectangle hitbox;
    public int health;
    private bool isVisible = true;

    private int screenWidth;
    private GraphicsDeviceManager graphics;

    public int y { get; set; }
    public int x { get; set; }
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public bool hitWall { get; set; }
    public float Gravity { set { gravity = 4.5f; } }

    // Constructor
    public SniperJoe(Texture2D texture, Vector2 position)
    {
        enemySheet = texture;
        SetPosition(position);
        initialY = y;
        gravity = 4.5f;
        currentFrame = 0;
        totalFrame = 30;
        delayCounter = 0;
        delayMax = 50;
        enemySizeX = 26;
        enemySizeY = 24;
        isJumping = false;
        isFalling = false;
        justLanded = false;
        hasShot = false;
        health = 100;

        projectileTexture = texture;
        screenWidth = 800;

        projectiles = new List<SniperJoeProjectile>();
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        screenWidth = _graphics.PreferredBackBufferWidth;
        this.graphics = _graphics;
    }

    // Update method with Camera parameter
    public void Update(GameTime gameTime, Camera camera, int megamanX)
    {
        if ((!istouchingfloor && !isJumping) || isFalling)
        {
            y += (int)gravity;
        }

        if (!isVisible)
        {
            return;
        }
        if (health <= 0)
        {
            y += 1000;
            hitbox.Y += 1000;
            isVisible = false;
        }

        hitbox = new Rectangle((int)x, (int)y, 26, 24);

        if (currentFrame == 20 && !isJumping && !isFalling && !justLanded)
        {
            isJumping = true;
            gravity = 4.5f;
        }

        if (isJumping)
        {
            if (gravity > 0)
            {
                y -= (int)gravity;
                gravity -= 0.25f;
            }
            else
            {
                isJumping = false;
                isFalling = true;
            }
        }
        else if (isFalling)
        {
            if (!istouchingfloor)
            {
                y += (int)gravity;
                gravity += 0.25f;
            }
            else
            {
                justLanded = true;
                gravity = 4.5f;
                currentFrame++;
            }
        }

        if (justLanded)
        {
            currentFrame++;
            if (currentFrame >= totalFrame)
            {
                currentFrame = 0;
            }
            justLanded = false;
            delayCounter = 0;
            hasShot = false;
        }

        if (!isJumping && !isFalling && !justLanded)
        {
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

        if (currentFrame == 12 && !hasShot)
        {
            ShootProjectile();
            hasShot = true;
        }

        // Update each projectile
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update(gameTime, camera, megamanX);

            if (projectiles[i].IsOffScreen(camera))
            {
                projectiles.RemoveAt(i);
            }
        }
    }

    private void ShootProjectile()
    {
        SniperJoeProjectile projectile = new SniperJoeProjectile(projectileTexture, x, y + 10, screenWidth);
        projectiles.Add(projectile);
    }

    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
    {
        if (!isVisible)
        {
            return;
        }
        SpriteEffects spriteEffects = SpriteEffects.None;

        if (flipHorizontally)
            spriteEffects |= SpriteEffects.FlipHorizontally;

        if (flipVertically)
            spriteEffects |= SpriteEffects.FlipVertically;

        Rectangle sourceRectangle;
        Rectangle destinationRectangle = new Rectangle((int)x, (int)y, enemySizeX, enemySizeY);

        switch (currentFrame)
        {
            case 0:
                sourceRectangle = new Rectangle(254, 243, 26, 24);
                enemySizeX = 26;
                enemySizeY = 24;
                break;
            case 11:
                sourceRectangle = new Rectangle(287, 243, 22, 24);
                enemySizeX = 22;
                enemySizeY = 24;
                break;
            case 12:
                sourceRectangle = new Rectangle(312, 243, 22, 24);
                enemySizeX = 22;
                enemySizeY = 24;
                break;
            case 20:
                sourceRectangle = new Rectangle(337, 243, 28, 31);
                enemySizeX = 28;
                enemySizeY = 31;
                break;
            default:
                sourceRectangle = new Rectangle(254, 243, 26, 24);
                enemySizeX = 26;
                enemySizeY = 24;
                break;
        }

        destinationRectangle.Width = enemySizeX;
        destinationRectangle.Height = enemySizeY;
        spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        foreach (var projectile in projectiles)
        {
            projectile.Draw(spriteBatch, false, false);
        }
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
        if (health <= 0)
        {
            EnemyDrop enemyDrop = new EnemyDrop();
            enemyDrop.Initialize(graphics, (int)x, (int)y);
            enemyDropList.Add(enemyDrop);
            isVisible = false;
            hitbox.Y += 1000;
            y += 1000;
        }
    }

    public void SetPosition(Vector2 position)
    {
        x = (int)position.X;
        y = (int)position.Y;
        initialY = y;
    }

    public void isTouchingFloor()
    {
        // Implement if necessary
    }
}
