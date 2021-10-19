using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.Collision;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public abstract class Enemy : Actor
    {
        protected Square _box;
        public override void Start()
        {
            base.Start();
            this.size = new Point(25, 25);
            this.Scene.AllActors.Add(this);

            this.CreateBox();
        }

        private void CreateBox()
        {
            this._box = new Square();
            this._box.Scene = this.Scene;
            this._box.size = new Point(25, 25);
            this._box.Position = this.Position;
        }

        protected float _speed = 1f;

        public override void UpdateData(GameTime gameTime)
        {
            this._box.Position = this.Position;

            var player = this.Scene.AllActors[0];
            if (this.overlapCheck(player))
                player.OnCollision(this.tag);

            base.UpdateData(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._box.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}