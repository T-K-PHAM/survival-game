using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using TiledSharp;
using Microsoft.Xna.Framework.Content;
using System;
using System.Diagnostics;
using FinalRPG.Content.States;
using FinalRPG.Content.Controls;

namespace FinalRPG.Content.States
{
    public class GameState : State
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Hitbox playerHitbox;
        private TilemapManager manager;
        private TmxMap map;
        private Texture2D tileset;
        private Rectangle playerStart;
        private List<Rectangle> colliders;
        private Song song;
        private List<Enemy> enemyList = new List<Enemy>();
        private int maxEnemies = 500;
        private int enemySpeed = 1;
        private int enemyMultiplication = 5;
        private double elapsedTime = 0;
        private double elapsedTimeLastEnemy = 0;
        private State _currentState;
        private State _nextState;
        private SpriteFont font;
        private bool gameOver = false;
        private List<Component> _components;

        public Matrix matrix;
        public GameState(Game1 game, GraphicsDevice graphicsDevice, ContentManager content)
      : base(game, graphicsDevice, content)
        {
            var buttonTexture = _content.Load<Texture2D>("Controls/GameButton");
            var buttonFont = _content.Load<SpriteFont>("Fonts/Font");
            var mainMenuButton = new Button(buttonTexture, buttonFont)
            {
                Position = new Vector2(810, 70),
                Text = "Back To Main Menu",
            };

            mainMenuButton.Click += Button_MainMenu_Click;

            _components = new List<Component>()
            {
                mainMenuButton,
            };

            font = _content.Load<SpriteFont>("Arial");
            _graphics = game._graphics;
            playerHitbox = new Hitbox(_graphics);
            map = new TmxMap("Content\\map.tmx");
            tileset = _content.Load<Texture2D>("Backgrounds\\Tilesets\\" + map.Tilesets[0].Name.ToString());
            int tileWidth = map.Tilesets[0].TileWidth;
            int tileHeight = map.Tilesets[0].TileHeight;
            int TileSetTilesWide = tileset.Width / tileWidth;
            manager = new TilemapManager(game._spriteBatch, map, tileset, TileSetTilesWide, tileWidth, tileHeight);
            playerHitbox.Load(16, 16);

            colliders = new List<Rectangle>();
            foreach (var o in map.ObjectGroups["Collisions"].Objects)
            {
                if (o.Name == "")
                {
                    colliders.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
                }
                if (o.Name == "Start")
                {
                    playerStart = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                }
            }
            player = new Player(
                _content.Load<Texture2D>("Actor\\Characters\\BlueNinja\\SeparateAnim\\Idle"),
                _content.Load<Texture2D>("Actor\\Characters\\BlueNinja\\SeparateAnim\\Walk"),
                new Vector2(playerStart.X, playerStart.Y)
                );
        }

        private void Button_MainMenu_Click(object sender, EventArgs args)
        {
            _game.ChangeState(new MenuState(_game, _graphicsDevice, _content));
        }

        //protected override void LoadContent()
        //{
        //    // _spriteBatch = new SpriteBatch(GraphicsDevice);

        //    // _currentState = new MenuState(this, _graphics.GraphicsDevice, _content);

        //}

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // GraphicsDevice.Clear(Color.CornflowerBlue);
            manager.Draw();
            spriteBatch.Begin();
            if (!gameOver)
            {
                playerHitbox.Draw(spriteBatch, player.movement);
                player.playerDraw(spriteBatch, gameTime);
            }
            else
            {
                spriteBatch.DrawString(font, "Game over! Your score is " + Math.Truncate(this.elapsedTime), new Vector2(70, 100), Color.Black);
                foreach (var component in _components)
                    component.Draw(gameTime, spriteBatch);
            }
            foreach (var enemy in enemyList)
            {
                enemy.enemyDraw(spriteBatch, gameTime);
            }
            spriteBatch.End();
            // TODO: Add your drawing code here
            // _currentState.Draw(gameTime, _spriteBatch);
           // base.Draw(gameTime);

        }

        public override void PostUpdate(GameTime gameTime)
        {

        }

        public override void Update(GameTime gameTime)
        {
            //if (_nextState != null)
            //{
            //    _currentState = _nextState;

            //    _nextState = null;
            //}

            //_currentState.Update(gameTime);

            //_currentState.PostUpdate(gameTime);

            // if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            //     Exit();
            var initPos = player.movement;
            //Should always be here.
            player.Update();
            foreach (var rect in colliders)
            {
                if (player.playerRect.Intersects(rect))
                {
                    player.movement = initPos;
                }
            }
            this.GenerateEnemies();
            this.UpdateEnemy();
            this.CheckCollision();
            if (!gameOver)
            {
                this.elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            }
            if (this.elapsedTime > 5.0 && this.enemySpeed == 1)
            {
                this.enemySpeed += 1;
                this.enemyMultiplication += 3;
            }
            else if (this.elapsedTime > 10.0 && this.enemySpeed == 2)
            {
                this.enemySpeed += 1;
                this.enemyMultiplication += 3;
            }
            else if (this.elapsedTime > 15.0 && this.enemySpeed == 3)
            {
                this.enemySpeed += 1;
                this.enemyMultiplication += 3;
            }
            else if (this.elapsedTime > 20.0 && this.enemySpeed == 4)
            {
                this.enemySpeed += 1;
                this.enemyMultiplication += 3;
            }
            else if (this.elapsedTime > 25.0 && this.enemySpeed == 5)
            {
                this.enemySpeed += 1;
                this.enemyMultiplication += 3;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Button_MainMenu_Click(this, new EventArgs());

            foreach (var component in _components)
                component.Update(gameTime);

            Debug.WriteLine("ElapsedTime: " + this.elapsedTime + " Speed: " + this.enemySpeed);
            // base.Update(gameTime);
        }

        protected void GenerateEnemies()
        {
            double timeSinceLastEnemy = this.elapsedTime - this.elapsedTimeLastEnemy;
            if (timeSinceLastEnemy > 1.0)
            {
                for (int i = 0; i < this.enemyMultiplication; i++)
                {
                    this.AddEnemy();
                }
                this.elapsedTimeLastEnemy = this.elapsedTime;
            }
        }
        protected void AddEnemy()
        {
            if (enemyList.Count < this.maxEnemies)
            {
                Random rnd = new Random();
                Enemy enemy = new Enemy(
                    _content.Load<Texture2D>("Actor\\Monsters\\Spirit\\Spirit"),
                    new Vector2(rnd.Next(10, 1014), -10),
                    rnd.Next(1, this.enemySpeed)
                    );
                enemyList.Add(enemy);
            }
        }

        protected void UpdateEnemy()
        {
            List<Enemy> enemiesToRemove = new List<Enemy>();
            foreach (var enemy in enemyList)
            {
                float Y = enemy.movement.Y;
                if (Y > 520)
                {
                    enemiesToRemove.Add(enemy);
                }
                enemy.Update();
            }
            foreach (var enemy in enemiesToRemove)
            {
                enemyList.Remove(enemy);
            }
        }

        protected void CheckCollision()
        {
            Rectangle playerPosition = player.playerRect;
            foreach (var enemy in enemyList)
            {
                Rectangle enemyPosition = enemy.enemyRect;
                if (playerPosition.Left < enemyPosition.Right && playerPosition.Right > enemyPosition.Left && playerPosition.Top < enemyPosition.Bottom && playerPosition.Bottom > enemyPosition.Top)
                {
                    this.GameOver();
                }
            }
        }
        protected void GameOver()
        {
            this.gameOver = true;
        }
    }
}
