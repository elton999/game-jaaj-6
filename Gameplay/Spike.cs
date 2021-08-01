using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolKit.Collision;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay
{
    public class Spike : Solid
    {
        public override void Start()
        {
            this.tag = "spikes";
            this.InitialPosition = Vector2.Subtract(this.Position, new Vector2(0, 4));
            this.Sprite = this.Scene.Content.Load<Texture2D>("Sprites/tilemap");
            this.Body = new Rectangle(new Point(32, 16), new Point(8, 8));
            base.Start();
        }

        private float _interval = 6000f;
        private float _speed = 2f;
        public override void UpdateData(GameTime gameTime)
        {
            float timer = (float)gameTime.TotalGameTime.TotalSeconds;
            this.Position.Y = this.InitialPosition.Y + MathF.Cos(timer * _speed) * _interval;
            this.Position.Y = MathF.Max(this.InitialPosition.Y - 4, this.Position.Y);
            this.Position.Y = MathF.Min(this.InitialPosition.Y + 4, this.Position.Y);

            var player = this.Scene.AllActors[0];
            if (this.check(player.size, player.Position))
            {
                player.OnCollision(this.tag);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            for (var i = 0; i < (int)(this.size.X / 8); i++)
            {
                this.Position.X = this.InitialPosition.X + (8 * i);
                DrawSprite(spriteBatch);
            }
            this.Position.X = this.InitialPosition.X;
            EndDraw(spriteBatch);
        }
    }
}