using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class fallingShootingMegaman : ISprite
    {

        private Texture2D megaManSheet;
        int megamanSizeX;
        int megamanSizeY;
        private Megaman megaman;
        private Rectangle MegamanBox;
        int interval;

        public fallingShootingMegaman(Texture2D texture)
        {
            megaManSheet = texture;

        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman Megaman, int intervalTime)
        {

            megamanSizeX = megamanSize + 11;
            megamanSizeY = megamanSize + 10;
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


            MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
            sourceRectangle = new Rectangle(146, 40, 29, 30);

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