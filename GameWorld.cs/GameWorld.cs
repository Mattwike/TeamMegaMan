using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project1.SpriteFactories;
using Project1.Sprites;
using Project1.GameObjects;
using Project1.Interfaces;
using Project1.Collisions;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Media;
using Project1.GameControllers;
using Microsoft.Xna.Framework.Content;
using Project1.Levels;
using System.IO;
using System;
using System.Threading;

namespace Project1.Level
{
    public class GameWorld
    {

        private Megaman megaman;
        private List<IBlocks> levelBlocks;
        private List<IEnemySprite> levelEnemies;
        private List<IEnemyProjectile> levelProjectiles;
        private List<Pellet> pellets;
        private List<EnemyDrop> enemyDropList;
        private int ypose;
        private int interval;
        private SpriteFont font;
        private SpriteFont GameOverFont;
        public int scoreX = 10;
        private bool MegamanDied = false;
        private HealthBar healthBar;
        private GraphicsDeviceManager graphics;
        private Camera camera;

        public GameWorld(GraphicsDeviceManager _graphics)
        {
            levelBlocks = new List<IBlocks>();
            levelEnemies = new List<IEnemySprite>();
            pellets = new List<Pellet>();
            enemyDropList = new List<EnemyDrop>();
            levelProjectiles = new List<IEnemyProjectile>();
            graphics = _graphics;
            megaman = new Megaman();
            healthBar = new HealthBar();
        }
        public void Initialize(ContentManager Content, List<IBlocks> blocks, List<IEnemySprite> enemies, Camera camera, SpriteFont font)
        {
            megaManSpriteFactory.Instance.LoadAllTextures(Content);
            EnemySpriteFactory.Instance.LoadAllTextures(Content);
            BombmanSpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.LoadAllTextures(Content);
            pelletSpriteFactory.Instance.CreatePellet();
            EnemyDropSpriteFactory.Instance.LoadAllTextures(Content);
            EnemyDropSpriteFactory.Instance.CreateEnemyDrop();
            healthBarSpriteFactory.Instance.LoadAllTextures(Content);
            healthBarSpriteFactory.Instance.CreateHealthBar();
            BlockSpriteFactory.Instance.LoadAllTextures(Content);

            megaman.Initialize(graphics, interval);
            megaman.reachedCheckpoint();
            healthBar.Initialize(graphics, megaman);
            levelBlocks = blocks;
            levelEnemies = enemies;
            this.camera = camera;
            this.font = font;

            foreach (var enemy in levelEnemies)
            {
                enemy.Initialize(graphics, 3, 40);

            }
        }

        public void Update(GameTime gameTime)
        {
            healthBar.Update(gameTime, ypose);
            megaman.Update(gameTime, interval);
            CollidionHandler.HandleMegamanCollisions(megaman, levelBlocks, levelEnemies, enemyDropList, levelProjectiles);
            foreach (var pellet in pellets)
            {
                pellet.Update(gameTime);
            }
            foreach (var enemyDrop in enemyDropList)
            {
                enemyDrop.Update(gameTime);
            }
            foreach (var enemy in levelEnemies)
            {
                enemy.Update(gameTime, camera, (int)megaman.x);
                if (enemy.hasProjectiles)
                {
                    if (enemy.GetProjectiles() != null)
                    {
                        levelProjectiles.AddRange(enemy.GetProjectiles());
                    }
                }
                CollidionHandler.HandleEnemyCollisions(enemy, levelBlocks, pellets, enemyDropList);
            }
            foreach (var block in levelBlocks)
            {
                block.Update();
            }

            camera.Position = new Vector2(megaman.x, ypose);
            scoreX = (int)megaman.x;

            if (!megaman.is_jumping && !megaman.is_falling)
            {
                ypose = (int)megaman.y - 210;
            }

            if (megaman.GetHealth() <= 0 || megaman.y > 1200)
            {
                MegamanDied = true;
            }
            else
            {
                MegamanDied = false;
            }

        }

        public void Draw(SpriteBatch _spriteBatch)
        {

            _spriteBatch.Begin(transformMatrix: camera.GetTransform());

            foreach (var block in levelBlocks)
            {
                block.Draw(_spriteBatch);
            }

            // Draw MegaMan and displayed enemy as before
            megaman.Draw(_spriteBatch, 10);
            _spriteBatch.DrawString(font, megaman.GetHealth().ToString(), new Vector2(scoreX - 150, ypose - 50), Color.White);
            _spriteBatch.DrawString(font, megaman.GetScore().ToString(), new Vector2(scoreX, ypose + 30), Color.White);

            healthBar.Draw(_spriteBatch);

            foreach (var pellet in pellets)
            {
                pellet.Draw(_spriteBatch, 3);
            }
            foreach (var enemyDrop in enemyDropList)
            {
                enemyDrop.Draw(_spriteBatch, 3);
            }
            foreach (var enemy in levelEnemies)
            {
                enemy.Draw(_spriteBatch, false, false);
            }

            _spriteBatch.End();
        }
    }
}
