﻿using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolsKit;

namespace game_jaaj_6
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public AssetManagement AssetManagement;
        public GameManagement GameManagement;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Window.AllowUserResizing = true;

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 426 * 4;
            _graphics.PreferredBackBufferHeight = 240 * 4;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            AssetManagement = new AssetManagement();
            AssetManagement.Set<Gameplay.Actors.Player>("player", "PLAYER");
            AssetManagement.Set<Gameplay.Actors.Items.Key>("key", "MIDDLEGROUND");
            AssetManagement.Set<Gameplay.Actors.Items.Relic>("relic", "MIDDLEGROUND");
            AssetManagement.Set<Gameplay.Solids.Gate>("gate", "MIDDLEGROUND");

            AssetManagement.Set<Gameplay.Actors.EndLevel>("endLevel", "FOREGROUND");

            AssetManagement.Set<Gameplay.Slider>("slider", "MIDDLEGROUND");
            AssetManagement.Set<Gameplay.Spike>("spikes", "MIDDLEGROUND");
            AssetManagement.Set<Gameplay.DemageArea>("demage", "MIDDLEGROUND");

            AssetManagement.Set<Gameplay.Actors.Enemies.FireBall>("fireBall", "ENEMIES");
            AssetManagement.Set<Gameplay.Actors.Enemies.Soldier>("soldier", "ENEMIES");

            GameManagement = new GameManagement();
            GameManagement.Game = this;

            GameManagement.Start();
        }

        protected override void Update(GameTime gameTime)
        {
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                //Exit();

            GameManagement.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            GraphicsDevice.Clear(Color.BlueViolet);
            GameManagement.Draw(_spriteBatch);

        }
    }
}
