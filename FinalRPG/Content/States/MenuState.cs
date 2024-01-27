using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FinalRPG.Content.Controls;


namespace FinalRPG.Content.States
{
    public class MenuState : State
    {
        private List<Component> _components;

        public MenuState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
          : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/GameButton");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");

            var newGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(400, 100),
                Text = "New Game",
            };

            newGameButton.Click += NewGameButton_Click;

            var HelpGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(400, 200),
                Text = "Help",
            };

            HelpGameButton.Click += HelpGameButton_Click;

            var AboutGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(400, 300),
                Text = "About",
            };

            AboutGameButton.Click += AboutGameButton_Click;

            var quitGameButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(400, 400),
                Text = "Quit Game",
            };

            quitGameButton.Click += QuitGameButton_Click;

            _components = new List<Component>()
      {
        newGameButton,
        HelpGameButton,
        AboutGameButton,
        quitGameButton,

      };
        }

        public Game1 Game1
        {
            get => default;
            set
            {
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            foreach (var component in _components)
                component.Draw(gameTime, spriteBatch);

            spriteBatch.End();
        }

        private void HelpGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new HelpState(_game, _graphicsDevice, _content));
        }

        private void AboutGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new AboutState(_game, _graphicsDevice, _content));
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            _game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }

        public override void PostUpdate(GameTime gameTime)
        {
            // remove sprites if they're not needed
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var component in _components)
                component.Update(gameTime);
        }

        private void QuitGameButton_Click(object sender, EventArgs e)
        {
            _game.Exit();
        }
    }
}
