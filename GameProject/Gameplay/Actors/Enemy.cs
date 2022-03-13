using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.Collision;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public abstract class Enemy : Actor
    {
        public override void Start()
        {
            base.Start();
            this.size = new Point(25, 25);
            this.Scene.AllActors.Add(this);
        }

        protected float _speed = 1f;

        public override void UpdateData(GameTime gameTime)
        {
            var player = this.Scene.AllActors[0];
            if (this.overlapCheck(player))
                player.OnCollision(this.tag);

            base.UpdateData(gameTime);
        }
    }
}