using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Sprites.BlockSprites
{
    public class BrickMiddle2 : IBlocks
    {
        private Texture2D blockSheet;
        private Vector2 position;
        public Rectangle boundingBox { get; private set; }

        public BrickMiddle2(Texture2D texture, Vector2 position)
        {
            blockSheet = texture;
            this.position = position;
            boundingBox = new Rectangle((int)position.X, (int)position.Y, 16, 16);
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
            Rectangle sourceRect = new Rectangle(94, 35, 16, 16);
            spriteBatch.Draw(blockSheet, position, sourceRect, Color.White);
        }
    }
}