using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace FinalRPG
{

    public class Hitbox
    {
        private GraphicsDeviceManager deviceManager;
        private Texture2D hitbox;
        private Color[] data;

        public Hitbox(GraphicsDeviceManager graphicsDevice)
        {
            deviceManager = graphicsDevice;
        }

        public Content.States.GameState GameState
        {
            get => default;
            set
            {
            }
        }

        public void Load(int width, int height)
        {
            hitbox = new Texture2D(deviceManager.GraphicsDevice, width, height);
            data = new Color[width * height];
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = Color.Transparent;               
            }
            hitbox.SetData(data);
        }

        public void Unload()
        {
            //If we create a texture we need to unload/dispose it
            hitbox.Dispose();
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {
            spriteBatch.Draw(hitbox, pos, Color.Transparent);
        }
    }
}
