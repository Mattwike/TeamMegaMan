using Microsoft.Xna.Framework;
using Project1.SpriteFactories;
using Project1.GameObjects;

namespace Project1.States.MegamanState
{
    public class BombmanStateMachine
    {
        private float stateTimer = 0f;
        private float stateDuration = 2f;
        private Bombman bombman;
        private enum states { jumpingLeft, throwingLeft, waitingLeft, idleLeft, jumpingRight, throwingRight, waitingRight, idleRight }
        private states CurrentState = states.idleLeft;

        public BombmanStateMachine(Bombman bombman)
        {
            this.bombman = bombman;
        }

        private void changeDirection()
        {
            switch (CurrentState)
            {
                case states.jumpingLeft:
                    CurrentState = states.jumpingRight;
                    break;
                case states.throwingLeft:
                    CurrentState = states.throwingRight;
                    break;
                case states.waitingLeft:
                    CurrentState = states.waitingRight;
                    break;
                case states.idleLeft:
                    CurrentState = states.idleRight;
                    break;
                case states.jumpingRight:
                    CurrentState = states.jumpingLeft;
                    break;
                case states.throwingRight:
                    CurrentState = states.throwingLeft;
                    break;
                case states.waitingRight:
                    CurrentState = states.waitingLeft;
                    break;
                case states.idleRight:
                    CurrentState = states.idleLeft;
                    break;
                default:
                    break;
            }
        }

        public void Bethrowing()
        {
            switch (CurrentState)
            {
                case states.idleLeft:
                    CurrentState = states.throwingLeft;
                    break;
                case states.idleRight:
                    CurrentState = states.throwingRight;
                    break;
                case states.waitingLeft:
                    CurrentState = states.throwingLeft;
                    break;
                case states.waitingRight:
                    CurrentState = states.throwingRight;
                    break;
                case states.jumpingLeft:
                    CurrentState = states.throwingRight;
                    break;
                case states.jumpingRight:
                    CurrentState = states.throwingRight;
                    break;
                default:
                    break;
            }
        }

        public void BeWaiting()
        {
            switch (CurrentState)
            {
                case states.idleLeft:
                    CurrentState = states.waitingLeft;
                    break;
                case states.idleRight:
                    CurrentState = states.waitingRight;
                    break;
                case states.throwingLeft:
                    CurrentState = states.waitingLeft;
                    break;
                case states.throwingRight:
                    CurrentState = states.waitingRight;
                    break;
                case states.jumpingLeft:
                    CurrentState = states.waitingLeft;
                    break;
                case states.jumpingRight:
                    CurrentState = states.waitingRight;
                    break;
                default:
                    break;
            }
        }

        public void BeJumping()
        {
            switch (CurrentState)
            {
                case states.idleLeft:
                    CurrentState = states.jumpingLeft;
                    break;
                case states.idleRight:
                    CurrentState = states.jumpingRight;
                    break;
                case states.waitingLeft:
                    CurrentState = states.jumpingLeft;
                    break;
                case states.waitingRight:
                    CurrentState = states.jumpingRight;
                    break;
                case states.throwingLeft:
                    CurrentState = states.jumpingLeft;
                    break;
                case states.throwingRight:
                    CurrentState = states.jumpingRight;
                    break;
                default:
                    break;
            }
        }

        public void Update()
        {
            Vector2 position = bombman.Position;

            if ((CurrentState == states.waitingLeft || CurrentState == states.waitingRight) && stateTimer >= stateDuration)
            {
                if (CurrentState == states.waitingLeft)
                {
                    CurrentState = states.idleLeft;
                }
                else if (CurrentState == states.waitingRight)
                {
                    CurrentState = states.idleRight;
                }

                stateTimer = 0f;
            }

            switch (CurrentState)
            {
                case states.jumpingLeft:
                    position.Y -= 3;
                    break;
                case states.jumpingRight:
                    position.Y += 3;
                    break;
                case states.throwingLeft:
                    position.X -= 3;
                    break;
                case states.throwingRight:
                    position.X += 3;
                    break;
                default:
                    break;
            }

            bombman.Position = position;

            stateTimer += 0.016f;

            UpdateSprite();
        }

        private void UpdateSprite()
        {
            Vector2 position = bombman.Position;
            bombman.currentSprite = BombmanSpriteFactory.Instance.CreateBombmanJump(position);
            //switch (CurrentState)
            //{
            //    case states.idleLeft:
            //    case states.idleRight:
            //        bombman.currentSprite = BombmanSpriteFactory.Instance.CreateIdleBombMan(position);
            //        break;
            //    case states.throwingLeft:
            //    case states.throwingRight:
            //        bombman.currentSprite = BombmanSpriteFactory.Instance.CreateBombManThrowBomb(position);
            //        break;
            //    case states.jumpingLeft:
            //    case states.jumpingRight:
            //        bombman.currentSprite = BombmanSpriteFactory.Instance.CreateBombmanJump(position);
            //        break;
            //    default:
            //        bombman.currentSprite = BombmanSpriteFactory.Instance.CreateIdleBombMan(position);
            //        break;
            //}
        }
    }
}
