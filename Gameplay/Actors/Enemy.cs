using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolKit;
using UmbrellaToolKit.Collision;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public abstract class Enemy : Actor
    {
        protected Square _box;
        public override void Start()
        {
            base.Start();
            this.size = new Point(16, 16);
            this.Scene.AllActors.Add(this);

            this.CreateBox();
        }

        private void CreateBox()
        {
            this._box = new Square();
            this._box.Scene = this.Scene;
            this._box.size = new Point(16, 16);
            this._box.Position = this.Position;
        }

        protected float _speed = 1f;

        public override void UpdateData(GameTime gameTime)
        {
            this._box.Position = this.Position;
            if (this.overlapCheckPixel(this.Scene.AllActors[0]))
                this.Scene.AllActors[0].OnCollision(this.tag);
            base.UpdateData(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._box.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}