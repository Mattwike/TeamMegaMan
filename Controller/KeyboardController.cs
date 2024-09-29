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
    int interval = 0;

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
<<<<<<< HEAD
        
=======

        commandDict.Add(Keys.O, new CycleEnemyBackwardCommand(displayedEnemy));
        commandDict.Add(Keys.P, new CycleEnemyForwardCommand(displayedEnemy));

>>>>>>> 4ca635ec8bdddcec745316d0361920af3eb3494c
    }

    public void Update(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, GameTime gameTime)
    {
        interval++;
        Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

        if (pressedKeys.Contains(Keys.Q))
        {
            game.Exit();
        }
        if (Keyboard.GetState().IsKeyDown(Keys.O))
        {
            commandDict[Keys.O].Execute(_graphics, movementSpeed, megamanSize);
        }else if (Keyboard.GetState().IsKeyDown(Keys.P))
        {
            commandDict[Keys.P].Execute(_graphics, movementSpeed, megamanSize);
        }
        // Check for key presses and execute the corresponding commands
        if (pressedKeys.Contains(Keys.A) && priorKeys != null)
        {
            commandDict[Keys.A].Execute(_graphics, movementSpeed, megamanSize, interval);
            megaman.x -= 3;
        }
        else if (pressedKeys.Contains(Keys.D) && priorKeys != null)
        {
            commandDict[Keys.D].Execute(_graphics, movementSpeed, megamanSize, interval);
            megaman.x += 3;
        }
        else
        {
            var Idle = new IdleMegamanCommand(megaman);
            Idle.Execute(_graphics, movementSpeed, megamanSize, interval);
        }

        priorKeys = pressedKeys;
        pressedKeys = new Keys[0];
        megaman.Update(gameTime);
        displayedEnemy.Update(gameTime);
    }
}