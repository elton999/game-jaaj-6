using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolKit;
using UmbrellaToolKit.Collision;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public class Player : Actor
    {
        private Square _box;
        public override void Start()
        {
            base.Start();
            this.tag = "Player";
            this.Scene.AllActors.Add(this);
            this.size = new Point(32, 32);

            this._box = new Square();
            this._box.Scene = this.Scene;
            this._box.size = new Point(32, 32);
            this._box.SquareColor = Color.Red;
            this._box.Position = this.Position;
            this._box.Start();

        }

        public override void Update(GameTime gameTime)
        {
            this.Scene.Camera.Target = new Vector2(this.Position.X + this.size.X / 2, this.Position.Y + this.size.Y / 2);
            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {

            //base.UpdateData(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._box.Position = this.Position;
            this._box.Scene = this.Scene;
            this._box.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}