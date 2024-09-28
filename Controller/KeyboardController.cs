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

    }

    public void Update(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize, GameTime gameTime)
    {
        Keys[] pressedKeys = Keyboard.GetState().GetPressedKeys();

        if (pressedKeys.Contains(Keys.D0))
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
        if (pressedKeys.Contains(Keys.A))
        {
            commandDict[Keys.A].Execute(_graphics, movementSpeed, megamanSize);
        }
        else if (pressedKeys.Contains(Keys.D))
        {
            commandDict[Keys.D].Execute(_graphics, movementSpeed, megamanSize);
        }
        else
        {
            // Execute idle command if no keys are pressed
            ICommand idleCommand = new IdleMegamanCommand(megaman);
            idleCommand.Execute(_graphics, movementSpeed, megamanSize);
        }
        megaman.Update(gameTime);
        displayedEnemy.Update(gameTime);
    }
}