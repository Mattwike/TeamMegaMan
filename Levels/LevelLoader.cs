using System;
using System.Collections.Generic;
using System.IO;

namespace Project1.Levels
{
    public class LevelLoader
    {
        public List<string> LoadLevel(string filePath)
        {
            List<string> levelData = new List<string>();

            // Get the full path to the file relative to the executable
            string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);

            // Output the full path for debugging
            Console.WriteLine($"Attempting to load level file from: {fullPath}");

            if (!File.Exists(fullPath))
            {
                Console.WriteLine($"Level file not found: {fullPath}");
                throw new FileNotFoundException("Level file not found.", fullPath);
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
