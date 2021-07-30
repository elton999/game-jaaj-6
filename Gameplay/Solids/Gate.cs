using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolKit.Collision;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay.Solids
{
    public class Gate : Solid
    {
        public override void Start()
        {
            base.Start();
            this.Scene.AllSolids.Add(this);
            this.tag = "gate";
            this.size = new Point(16, 64);
            this.CreateBox();
        }

        protected Square _box;
        private void CreateBox()
        {
            this._box = new Square();
            this._box.Scene = this.Scene;
            this._box.size = this.size;
            this._box.Position = this.Position;
            this._box.SquareColor = Color.Green;
            this._box.Start();
        }

        public override void Destroy()
        {
            base.Destroy();
            this.Scene.AllSolids.Remove(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._box.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}
