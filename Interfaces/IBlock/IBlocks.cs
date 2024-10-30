using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Interfaces
{
    public interface IBlocks
    {
        Rectangle boundingBox { get; }

        void Update();

        void Draw(SpriteBatch spriteBatch);
    }
}
