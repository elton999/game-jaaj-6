using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors.Enemies
{
    public class Soldier : Enemy
    {

        AsepriteAnimation animation;
        public override void Start()
        {
            base.Start();
            this.tag = "soldier";
            this._box.SquareColor = Color.Purple;
            this._box.Start();
            this._speed = -0.10f;
            
            Sprite = Content.Load<Texture2D>("Sprites/robot");
            animation = new AsepriteAnimation(Content.Load<AsepriteDefinitions>("Sprites/robot_animation"));
        }

        bool turn = false;
        public override void Update(GameTime gameTime)
        {
            if (!turn)
            {
                animation.Play(gameTime, "walk", AsepriteAnimation.AnimationDirection.LOOP);
                spriteEffect = _speed > 0 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            } else
            {
                animation.Play(gameTime, "turn", AsepriteAnimation.AnimationDirection.FORWARD);
                if (animation.lastFrame) turn = false;
            }
            base.Update(gameTime);
        }

        public override void UpdateData(GameTime gameTime)
        {
            if (turn) return;
            float timer = (float)gameTime.ElapsedGameTime.Milliseconds;
            moveX(timer * this._speed, (_) =>
            {
                turn = true;
                this._speed = -this._speed;
            });
            base.UpdateData(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Body = animation.Body;
            BeginDraw(spriteBatch);
            DrawSprite(spriteBatch);
            EndDraw(spriteBatch);
        }

    }
}