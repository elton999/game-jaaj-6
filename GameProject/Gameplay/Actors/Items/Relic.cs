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
            tag = "relic";
            base.Start();
            Sprite = Content.Load<Texture2D>("Sprites/tilemap");
            size = new Point(15, 12);
            Body = new Rectangle(new Point(48, 80), new Point(15, 12));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            DrawSprite(spriteBatch);
            EndDraw(spriteBatch);
        }
    }
}
