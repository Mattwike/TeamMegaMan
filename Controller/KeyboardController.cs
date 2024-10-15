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

    public KeyboardController(Game1 gameInstance, Megaman megaman, GenericEnemy displayedEnemy)
    {
        game = gameInstance;
        this.megaman = megaman;
        this.displayedEnemy = displayedEnemy;
    }

    public void Initialize()
    {
    
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

        megaman.is_running = false;
        megaman.is_shooting = false;
        megaman.is_damaged = false;

        if (keyboardState.IsKeyDown(Keys.O) && previousKeyState.IsKeyUp(Keys.O)) {
            commandDict[Keys.O].Execute(_graphics, movementSpeed, megamanSize, 0);
        }
        if (keyboardState.IsKeyDown(Keys.P) && previousKeyState.IsKeyUp(Keys.P)) {
            commandDict[Keys.P].Execute(_graphics, movementSpeed, megamanSize, 0);
        }
        interval++;
         
        if (pressedKeys.Contains(Keys.Q))
        {
            game.Exit();
        }
        // Check for key presses and execute the corresponding commands

        megaman.Jump(pressedKeys);

        if ((megaman.is_jumping || megaman.is_falling) && pressedKeys.Contains(Keys.S))
        {
            commandDict[Keys.D5].Execute(_graphics, movementSpeed, megamanSize, interval);
        }

        else if (megaman.is_falling || megaman.is_jumping)
        {
            commandDict[Keys.D6].Execute(_graphics, movementSpeed, megamanSize, interval);
        }

        if (pressedKeys.Contains(Keys.Z) || megaman.is_damaged)
        {

            commandDict[Keys.D6].Execute(_graphics, movementSpeed, megamanSize, interval);
            megaman.is_damaged = true;
        }

        if (megaman.x < 10 && pressedKeys.Contains(Keys.W) && !megaman.reached_top)
        {
            commandDict[Keys.OemQuestion].Execute(_graphics, movementSpeed, megamanSize, interval);
            megaman.y -= 3;
            megaman.is_climbing = true;
        }

        if (megaman.is_climbing && pressedKeys.Contains(Keys.D))
        {
            commandDict[Keys.D9].Execute(_graphics, movementSpeed, megamanSize, interval);
        }

        if (megaman.is_climbing && megaman.y < 25)
        {
            commandDict[Keys.D8].Execute(_graphics, movementSpeed, megamanSize, interval);
            megaman.is_climbing = false;
            megaman.reached_top = true;
        }

        if (pressedKeys.Contains(Keys.A) && pressedKeys.Contains(Keys.S) && !megaman.is_climbing && !megaman.is_jumping && !megaman.is_falling)
        {
            commandDict[Keys.L].Execute(_graphics, movementSpeed, megamanSize, interval);
            megaman.x -= 3;
            megaman.is_running = true;
            megaman.is_shooting = true;
        }

        else if (pressedKeys.Contains(Keys.D) && pressedKeys.Contains(Keys.S) && !megaman.is_climbing && !megaman.is_jumping && !megaman.is_falling)
        {
            commandDict[Keys.K].Execute(_graphics, movementSpeed, megamanSize, interval);
            megaman.x += 3;
            megaman.is_running = true;
            megaman.is_shooting = true;
        }

        if (!megaman.is_shooting && !megaman.is_climbing && pressedKeys.Contains(Keys.A))
        {
            if (!megaman.is_jumping && !megaman.is_falling)
            {
                commandDict[Keys.A].Execute(_graphics, movementSpeed, megamanSize, interval);
            }

            megaman.x -= 3;
            megaman.is_running = true;
        }
        else if (!megaman.is_shooting && !megaman.is_climbing && pressedKeys.Contains(Keys.D))
        {
            if (!megaman.is_jumping && !megaman.is_falling)
            {
                commandDict[Keys.D].Execute(_graphics, movementSpeed, megamanSize, interval);
            }

            megaman.x += 3;
            megaman.is_running = true;
        }
        if (!megaman.is_shooting && !megaman.is_running && !megaman.is_falling && !megaman.is_jumping && !megaman.is_climbing && !megaman.is_damaged)
        {
            var Idle = new IdleMegamanCommand(megaman);
            Idle.Execute(_graphics, movementSpeed, megamanSize, interval);
        }

        previousKeyState = keyboardState;

        megaman.Update(gameTime);
        displayedEnemy.Update(gameTime);
    }
}