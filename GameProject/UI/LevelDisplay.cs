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
            tag = "Display Level";
            Font = Scene.Content.Load<SpriteFont>("Kenney_Rocket_Big");

            Position = new Vector2(-100, Scene.Sizes.Y / 2f);
            InitialPosition = Position;

            CreateBackground();
        }

        public void CreateBackground()
        {
            Background = new Square();
            Background.Scene = Scene;
            Background.size = Scene.Sizes;
            Background.SquareColor = Color.Black;
            Background.Start();
        }

        float timer = 0;
        bool AnimationStart = true;
        bool ShowLevel = false;
        bool AnimationEnd = false;

        public override void Update(GameTime gameTime)
        {
            if (Scene.GameManagement.CurrentStatus != GameManagement.Status.LOADING) return;

            if (AnimationEnd) return;
            
            timer += (float)gameTime.ElapsedGameTime.Milliseconds;
            if (!ShowLevel)
            {
                float moveTo = AnimationStart ? 270f : 426f;
                Position.X = EaseOutQuad(timer, InitialPosition.X, moveTo, 1000.0f);
            }

            Background.Position.Y = ShowLevel ? LinearTween(timer, 0, -426.0f, 1000.0f) : Background.Position.Y;

            if (timer < 1000.0f) return;
            if (AnimationStart)
            {
                AnimationStart = false;
                InitialPosition.X = Position.X;
            }
            else if (!AnimationStart && !ShowLevel)
                ShowLevel = true;
            else if (!AnimationStart && ShowLevel)
            {
                AnimationEnd = true;
                Scene.GameManagement.CurrentStatus = GameManagement.Status.PLAYING;
            }
            timer = 0;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch, false);
            Background.DrawSprite(spriteBatch);

            int level = Scene.GameManagement.SceneManagement.CurrentScene;
            spriteBatch.DrawString(Font, $"level {level}", Position, SpriteColor);
            EndDraw(spriteBatch);
        }

        public override void Dispose()
        {
            Background.Dispose();
            base.Dispose();
        }
    }
}