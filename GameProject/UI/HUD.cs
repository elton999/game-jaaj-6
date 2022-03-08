using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.UI
{
    public class HUD : GameObject
    {
        private Texture2D _spriteAtlas;
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

            _spriteAtlas = this.Scene.Content.Load<Texture2D>("Sprites/tilemap");

            this.BoxKey = new GameObject();
            this.BoxKey.Sprite = _spriteAtlas;
            this.BoxKey.Body = new Rectangle(new Point(56, 16), new Point(16, 16));
            this.BoxKey.Position = new Vector2(5, 7);

            this.KeySprite = new GameObject();
            this.KeySprite.Sprite = _spriteAtlas;
            this.KeySprite.Body = new Rectangle(new Point(72, 16), new Point(16, 16));
            this.KeySprite.Position = new Vector2(5, 7);

            this.Font = this.Scene.Content.Load<SpriteFont>("Kenney_Rocket");
        }

        public void DrawKeyboardInfo(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, "Z - jump", new Vector2(147, 5), Color.White);
            spriteBatch.DrawString(Font, "R - Restart", new Vector2(147, 15), Color.White);

            spriteBatch.DrawString(Font, "Arrows - Move", new Vector2(237, 5), Color.White);
        }

        public void DrawCollectableInfo(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                _spriteAtlas, new Vector2(27, 7), 
                new Rectangle(new Point(96, 0), new Point(16, 16)), 
                Color.White);

            int allRelicsOnLevel = Scene.GameManagement.Values["AllRelicsOnLevel"];
            int RelicsCollected = Scene.GameManagement.Values["AllRelicsCollected"];

            spriteBatch.DrawString(this.Font, $"{RelicsCollected}/{allRelicsOnLevel}", new Vector2(45, 10), Color.White);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch, false);

            this.BlackBox.DrawSprite(spriteBatch);
            if (this.Scene.GameManagement.Values["key"])
                this.KeySprite.DrawSprite(spriteBatch);
            this.BoxKey.DrawSprite(spriteBatch);
            
            DrawCollectableInfo(spriteBatch);

            int level = this.Scene.GameManagement.SceneManagement.CurrentScene;
            spriteBatch.DrawString(this.Font, $"- LEVEL {level}", new Vector2(73, 10), Color.White);

            this.DrawKeyboardInfo(spriteBatch);


            EndDraw(spriteBatch);
        }

        public override void Dispose()
        {
            BlackBox.Dispose();
            base.Dispose();
        }
    }
}