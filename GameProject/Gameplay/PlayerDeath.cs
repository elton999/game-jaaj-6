using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public class PlayerDeath : GameObject
    {
        public override void Start()
        {
            base.Start();
            this.Sprite = this.Scene.Content.Load<Texture2D>("Sprites/tilemap");
            this.Body = new Rectangle(new Point(48, 16), new Point(8, 8));
            this.InitialPosition = this.Position;
        }

        public void Play(Vector2 position)
        {
            this.InitialPosition = Vector2.Subtract(position, new Vector2(-16, -16));
            renderFx = true;
            radius = 5;
        }

        bool renderFx = false;
        float _speed = 2.0f;
        float timer = 0;
        float radius = 5;
        public override void Update(GameTime gameTime)
        {
            timer = (float)gameTime.TotalGameTime.TotalSeconds;
            float timerElapsed = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            radius += timerElapsed * 0.05f;
            if (radius > 50)
                renderFx = false;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            if (renderFx)
            {
                for (var i = 0; i < 8; i++)
                {
                    this.Position.X = this.InitialPosition.X + (float)Math.Cos((timer + i * 0.4f) * _speed) * radius;
                    this.Position.Y = this.InitialPosition.Y + (float)Math.Sin((timer + i * 0.4f) * _speed) * radius;
                    DrawSprite(spriteBatch);
                }
            }
            EndDraw(spriteBatch);
        }
    }
}