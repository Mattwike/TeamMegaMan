using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class pellet : ISprite
    {

        int pelletSizeX;
        int pelletSizeY;
        int pelletX;
        int pelletY;
        private Texture2D pelletSheet;
        private Megaman megaman;
        int interval;
        bool isRight;

        public pellet(Texture2D texture)
        {
            pelletSheet = texture;
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman Megaman, int intervalTime, bool isRight)
        {

            pelletSizeX = 10;
            pelletSizeY = 10;
            this.megaman = Megaman;
            interval = intervalTime;
            if (isRight)
            {
                pelletX = (int)Megaman.x + 20 + megamanSize / 2;
            }
            else
            {
                pelletX = (int)Megaman.x - 20 + megamanSize / 2;
            }

            pelletY = (int)Megaman.y + 12;
            this.isRight = isRight;

        }

        public void Update(GameTime gameTime)
        {
            if (isRight)
            {
                pelletX += 5;
            }
            else
            {
                pelletX -= 5;
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

            // TODO: Add your drawing code here

            //pelletX += 5;
            sourceRectangle = new Rectangle(253, 13, 9, 7);
            destinationRectangle = new Rectangle(pelletX, pelletY, pelletSizeX, pelletSizeY);




            _spriteBatch.Draw(pelletSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        }
        public Rectangle getRectangle()
        {
            return new Rectangle();
        }
    }
}