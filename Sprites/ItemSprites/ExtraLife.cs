using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Temporary using IEnemySprite
public class ExtraLife : IEnemySprite
{
    float x;
    float y;
    private Texture2D itemSheet;
    int itemSizeX;
    int itemSizeY;
    public ExtraLife(Texture2D texture)
    {
        itemSheet = texture;
        x = 400;
        y = 90;
    }

    public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
    {
        itemSizeX = megamanSize;
        itemSizeY = megamanSize;
    }

    public void Update(GameTime gameTime)
    {

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

        destinationRectangle = new Rectangle((int)x, (int)y, itemSizeX, itemSizeY);
        sourceRectangle = new Rectangle(99, 52, 16, 15);

        _spriteBatch.Begin();
        _spriteBatch.Draw(itemSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        _spriteBatch.End();
    }
}