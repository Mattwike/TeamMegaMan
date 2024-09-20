using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class MouseController : IController
{

    int height;
    int width;

    public void Initialize(int screenHeight, int screenWidth)
    {
        height = screenHeight;
        width = screenWidth;
    }

    public int Update(int lastmouse)
    {
        var mState = Mouse.GetState();

        if (mState.X < width && mState.Y < height && mState.LeftButton == ButtonState.Pressed)
        {
            return 1;
        }
        else if (mState.X > width && mState.Y < height && mState.LeftButton == ButtonState.Pressed)
        {
            return 2;
        }
        else if (mState.X < width && mState.Y > height && mState.LeftButton == ButtonState.Pressed)
        {
            return 3;
        }
        if (mState.X > width && mState.Y > height && mState.LeftButton == ButtonState.Pressed)
        {
            return 4;
        }

        return lastmouse;
    }
}