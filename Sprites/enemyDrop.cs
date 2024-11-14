using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;
//using System.Drawing;

namespace Project1.Sprites
{
    public class enemyDrop : IEnemyDropSprite
    {

        int enemyDropSizeX;
        int enemyDropSizeY;
        int enemyDropX;
        int enemyDropY;
        private Texture2D pelletSheet;
        private Megaman megaman;
        int interval;
        Rectangle hitbox;
        bool isVisible = true;

        public enemyDrop(Texture2D texture)
        {
            pelletSheet = texture;
        }

        public void Initialize(GraphicsDeviceManager _graphics, int enemyX, int enemyY)
        {

            enemyDropSizeX = 10;
            enemyDropSizeY = 10;

            enemyDropY = enemyY + 12;
            enemyDropX = enemyX + 12;

            hitbox.X = enemyDropX;
            hitbox.Y = enemyDropY;
            hitbox.Width = 10;
            hitbox.Height = 10;

        }

        public void Update(GameTime gameTime)
        {
            if (!isVisible)
            {
                return;
            }

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            if (!isVisible)
            {
                return;
            }

            SpriteEffects spriteEffects = SpriteEffects.None;

    

            Rectangle sourceRectangle;
            Rectangle destinationRectangle;

            // TODO: Add your drawing code here

            //pelletX += 5;
            sourceRectangle = new Rectangle(253, 13, 9, 7);
            destinationRectangle = new Rectangle(enemyDropX, enemyDropY, enemyDropSizeX, enemyDropSizeY);




            _spriteBatch.Draw(pelletSheet, destinationRectangle, sourceRectangle, Color.Blue, 0f, Vector2.Zero, spriteEffects, 0f);
        }
        public Rectangle getRectangle()
        {
            return hitbox;
        }
        public void removeEnemyDrop()
        {
            enemyDropY += 1000;
            hitbox.Y += 1000;
            isVisible = false;
        }
        
    }
}