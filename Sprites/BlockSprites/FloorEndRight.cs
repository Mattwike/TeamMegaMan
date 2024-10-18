using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

public class FloorEndRight : IBlock
{
    Texture2D blockSheet;
    public FloorEndRight(Texture2D texture)
    {
        blockSheet = texture;
    }

    public void Draw(SpriteBatch spriteBatch, int blockNum, int blockWidth, Vector2 pos)
    {
   
        Rectangle sourceRect = new Rectangle(85, 479, 16, 16);
        Rectangle destRect = new Rectangle((int)pos.X + blockNum * blockWidth, (int)pos.Y, 16, 16);

        spriteBatch.Begin();
        spriteBatch.Draw(blockSheet, destRect, sourceRect, Color.White);
        spriteBatch.End();
    }


    public void Initialize()
    {
        throw new NotImplementedException();
    }

    public void Update()
    {
        throw new NotImplementedException();
    }
}
