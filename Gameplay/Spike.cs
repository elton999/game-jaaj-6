using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolKit.Collision;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay
{
    public class Spike : Solid
    {
        private Square _box;
        public override void Start()
        {
            this.tag = "spikes";
            this.InitialPosition = Vector2.Subtract(this.Position, new Vector2(0, 4));

            this._box = new Square();
            this._box.Scene = this.Scene;
            this._box.size = this.size;
            this._box.SquareColor = Color.DarkRed;
            this._box.Position = this.Position;
            this._box.Start();

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

            if (this.overlapCheck(this.Scene.AllActors[0]))
                this.Scene.AllActors[0].OnCollision(this.tag);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._box.Position = this.Position;
            this._box.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}