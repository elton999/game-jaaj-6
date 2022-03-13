
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.Collision;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Gameplay
{
    public class Slider : Solid
    {
        private Vector2 _gravityDirection;
        public override void Start()
        {
            this.tag = "slider";
            this.Scene.AllSolids.Add(this);
            this._gravityDirection = new Vector2(
                float.Parse(this.Values["directionX"]),
                float.Parse(this.Values["directionY"])
            );
            base.Start();
        }
    }
}