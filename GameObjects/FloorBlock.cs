using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.SpriteFactories;

namespace Project1.GameObjects
{
    public enum BlockType
    {
        FloorEndLeft,
        FloorMiddle,
        FloorEndRight,
        // Add other block types as needed
    }

    public class FloorBlock : IBlock
    {
        public Rectangle boundingBox { get; private set; }
        private Vector2 position;
        private Texture2D texture;
        private Rectangle sourceRectangle;
        private int blockWidth = 16;
        private int blockHeight = 16;

        public FloorBlock(Vector2 position, BlockType blockType)
        {
            this.position = position;
            this.texture = BlockSpriteFactory.Instance.GetBlockSheet();

            // Get the appropriate source rectangle based on block type
            switch (blockType)
            {
                case BlockType.FloorEndLeft:
                    sourceRectangle = BlockSpriteFactory.Instance.GetFloorEndLeftSource();
                    break;
                case BlockType.FloorMiddle:
                    sourceRectangle = BlockSpriteFactory.Instance.GetFloorMiddleSource();
                    break;
                case BlockType.FloorEndRight:
                    sourceRectangle = BlockSpriteFactory.Instance.GetFloorEndRightSource();
                    break;
                default:
                    sourceRectangle = BlockSpriteFactory.Instance.GetFloorMiddleSource();
                    break;
            }

            boundingBox = new Rectangle((int)position.X, (int)position.Y, blockWidth, blockHeight);
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
            spriteBatch.Begin();
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}
