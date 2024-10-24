using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project1.Interfaces;

public class Floor : IBlocks
{
    //Floor modeled as a list of blocks arranged in a straight line.
    private int numBlocks;
    private Vector2 pos;
    public Rectangle boundingBox { get; private set; }

    int blockWidth = 16;
    int blockHeight = 16;

    List<IBlock> blocksInFloorSegment;

    public Floor(int numOfBlocks, Vector2 startPos)
    {
        this.numBlocks = numOfBlocks;
        pos = startPos;

        blocksInFloorSegment = new List<IBlock>(numOfBlocks);
        for (int i = 0; i < numOfBlocks; i++)
        {
            blocksInFloorSegment.Add(BlockSpriteFactory.Instance.CreateFloorMiddle());
        }


        if (blocksInFloorSegment.Count > 0)
        {

            blocksInFloorSegment[0] = BlockSpriteFactory.Instance.CreateFloorEndLeft();
            for (int i = 1; i < numOfBlocks - 1; i++)
            {
                blocksInFloorSegment[i] = BlockSpriteFactory.Instance.CreateFloorMiddle();
            }
            blocksInFloorSegment[numOfBlocks - 1] = BlockSpriteFactory.Instance.CreateFloorEndRight();

            boundingBox = new Rectangle((int)pos.X, (int)pos.Y, blockWidth * numOfBlocks, blockHeight);
        }

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        int blockNum = 0;
        foreach (IBlock block in blocksInFloorSegment)
        {
            block.Draw(spriteBatch, blockNum, blockWidth, pos);
            blockNum++;
        }
       
        

    }


}
