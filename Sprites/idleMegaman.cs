using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class idleMegaman : ISprite
    {

        private Texture2D megaManSheet;
        int megamanSizeX;
        int megamanSizeY;
        private Megaman megaman;
        private Rectangle MegamanBox;
        int interval;

        public idleMegaman(Texture2D texture)
        {
            megaManSheet = texture;

        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman Megaman, int intervalTime, bool isRight)
        {

            megamanSizeX = megamanSize;
            megamanSizeY = megamanSize;
            this.megaman = Megaman;
            interval = intervalTime;
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

            if (interval % 24 < 6)
            {
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
                sourceRectangle = new Rectangle(103, 10, 21, 24);
            }
            else if (interval % 24 < 12)
            {
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
                sourceRectangle = new Rectangle(133, 10, 21, 24);
            }
            else
            {
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX - 1, megamanSizeY);
                sourceRectangle = new Rectangle(160, 10, 20, 24);
            }

            _spriteBatch.Draw(megaManSheet, MegamanBox, sourceRectangle, currentColor, 0f, Vector2.Zero, spriteEffects, 0f);
        }

        public Rectangle getRectangle()
        {
            return MegamanBox;
        }
    }
}