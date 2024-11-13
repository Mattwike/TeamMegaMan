using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Project1.Sprites;
using Project1.GameObjects;

namespace Project1.SpriteFactories
{

    public class healthBarSpriteFactory
    {
        private static healthBarSpriteFactory instance = new healthBarSpriteFactory();
        private Texture2D healthBarSheet;

        public static healthBarSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private healthBarSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            healthBarSheet = content.Load<Texture2D>("MegamanHealthBarSprites");
        }

        public IHealthBarSprite CreateHealthBar()
        {
            return new healthBar(healthBarSheet);
        }

    }
}