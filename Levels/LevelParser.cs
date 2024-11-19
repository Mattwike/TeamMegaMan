using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Project1.GameObjects;
using Project1.Interfaces;
using Project1.SpriteFactories;

namespace Project1.Levels
{
    public class LevelParser
    {
        private Dictionary<string, Action<int, int>> tileActions;
        private int blockWidth = 16;
        private int blockHeight = 16;

        public List<IBlocks> Blocks { get; private set; }
        public List<IEnemySprite> Enemies { get; private set; }

        public LevelParser()
        {
            Blocks = new List<IBlocks>();
            Enemies = new List<IEnemySprite>();

            tileActions = new Dictionary<string, Action<int, int>>()
            {
                { "L", (x, y) => CreateFloorBlock(x, y, BlockType.FloorEndLeft) },
                { "M", (x, y) => CreateFloorBlock(x, y, BlockType.FloorMiddle) },
                { "R", (x, y) => CreateFloorBlock(x, y, BlockType.FloorEndRight) },
                { "SJ", (x, y) => CreateEnemy("SJ", x, y) },
                // Add other mappings as needed
            };
        }

        public void ParseLevel(List<string> levelData)
        {
            for (int y = 0; y < levelData.Count; y++)
            {
                string line = levelData[y];
                string[] tokens = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                for (int x = 0; x < tokens.Length; x++)
                {
                    string tileToken = tokens[x];
                    if (tileActions.ContainsKey(tileToken))
                    {
                        tileActions[tileToken](x, y);
                    }
                }
            }
        }

        private void CreateFloorBlock(int x, int y, BlockType blockType)
        {
            Vector2 position = new Vector2(x * blockWidth, y * blockHeight);
            FloorBlock floorBlock = new FloorBlock(position, blockType);
            Blocks.Add(floorBlock);
        }

        private void CreateEnemy(string enemyType, int x, int y)
        {
            Vector2 position = new Vector2(x * blockWidth, y * blockHeight);
            IEnemySprite enemy = null;

            switch (enemyType)
            {
                case "SJ":
                    enemy = EnemySpriteFactory.Instance.CreateSniperJoe(position);
                    break;
                // Add other enemy types as needed
                default:
                    throw new ArgumentException($"Unknown enemy type: {enemyType}");
            }

            Enemies.Add(enemy);
        }
    }
}
