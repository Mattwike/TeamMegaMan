using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class fallingMegaman : ISprite
    {

        private Texture2D megaManSheet;
        int megamanSizeX;
        int megamanSizeY;
        private Megaman megaman;
        int interval;
        private Rectangle MegamanBox;
        private Rectangle Hitbox;

        public fallingMegaman(Texture2D texture)
        {
            megaManSheet = texture;

        }

        public void Initialize(GraphicsDeviceManager _graphics, Megaman Megaman, int intervalTime, bool isRight)
        {

            megamanSizeX = 20 + 5;
            megamanSizeY = 26 + 7;
            this.megaman = Megaman;
            interval = intervalTime;

            Hitbox.Width = megamanSizeX;
            Hitbox.Height = megamanSizeY;

        }

        public void Update(GameTime gameTime)
        {
            Hitbox.X = (int)megaman.x;
            Hitbox.Y = (int)megaman.y;
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


            MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
            sourceRectangle = new Rectangle(265, 4, 26, 30);

            _spriteBatch.Draw(megaManSheet, MegamanBox, sourceRectangle, currentColor, 0f, Vector2.Zero, spriteEffects, 0f);
        }

        public Rectangle getRectangle()
        {
            return Hitbox;
        }
    }
}