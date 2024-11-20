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
        BrickEndRight,
        BrickEndLeft,
        BrickMiddle, 
        BrickMiddle2,
        BrickEndLeft2,
        BrickEndRight2,
        LadderBlock,
        // Add other block types as needed
    }

    public class FloorBlock : IBlocks
    {
        public Rectangle boundingBox { get; private set; }
        private Vector2 position;
        private Texture2D texture;
        private Rectangle sourceRectangle;
        private int blockWidth = 16;
        private int blockHeight = 16;
        public bool IsLadder { get; private set; }

        public FloorBlock(Vector2 position, BlockType blockType)
        {
            this.position = position;
            this.texture = BlockSpriteFactory.Instance.GetBlockSheet();

            // Get the appropriate source rectangle based on block type
            switch (blockType)
            {
                case BlockType.FloorEndLeft:
                    sourceRectangle = BlockSpriteFactory.Instance.GetFloorEndLeftSource();
                    IsLadder = false;
                    break;
                case BlockType.FloorMiddle:
                    sourceRectangle = BlockSpriteFactory.Instance.GetFloorMiddleSource();
                    IsLadder = false;
                    break;
                case BlockType.FloorEndRight:
                    sourceRectangle = BlockSpriteFactory.Instance.GetFloorEndRightSource();
                    IsLadder = false;
                    break;
                case BlockType.BrickEndRight:
                    sourceRectangle = BlockSpriteFactory.Instance.GetBrickEndRightSource();
                    IsLadder = false;
                    break;
                case BlockType.BrickEndLeft:
                    sourceRectangle = BlockSpriteFactory.Instance.GetBrickEndLeftSource();
                    IsLadder = false;
                    break;
                case BlockType.BrickMiddle:
                    sourceRectangle = BlockSpriteFactory.Instance.GetBrickMiddleSource();
                    IsLadder = false;
                    break;
                case BlockType.BrickMiddle2:
                    sourceRectangle = BlockSpriteFactory.Instance.GetBrickMiddle2Source();
                    IsLadder = false;
                    break;
                case BlockType.BrickEndLeft2:
                    sourceRectangle = BlockSpriteFactory.Instance.GetBrickEndLeft2Source();
                    IsLadder = false;
                    break;
                case BlockType.BrickEndRight2:
                    sourceRectangle = BlockSpriteFactory.Instance.GetBrickEndRight2Source();
                    IsLadder = false;
                    break;
                case BlockType.LadderBlock:
                    sourceRectangle = BlockSpriteFactory.Instance.GetLadderBlockSource();
                    IsLadder = true;
                    break;
                default:
                    sourceRectangle = BlockSpriteFactory.Instance.GetFloorMiddleSource();
                    IsLadder = false;
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
            
            spriteBatch.Draw(texture, position, sourceRectangle, Color.White);
            
        }
    }
}
