using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Project1.Sprites;
using Project1.GameObjects;

namespace Project1.SpriteFactories
{

    public class pelletSpriteFactory
    {
        private static pelletSpriteFactory instance = new pelletSpriteFactory();
        private Texture2D pelletSheet;

        public static pelletSpriteFactory Instance
        {
            get
            {
                return instance;
            }
        }

        private pelletSpriteFactory()
        {
        }

        public void LoadAllTextures(ContentManager content)
        {
            pelletSheet = content.Load<Texture2D>("PelletSpriteSheet");
        }

        public IPelletSprite CreatePellet()
        {
            return new pellet(pelletSheet);
        }


    }
}