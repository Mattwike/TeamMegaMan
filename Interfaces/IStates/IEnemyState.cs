using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1.Interfaces.IStates
{
    public interface IEnemyState
    {
        public int getID();
        void Update(GameTime gameTime);

        public void Initialize(GraphicsDeviceManager _graphics, float movementSpeed, int megamanSize);

        void Draw(SpriteBatch _spriteBatch);

        public void beJumpingFlea();

        public void beBombManIdle();

        public void beScrewDriver();
     
    }
}
