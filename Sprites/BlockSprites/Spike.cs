using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Sprites.BlockSprites
{
    public class Spike : IBlocks
    {
        private Texture2D blockSheet;
        private Vector2 position;
        public Rectangle boundingBox { get; private set; }

        public Spike(Texture2D texture, Vector2 position)
        {
            blockSheet = texture;
            this.position = position;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 0, 0);
        }

        public void Initialize()
        {
            // Initialization logic if necessary
        }

        public void Update()
        {
            // Update logic if necessary

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Rectangle sourceRect = new Rectangle(64, 35, 16, 16);
            spriteBatch.Draw(blockSheet, position, sourceRect, Color.White);
        }
    }
}