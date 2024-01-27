using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FinalRPG.Content.Controls;
using Microsoft.Xna.Framework.Input;


namespace FinalRPG.Content.States
{
    public class HelpState : State
    {
        private List<Component> _components;
        private SpriteFont font;

        public HelpState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/GameButton");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            font = _content.Load<SpriteFont>("Arial");

            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(400, 400),
                Text = "Back To Main Menu",
            };

            mainMenuButton.Click += Button_MainMenu_Click;

            _components = new List<Component>()
            {
                mainMenuButton,
            };
        }

        public Game1 Game1
        {
            get => default;
            set
            {
            }
        }

        private void Button_MainMenu_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_MainMenu_Click(this, new EventArgs());

            foreach (var component in _components)
                component.Update(gameTime);
        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Dodge the spirits to rack up your score! \n Use the WASD key to move the player.\n You get hit once and it's GAME OVER!",new Vector2 (350, 100), Color.Black);

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }
    }
}
