using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Project1.Sprites;
using Project1.GameObjects;

namespace Project1.SpriteFactories
{

    public class EnemyDropSpriteFactory
    {
        private static EnemyDropSpriteFactory instance = new EnemyDropSpriteFactory();
        private Texture2D pelletSheet;

        public static EnemyDropSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private EnemyDropSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            pelletSheet = content.Load<Texture2D>("PelletSpriteSheet");
        }

        public IEnemyDropSprite CreateEnemyDrop()
        {
            return new enemyDrop(pelletSheet);
        }


    }
}