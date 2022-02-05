using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;

namespace game_jaaj_6.Parallax
{
    public class Background : GameObject
    {
        public override void Start()
        {
            base.Start();
            this.Sprite = Scene.Content.Load<Texture2D>("Sprites/sky");
            this.SamplerState = SamplerState.LinearWrap;
        }

        float _speed = 0.05f;
        public override void Update(GameTime gameTime)
        {
            float timer = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(this.Scene.Camera != null)
            {
                this.Position = Vector2.Subtract(Vector2.Lerp(this.Position, this.Scene.Camera.Position, timer * _speed), new Vector2(300, 100));
                this.Position = this.Position.ToPoint().ToVector2();
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            spriteBatch.Draw(
                    Sprite,
                    Scene.Camera.Position - Scene.Camera.Origin,
                    new Rectangle(new Point((int)Scene.Camera.Position.X, 0), new Point(Sprite.Width, Sprite.Height)),
                    SpriteColor
                );
            EndDraw(spriteBatch);
        }
    }
}