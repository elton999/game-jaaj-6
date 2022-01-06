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
            tag = "key";
            Sprite = Scene.Content.Load<Texture2D>("Sprites/tilemap");
            Body = new Rectangle(new Point(40, 0), new Point(16, 16));

            GetKeySound = Content.Load<SoundEffect>("Sound/key");
            base.Start();
            FeedBackFX.CurrentType = UI.ItemFeedBackFX.Type.KEY;
        }
        SoundEffect GetKeySound;

        public override void OnGetItem()
        {
            base.OnGetItem();
            Scene.GameManagement.Values["key"] = true;
            GetKeySound.Play();
            Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            DrawSprite(spriteBatch);
            EndDraw(spriteBatch);
        }
    }
}