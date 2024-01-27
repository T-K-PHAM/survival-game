using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;


namespace FinalRPG
{
    public class Player
    {
        private Texture2D playerIdleSheet;
        private Texture2D playerWalkSheet;
        private Animation[] playerIdle;
        private Animation[] playerWalk;
        private Animation currentAnimation;
        private Animation currentIdleAnimation;

        public Rectangle playerRect;
        public Vector2 movement;

        public  Player(Texture2D idle, Texture2D walk, Vector2 startPnt)
        {
            movement = startPnt;
            playerRect = new Rectangle((int)movement.X, (int)movement.Y, 16, 16);

            playerIdle = new Animation[4];
            playerWalk = new Animation[4];
            playerIdleSheet = idle;
            playerWalkSheet = walk;

            //Idle down
            playerIdle[0] = new Animation(playerIdleSheet, 0, 16, 16);
            //Idle up
            playerIdle[1] = new Animation(playerIdleSheet, 1, 16, 16);
            //Idle left
            playerIdle[2] = new Animation(playerIdleSheet, 2, 16, 16);
            //Idle right
            playerIdle[3] = new Animation(playerIdleSheet, 3, 16, 16);

            //Walk down
            playerWalk[0] = new Animation(playerWalkSheet, 0, 16, 16);
            //Walk up
            playerWalk[1] = new Animation(playerWalkSheet, 1, 16, 16);
            //Walk left
            playerWalk[2] = new Animation(playerWalkSheet, 2, 16, 16);
            //Walk right
            playerWalk[3] = new Animation(playerWalkSheet, 3, 16, 16);

            currentIdleAnimation = playerIdle[0];//To avoid null value
        }

        public Content.States.GameState GameState
        {
            get => default;
            set
            {
            }
        }

        public void Update(float moveSpeed=3f)
        {
            KeyboardState keyboard = Keyboard.GetState();
            currentAnimation = currentIdleAnimation;

            if (keyboard.IsKeyDown(Keys.A))
            {
                //Walk left
                movement.X -= moveSpeed;
                currentAnimation = playerWalk[2];
                currentIdleAnimation = playerIdle[2];
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                //Walk right
                movement.X += moveSpeed;
                currentAnimation = playerWalk[3];
                currentIdleAnimation = playerIdle[3];
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                //Walk up
                movement.Y -= moveSpeed;
                currentAnimation = playerWalk[1];
                currentIdleAnimation = playerIdle[1];
            }
            if (keyboard.IsKeyDown(Keys.S))
            {
                //Walk down
                movement.Y += moveSpeed;
                currentAnimation = playerWalk[0];
                currentIdleAnimation = playerIdle[0];
            }

            //Update the hitbox pos
            playerRect.X = (int) movement.X;
            playerRect.Y = (int) movement.Y;

        }

        public void playerDraw(SpriteBatch spriteBatch,GameTime gameTime)
        {
            currentAnimation.Draw(spriteBatch, movement, gameTime, 300);
        }
    }
}
