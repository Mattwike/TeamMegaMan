using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class climbingShootingMegaman : ISprite
    {

        float x;
        float y;
        int megamanSizeX;
        int megamanSizeY;
        private Texture2D megaManSheet;
        private Megaman megaman;

        public climbingShootingMegaman(Texture2D texture)
        {
            megaManSheet = texture;
            x = 250;
            y = 15;
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman Megaman, int interval)
        {
            megamanSizeX = megamanSize + 3;
            megamanSizeY = megamanSize;
            this.megaman = Megaman;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch _spriteBatch, float movementSpeed, bool flipHorizontally, bool flipVertically)
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

            destinationRectangle = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
            sourceRectangle = new Rectangle(60, 81, 24, 29);

            _spriteBatch.Begin();
            _spriteBatch.Draw(megaManSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
            _spriteBatch.End();
        }

    }
}