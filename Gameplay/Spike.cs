
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
            this._box = new Square();
            this._box.Scene = this.Scene;
            this._box.size = this.size;
            this._box.SquareColor = Color.DarkRed;
            this._box.Position = this.Position;
            base.Start();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._box.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}