using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class climbingMegaman : ISprite
    {

        int megamanSizeX;
        int megamanSizeY;
        private Texture2D megaManSheet;
        private Megaman megaman;
        int interval;

        public climbingMegaman(Texture2D texture)
        {
            megaManSheet = texture;
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman megaman, int interval, bool isRight)
        {

            megamanSizeX = megamanSize - 5;
            megamanSizeY = megamanSize;
            this.megaman = megaman;
            this.interval = interval;
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

            if (interval % 24 < 6)
            {
                destinationRectangle = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
                sourceRectangle = new Rectangle(5, 81, 16, 29);
            }
            else
            {
                destinationRectangle = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
                sourceRectangle = new Rectangle(32, 81, 16, 29);
            }

            _spriteBatch.Begin();
            _spriteBatch.Draw(megaManSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
            _spriteBatch.End();
        }
    }
}