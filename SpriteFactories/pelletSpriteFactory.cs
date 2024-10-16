using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Project1.Sprites;

namespace Project1.SpriteFactories
{

    public class pelletSpriteFactory
    {
	    private static pelletSpriteFactory instance = new pelletSpriteFactory();
	    private Texture2D megaManSheet;

	    public static pelletSpriteFactory Instance
	    {
		    get
		    {
			    return instance;
		    }
	    }

	    private pelletSpriteFactory()
	    {
	    }

	    public void LoadAllTextures(ContentManager content)
	    {
		    megaManSheet = content.Load<Texture2D>("Megaman");
	    }

	    public ISprite CreatePellet()
	    {
		    return new pellet(megaManSheet);
	    }


    }
}