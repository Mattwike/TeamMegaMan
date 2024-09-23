public class EnemySprite : ISprite
{
	private Texture2D spriteSheet;
	private Rectangle[] frames;
	private int frameIndex;
	private double timeSinceLastFrame;
	private double frameTime;

	public EnemySprite(Texture2D spriteSheet, Rectangle initialFrame, int frameCount)
	{
		this.spriteSheet = spriteSheet;
		frames = new Rectangle[frameCount];

		// Set up frames based on the sprite sheet
		for (int i = 0; i < frameCount; i++)
		{
			frames[i] = new Rectangle(
				initialFrame.X + (i * initialFrame.Width),
				initialFrame.Y,
				initialFrame.Width,
				initialFrame.Height);
		}

		frameIndex = 0;
		timeSinceLastFrame = 0;
		frameTime = 0.1; // Adjust for animation speed
	}

	public void Update(GameTime gameTime)
	{
		timeSinceLastFrame += gameTime.ElapsedGameTime.TotalSeconds;

		if (timeSinceLastFrame >= frameTime)
		{
			frameIndex = (frameIndex + 1) % frames.Length; // Loop through frames
			timeSinceLastFrame = 0;
		}
	}

	public void Draw(SpriteBatch spriteBatch, Vector2 position)
	{
		spriteBatch.Draw(spriteSheet, position, frames[frameIndex], Color.White);
	}
}