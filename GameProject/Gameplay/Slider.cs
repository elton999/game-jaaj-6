
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.Collision;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Gameplay
{
    public class Slider : Solid
    {
        private Square _box;
        private Vector2 _gravityDirection;
        public override void Start()
        {
            this.tag = "slider";
            this.Scene.AllSolids.Add(this);
            this._gravityDirection = new Vector2(
                float.Parse(this.Values["directionX"]),
                float.Parse(this.Values["directionY"])
            );
            this.CreateBox();
            base.Start();
        }

        private void CreateBox()
        {
            this._box = new Square();
            this._box.Scene = this.Scene;
            this._box.SquareColor = Color.DarkGreen;
            this._box.size = this.size;
            this._box.Position = this.Position;
            this._box.Start();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //this._box.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}