
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
        Vector2 newPosition = Position - (new Vector2(view.Width, view.Height - 300*zoom) * 0.5f / zoom);

        Matrix positionMatrix = Matrix.CreateTranslation(-newPosition.X, -newPosition.Y, 0f);
        Matrix zoomMatrix = Matrix.CreateScale(zoom, zoom, 1f);

        return positionMatrix * zoomMatrix;
    }

    public void Zoom(float newZoom)
    {
        zoom = newZoom;
        if(zoom < 0.1f)
        {
            zoom = 0.1f;
        }
    }

 

}