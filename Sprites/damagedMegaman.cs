using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class damagedMegaman : ISprite
    {

        int megamanSizeX;
        int megamanSizeY;
        private Texture2D megaManSheet;
        private Megaman megaman;
        private Rectangle MegamanBox;
        int interval;

        public damagedMegaman(Texture2D texture)
        {
            megaManSheet = texture;

        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman Megaman, int interval, bool isRight)
        {
        
            megamanSizeX = megamanSize + 7;
            megamanSizeY = megamanSize;
            this.megaman = Megaman;
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

            if (interval % 24 < 6)
            {
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
                sourceRectangle = new Rectangle(213, 40, 28, 30);
            }
            else if (interval % 24 < 12)
            {
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y + 2, megamanSizeX - 2, megamanSizeY - 2);
                sourceRectangle = new Rectangle(248, 42, 26, 28);
            }
            else
            {
                MegamanBox = new Rectangle((int)megaman.x, (int)megaman.y + 8, megamanSizeX, megamanSizeY - 8);
                sourceRectangle = new Rectangle(280, 48, 28, 22);
            }

            _spriteBatch.Draw(megaManSheet, MegamanBox, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        }

        public Rectangle getRectangle()
        {
            return MegamanBox;
        }
    }
}