using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project1.GameObjects;
using Project1.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.SpriteFactories;

namespace Project1.Levels
{
    public class LevelParser
    {
        private Dictionary<char, Action<int, int>> tileActions;
        private int blockWidth = 16;
        private int blockHeight = 16;

        public List<IBlocks> Blocks { get; private set; }

        public LevelParser()
        {
            Blocks = new List<IBlocks>();

            tileActions = new Dictionary<char, Action<int, int>>()
            {
                { 'L', (x, y) => CreateFloorBlock(x, y, BlockType.FloorEndLeft) },
                { 'M', (x, y) => CreateFloorBlock(x, y, BlockType.FloorMiddle) },
                { 'R', (x, y) => CreateFloorBlock(x, y, BlockType.FloorEndRight) },
                { 'D', (x, y) => CreateFloorBlock(x, y, BlockType.BrickEndRight) },
                { 'E', (x, y) => CreateFloorBlock(x, y, BlockType.BrickEndLeft) },
                { 'B', (x, y) => CreateFloorBlock(x, y, BlockType.BrickMiddle) },
                { '2', (x, y) => CreateFloorBlock(x, y, BlockType.BrickMiddle2) },
                { '3', (x, y) => CreateFloorBlock(x, y, BlockType.BrickEndLeft2) },
                { '4', (x, y) => CreateFloorBlock(x, y, BlockType.BrickEndRight2) },
                { 'C', (x, y) => CreateFloorBlock(x, y, BlockType.LadderBlock) },
            // Add other mappings as needed
        };
        }

        public void ParseLevel(List<string> levelData)
        {
            for (int y = 0; y < levelData.Count; y++)
            {
                string line = levelData[y];
                for (int x = 0; x < line.Length; x++)
                {
                    char tileChar = line[x];
                    if (tileActions.ContainsKey(tileChar))
                    {
                        tileActions[tileChar](x, y);
                    }
                }
            }

        }

        private void CreateFloorBlock(int x, int y, BlockType blockType)
        {
            Vector2 position = new Vector2(x * blockWidth, y * blockHeight);
            FloorBlock floorBlock = new FloorBlock(position, blockType);
            Blocks.Add(floorBlock); // Now adding to List<IBlock>
        }
    }
}
