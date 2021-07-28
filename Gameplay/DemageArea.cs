
using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolKit;
using UmbrellaToolKit.Collision;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay
{
    public class DemageArea : Actor
    {
        public override void Start()
        {
            this.tag = "demage area";
            this.size = new Point(
                int.Parse(this.Values["width"]),
                int.Parse(this.Values["height"])
            );
            this.gravity2D = new Vector2(0, 0);
            base.Start();
        }

        public override void UpdateData(GameTime gameTime)
        {
            if (this.overlapCheckPixel(this.Scene.AllActors[0]))
                this.Scene.AllActors[0].OnCollision(this.tag);
        }
    }
}