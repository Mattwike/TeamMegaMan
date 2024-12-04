// Bombomb.cs

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.GameObjects;  // Adjust the namespace as needed

public class Bombomb : IEnemySprite
{
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;
    float initialX;
    float initialY;
    float jumpHeight;
    bool isJumping;
    bool hasExploded;
    bool isVisible;

    private int screenHeight;

    private Texture2D bombombSheet;
    List<BombombProjectile> projectiles;

    Rectangle[] bombombFrames;

    public Rectangle hitbox;
    public int health;

    public int y { get; set; }
    public int x { get; set; }
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public bool hitWall { get; set; }
    public float Gravity { set { gravity = 4.5f; } }

    // Updated Constructor: Removed startY parameter
    public Bombomb(Texture2D texture, float startX, Vector2 position)
    {
        bombombSheet = texture;
        initialX = startX;
        initialY = position.Y;  // Set initialY based on position.Y
        SetPosition(position);   // This sets x and y based on position

        jumpHeight = 100;
        isJumping = true;
        hasExploded = false;
        isVisible = true;

        screenHeight = 600;

        bombombFrames = new Rectangle[]
        {
            new Rectangle(400, 21, 16, 12),
        };

        currentFrame = 0;
        totalFrame = bombombFrames.Length;
        delayCounter = 0;
        delayMax = 10;
        projectiles = new List<BombombProjectile>();

        health = 100;
    }

    public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
    {
        currentFrame = 0;
        delayCounter = 0;
        isJumping = true;
        hasExploded = false;
        isVisible = true;
        projectiles.Clear();
    }

    public void Update(GameTime gameTime, Camera camera, int megamanX)
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

        if (isJumping && !hasExploded)
        {
            y -= 1;

            if (initialY - y >= jumpHeight)
            {
                isJumping = false;
                hasExploded = true;
                isVisible = false;
                Explode();
            }
        }

        // Update all projectiles
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update(gameTime, camera, megamanX);

            if (projectiles[i].IsOffScreen(camera))
            {
                projectiles.RemoveAt(i);
            }
        }

        // **Updated Reset Condition**
        // Checks if Bombomb has exploded and all projectiles have been handled
        if (hasExploded && projectiles.Count == 0)
        {
            ResetBombomb();
        }

        hitbox = new Rectangle(x, y, bombombFrames[currentFrame].Width, bombombFrames[currentFrame].Height);
    }

    private void Explode()
    {
        float projectileX = x;
        float projectileY = y - bombombFrames[currentFrame].Height / 2;

        projectiles.Add(new BombombProjectile(bombombSheet, projectileX - 10, projectileY, 800, -0.25f));
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX + 10, projectileY, 800, 0.25f));
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX - 50, projectileY, 800, -0.25f));
        projectiles.Add(new BombombProjectile(bombombSheet, projectileX + 50, projectileY, 800, 0.25f));
    }

    private void ResetBombomb()
    {
        x = (int)initialX;
        y = (int)initialY;

        // Uncomment the following lines for debugging purposes
        // System.Diagnostics.Debug.WriteLine($"ResetBombomb called. Position set to X={x}, Y={y}");

        projectiles.Clear();

        isJumping = true;
        hasExploded = false;
        isVisible = true;

        // Uncomment for additional debugging
        // System.Diagnostics.Debug.WriteLine("Bombomb reset: isJumping=true, hasExploded=false, isVisible=true");
    }

    public void Draw(SpriteBatch spriteBatch, bool flipHorizontally, bool flipVertically)
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

        if (isVisible)
        {
            Rectangle destinationRectangle = new Rectangle(x, y, bombombFrames[currentFrame].Width, bombombFrames[currentFrame].Height);
            Rectangle sourceRectangle = bombombFrames[currentFrame];

            spriteBatch.Draw(bombombSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        }

        foreach (var projectile in projectiles)
        {
            projectile.Draw(spriteBatch, flipHorizontally, flipVertically);
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
    }

    // Updated SetPosition method: Ensures initialY is set correctly
    public void SetPosition(Vector2 position)
    {
        x = (int)position.X;
        y = (int)position.Y;
        // initialY is already set in the constructor based on position.Y
    }

    public void isTouchingFloor()
    {
        // Implement if necessary
    }
}
