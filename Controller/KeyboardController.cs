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


public class KeyboardController : IController
{
    private Game1 game;
    private Megaman megaman;
    Dictionary<Keys, ICommand> commandDict = new Dictionary<Keys, ICommand>();
    List<Pellet> pellets;
    Pellet pellet;
    private Keys[] priorKeys = new Keys[0];
    private KeyboardState previousKeyState;
    int interval = 0;
    bool mouseClicked = false;
    private bool paused = false;
    private KeyboardState previousKeyboardState;
    KeyboardMethods keyboardMethods;
    Game1 gameInstance;
    public bool start = false;

    public KeyboardController(Game1 gameInstance, Megaman megaman, List<Pellet> pellets)
    {
        game = gameInstance;
        this.megaman = megaman;
        this.pellets = pellets;
        this.gameInstance = gameInstance;
        
    }

    public bool isPaused()
    {
        KeyboardState keyState = Keyboard.GetState();

        if (keyState.IsKeyDown(Keys.Escape) && previousKeyboardState.IsKeyUp(Keys.Escape))
        {
            paused = !paused;
        }

        previousKeyboardState = keyState;

        return paused;
    }

    public void checkExit()
    {
        KeyboardState keyState = Keyboard.GetState();

        if (keyState.IsKeyDown(Keys.Q))
        {
            game.Exit();
        }
    }

    public void Initialize()
    {
        keyboardMethods = new KeyboardMethods(gameInstance, megaman, pellets);
        keyboardMethods.Initialize();
    }

    public void Update(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, GameTime gameTime)
    {

        KeyboardState keyboardState = Keyboard.GetState();
        Keys[] pressedKeys = keyboardState.GetPressedKeys();

        megaman.is_running = false;
        megaman.is_shooting = false;
        megaman.is_damaged = false;

        if (pressedKeys.Contains(Keys.Enter))
        {
            start = true;
        }

        if (pressedKeys.Contains(Keys.L))
        {
            megaman.x = 3450;
            megaman.y = 175;
        }

        if (pressedKeys.Contains(Keys.J))
        {
            megaman.x = 5000;
            megaman.y = 900;
        }

        if (!megaman.is_jumping && !megaman.istouchingfloor && !megaman.is_climbing)
        {
            megaman.y += megaman.gravity;
        }
        
        interval++;
       
        //Check reset
        if (pressedKeys.Contains(Keys.R))
        {
            megaman.reset();
        }
        if (pressedKeys.Contains(Keys.M))
        {
            megaman.win = true;
        }

        // Check each megaman state
        megaman.Jump(pressedKeys);
        keyboardMethods.CheckFalling(megaman, _graphics, movementSpeed, megamanSize, interval, pressedKeys);
        keyboardMethods.CheckFallingShooting(megaman, _graphics, movementSpeed, megamanSize, interval, pressedKeys);
        keyboardMethods.CheckClimbing(megaman, _graphics, movementSpeed, megamanSize, interval, pressedKeys);
        keyboardMethods.CheckRunningShooting(megaman, _graphics, movementSpeed, megamanSize, interval, pressedKeys);
        keyboardMethods.CheckRunning(megaman, _graphics, movementSpeed, megamanSize, interval, pressedKeys);
        keyboardMethods.CheckIdle(megaman, _graphics, movementSpeed, megamanSize, interval, pressedKeys);
        keyboardMethods.CheckVulnerability(megaman, _graphics, movementSpeed, megamanSize, interval);

        previousKeyState = keyboardState;

        megaman.Update(gameTime, interval);
    }
    public bool GameStarted()
    {
        KeyboardState keyState = Keyboard.GetState();

        if (keyState.IsKeyDown(Keys.Enter))
        {

            start = true;
        }

        return start;
    }
}