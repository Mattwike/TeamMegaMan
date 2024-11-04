
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    private Viewport view;
    public Vector2 Position { get; set; }

    public Camera(Viewport port)
    {
        view = port;
        Position = Vector2.Zero;
    }

    public Matrix GetTransform()
    {
        return Matrix.CreateTranslation(-Position.X + (view.Width / 2), -Position.Y + (view.Height / 2), 0f);
    }

}