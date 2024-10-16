using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class pellet: ISprite
    {

        int pelletSizeX;
        int pelletSizeY;
        int pelletX;
        int pelletY;
        private Texture2D megaManSheet;
        private Megaman megaman;
        int interval;

        public pellet(Texture2D texture)
        {
            megaManSheet = texture;
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman Megaman, int intervalTime)
        {

            pelletSizeX = megamanSize + 10;
            pelletSizeY = megamanSize;
            this.megaman = Megaman;
            interval = intervalTime;
            pelletX = (int)Megaman.x;
            pelletY = (int)Megaman.y;


        }

        public void Update(GameTime gameTime)
        {
            pelletX += 5;
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
            sourceRectangle = new Rectangle(103, 10, 21, 24);
            destinationRectangle = new Rectangle(pelletX, pelletY, pelletSizeX*7, pelletSizeY*7);

            
            

            _spriteBatch.Begin();
            _spriteBatch.Draw(megaManSheet, destinationRectangle, sourceRectangle, Color.Black, 0f, Vector2.Zero, spriteEffects, 0f);
            _spriteBatch.End();
        }
    }
}