using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Project1;
using Project1.SpriteFactories;
using Project1.Interfaces;
using Project1.GameObjects;
using Project1.States.MegamanState;
using Project1.Commands;
using Project1.Sprites;


public class KeyboardMethods
{
    private Game1 game;
    private Megaman megaman;
    private Dictionary<Keys, ICommand> commandDict = new Dictionary<Keys, ICommand>();
    List<Pellet> pellets;
    Pellet pellet;
    int interval = 0;
    bool mouseClicked = false;
    private bool paused = false;

    public KeyboardMethods(Game1 gameInstance, Megaman megaman, List<Pellet> pellets)
    {
        game = gameInstance;
        this.megaman = megaman;
        this.pellets = pellets;
        //this.commandDict = commandDict;
    }

    public void Initialize()
    {
        commandDict.Add(Keys.A, new RunningLeftMegamanCommand(megaman));
        commandDict.Add(Keys.D, new RunningRightMegamanCommand(megaman));
        commandDict.Add(Keys.L, new RunningShootingLeftMegamanCommand(megaman));
        commandDict.Add(Keys.K, new RunningShootingRightMegamanCommand(megaman));
        commandDict.Add(Keys.OemQuestion, new ClimbingMegamanCommand(megaman));
        commandDict.Add(Keys.D0, new ClimbingShootingLeftMegamanCommand(megaman));
        commandDict.Add(Keys.D9, new ClimbingShootingRightMegamanCommand(megaman));
        commandDict.Add(Keys.D8, new ClimbingReachedTopMegamanCommand(megaman));
        commandDict.Add(Keys.D7, new DamagedMegamanCommand(megaman));
        commandDict.Add(Keys.D6, new FallingMegamanCommand(megaman));
        commandDict.Add(Keys.D5, new FallingShootingMegamanCommand(megaman));
        commandDict.Add(Keys.D4, new IdleShootingMegamanCommand(megaman));
    }

    
    public void CheckFalling(Megaman megaman, GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval, Keys[] pressedKeys)
    {
        if (megaman.is_falling)
        {
            commandDict[Keys.D6].Execute(_graphics, interval);
            megaman.is_running = false;
            megaman.is_shooting = false;
        }
    }
    public void CheckFallingShooting(Megaman megaman, GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval, Keys[] pressedKeys)
    {
        
        if ((megaman.is_jumping || megaman.is_falling) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
        {
            commandDict[Keys.D5].Execute(_graphics, interval);
            megaman.is_running = false;
            megaman.is_shooting = true;
            if (pressedKeys.Contains(Keys.A))
            {
                megaman.x -= 3 * megaman.velocity;
            }
            else if (pressedKeys.Contains(Keys.D))
            {
                megaman.x += 3 * megaman.velocity;
            }

            if (interval % 10 == 0)
            {
                if (megaman.isfacingLeft)
                {
                    pellet = new Pellet();
                    pellet.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval, false);
                    pellets.Add(pellet);
                }
                else
                {
                    pellet = new Pellet();
                    pellet.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval, true);
                    pellets.Add(pellet);
                }
            }
        }
        else if (megaman.is_falling || megaman.is_jumping)
        {
            commandDict[Keys.D6].Execute(_graphics, interval);
            megaman.is_running = false;
            megaman.is_shooting = false;
        }
    }
    public void CheckClimbing(Megaman megaman, GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval, Keys[] pressedKeys)
    {
        if (megaman.is_climbing && pressedKeys.Contains(Keys.W))
        {
            megaman.is_climbing = true;
            megaman.y -= 3;
            commandDict[Keys.OemQuestion].Execute(_graphics, interval);
            //megaman.is_climbing = true;
            megaman.is_running = false;
            megaman.is_shooting = false;
        }
        else if (megaman.is_climbing && pressedKeys.Contains(Keys.S))
        {
            megaman.is_climbing = true;
            megaman.y += 3;
            commandDict[Keys.OemQuestion].Execute(_graphics, interval);
            megaman.is_running = false;
            megaman.is_shooting = false;
        }
        else if (!megaman.is_climable && megaman.is_climbing)
        {
            megaman.is_climbing = false;
        }

        if (megaman.is_climable && pressedKeys.Contains(Keys.W))
        {
            megaman.is_climbing = true;
        }
        else
        {
            megaman.is_climable = false;
        }

        if (megaman.is_climbing && pressedKeys.Contains(Keys.D))
        {
            commandDict[Keys.D9].Execute(_graphics, interval);
            megaman.is_running = false;
            megaman.is_shooting = false;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                megaman.is_shooting = true;
                if (interval % 10 == 0)
                {
                    pellet = new Pellet();
                    pellet.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval, true);
                    pellets.Add(pellet);
                }
            }
            
        }
        if (megaman.is_climbing && pressedKeys.Contains(Keys.A))
        {
            commandDict[Keys.D0].Execute(_graphics, interval);
            megaman.is_running = false;
            megaman.is_shooting = false;
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                megaman.is_shooting = true;
                if (interval % 10 == 0)
                {
                    pellet = new Pellet();
                    pellet.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval, false);
                    pellets.Add(pellet);
                }
            }
            
        }
    }
    public void CheckRunningShooting(Megaman megaman, GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval, Keys[] pressedKeys)
    {
        if (pressedKeys.Contains(Keys.A) && Mouse.GetState().LeftButton == ButtonState.Pressed && !megaman.is_climbing && !megaman.is_jumping && !megaman.is_falling)
        {
            commandDict[Keys.L].Execute(_graphics, interval);
            megaman.x -= 3 * megaman.velocity;
            megaman.is_running = true;
            megaman.is_shooting = true;
            mouseClicked = true;

            if (interval % 10 == 0)
            {
                pellet = new Pellet();
                pellet.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval, false);
                pellets.Add(pellet);
            }
        }
        else if (pressedKeys.Contains(Keys.D) && Mouse.GetState().LeftButton == ButtonState.Pressed && !megaman.is_climbing && !megaman.is_jumping && !megaman.is_falling)
        {
            commandDict[Keys.K].Execute(_graphics, interval);
            megaman.x += 3 * megaman.velocity;
            megaman.is_running = true;
            megaman.is_shooting = true;
            mouseClicked = true;

            if (interval % 10 == 0)
            {
                pellet = new Pellet();
                pellet.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval, true);
                pellets.Add(pellet);
            }
        }
    }
    public void CheckRunning(Megaman megaman, GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval, Keys[] pressedKeys)
    {
        if (!megaman.is_shooting && !megaman.is_climbing && pressedKeys.Contains(Keys.A))
        {
            if (!megaman.is_jumping && !megaman.is_falling)
            {
                commandDict[Keys.A].Execute(_graphics, interval);
            }

            megaman.x -= 3 * megaman.velocity;
            megaman.is_running = true;
            mouseClicked = true;
        }
        else if (!megaman.is_shooting && !megaman.is_climbing && pressedKeys.Contains(Keys.D))
        {
            if (!megaman.is_jumping && !megaman.is_falling)
            {
                commandDict[Keys.D].Execute(_graphics, interval);
            }

            megaman.x += 3 * megaman.velocity;
            megaman.is_running = true;
            mouseClicked = true;
        }
    }
    public void CheckIdle(Megaman megaman, GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval, Keys[] pressedKeys)
    {
        if (!megaman.is_shooting && !megaman.is_running && !megaman.is_falling && !megaman.is_jumping && !megaman.is_climbing)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (interval % 10 == 0)
                {
                    if (megaman.isfacingLeft)
                    {
                        pellet = new Pellet();
                        pellet.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval, false);
                        pellets.Add(pellet);
                    }
                    else
                    {
                        pellet = new Pellet();
                        pellet.Initialize(_graphics, movementSpeed, megamanSize, megaman, interval, true);
                        pellets.Add(pellet);
                    }
                }
                var Idle = new IdleShootingMegamanCommand(megaman);
                Idle.Execute(_graphics, interval);
            }
            else
            {
                var Idle = new IdleMegamanCommand(megaman);
                Idle.Execute(_graphics, interval);
            }

        }
    }
    public void CheckVulnerability(Megaman megaman, GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, int interval)
    {
        if (!megaman.isVulnerable && !megaman.stoodup)
        {
            commandDict[Keys.D7].Execute(_graphics, interval);
        }
    }
}