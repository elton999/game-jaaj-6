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
            gravity2D = new Vector2(0, 0);
            size = new Point(16, 16);
            InitialPosition = Position;

            FeedBackFX = new UI.ItemFeedBackFX();
            FeedBackFX.Scene = Scene;
            FeedBackFX.Position = Position - (Vector2.UnitY * 8);
            FeedBackFX.Start();
        }

        public override void UpdateData(GameTime gameTime)
        {
            this.Animation(gameTime);

            var player = this.Scene.AllActors[0];
            if (this.overlapCheck(player))
            {
                player.OnCollision(this.tag);
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
    }
}