using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project1.Sprites
{
    public class damagedMegaman : ISprite
    {
        int currentframe;
        int totalframe;
        int delaycounter;
        int delaymax;
        float x;
        float y;
        int megamanSizeX;
        int megamanSizeY;
        private Texture2D megaManSheet;

        public damagedMegaman(Texture2D texture)
        {
            megaManSheet = texture;

        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
        {
            currentframe = 0;
            totalframe = 3;
            delaycounter = 0;
            delaymax = 10;
            x = 150;
            y = 15;
            megamanSizeX = megamanSize + 7;
            megamanSizeY = megamanSize;
        }

        public void Update(GameTime gameTime)
        {

            if (delaycounter == delaymax)
            {
                currentframe++;
                delaycounter = 0;
            }

            if (currentframe == totalframe)
            {
                currentframe = 0;
            }
            delaycounter++;
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

            if (currentframe == 0)
            {
                destinationRectangle = new Rectangle((int)x, (int)y, megamanSizeX, megamanSizeY);
                sourceRectangle = new Rectangle(213, 40, 28, 30);
            }
            else if (currentframe == 1)
            {
                destinationRectangle = new Rectangle((int)x, (int)y + 2, megamanSizeX - 2, megamanSizeY - 2);
                sourceRectangle = new Rectangle(248, 42, 26, 28);
            }
            else
            {
                destinationRectangle = new Rectangle((int)x, (int)y + 8, megamanSizeX, megamanSizeY - 8);
                sourceRectangle = new Rectangle(280, 48, 28, 22);
            }

            _spriteBatch.Begin();
            _spriteBatch.Draw(megaManSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
            _spriteBatch.End();
        }
    }
}