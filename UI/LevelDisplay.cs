using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolKit;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.UI
{
    public class LevelDisplay : GameObject
    {
        SpriteFont Font;
        Square Background;
        public override void Start()
        {
            this.tag = "Display Level";
            base.Start();
            this.Font = this.Scene.Content.Load<SpriteFont>("Kenney_Rocket_Big");

            this.Position = new Vector2(-100, this.Scene.Sizes.Y / 2f);
            this.InitialPosition = this.Position;

            this.CreateBackground();
        }

        public void CreateBackground()
        {
            this.Background = new Square();
            this.Background.Scene = this.Scene;
            this.Background.size = this.Scene.Sizes;
            this.Background.SquareColor = Color.Black;
            this.Background.Start();
        }

        float timer = 0;
        bool AnimationStart = true;
        bool ShowLevel = false;
        bool AnimationEnd = false;

        public override void Update(GameTime gameTime)
        {
            if (!AnimationEnd)
            {
                timer += (float)gameTime.ElapsedGameTime.Milliseconds;
                if (!ShowLevel)
                {
                    float moveTo = AnimationStart ? 270f : 426f;
                    this.Position.X = EaseOutQuad(timer, this.InitialPosition.X, moveTo, 1000.0f);
                }
                else if (ShowLevel)
                    Background.Position.Y = LinearTween(timer, 0, -426.0f, 1000.0f);

                if (timer > 1000.0f)
                {
                    if (AnimationStart)
                    {
                        AnimationStart = false;
                        this.InitialPosition.X = this.Position.X;
                    }
                    else if (!AnimationStart && !ShowLevel)
                        ShowLevel = true;
                    else if (!AnimationStart && ShowLevel)
                    {
                        AnimationEnd = true;
                        this.Scene.GameManagement.CurrentStatus = UmbrellaToolKit.GameManagement.Status.PLAYING;
                    }
                    timer = 0;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch, false);
            Background.DrawSprite(spriteBatch);

            int level = this.Scene.GameManagement.SceneManagement.CurrentScene;
            spriteBatch.DrawString(this.Font, $"level {level}", this.Position, this.SpriteColor);
            EndDraw(spriteBatch);
        }
    }
}