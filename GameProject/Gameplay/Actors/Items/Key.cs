using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace game_jaaj_6.Gameplay.Actors.Items
{
    public class Key : Item
    {
        SoundEffect _onGetKeySound;
        public override void Start()
        {
            tag = "key";
            Sprite = Scene.Content.Load<Texture2D>("Sprites/tilemap");
            Body = new Rectangle(new Point(40, 0), new Point(16, 16));

            _onGetKeySound = Content.Load<SoundEffect>("Sound/key");
            base.Start();
            FeedBackFX.CurrentType = UI.ItemFeedBackFX.Type.KEY;
        }

        public override void OnGetItem()
        {
            base.OnGetItem();
            Scene.GameManagement.Values["key"] = true;
            _onGetKeySound.Play();
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