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
            //base.Start();
            this.tag = "key";
            this.gravity2D = new Vector2(0, 0);
            this.size = new Point(16, 16);
            this.InitialPosition = this.Position;
            this.Sprite = this.Scene.Content.Load<Texture2D>("Sprites/tilemap");
            this.Body = new Rectangle(new Point(40, 0), new Point(16, 16));

            GetKeySound = Content.Load<SoundEffect>("Sound/key");
        }
        SoundEffect GetKeySound;


        public override void OnGetItem()
        {
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