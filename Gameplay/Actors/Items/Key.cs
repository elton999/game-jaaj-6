using System;
using Microsoft.Xna.Framework;

namespace game_jaaj_6.Gameplay.Actors.Items
{
    public class Key : Item
    {
        public override void Start()
        {
            base.Start();
            this.tag = "key";
            this.gravity2D = new Vector2(0, 0);
        }
    }
}