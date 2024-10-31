using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.SpriteFactories
{
    public class BlockSpriteFactory
    {
        private static BlockSpriteFactory instance = new BlockSpriteFactory();
        private Texture2D blockSheet;

        // Define source rectangles for each block type
        private Rectangle floorEndLeftSource;
        private Rectangle floorMiddleSource;
        private Rectangle floorEndRightSource;

        public static BlockSpriteFactory Instance => instance;

        private BlockSpriteFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            blockSheet = content.Load<Texture2D>("megamanblocks2");

            // Define the source rectangles based on the positions of the sprites in your texture atlas
            floorEndLeftSource = new Rectangle(77, 1, 16, 16);  
            floorMiddleSource = new Rectangle(94, 1, 16, 16);   
            floorEndRightSource = new Rectangle(111, 1, 16, 16); 

            // Add source rectangles for other block types as needed
        }

        // Methods to get the textures or source rectangles for each block type
        public Texture2D GetBlockSheet()
        {
            return blockSheet;
        }

        public Texture2D GetFloorTexture()
        {
            return blockSheet; // If using a separate texture for floors, adjust accordingly
        }

        public Rectangle GetFloorEndLeftSource()
        {
            return floorEndLeftSource;
        }

        public Rectangle GetFloorMiddleSource()
        {
            return floorMiddleSource;
        }

        public Rectangle GetFloorEndRightSource()
        {
            return floorEndRightSource;
        }

        // Add methods for other block types as needed
    }
}
