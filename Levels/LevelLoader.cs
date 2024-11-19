using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Content;
using System;

namespace Project1.Levels
{
    public class LevelLoader
    {
        private string levelFilePath;

        public LevelLoader(string levelFilePath)
        {
            this.levelFilePath = levelFilePath;
        }

        public List<string> LoadLevelData()
        {
            List<string> levelData = new List<string>();

            // Adjust the path as necessary
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Content", levelFilePath);

            // Check if file exists
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"Level file not found: {fullPath}");
            }

            using (StreamReader reader = new StreamReader(fullPath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    levelData.Add(line);
                }
            }

            return levelData;
        }
    }
}
