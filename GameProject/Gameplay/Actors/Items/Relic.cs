using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace game_jaaj_6.Gameplay.Actors.Items
{
    public class Relic : Item
    {
        public override void Start()
        {
            this.tag = "relic";
            base.Start();
        }
    }
}
