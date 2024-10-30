
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Project1.Interfaces
{
    public interface IBlock
    {
        void Initialize();
        void Update();
        void Draw(SpriteBatch spriteBatch);
        Rectangle boundingBox { get; }
    }
}
