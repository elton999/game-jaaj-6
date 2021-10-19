using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmbrellaToolsKit;

namespace game_jaaj_6.Gameplay
{
    public class CirclePath : GameObject
    {
        public override void Start()
        {
            base.Start();
            this._RenderTarget = new RenderTarget2D(this.Scene.ScreemGraphicsDevice, 426, 426);
            this.Effect = this.Scene.Content.Load<Effect>("Shaders/circle");
        }

        public float distance = 0;
        public bool isDistanceEnough { get => this.distance > 40.0f; }
        public Vector2 PostionOnGround;
        public Actors.Player Player;
        public override void Update(GameTime gameTime)
        {
            this.distance = Vector2.Distance(this.Scene.Players[0].Position, this.PostionOnGround);
            base.Update(gameTime);
        }

        private RenderTarget2D _RenderTarget;
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (isDistanceEnough)
            {
                float distanceEffect = (float)Math.Pow(this.distance / 426, 2);
                this.Effect.Parameters["Radius"].SetValue(distanceEffect);

                this.Sprite = (Texture2D)this._RenderTarget;
                BeginDraw(spriteBatch);
                DrawSprite(spriteBatch);
                EndDraw(spriteBatch);
            }
        }
    }
}
