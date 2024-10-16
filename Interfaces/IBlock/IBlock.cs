using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IBlock
{
    public void Update();

    public void Initialize();

    public void Draw(SpriteBatch spriteBatch, int blockNum, int blockWidth, Vector2 pos);

}