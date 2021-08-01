using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_jaaj_6.Gameplay.Actors.Enemies
{
    public class Fish : Enemy
    {
        public override void Start()
        {
            base.Start();
            this.tag = "fish";
            this._box.SquareColor = Color.Green;
            this._box.Start();

            this._speed = 1.5f;

            this.SetInitialPosition();

            this.gravity2D = new Vector2(0, 0);
            this.Sprite = this.Scene.Content.Load<Texture2D>("Sprites/tilemap");
            this.Body = new Rectangle(new Point(56, 48), new Point(24, 24));
        }

        private void SetInitialPosition()
        {
            _distance = Vector2.Distance(this.Position, this.Nodes[0]) / 2f;
            this.InitialPosition = new Vector2(this.Position.X, this.Position.Y - _distance);
        }

        private float _distance;
        public override void UpdateData(GameTime gameTime)
        {
            float timer = (float)gameTime.TotalGameTime.TotalSeconds;
            this.Position.Y = this.InitialPosition.Y + MathF.Cos(timer * this._speed) * _distance;
            base.UpdateData(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            DrawSprite(spriteBatch);
            EndDraw(spriteBatch);
        }
    }
}