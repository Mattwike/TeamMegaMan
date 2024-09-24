using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class climbingMegaman : ISprite
    {
        int currentFrame;    // Make sure to use camelCase consistently
        int totalFrame;
        int delayCounter;
        int delayMax;
        float x;
        float y;
        int megamanSizeX;
        int megamanSizeY;
        private Texture2D megaManSheet;
        private Megaman megaman;

        public climbingMegaman(Texture2D texture)
        {
            megaManSheet = texture;
            x = 200;
            y = 15;
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman megaman)
        {
            currentFrame = 0;
            totalFrame = 2;
            delayCounter = 0;
            delayMax = 10;
            megamanSizeX = megamanSize - 5;
            megamanSizeY = megamanSize;
            this.megaman = megaman;
        }

        public void Update(GameTime gameTime)
        {
            delayCounter++;
            if (delayCounter >= delayMax)
            {
                currentFrame++;
                if (currentFrame >= totalFrame)
                {
                    currentFrame = 0;
                }
                delayCounter = 0;
            }
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

            if (currentFrame == 0)
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