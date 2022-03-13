using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.UI
{
    public class TitleLevelDisplay : GameObject
    {
        SpriteFont Font;
        Square Background;
        public override void Start()
        {
            this.tag = "Display Level";
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
            if (this.Scene.GameManagement.CurrentStatus != UmbrellaToolsKit.GameManagement.Status.LOADING) return;

            if (AnimationEnd) return;
            
            timer += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (!ShowLevel)
            {
                float moveTo = AnimationStart ? 270f : 426f;
                this.Position.X = EaseOutQuad(timer, this.InitialPosition.X, moveTo, 1000.0f);
            }

            Background.Position.Y = ShowLevel ? LinearTween(timer, 0, -426.0f, 1000.0f) : Background.Position.Y;

            if (timer < 1000.0f) return;
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
                this.Scene.GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.PLAYING;
            }
            timer = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch, false);
            Background.DrawSprite(spriteBatch);

            int level = this.Scene.GameManagement.SceneManagement.CurrentScene;
            spriteBatch.DrawString(this.Font, $"level {level}", this.Position, this.SpriteColor);
            EndDraw(spriteBatch);
        }

        public override void Dispose()
        {
            Background.Dispose();
            base.Dispose();
        }
    }
}