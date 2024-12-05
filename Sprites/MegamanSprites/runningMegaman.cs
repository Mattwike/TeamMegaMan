using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class runningMegaman : ISprite
    {

        int megamanSizeX;
        int megamanSizeY;
        private Texture2D megaManSheet;
        private Megaman megaman;
        private Rectangle MegamanBox;
        int interval;

        public runningMegaman(Texture2D texture)
        {
            megaManSheet = texture;
        }

        public void Initialize(GraphicsDeviceManager _graphics, Megaman Megaman, int interval, bool isRight)
        {

            megamanSizeX = 20 + 3;
            megamanSizeY = 26;
            this.megaman = Megaman;
            this.interval = interval;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch _spriteBatch, bool flipHorizontally, bool flipVertically, Color currentColor)
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
                sourceRectangle = new Rectangle(188, 12, 24, 22);
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
            }

            else if (interval % 24 < 12)
            {
                sourceRectangle = new Rectangle(218, 10, 16, 24);
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y - 2, megamanSizeX - 8, megamanSizeY + 2);
            }

            else
            {
                sourceRectangle = new Rectangle(239, 12, 21, 22);
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX - 3, megamanSizeY);
            }

            _spriteBatch.Draw(megaManSheet, MegamanBox, sourceRectangle, currentColor, 0f, Vector2.Zero, spriteEffects, 0f);

        }

        public Rectangle getRectangle()
        {
            return MegamanBox;
        }
    }
}