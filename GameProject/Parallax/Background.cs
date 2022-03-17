using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;

namespace game_jaaj_6.Parallax
{
    public class Background : GameObject
    {
        private int _spriteBackground = 1;

        public override void Start()
        {
            base.Start();
            int currentLevel = Scene.GameManagement.SceneManagement.CurrentScene;
            _spriteBackground = currentLevel > 5 ? 2 : 1;
            Sprite = Scene.Content.Load<Texture2D>($"Sprites/sky{_spriteBackground}");
            SamplerState = SamplerState.LinearWrap;
        }

        float _speed = 0.05f;
        public override void Update(GameTime gameTime)
        {
            float timer = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if(this.Scene.Camera != null)
            {
                Position = Vector2.Lerp(Position, Scene.Camera.Position, timer * _speed) - new Vector2(300, 100);
                Position = Position.ToPoint().ToVector2();
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