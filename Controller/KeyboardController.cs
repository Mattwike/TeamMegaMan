using Microsoft.Xna.Framework.Input;
using Project1;

public class KeyboardController : IController
{

    private Game1 game;

    public KeyboardController(Game1 gameInstance)
    {
        game = gameInstance;
    }

    public int Update(int lastoutput)
	{
        var kstate = Keyboard.GetState();

        if (kstate.IsKeyDown(Keys.D0))
        {
            game.Exit();
        }

        if (kstate.IsKeyDown(Keys.D1))
        {
            return 1;
        }
        else if (kstate.IsKeyDown(Keys.D2))
        {
            return 2;
        }
        else if (kstate.IsKeyDown(Keys.D3))
        {
            return 3;
        }
        else if (kstate.IsKeyDown(Keys.D4))
        {
            return 4;
        }

        return lastoutput;
    }
}
