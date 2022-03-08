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
            Sprite = Content.Load<Texture2D>("Sprites/relic");
            size = new Point(15, 12);
            Animation = new AsepriteAnimation(Content.Load<AsepriteDefinitions>("Sprites/relic animation"));
            base.Start();
            FeedBackFX.CurrentType = UI.ItemFeedBackFX.Type.RELIC;

            Scene.GameManagement.Values["AllRelicsOnLevel"] = (int)Scene.GameManagement.Values["AllRelicsOnLevel"] + 1;
        }

        public override void OnGetItem()
        {
            base.OnGetItem();
            if(!destroyAnimation)
                Scene.GameManagement.Values["AllRelicsCollected"] = (int)Scene.GameManagement.Values["AllRelicsCollected"] + 1;
            destroyAnimation = true;
        }

        bool destroyAnimation = false;
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if(!destroyAnimation)
                Animation.Play(gameTime, "idle", AsepriteAnimation.AnimationDirection.LOOP);
            else
            {
                Animation.Play(gameTime, "destroy", AsepriteAnimation.AnimationDirection.FORWARD);
                if (Animation.lastFrame) Destroy();
            }
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
