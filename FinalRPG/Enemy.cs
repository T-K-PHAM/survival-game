using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalRPG
{
    public class Enemy
    {
        private Texture2D enemyWalkSheet;
        private Animation[] enemyWalk;
        private Animation currentAnimation;
        private Animation currentIdleAnimation;

        public Rectangle enemyRect;
        public Vector2 movement;
        public int speed = 1;

        public Enemy(Texture2D walk, Vector2 startPnt, int speed)
        {
            movement = startPnt;
            enemyRect = new Rectangle((int)movement.X, (int)movement.Y, 16, 16);

            enemyWalk = new Animation[4];
            enemyWalkSheet = walk;

            //Walk down
            enemyWalk[0] = new Animation(enemyWalkSheet, 0, 16, 16);
            //Walk up
            enemyWalk[1] = new Animation(enemyWalkSheet, 1, 16, 16);
            //Walk left
            enemyWalk[2] = new Animation(enemyWalkSheet, 2, 16, 16);
            //Walk right
            enemyWalk[3] = new Animation(enemyWalkSheet, 3, 16, 16);

            currentIdleAnimation = enemyWalk[0];//To avoid null value
            currentAnimation = enemyWalk[0];

            this.speed = speed;

        }

        public Content.States.GameState GameState
        {
            get => default;
            set
            {
            }
        }

        public void Update()
        {
            movement.Y += speed;
            currentAnimation = enemyWalk[0];
            
            //Update the hitbox pos
            enemyRect.X = (int)movement.X;
            enemyRect.Y = (int)movement.Y;
        }

        public void enemyDraw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentAnimation.Draw(spriteBatch, movement, gameTime, 300);
        }
    }
}
