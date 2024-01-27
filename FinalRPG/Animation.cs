using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalRPG
{
    public class Animation
    {
        private Texture2D anim;
        private int column;
        private int width;
        private int height;
        private int frames;
        private int c = 0;
        private int timeSinceLastFrame = 0;
        public Animation(Texture2D spriteSheet, int col, int w, int h)
        {
            anim = spriteSheet;
            column = col;
            width = w;
            height = h;
            frames = anim.Height / h;
        }

        public Content.States.GameState GameState
        {
            get => default;
            set
            {
            }
        }

        public void Draw(SpriteBatch _spriteBatch, Vector2 pos, GameTime gameTime, int millisecondsPerFrame = 500)
        {
            if (c < frames)
            {
                _spriteBatch.Draw(anim, pos, new Rectangle(width*column, height*c, 16, 16), Color.White);
                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceLastFrame > millisecondsPerFrame)
                {
                    timeSinceLastFrame -= millisecondsPerFrame;
                    c++;
                    if (c == frames)                  
                        c = 0;                                                          
                }
            }
        }
    }
}
