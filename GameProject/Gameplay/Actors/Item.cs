using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.Collision;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public abstract class Item : Actor
    {
        public override void Start()
        {
            base.Start();
            this.gravity2D = new Vector2(0, 0);
            this.size = new Point(16, 16);
            this.CreateBox();
            this.InitialPosition = this.Position;

            FeedBackFX = new UI.ItemFeedBackFX();
            FeedBackFX.Scene = Scene;
            FeedBackFX.Position = this.Position;
            FeedBackFX.Start();
        }

        protected Square _box;
        private void CreateBox()
        {
            this._box = new Square();
            this._box.Scene = this.Scene;
            this._box.size = this.size;
            this._box.Position = this.Position;
            this._box.SquareColor = Color.Orange;
            this._box.Start();
        }

        public override void UpdateData(GameTime gameTime)
        {
            this.Animation(gameTime);

            var player = this.Scene.AllActors[0];
            if (this.overlapCheck(player))
            {
                player.OnCollision(this.tag);
                this.Destroy();
                this.OnGetItem();
            }
        }
        public UI.ItemFeedBackFX FeedBackFX;
        public virtual void OnGetItem() 
        {
            Scene.Foreground.Add(FeedBackFX);
        }

        private float _speed = 2f;
        private void Animation(GameTime gameTime)
        {
            float timer = (float)gameTime.TotalGameTime.TotalSeconds;
            this.Position.Y = this.InitialPosition.Y + (float)Math.Cos((double)(timer * this._speed)) * 10f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._box.Position = this.Position;
            this._box.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}