using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Sprites
{
    public class idleMegaman : ISprite
    {
        int currentFrame;
        int totalFrame;
        int delayCounter;
        int delayMax;
        float x;
        float y;
        private Texture2D megaManSheet;
        int megamanSizeX;
        int megamanSizeY;
        public idleMegaman(Texture2D texture)
        {
            megaManSheet = texture;
            x = 15;
            y = 15;
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize)
        {
            currentFrame = 0;
            totalFrame = 3;
            delayCounter = 0;
            delayMax = 10;
            megamanSizeX = megamanSize;
            megamanSizeY = megamanSize;
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


        public void Draw(Texture2D spriteTexture, SpriteBatch _spriteBatch, float movementSpeed, bool flipHorizontally, bool flipVertically)
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
                destinationRectangle = new Rectangle((int)x, (int)y, megamanSizeX, megamanSizeY);
                sourceRectangle = new Rectangle(103, 10, 21, 24);
            }
            else if (currentFrame == 1)
            {
                destinationRectangle = new Rectangle((int)x, (int)y, megamanSizeX, megamanSizeY);
                sourceRectangle = new Rectangle(133, 10, 21, 24);
            }
            else
            {
                destinationRectangle = new Rectangle((int)x, (int)y, megamanSizeX - 1, megamanSizeY);
                sourceRectangle = new Rectangle(160, 10, 20, 24);
            }

            _spriteBatch.Begin();
            _spriteBatch.Draw(spriteTexture, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
            _spriteBatch.End();
        }
    }
}