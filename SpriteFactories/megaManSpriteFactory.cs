using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Project1.Sprites;

namespace Project1.SpriteFactories
{

    public class megaManSpriteFactory
    {
	    private static megaManSpriteFactory instance = new megaManSpriteFactory();
	    private Texture2D megaManSheet;

	    public static megaManSpriteFactory Instance
	    {
		    get
		    {
			    return instance;
		    }
	    }

	    private megaManSpriteFactory()
	    {
	    }

	    public void LoadAllTextures(ContentManager content)
	    {
		    megaManSheet = content.Load<Texture2D>("Megaman");
	    }

	    public ISprite CreateIdleMegaman()
	    {
		    return new idleMegaman(megaManSheet);
	    }

        public ISprite CreateRunningMegaman()
        {
            return new runningMegaman(megaManSheet);
        }

        public ISprite CreateRunningShootingMegaman()
        {
            return new runningShootingMegaman(megaManSheet);
        }

        public ISprite CreateDamagedMegaman()
        {
            return new damagedMegaman(megaManSheet);
        }

        public ISprite CreateClimbingMegaman()
        {
            return new climbingMegaman(megaManSheet);
        }

        public ISprite CreateClimbingShootingMegaman()
        {
            return new climbingShootingMegaman(megaManSheet);
        }

        public ISprite CreateClimbingReachedTopMegaman()
        {
            return new climbingReachedTopMegaman(megaManSheet);
        }

    }
}