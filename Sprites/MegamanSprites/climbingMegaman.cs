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
        private Rectangle MegamanBox;

        public climbingMegaman(Texture2D texture)
        {
            megaManSheet = texture;
        }

        public void Initialize(GraphicsDeviceManager _graphics, Megaman megaman, int interval, bool isRight)
        {

            megamanSizeX = 20 - 5;
            megamanSizeY = 26;
            this.megaman = megaman;
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
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
                sourceRectangle = new Rectangle(5, 81, 16, 29);
            }
            else
            {
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
                sourceRectangle = new Rectangle(32, 81, 16, 29);
            }

            _spriteBatch.Draw(megaManSheet, MegamanBox, sourceRectangle, currentColor, 0f, Vector2.Zero, spriteEffects, 0f);
        }

        public Rectangle getRectangle()
        {
            return MegamanBox;
        }
    }
}