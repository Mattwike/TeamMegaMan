using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Project1.GameObjects;
using System;


public class screwDriver : IEnemySprite
{
    // Existing fields
    int currentFrame;
    int totalFrame;
    int delayCounter;
    int delayMax;
    private Texture2D enemySheet;
    int enemySizeX;
    int enemySizeY;
    public Rectangle hitbox;
    public int health;
    private GraphicsDeviceManager graphics;
    bool isVisible = true;

    public int y { get; set; }
    public int x { get; set; }
    public bool isFalling { get; set; }
    public bool istouchingfloor { get; set; }
    public float gravity { get; set; }
    public bool hitWall { get; set; }
    public bool hasProjectiles { get; set; }
    public bool IgnoresFloors { get; set; }
    public float Gravity
    {
        set { gravity = 4.5f; }
    }

    // New fields for projectiles
    private List<IEnemyProjectile> projectiles;
    private bool hasShotOnThirdFrame;
    private bool hasShotOnFifthFrame;

    public screwDriver(Texture2D texture, Vector2 position)
    {
        enemySheet = texture;
        SetPosition(position);

        // Initialize projectiles
        projectiles = new List<IEnemyProjectile>();
        hasShotOnThirdFrame = false;
        hasShotOnFifthFrame = false;
        hasProjectiles = true;
        IgnoresFloors = false;
}

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        currentFrame = 0;
        totalFrame = 50;
        delayCounter = 0;
        delayMax = 5;
        this.graphics = _graphics;

        // Optional: Initialize other properties
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
                hasShotOnThirdFrame = false; // Reset shot flags
                hasShotOnFifthFrame = false;
            }
            delayCounter = 0;
        }

        // Shoot projectiles on the 3rd and 5th frames
        if (currentFrame == 3 && !hasShotOnThirdFrame)
        {
            ShootProjectiles();
            hasShotOnThirdFrame = true;
        }

        if (currentFrame == 5 && !hasShotOnFifthFrame)
        {
            ShootProjectiles();
            hasShotOnFifthFrame = true;
        }

        // Update projectiles
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            projectiles[i].Update(gameTime, camera, megamanX);

            if (projectiles[i].IsOffScreen(camera))
            {
                projectiles.RemoveAt(i);
            }
        }

        // Update hitbox
        hitbox = new Rectangle(x, y, 16, 16); // Adjust dimensions as necessary
    }

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

        Rectangle sourceRectangle;
        Rectangle destinationRectangle;

        if (currentFrame < 10)
        {
            destinationRectangle = new Rectangle((int)x, (int)y + 6, 16, 10);
            sourceRectangle = new Rectangle(155, 250, 18, 18);
        }
        else if (currentFrame < 15)
        {
            destinationRectangle = new Rectangle((int)x, (int)y + 2, 16, 14);
            sourceRectangle = new Rectangle(172, 250, 18, 18);
        }
        else if (currentFrame < 20)
        {
            destinationRectangle = new Rectangle((int)x, (int)y, 16, 16);
            sourceRectangle = new Rectangle(189, 250, 18, 18);
        }
        else if (currentFrame < 30)
        {
            destinationRectangle = new Rectangle((int)x + 1, (int)y, 15, 16);
            sourceRectangle = new Rectangle(206, 250, 18, 18);
        }
        else if (currentFrame < 40)
        {
            destinationRectangle = new Rectangle((int)x + 1, (int)y, 15, 16);
            sourceRectangle = new Rectangle(223, 250, 18, 18);
        }
        else
        {
            destinationRectangle = new Rectangle((int)x, (int)y, 16, 16);
            sourceRectangle = new Rectangle(189, 250, 18, 18);
        }

        _spriteBatch.Draw(enemySheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);

        // Draw projectiles
        foreach (var projectile in projectiles)
        {
            projectile.Draw(_spriteBatch, false, flipVertically);
        }
    }

    private void ShootProjectiles()
    {
        // Shoot bullets in specified directions
        var directions = new Vector2[]
        {
            new Vector2(0, -3), // Upward
            new Vector2(-2, -2), // Diagonally left
            new Vector2(2, -2), // Diagonally right
            new Vector2(-3, 0), // Directly left
            new Vector2(3, 0) // Directly right
        };

        foreach (var direction in directions)
        {
            var projectile = new ScrewDriverProjectile(
                enemySheet,
                x + 8, // Center the projectile horizontally
                y + 8, // Center the projectile vertically
                800, // Example screen width
                600 // Example screen height
            );

            projectile.speedX = direction.X;
            projectile.speedY = direction.Y;
            projectiles.Add(projectile);
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

    public void TakeDamage(List<EnemyDrop> enemyDropList, Megaman megaman)
    {
        health -= 10;
        if (health == 0)
        {
            megaman.megamanScore += 100;
            Random rnd = new Random();
            int num = rnd.Next(1, 6);
            if (num == 5)
            {
                EnemyDrop enemyDrop = new EnemyDrop();
                enemyDrop.Initialize(graphics, (int)x, (int)y);
                enemyDropList.Add(enemyDrop);
            }
            isVisible = false;
            hitbox.Y += 1000;
            y += 1000;
        }
    }

    public void SetPosition(Vector2 position)
    {
        x = (int)position.X; y = (int)position.Y;
    }

    public void isTouchingFloor()
    {
        //istouchingfloor = false;
    } 

    public List<IEnemyProjectile> GetProjectiles()
    {
        return projectiles;
    }
}
