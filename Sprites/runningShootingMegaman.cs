using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class runningShootingMegaman : ISprite
    {

        int megamanSizeX;
        int megamanSizeY;
        private Texture2D megaManSheet;
        private Megaman megaman;
        private Rectangle MegamanBox;
        int interval;

        public runningShootingMegaman(Texture2D texture)
        {
            megaManSheet = texture;
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman Megaman, int intervalTime)
        {

            megamanSizeX = megamanSize + 10;
            megamanSizeY = megamanSize;
            this.megaman = Megaman;
            interval = intervalTime;
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

            // TODO: Add your drawing code here
            if (interval % 24 < 6)
            {
                
                sourceRectangle = new Rectangle(14, 46, 31, 24);
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
            }

            else if (interval % 24 < 12)
            {
                sourceRectangle = new Rectangle(50, 48, 29, 22);
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y + 2, megamanSizeX - 2, megamanSizeY - 2);
            }

            else if (interval % 24 < 18)
            {
                sourceRectangle = new Rectangle(84, 46, 26, 24);
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX - 5, megamanSizeY);
            }

            else
            {
                sourceRectangle = new Rectangle(113, 48, 30, 22);
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y+2, megamanSizeX - 1, megamanSizeY - 2);
            }

            _spriteBatch.Begin();
            _spriteBatch.Draw(megaManSheet, MegamanBox, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
            _spriteBatch.End();
        }

        public Rectangle getRectangle()
        {
            return MegamanBox;
        }

    }
}