using System.ComponentModel.Design.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolKit;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.UI
{
    public class HUD : GameObject
    {
        public Square BlackBox;
        public GameObject BoxKey;
        public GameObject KeySprite;
        public SpriteFont Font;

        public override void Start()
        {
            base.Start();
            this.BlackBox = new Square();
            this.BlackBox.size = new Point(426, 30);
            this.BlackBox.SquareColor = Color.Black;
            this.BlackBox.Scene = this.Scene;
            this.BlackBox.Start();

            this.BoxKey = new GameObject();
            this.BoxKey.Sprite = this.Scene.Content.Load<Texture2D>("Sprites/tilemap");
            this.BoxKey.Body = new Rectangle(new Point(56, 16), new Point(16, 16));
            this.BoxKey.Position = new Vector2(5, 7);

            this.KeySprite = new GameObject();
            this.KeySprite.Sprite = this.Scene.Content.Load<Texture2D>("Sprites/tilemap");
            this.KeySprite.Body = new Rectangle(new Point(72, 16), new Point(16, 16));
            this.KeySprite.Position = new Vector2(5, 7);

            this.Font = this.Scene.Content.Load<SpriteFont>("Kenney_Rocket");
        }

        public void DrawKeyboardInfo(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "Z - jump", new Vector2(100, 5), Color.White);
            spriteBatch.DrawString(Font, "R - Restart", new Vector2(100, 15), Color.White);

            spriteBatch.DrawString(Font, "Arrows - Move", new Vector2(190, 5), Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch, false);

            this.BlackBox.DrawSprite(spriteBatch);
            if (this.Scene.GameManagement.Values["key"])
                this.KeySprite.DrawSprite(spriteBatch);
            this.BoxKey.DrawSprite(spriteBatch);

            int level = this.Scene.GameManagement.SceneManagement.CurrentScene;
            spriteBatch.DrawString(this.Font, $"- LEVEL {level}", new Vector2(25, 10), Color.White);

            this.DrawKeyboardInfo(spriteBatch);

            EndDraw(spriteBatch);
        }
    }
}