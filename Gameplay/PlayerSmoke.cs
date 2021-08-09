using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolKit;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public class PlayerSmoke : GameObject
    {
        public override void Start()
        {
            base.Start();
            this.Sprite = Scene.Content.Load<Texture2D>("Sprites/player");
            this.Animation = new AsepriteAnimation(Scene.Content.Load<AsepriteDefinitions>("Sprites/player_animation"));
            this.Origin = new Vector2(64, 32);
        }

        AsepriteAnimation Animation;
        public void StartAnimation()
        {
            this.Animation.Restart();
        }
        public void AnimationUpdate(GameTime gameTime)
        {
            this.Animation.Play(gameTime, "smoke", AsepriteAnimation.AnimationDirection.FORWARD);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Body = this.Animation.Body;
            base.Draw(spriteBatch);
        }
    }
}