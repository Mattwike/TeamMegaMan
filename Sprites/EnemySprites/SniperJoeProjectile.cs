using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class SniperJoeProjectile : IEnemySprite
{
	float x;
	float y;
	float speedX;  // Horizontal speed for the projectile
	private Texture2D projectileSheet;
	int projectileSizeX;
	int projectileSizeY;
	int screenWidth;

	Rectangle[] projectileFrames;
	int currentFrame;
	int totalFrame;
	int delayCounter;
	int delayMax;

	public SniperJoeProjectile(Texture2D texture, float startX, float startY, int screenWidth)
	{
		projectileSheet = texture;
		x = startX;
		y = startY;
		this.screenWidth = screenWidth;

		this.speedX = -5f;  // Sniper Joe projectiles always move to the left

		// Define source frame for the projectile (adjust based on the sprite sheet)
		projectileFrames = new Rectangle[]
		{
			new Rectangle(371, 244, 6, 6),  // Projectile frame from sprite sheet (coordinates for Sniper Joe's particle)
        };

		currentFrame = 0;
		totalFrame = projectileFrames.Length;
		delayCounter = 0;
		delayMax = 10;  // Adjust delayMax to control animation speed (if necessary)
		projectileSizeX = projectileFrames[currentFrame].Width;
		projectileSizeY = projectileFrames[currentFrame].Height;
	}

	public void Initialize(GraphicsDeviceManager graphics, float movementSpeed, int size)
	{
		currentFrame = 0;
		delayCounter = 0;
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

		
		Rectangle destinationRectangle = new Rectangle((int)x, (int)y, projectileSizeX, projectileSizeY);
		Rectangle sourceRectangle = projectileFrames[currentFrame];
		_spriteBatch.Draw(projectileSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
		
	}

	public void Update(GameTime gameTime)
	{
		// Move the projectile horizontally at a constant speed (to the left)
		x += speedX;

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

	public bool IsOffScreen()
	{
		// Check if the projectile is off-screen based on screen width
		return x < 0 || x > screenWidth;  // Remove the projectile when it goes off the left or right of the screen
	}
}
