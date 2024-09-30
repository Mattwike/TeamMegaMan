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

public class KeyboardController : IController
{
    private Game1 game;
    private Megaman megaman;
    private GenericEnemy displayedEnemy;
    private Dictionary<Keys, ICommand> commandDict = new Dictionary<Keys, ICommand>();
    private Keys[] priorKeys = new Keys[0];
    private KeyboardState previousKeyState;
    int interval = 0;
    private bool is_jumping = false;
    private bool is_falling = false;
    private bool is_climbing = false;
    private bool reached_top = false;
    //private bool is_damaged = false;
    private float initialY;
    private float gravity = 4.5f;


    public KeyboardController(Game1 gameInstance, Megaman megaman, GenericEnemy displayedEnemy)
    {
        game = gameInstance;
        this.megaman = megaman;
        this.displayedEnemy = displayedEnemy;
    }

    public void Initialize()
    {
        commandDict.Add(Keys.A, new RunningShootingLeftMegamanCommand(megaman));
        commandDict.Add(Keys.D, new RunningShootingRightMegamanCommand(megaman));

        commandDict.Add(Keys.O, new CycleEnemyBackwardCommand(displayedEnemy));
        commandDict.Add(Keys.P, new CycleEnemyForwardCommand(displayedEnemy));

        commandDict.Add(Keys.A, new RunningLeftMegamanCommand(megaman));
        commandDict.Add(Keys.D, new RunningRightMegamanCommand(megaman));
        commandDict.Add(Keys.L, new RunningShootingLeftMegamanCommand(megaman));
        commandDict.Add(Keys.K, new RunningShootingRightMegamanCommand(megaman));
        commandDict.Add(Keys.OemQuestion, new ClimbingMegamanCommand(megaman));
        commandDict.Add(Keys.D9, new ClimbingShootingRightMegamanCommand(megaman));
        commandDict.Add(Keys.D8, new ClimbingReachedTopMegamanCommand(megaman));
        commandDict.Add(Keys.D7, new DamagedMegamanCommand(megaman));
        commandDict.Add(Keys.D6, new FallingMegamanCommand(megaman));
        commandDict.Add(Keys.D5, new FallingShootingMegamanCommand(megaman));
    }

    public void Update(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, GameTime gameTime)
    {
        KeyboardState keyboardState = Keyboard.GetState();
        Keys[] pressedKeys = keyboardState.GetPressedKeys();
     


        if (keyboardState.IsKeyDown(Keys.O) && previousKeyState.IsKeyUp(Keys.O)) {
            commandDict[Keys.O].Execute(_graphics, movementSpeed, megamanSize);
        }
        if (keyboardState.IsKeyDown(Keys.P) && previousKeyState.IsKeyUp(Keys.P)) {
            commandDict[Keys.P].Execute(_graphics, movementSpeed, megamanSize);
        }
        interval++;
        Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();
        bool is_running = false;
        bool is_shooting = false;
        bool is_damaged = false;

        if (pressedKeys.Contains(Keys.Q))
        {
            game.Exit();
        }
        // Check for key presses and execute the corresponding commands

        if (!is_jumping && !is_falling && pressedKeys.Contains(Keys.Space))
        {
            is_jumping = true;
            initialY = megaman.y;
            gravity = 4.5f;

        }

        if (is_jumping)
        {
            if (gravity > 0)
            {
                megaman.y -= gravity;
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
            if (megaman.y < initialY)
            {
                megaman.y += gravity;
                gravity += .25f;
            }
            else
            {
                megaman.y = initialY;
                is_falling = false;
                gravity = 4.5f;
            }
        }

        if ((is_jumping || is_falling) && pressedKeys.Contains(Keys.S))
        {
            commandDict[Keys.D5].Execute(_graphics, movementSpeed, megamanSize, interval);
        }

        else if(is_falling || is_jumping)
        {
            commandDict[Keys.D6].Execute(_graphics, movementSpeed, megamanSize, interval);
        }

        if (pressedKeys.Contains(Keys.Z) || is_damaged)
        {
            
            commandDict[Keys.D6].Execute(_graphics, movementSpeed, megamanSize, interval);
            is_damaged = true;
        }

        if (megaman.x < 10 && pressedKeys.Contains(Keys.W) && !reached_top)
        {
            commandDict[Keys.OemQuestion].Execute(_graphics, movementSpeed, megamanSize, interval);
            megaman.y -= 3;
            is_climbing = true;
        }

        if (is_climbing && pressedKeys.Contains(Keys.D))
        {
            commandDict[Keys.D9].Execute(_graphics, movementSpeed, megamanSize, interval);
        }

        if (is_climbing && megaman.y < 25)
        {
            commandDict[Keys.D8].Execute(_graphics, movementSpeed, megamanSize, interval);
            is_climbing = false;
            reached_top = true;
        }

        if (pressedKeys.Contains(Keys.A) && pressedKeys.Contains(Keys.S) && !is_climbing && !is_jumping && !is_falling)
        {
            commandDict[Keys.L].Execute(_graphics, movementSpeed, megamanSize, interval);
            megaman.x -= 3;
            is_running = true;
            is_shooting = true;
        }

        else if (pressedKeys.Contains(Keys.D) && pressedKeys.Contains(Keys.S) && !is_climbing && !is_jumping && !is_falling)
        {
            commandDict[Keys.K].Execute(_graphics, movementSpeed, megamanSize, interval);
            megaman.x += 3;
            is_running = true;
            is_shooting = true;
        }

        if (!is_shooting && !is_climbing && pressedKeys.Contains(Keys.A))
        {
            if (!is_jumping && !is_falling)
            {
                commandDict[Keys.A].Execute(_graphics, movementSpeed, megamanSize, interval);
            }

            megaman.x -= 3;
            is_running = true;
        }
        else if (!is_shooting && !is_climbing && pressedKeys.Contains(Keys.D))
        {
            if (!is_jumping && !is_falling)
            {
                commandDict[Keys.D].Execute(_graphics, movementSpeed, megamanSize, interval);
            }

            megaman.x += 3;
            is_running = true;
        }
        if(!is_shooting && !is_running && !is_falling && ! is_jumping && !is_climbing && !is_damaged)
        {
            var Idle = new IdleMegamanCommand(megaman);
            Idle.Execute(_graphics, movementSpeed, megamanSize, interval);
        }

        previousKeyState = keyboardState;

        megaman.Update(gameTime);
        displayedEnemy.Update(gameTime);
    }
}