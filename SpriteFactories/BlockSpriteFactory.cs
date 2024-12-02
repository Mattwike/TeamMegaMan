using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project1.SpriteFactories
{
    public class BlockSpriteFactory
    {
        private static BlockSpriteFactory instance = new BlockSpriteFactory();
        private Texture2D blockSheet;

        // Define source rectangles for each block type
        private Rectangle floorEndLeftSource;
        private Rectangle floorMiddleSource;
        private Rectangle floorEndRightSource;
        private Rectangle BrickEndRightSource;
        private Rectangle BrickEndLeftSource;
        private Rectangle BrickMiddleSource;
        private Rectangle BrickMiddle2Source;
        private Rectangle BrickEndRight2Source;
        private Rectangle BrickEndLeft2Source;
        private Rectangle LadderBlockSource;
        private Rectangle BossRoomSource;
        private Rectangle BossRoomTopSource;
        private Rectangle Electric;
        private Rectangle ElectricConnector;
        private Rectangle Door;
        private Rectangle DoorConnector;
        private Rectangle Spike;

        public static BlockSpriteFactory Instance => instance;

        private BlockSpriteFactory() { }

        public void LoadAllTextures(ContentManager content)
        {
            blockSheet = content.Load<Texture2D>("megamanblocks2");

            // Define the source rectangles based on the positions of the sprites in your texture atlas
            floorEndLeftSource = new Rectangle(77, 1, 16, 16);  
            floorMiddleSource = new Rectangle(94, 1, 16, 16);   
            floorEndRightSource = new Rectangle(111, 1, 16, 16);
            BrickEndRightSource = new Rectangle(111, 18, 16, 16);
            BrickEndLeftSource = new Rectangle(77, 18, 16, 16);
            BrickMiddleSource = new Rectangle(94, 18, 16, 16);
            BrickMiddle2Source = new Rectangle(94, 35, 16, 16);
            BrickEndLeft2Source = new Rectangle(77, 35, 16, 16);
            BrickEndRight2Source = new Rectangle(111, 35, 16, 16);
            LadderBlockSource = new Rectangle(20, 69, 16, 16);
            BossRoomSource = new Rectangle(18, 52, 16, 16);
            BossRoomTopSource = new Rectangle(1, 52, 16, 16);
            Electric = new Rectangle(70, 52, 16, 16);
            ElectricConnector = new Rectangle(52, 52, 16, 16);
            Door = new Rectangle(35, 52, 16, 16);
            DoorConnector = new Rectangle(36, 69, 16, 16);
            Spike = new Rectangle(64, 35, 16, 16);

            // Add source rectangles for other block types as needed
        }

        // Methods to get the textures or source rectangles for each block type
        public Texture2D GetBlockSheet()
        {
            return blockSheet;
        }

        public Texture2D GetFloorTexture()
        {
            return blockSheet; // If using a separate texture for floors, adjust accordingly
        }

        public Rectangle GetFloorEndLeftSource()
        {
            return floorEndLeftSource;
        }

        public Rectangle GetFloorMiddleSource()
        {
            return floorMiddleSource;
        }

        public Rectangle GetFloorEndRightSource()
        {
            return floorEndRightSource;
        }

        public Rectangle GetBrickEndLeftSource()
        {
            return BrickEndLeftSource;
        }

        public Rectangle GetBrickEndRightSource()
        {
            return BrickEndRightSource;
        }

        public Rectangle GetBrickMiddleSource()
        {
            return BrickMiddleSource;
        }

        public Rectangle GetBrickMiddle2Source()
        {
            return BrickMiddle2Source;
        }

        public Rectangle GetBrickEndRight2Source()
        {
            return BrickEndRight2Source;
        }

        public Rectangle GetBrickEndLeft2Source()
        {
            return BrickEndLeft2Source;
        }

        public Rectangle GetLadderBlockSource()
        {
            return LadderBlockSource;
        }

        public Rectangle GetBossRoomSource()
        {
            return BossRoomSource;
        }

        public Rectangle GetBossRoomTopSource()
        {
            return BossRoomTopSource;
        }

        public Rectangle GetElectricSource()
        {
            return Electric;
        }

        public Rectangle GetElectricConnectorSource()
        {
            return ElectricConnector;
        }

        public Rectangle GetDoorSource()
        {
            return Door;
        }

        public Rectangle GetDoorConnectorSource()
        {
            return DoorConnector;
        }
        public Rectangle GetSpikeSource()
        {
            return Spike;
        }

        // Add methods for other block types as needed
    }
}
