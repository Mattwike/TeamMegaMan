using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.GameObjects;
//using System.Drawing;

namespace Project1.Sprites
{
    public class healthBar : IHealthBarSprite
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

        public healthBar(Texture2D texture)
        {
            healthBarSheet = texture;
        }

        public void Initialize(GraphicsDeviceManager _graphics, Megaman megaman)
        {
            this.megaman = megaman;

            healthBarSizeX = 10;
            healthBarSizeY = 75;
            healthBarY = (int)megaman.y - 370;
            healthBarX = (int)megaman.x - 375;

        }

        public void Update(GameTime gameTime, int ypose)
        {
            healthBarY = ypose + 30;
            healthBarX = (int)megaman.x - 205;

        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            SpriteEffects spriteEffects = SpriteEffects.None;
            
            Rectangle sourceRectangle = new Rectangle(1 + 18 * GetMultiplier(megaman.GetHealth()), 266, 8, 56);
            Rectangle destinationRectangle = new Rectangle(healthBarX, healthBarY, healthBarSizeX, healthBarSizeY);

            _spriteBatch.Draw(healthBarSheet, destinationRectangle, sourceRectangle, Color.White, 0f, Vector2.Zero, spriteEffects, 0f);
        }

        private int GetMultiplier(int health)
        {
            int multiplier;
            if (health >= 140)
            {
                multiplier = 0;
            }
            else if (megaman.GetHealth() >= 120)
            {
                multiplier = 2;
            }
            else if (megaman.GetHealth() >= 100)
            {
                multiplier = 4;
            }
            else if (megaman.GetHealth() >= 80)
            {
                multiplier = 6;
            }
            else if (megaman.GetHealth() >= 60)
            {
                multiplier = 8;
            }
            else if (megaman.GetHealth() >= 40)
            {
                multiplier = 10;
            }
            else if (megaman.GetHealth() >= 20)
            {
                multiplier = 12;
            }
            else
            {
                multiplier = 14;
            }
            return multiplier;
        }
    }
}