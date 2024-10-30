using System.Collections.Generic;
using System.IO;

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.SpriteFactories;
using Project1.GameObjects;


namespace Project1.Levels
{
    public class LevelLoader
    {
        public List<string> LoadLevel(string filePath)
        {
            List<string> levelData = new List<string>();

            using (StreamReader reader = new StreamReader(filePath))
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
