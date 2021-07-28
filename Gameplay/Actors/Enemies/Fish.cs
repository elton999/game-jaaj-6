using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolKit;
using UmbrellaToolKit.Collision;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors.Enemies
{
    public class Fish : Enemy
    {
        public override void Start()
        {
            base.Start();
            this._box.SquareColor = Color.Green;
            this._box.Start();
        }

        public override void UpdateData(GameTime gameTime)
        {
            base.UpdateData(gameTime);
        }
    }
}