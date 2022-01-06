﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;

namespace game_jaaj_6.UI
{
    public class ItemFeedBackFX : GameObject
    {
        public enum Type { RELIC, KEY}
        public Type CurrentType;
        public override void Start()
        {
            base.Start();
            Sprite = Scene.Content.Load<Texture2D>("Sprites/tilemap");
        }

        float speed = 0.002f;
        float speedTransparent = 3f;
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            Position.Y -= speed * deltaTime;
            Transparent = Transparent > 0f ? Transparent - (speedTransparent / 10000f * deltaTime) : 0f;
            System.Console.WriteLine(Transparent);
            if (Transparent == 0) Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Point location = CurrentType == Type.KEY ? new Point(88, 16) : new Point(88,0);
            Body = new Rectangle(location, new Point(8, 16));
            base.Draw(spriteBatch);
        }
    }
}
