using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors.Items
{
    public class Relic : Item
    {

        AsepriteAnimation Animation;
        public override void Start()
        {
            tag = "relic";
            base.Start();
            Sprite = Content.Load<Texture2D>("Sprites/relic");
            size = new Point(15, 12);
            Animation = new AsepriteAnimation(Content.Load<AsepriteDefinitions>("Sprites/relic animation"));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Animation.Play(gameTime, "idle", AsepriteAnimation.AnimationDirection.LOOP);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Body = Animation.Body;
            BeginDraw(spriteBatch);
            DrawSprite(spriteBatch);
            EndDraw(spriteBatch);
        }
    }
}
