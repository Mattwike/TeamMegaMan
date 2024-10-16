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
        int interval;
        GraphicsDeviceManager graphics;

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
            graphics = _graphics;
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
            Rectangle destinationRectangle;
            
            // TODO: Add your drawing code here
            if (interval % 3 == 0)
            {
                Pellet pellet;
                pellet = new Pellet();
                pellet.Initialize(graphics, movementSpeed, 20, megaman, interval);
                pellet.Draw(_spriteBatch, movementSpeed);
            }
            if (interval % 24 < 6)
            {
                
                sourceRectangle = new Rectangle(14, 46, 31, 24);
                destinationRectangle = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX, megamanSizeY);
                //Pellet pellet;
                
                //new pellet(megaManSheet).Draw(_spriteBatch, movementSpeed, false, false);
                //pellet.Draw(_spriteBatch, movementSpeed, false, false);
                
            }

            else if (interval % 24 < 12)
            {
                sourceRectangle = new Rectangle(50, 48, 29, 22);
                destinationRectangle = new Rectangle((int)megaman.x, (int)megaman.y + 2, megamanSizeX - 2, megamanSizeY - 2);
            }

            else if (interval % 24 < 18)
            {
                sourceRectangle = new Rectangle(84, 46, 26, 24);
                destinationRectangle = new Rectangle((int)megaman.x, (int)megaman.y, megamanSizeX - 5, megamanSizeY);
            }

            else
            {
                sourceRectangle = new Rectangle(113, 48, 30, 22);
                destinationRectangle = new Rectangle((int)megaman.x, (int)megaman.y+2, megamanSizeX - 1, megamanSizeY - 2);
            }

            _spriteBatch.Begin();
            _spriteBatch.Draw(megaManSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
            _spriteBatch.End();
        }
    }
}