using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project1.Interfaces;
using Project1.States.MegamanState;
using Project1.Collision;
using Project1.Enum;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Project1.Collisions;
using System.ComponentModel.Design.Serialization;
using Project1.Levels;
using Project1.Sprites;
using System;

namespace Project1.GameObjects
{
    public class Megaman
    {
        public bool is_running = false;
        public bool is_shooting = false;
        public bool is_damaged;
        public bool is_jumping = false;
        public bool is_falling = false;
        public bool is_climbing = false;
        public bool reached_top = false;
        public bool is_climable = false;
        public bool stoodup = true;
        private float origionalx;
        private float origionaly;
        public bool isVulnerable;
        private double invulnerabilityTimer = 0;
        private int timeInvunerable = 6500;
        public float gravity = 4.5f;
        public int MegamanSize;
        public Rectangle MegamanBox;
        private int count = 100;
        public bool istouchingfloor;
        public float velocity = 1f;
        public Color currentColor = Color.White;

        public IMegamanState State;

        public float x {  get; set; }
        public float y { get; set; }

        public bool isfacingLeft { get; set; }
        private LevelParser levelparser;


        public int megamanHealth = 140;
        public int megamanScore = 0;

        public Megaman()
        {
            State = new IdleMegamanState(this);
            isVulnerable = true;
            is_damaged = false;
        }

        public void SetDirection(bool isFacingLeft)
        {
            isfacingLeft = isFacingLeft;
        }

        public void reachedCheckpoint()
        {
            origionalx = x;
            origionaly = y;
        }

        public void reset()
        {
            x = origionalx;
            y = origionaly;
            megamanHealth = 140;
            isVulnerable = true;
            currentColor = Color.White;
        }

        public void ChangeDirection()
        {
            State.ChangeDirection();
        }

        public void Update(GameTime gameTime, int interval)
        {
            MegamanBox = new Rectangle((int)x, (int)y, 18, 24);
            State.Update(gameTime);
            CheckVulnerability(gameTime);
            
        }

        public void TakeDamage()
        {
            if (isVulnerable)
            {
                megamanHealth -= 20;
                isVulnerable = false;
                stoodup = false;
                invulnerabilityTimer = 0;
                currentColor = Color.White;
            }
        }

        public void Initialize(GraphicsDeviceManager _graphics, int interval)
        {
            State.Initialize(_graphics, interval);
            x = 0;
            y = 1113;
        }

        public void Draw(SpriteBatch _spriteBatch, float movementSpeed)
        {
            State.Draw(_spriteBatch, currentColor);
        }
        
        public void Jump(Keys[] pressedKeys)
        {

            if (!is_jumping && !is_falling && pressedKeys.Contains(Keys.Space))
            {
                is_jumping = true;
                gravity = 5f;

            }

            if (is_jumping)
            {
                if (gravity > 0)
                {
                    y -= gravity;
                    gravity -= .25f;
                }
                else
                {
                    is_jumping = false;
                    is_falling = true;
                }
            }
            else if (is_falling)
            {
                if (!istouchingfloor)
                {

                    gravity += .25f;
                }
                else
                {
                    is_falling = false;
                    gravity = 4.5f;
                }
            }
        }
        public int GetHealth()
        {
            return megamanHealth;
        }
        public int GetScore()
        {
            return megamanScore;
        }
        public void UpdateScore(int increase)
        {
            megamanScore += increase;
        }
        public void CheckVulnerability(GameTime gameTime)
        {
            if (!isVulnerable)
            {
                invulnerabilityTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
                if (invulnerabilityTimer >= timeInvunerable)
                {
                    isVulnerable = true;
                    invulnerabilityTimer = 0;
                }
                if (!isVulnerable)
                {
                    invulnerabilityTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

                    if ((int)(invulnerabilityTimer / 100) % 2 == 0)
                    {
                        currentColor = Color.White;
                    }
                    else
                    {
                        currentColor = Color.Blue;
                    }

                    if(invulnerabilityTimer >= 1500)
                    {
                        stoodup = true;
                    }

                    if (invulnerabilityTimer >= timeInvunerable * 100)
                    {
                        isVulnerable = true;
                        invulnerabilityTimer = 0;
                        currentColor = Color.Blue;

                    }
                }
                else
                {
                    currentColor = Color.White;
                }
            }
        }
    }
}