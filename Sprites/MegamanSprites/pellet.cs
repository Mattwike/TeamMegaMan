using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;

namespace Project1.Sprites
{
    public class pellet : IPelletSprite
    {

        int pelletSizeX;
        int pelletSizeY;
        int pelletX;
        int pelletY;
        private Texture2D pelletSheet;
        int interval;
        bool isRight;
        Rectangle hitbox;
        bool isVisible = true;

        public pellet(Texture2D texture)
        {
            pelletSheet = texture;
        }

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, Megaman megaman, int intervalTime, bool isRight)
        {
            if (isRight)
            {
                pelletX = (int)megaman.x + 20;
            }
            else
            {
                pelletX = (int)megaman.x - 20 + megamanSize / 2;
            }
            pelletY = (int)megaman.y + 5;

            pelletSizeX = 10;
            pelletSizeY = 7;
            hitbox.X = pelletX;
            hitbox.Y = pelletY;
            hitbox.Width = pelletSizeX;
            hitbox.Height = pelletSizeY;
            this.isRight = isRight;

        }

        public void Update(GameTime gameTime)
        {
            if (!isVisible)
            {
                return;
            }
            if (isRight)
            {
                pelletX += 5;
                hitbox.X += 5;
            }
            else
            {
                pelletX -= 5;
                hitbox.X -= 5;
            }

        }

        public void Draw(SpriteBatch _spriteBatch, float movementSpeed, bool flipHorizontally, bool flipVertically, Color color)
        {
            if (!isVisible)
            {
                return;
            }

            SpriteEffects spriteEffects = SpriteEffects.None;

            Rectangle sourceRectangle;
            Rectangle destinationRectangle;
            sourceRectangle = new Rectangle(253, 13, 9, 7);
            destinationRectangle = new Rectangle(pelletX, pelletY, pelletSizeX, pelletSizeY);

            _spriteBatch.Draw(pelletSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        }
        public Rectangle getRectangle()
        {
            return hitbox;
        }
        public void removePellet()
        {
            pelletY += 1000;
            hitbox.Y += 1000;
            isVisible = false;
        }
        
    }
}