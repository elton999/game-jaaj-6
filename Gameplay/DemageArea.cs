
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
            this.gravity2D = new Vector2(0, 0);
            base.Start();
        }

        public override void UpdateData(GameTime gameTime)
        {
            var player = this.Scene.AllActors[0];
            if (this.overlapCheck(player))
                player.OnCollision(this.tag);
        }
    }
}