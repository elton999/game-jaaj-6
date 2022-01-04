using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace game_jaaj_6.Gameplay.Actors.Items
{
    public class Key : Item
    {
        public override void Start()
        {
            this.tag = "key";
            this.Sprite = this.Scene.Content.Load<Texture2D>("Sprites/tilemap");
            this.Body = new Rectangle(new Point(40, 0), new Point(16, 16));

            GetKeySound = Content.Load<SoundEffect>("Sound/key");
            base.Start();
            FeedBackFX.CurrentType = UI.ItemFeedBackFX.Type.KEY;
        }
        SoundEffect GetKeySound;

        public override void OnGetItem()
        {
            base.OnGetItem();
            this.Scene.GameManagement.Values["key"] = true;
            GetKeySound.Play();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            DrawSprite(spriteBatch);
            EndDraw(spriteBatch);
        }
    }
}