using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Camera
{
    private Viewport view;
    public Vector2 Position { get; set; }
    private float zoom;

    public Camera(Viewport port)
    {
        view = port;
        Position = Vector2.Zero;
        zoom = 1.2f;
    }

    public Matrix GetTransform()
    {
        Vector2 newPosition = Position - (new Vector2(view.Width, view.Height - 300 * zoom) * 0.5f / zoom);

        Matrix positionMatrix = Matrix.CreateTranslation(-newPosition.X, -newPosition.Y, 0f);
        Matrix zoomMatrix = Matrix.CreateScale(zoom, zoom, 1f);

        return positionMatrix * zoomMatrix;
    }

    public void Zoom(float newZoom)
    {
        zoom = newZoom;
        if (zoom < 0.1f)
        {
            zoom = 0.1f;
        }
    }

    public Rectangle GetVisibleArea()
    {
        var inverseViewMatrix = Matrix.Invert(GetTransform());

        // Top-left corner of the visible area in world coordinates
        var topLeft = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
        // Bottom-right corner
        var bottomRight = Vector2.Transform(new Vector2(view.Width, view.Height), inverseViewMatrix);

        return new Rectangle(
            (int)topLeft.X,
            (int)topLeft.Y,
            (int)(bottomRight.X - topLeft.X),
            (int)(bottomRight.Y - topLeft.Y)
        );
    }
}
