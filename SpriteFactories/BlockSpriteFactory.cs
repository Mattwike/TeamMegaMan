using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BlockSpriteFactory
{
    private static BlockSpriteFactory instance = new BlockSpriteFactory();
    private Texture2D blockSheet;

    public static BlockSpriteFactory Instance
    {
        get
        {
            return instance;
        }
    }

    private BlockSpriteFactory() { }

    public void LoadAllTextures(ContentManager content)
    {
        blockSheet = content.Load<Texture2D>("megamanBlocks");

    }

    public IBlock CreateFloorEndLeft()
    {
        return new FloorEndLeft(blockSheet);
    }
    public IBlock CreateFloorEndRight()
    {
        return new FloorEndRight(blockSheet);
    }

    public IBlock CreateFloorMiddle()
    {
        return new FloorMiddle(blockSheet);
    }

 


}
