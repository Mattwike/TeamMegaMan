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
        private Dictionary<string, Action<int, int>> tileActions;
        private int blockWidth = 16;
        private int blockHeight = 16;
        private int maxTokenLength;
        Megaman megaman;

        public List<IBlocks> Blocks { get; private set; }
        public List<IEnemySprite> Enemies { get; private set; }

        public LevelParser(Megaman megaman)
        {
            Blocks = new List<IBlocks>();
            Enemies = new List<IEnemySprite>();
            this.megaman = megaman;

            tileActions = new Dictionary<string, Action<int, int>>()
            {

                { "L", (x, y) => CreateFloorBlock(x, y, BlockType.FloorEndLeft) },
                { "M", (x, y) => CreateFloorBlock(x, y, BlockType.FloorMiddle) },
                { "R", (x, y) => CreateFloorBlock(x, y, BlockType.FloorEndRight) },
                { "D", (x, y) => CreateFloorBlock(x, y, BlockType.BrickEndRight) },
                { "E", (x, y) => CreateFloorBlock(x, y, BlockType.BrickEndLeft) },
                { "V", (x, y) => CreateFloorBlock(x, y, BlockType.BrickMiddle) },
                { "2", (x, y) => CreateFloorBlock(x, y, BlockType.BrickMiddle2) },
                { "3", (x, y) => CreateFloorBlock(x, y, BlockType.BrickEndLeft2) },
                { "4", (x, y) => CreateFloorBlock(x, y, BlockType.BrickEndRight2) },
                { "C", (x, y) => CreateFloorBlock(x, y, BlockType.LadderBlock) },
                { "(", (x, y) => CreateFloorBlock(x -1, y, BlockType.LadderBlock) },
                { ")", (x, y) => CreateFloorBlock(x +1, y, BlockType.LadderBlock) },
                { "8", (x, y) => CreateFloorBlock(x, y, BlockType.BossRoom) },
                { "!", (x, y) => CreateFloorBlock(x -1, y, BlockType.BossRoom) },
                { "$", (x, y) => CreateFloorBlock(x +1, y, BlockType.BossRoom) },
                { "/", (x, y) => CreateFloorBlock(x -1, y, BlockType.BrickMiddle2) },
                { "'", (x, y) => CreateFloorBlock(x -1, y, BlockType.BrickEndLeft2) },
                { ";", (x, y) => CreateFloorBlock(x -1, y, BlockType.BrickEndRight2) },
                { "&", (x, y) => CreateFloorBlock(x +1, y, BlockType.BrickMiddle2) },
                { "%", (x, y) => CreateFloorBlock(x +1, y, BlockType.BrickEndLeft2) },
                { "^", (x, y) => CreateFloorBlock(x +1, y, BlockType.BrickEndRight2) },
                { "T", (x, y) => CreateFloorBlock(x, y, BlockType.BossRoomTop) },
                { "6", (x, y) => CreateFloorBlock(x, y, BlockType.ElectricConnector) },
                { "7", (x, y) => CreateFloorBlock(x, y, BlockType.Electric) },
                { "5", (x, y) => CreateFloorBlock(x, y, BlockType.Door) },
                { "P", (x, y) => CreateFloorBlock(x, y, BlockType.DoorConnector) },
                { "S", (x, y) => CreateFloorBlock(x, y, BlockType.Spike) },


                // Enemy mappings based on your EnemySpriteFactory
                { "JF", (x, y) => CreateEnemy("JF", x, y) }, // Jumping Flea
                { "SD", (x, y) => CreateEnemy("SD", x, y) }, // Screw Driver
                { "BB", (x, y) => CreateEnemy("BB", x, y) }, // Bombomb
                { "OC", (x, y) => CreateEnemy("OC", x - 1, y) }, // Octopus
                { "O2", (x, y) => CreateEnemy("OC", x, y) }, // Octopus
                { "GA", (x, y) => CreateEnemy("GA", x, y) }, // Gabyoall
                { "MA", (x, y) => CreateEnemy("MA", x, y) }, // Mambu
                { "SJ", (x, y) => CreateEnemy("SJ", x, y) }, // Sniper Joe
                { "RB", (x, y) => CreateEnemy("RB", x, y) }, // Red Blaster
                { "KB", (x, y) => CreateEnemy("KB", x, y) }, // Killer Bomb
                { "BM", (x, y) => CreateEnemy("BM", x, y) }, 
                // Add other mappings as per your EnemySpriteFactory


            };

            // Calculate the maximum token length to optimize parsing
            maxTokenLength = 0;
            foreach (var token in tileActions.Keys)
            {
                if (token.Length > maxTokenLength)
                {
                    maxTokenLength = token.Length;
                }
            }
        }



        public void ParseLevel(List<string> levelData)
        {
            for (int y = 0; y < levelData.Count; y++)
            {
                string line = levelData[y];
                int x = 0;
                int index = 0;

                while (index < line.Length)
                {
                    bool matched = false;

                    // Try to match the longest possible token at the current index
                    for (int length = maxTokenLength; length > 0; length--)
                    {
                        if (index + length <= line.Length)
                        {
                            string substring = line.Substring(index, length);
                            if (tileActions.ContainsKey(substring))
                            {
                                tileActions[substring](x, y);
                                index += length;
                                x++;
                                matched = true;
                                break;
                            }
                        }
                    }

                    if (!matched)
                    {
                        // If no token matched, move to the next character
                        index++;
                        x++;
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
                case "JF":
                    enemy = EnemySpriteFactory.Instance.CreateJumpingFlea(position);
                    break;
                case "SD":
                    enemy = EnemySpriteFactory.Instance.CreateScrewDriver(position);
                    break;
                case "BB":
                    enemy = EnemySpriteFactory.Instance.CreateBombomb(position, megaman);
                    break;
                case "OC":
                    enemy = EnemySpriteFactory.Instance.CreateOctopus(position);
                    break;
                case "GA":
                    enemy = EnemySpriteFactory.Instance.CreateGabyoall(position);
                    break;
                case "MA":
                    enemy = EnemySpriteFactory.Instance.CreateMambu(position);
                    break;
                case "SJ":
                    enemy = EnemySpriteFactory.Instance.CreateSniperJoe(position);
                    break;

                case "RB":
                    enemy = EnemySpriteFactory.Instance.CreateRedBlaster(position);
                    break;

                case "KB":
                    enemy = EnemySpriteFactory.Instance.CreateKillerBomb(position, megaman);
                    break;

                case "BM":
                    enemy = BombmanSpriteFactory.Instance.CreateIdleBombMan(position);
                    break;
                // Exclude Bombman-related enemies
                // Add other enemy cases as needed
                default:
                    throw new ArgumentException($"Unknown enemy type: {enemyType}");
            }

            Enemies.Add(enemy);
        }
    }
}
