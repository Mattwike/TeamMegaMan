using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.Interfaces
{
    public interface IBlocks
    {
        void Initialize();
        void Update();
        void Draw(SpriteBatch spriteBatch);
        Rectangle boundingBox { get; }
    }
}
