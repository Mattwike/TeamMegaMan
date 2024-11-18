using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class climbingShootingMegaman : ISprite
    {

        int megamanSizeX;
        int megamanSizeY;
        private Texture2D megaManSheet;
        private Megaman megaman;
        private Rectangle MegamanBox;

        public climbingShootingMegaman(Texture2D texture)
        {
            megaManSheet = texture;

        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman Megaman, int interval, bool isRight)
        {
            megamanSizeX = 20 + 3;
            megamanSizeY = 26;
            this.megaman = Megaman;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch _spriteBatch, float movementSpeed, bool flipHorizontally, bool flipVertically, Color currentColor)
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

            MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
            sourceRectangle = new Rectangle(60, 81, 24, 29);

            _spriteBatch.Draw(megaManSheet, MegamanBox, sourceRectangle, currentColor, 0f, Vector2.Zero, spriteEffects, 0f);
        }

        public Rectangle getRectangle()
        {
            return MegamanBox;
        }

    }
}