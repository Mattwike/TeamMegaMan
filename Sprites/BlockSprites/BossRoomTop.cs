using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;

namespace Project1.Sprites.BlockSprites
{
    public class BossRoomTop : IBlocks
    {
        private Texture2D blockSheet;
        private Vector2 position;
        public Rectangle boundingBox { get; private set; }

        public BossRoomTop(Texture2D texture, Vector2 position)
        {
            blockSheet = texture;
            this.position = position;
            boundingBox = new Rectangle(0, 0, 16, 16);
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
            Rectangle sourceRect = new Rectangle(1, 52, 16, 16);
            spriteBatch.Draw(blockSheet, position, sourceRect, Color.White);
        }
    }
}