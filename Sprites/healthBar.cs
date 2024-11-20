using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;
//using System.Drawing;

namespace Project1.Sprites
{
    public class healthBar: IHealthBarSprite
    {

        int healthBarSizeX;
        int healthBarSizeY;
        int healthBarX;
        int healthBarY;
        private Texture2D healthBarSheet;
        private Megaman megaman;
        int interval;
        bool isRight;
        bool isVisible = true;
        Rectangle sourceRectangle;

        public healthBar(Texture2D texture)
        {
            healthBarSheet = texture;
        }

        public void Initialize(GraphicsDeviceManager _graphics, Megaman megaman)
        {

            healthBarSizeX = 10;
            healthBarSizeY = 100;
            this.megaman = megaman;

            healthBarY = (int)megaman.y - 370;
            healthBarX = (int)megaman.x - 375;
            
        }

        public void Update(GameTime gameTime)
        {
            healthBarY = (int)megaman.y - 370;
            healthBarX = (int)megaman.x - 375;

        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            
            SpriteEffects spriteEffects = SpriteEffects.None;

            Rectangle destinationRectangle;

            sourceRectangle = new Rectangle(1, 266, 8, 56);
            destinationRectangle = new Rectangle(healthBarX, healthBarY, healthBarSizeX, healthBarSizeY);


            _spriteBatch.Draw(healthBarSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        }
        
        
    }
}