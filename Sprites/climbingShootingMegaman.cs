using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Sprites
{
    public class climbingShootingMegaman : ISprite
    {

        float x;
        float y;
        int megamanSizeX;
        int megamanSizeY;
        private Texture2D megaManSheet;

        public climbingShootingMegaman(Texture2D texture)
        {
            megaManSheet = texture;
            x = 250;
            y = 15;
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
        {
            megamanSizeX = megamanSize + 3;
            megamanSizeY = megamanSize;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(Texture2D spriteTexture, SpriteBatch _spriteBatch, float movementSpeed, bool flipHorizontally, bool flipVertically)
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

            destinationRectangle = new Rectangle((int)x, (int)y, megamanSizeX, megamanSizeY);
            sourceRectangle = new Rectangle(60, 81, 24, 29);

            _spriteBatch.Begin();
            _spriteBatch.Draw(spriteTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
            _spriteBatch.End();
        }

    }
}