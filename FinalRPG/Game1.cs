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

namespace FinalRPG
{
    public class Game1 : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
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

        public Matrix matrix;
        public void ChangeState(State state)
        {
            _nextState = state;
        }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //playerHitbox = new Hitbox(_graphics);
        }

        protected override void Initialize()
        {
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1024;
            _graphics.PreferredBackBufferHeight = 510;
            _graphics.ApplyChanges();

            var Width = _graphics.PreferredBackBufferWidth;
            var Height = _graphics.PreferredBackBufferHeight;
            var WindowSize = new Vector2(Width, Height);
            var mapSize = new Vector2(1024, 510);
            matrix = Matrix.CreateScale(new Vector3(WindowSize / mapSize, 1));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _currentState = new MenuState(this, _graphics.GraphicsDevice, Content);


            this.song = Content.Load<Song>("Musics\\Final Fantasy VI - 48 - Mog");
            MediaPlayer.Play(song);
            //  Uncomment the following line will also loop the song
            //  MediaPlayer.IsRepeating = true;
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;

            //map = new TmxMap("Content\\map.tmx");
            //tileset = Content.Load<Texture2D>("Backgrounds\\Tilesets\\" + map.Tilesets[0].Name.ToString());
            //int tileWidth = map.Tilesets[0].TileWidth;
            //int tileHeight = map.Tilesets[0].TileHeight;
            //int TileSetTilesWide = tileset.Width / tileWidth;
            //manager = new TilemapManager(_spriteBatch, map, tileset, TileSetTilesWide, tileWidth, tileHeight);
            //playerHitbox.Load(16, 16);

            //colliders = new List<Rectangle>();
            //foreach (var o in map.ObjectGroups["Collisions"].Objects)
            //{
            //    if (o.Name == "")
            //    {
            //        colliders.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            //    }
            //    if (o.Name == "Start")
            //    {
            //        playerStart = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
            //    }
            //}
            //player = new Player(
            //    Content.Load<Texture2D>("Actor\\Characters\\BlueNinja\\SeparateAnim\\Idle"),
            //    Content.Load<Texture2D>("Actor\\Characters\\BlueNinja\\SeparateAnim\\Walk"),
            //    new Vector2(playerStart.X, playerStart.Y)
            //    );
        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs e)
        {
            // 0.0f is silent, 1.0f is full volume
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(song);
        }

        protected override void UnloadContent()
        {
            // playerHitbox.Unload();
            base.UnloadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            if(_nextState != null)
            {
                _currentState = _nextState;

                _nextState = null;
            }

            _currentState.Update(gameTime);

            _currentState.PostUpdate(gameTime);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();        
            //var initPos = player.movement;
            ////Should always be here.
            //player.Update();
            //foreach (var rect in colliders)
            //{
            //    if (player.playerRect.Intersects(rect))
            //    {
            //        player.movement = initPos;
            //    }
            //}
            //this.GenerateEnemies();
            //this.UpdateEnemy();
            //this.CheckCollision();
            //this.elapsedTime += gameTime.ElapsedGameTime.TotalSeconds;
            //if (this.elapsedTime > 5.0 && this.enemySpeed == 1)
            //{
            //    this.enemySpeed += 1;
            //    this.enemyMultiplication += 3;
            //}
            //else if (this.elapsedTime > 10.0 && this.enemySpeed == 2)
            //{
            //    this.enemySpeed += 1;
            //    this.enemyMultiplication += 3;
            //}
            //else if (this.elapsedTime > 15.0 && this.enemySpeed == 3)
            //{
            //    this.enemySpeed += 1;
            //    this.enemyMultiplication += 3;
            //}
            //else if (this.elapsedTime > 20.0 && this.enemySpeed == 4)
            //{
            //    this.enemySpeed += 1;
            //    this.enemyMultiplication += 3;
            //}
            //else if (this.elapsedTime > 25.0 && this.enemySpeed == 5)
            //{
            //    this.enemySpeed += 1;
            //    this.enemyMultiplication += 3;
            //}
            //Debug.WriteLine("ElapsedTime: " + this.elapsedTime + " Speed: " + this.enemySpeed);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //manager.Draw();
            _spriteBatch.Begin();          
            //playerHitbox.Draw(_spriteBatch, player.movement);
            //player.playerDraw(_spriteBatch,gameTime);
            //foreach (var enemy in enemyList)
            //{
            //    enemy.enemyDraw(_spriteBatch, gameTime);
            //}
            _spriteBatch.End();
            // TODO: Add your drawing code here
            _currentState.Draw(gameTime, _spriteBatch);
            base.Draw(gameTime);
        }

        //protected void GenerateEnemies()
        //{
        //    double timeSinceLastEnemy = this.elapsedTime - this.elapsedTimeLastEnemy;
        //    if (timeSinceLastEnemy > 1.0)
        //    {
        //        for(int i = 0; i < this.enemyMultiplication; i++)
        //        {
        //            this.AddEnemy();
        //        }
        //        this.elapsedTimeLastEnemy = this.elapsedTime;
        //    }
        //}
        //protected void AddEnemy()
        //{
        //    if (enemyList.Count < this.maxEnemies)
        //    {
        //        Random rnd = new Random();
        //        Enemy enemy = new Enemy(
        //            Content.Load<Texture2D>("Actor\\Monsters\\Spirit\\Spirit"),
        //            new Vector2(rnd.Next(10, 1014), -10),
        //            rnd.Next(1, this.enemySpeed)
        //            );
        //        enemyList.Add(enemy);
        //    }
        //}

        //protected void UpdateEnemy()
        //{
        //    List<Enemy> enemiesToRemove = new List<Enemy>();
        //    foreach (var enemy in enemyList)
        //    {
        //        float Y = enemy.movement.Y;
        //        if (Y > 520)
        //        {
        //            enemiesToRemove.Add(enemy);
        //        }
        //        enemy.Update();
        //    }
        //    foreach (var enemy in enemiesToRemove)
        //    {
        //        enemyList.Remove(enemy);
        //    }
        //}

        //protected void CheckCollision()
        //{
        //    Rectangle playerPosition = player.playerRect;
        //    foreach (var enemy in enemyList)
        //    {
        //        Rectangle enemyPosition = enemy.enemyRect;
        //        if (playerPosition.Left < enemyPosition.Right && playerPosition.Right > enemyPosition.Left && playerPosition.Top < enemyPosition.Bottom && playerPosition.Bottom > enemyPosition.Top)
        //        {
        //            this.GameOver();
        //        }
        //    }
        //}
        //protected void GameOver()
        //{

        //}
    }
}
