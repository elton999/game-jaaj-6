using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbrellaToolKit;

namespace game_jaaj_6.UI
{
    public class Transition : GameObject
    {
        public override void Start()
        {
            this._RenderTarget = new Texture2D(this.Scene.ScreemGraphicsDevice, 426, 240);
            this.Effect = this.Scene.Content.Load<Effect>("Shaders/transition");
            this.Open();
        }

        private float _speed = 0.05f;
        public override void Update(GameTime gameTime)
        {
            float timer = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this._radious = MathHelper.Lerp(this._radiousTo, this._radious, this._speed * timer);
        }

        public void Open()
        {
            this._radious = 0.0f;
            this._radiousTo = 1000.0f;
        }

        public void Close()
        {
            this._radious = 1000.0f;
            this._radiousTo = 0.0f;

        }

        private Texture2D _RenderTarget;
        private float _radious = 0f;
        private float _radiousTo = 1000.0f;
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Sprite = this._RenderTarget;
            this.Effect.Parameters["radious"].SetValue(_radious / 1000.0f);

            BeginDraw(spriteBatch, false);
            DrawSprite(spriteBatch);
            EndDraw(spriteBatch);
        }
    }
}