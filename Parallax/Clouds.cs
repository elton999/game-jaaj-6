using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolKit;

namespace game_jaaj_6.Parallax
{
    public class Clouds : GameObject
    {
        public override void Start()
        {
            base.Start();
            this.Sprite = Scene.Content.Load<Texture2D>("Sprites/sky");
        }

        float _speed = 0.05f;
        public override void Update(GameTime gameTime)
        {
            float timer = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            this.Position = Vector2.Subtract(Vector2.Lerp(this.Position, this.Scene.Camera.Position, timer * _speed), new Vector2(300, 100));
            base.Update(gameTime);

            // float timer = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            // var parallax = Vector2.Multiply(Vector2.Subtract(cameraPreview, this.Scene.Camera.Position), -0.5f);
            // var target = Vector2.Subtract(this.Position, parallax);
            // this.Position = Vector2.Lerp(this.Position, target, timer * _speed);

            // this.cameraPreview = this.Scene.Camera.Position;
        }
    }
}