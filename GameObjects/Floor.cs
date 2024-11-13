using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.SpriteFactories;

namespace Project1.GameObjects
{
    public class Floor : IBlocks
    {
        // Fields for original floor creation
        private int numBlocks;
        private Vector2 pos;
        private List<IBlocks> blocksInFloorSegment;

        // Fields for single block creation
        private Vector2 position;
        private Texture2D texture;
        public Rectangle boundingBox { get; private set; }

        private int blockWidth = 16;
        private int blockHeight = 16;

        // Original constructor (Restored)
        public Floor(int numOfBlocks, Vector2 startPos)
        {
            this.numBlocks = numOfBlocks;
            pos = startPos;

            blocksInFloorSegment = new List<IBlocks>(numOfBlocks);

            // Initialize the blocks in the floor segment
            for (int i = 0; i < numOfBlocks; i++)
            {
                Vector2 blockPosition = new Vector2(pos.X + i * blockWidth, pos.Y);
                BlockType blockType = BlockType.FloorMiddle;

                if (i == 0)
                    blockType = BlockType.FloorEndLeft;
                else if (i == numBlocks - 1)
                    blockType = BlockType.FloorEndRight;

                IBlocks block = new FloorBlock(blockPosition, blockType);
                blocksInFloorSegment.Add(block);
            }

            // Calculate bounding box
            boundingBox = new Rectangle((int)pos.X, (int)pos.Y, blockWidth * numBlocks, blockHeight);
        }

        // New constructor for level loader
        public Floor(Vector2 position)
        {
            this.position = position;
            this.texture = BlockSpriteFactory.Instance.GetFloorTexture();
            boundingBox = new Rectangle((int)position.X, (int)position.Y, blockWidth, blockHeight);
        }

        public void Initialize()
        {
            // Initialization logic if necessary
        }

        // Implement Update method from IBlocks
        public void Update()
        {
            // Update logic for the floor
            if (blocksInFloorSegment != null)
            {
                foreach (var block in blocksInFloorSegment)
                {
                    block.Update();
                }
            }
            else
            {
                // Update logic for single block
            }
        }

        // Implement Draw method from IBlocks
        public void Draw(SpriteBatch spriteBatch)
        {
            if (blocksInFloorSegment != null)
            {
                foreach (IBlocks block in blocksInFloorSegment)
                {
                    block.Draw(spriteBatch);
                }
            }
            else
            {
                spriteBatch.Draw(texture, position, Color.White);
            }
        }
    }
}
