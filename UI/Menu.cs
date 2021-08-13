using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolKit;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.UI
{
    class Menu : GameObject
    {
        SpriteFont Font;
        Point TextSize;
        string Text = "- press any key to start -";
        Square Background;
        Point ScreenSize = new Point(426, 240);
        public override void Start()
        {
            base.Start();
            this.tag = "menu";
            this.Scene.UI.Add(this);
            this.Font = this.Scene.Content.Load<SpriteFont>("Kenney_Rocket");
            TextSize = this.Font.MeasureString(Text).ToPoint();


            Background = new Square();
            Background.Scene = this.Scene;
            Background.SquareColor = Color.Black;
            Background.size = ScreenSize;
            Background.Start();
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0 && _isOnMenu)
                this.Scene.GameManagement.CurrentStatus = UmbrellaToolKit.GameManagement.Status.PLAYING;
        }

        private Vector2 CenterPosition()
        {
            var originText = Vector2.Divide(TextSize.ToVector2(), 2f);
            var centerScreen = Vector2.Divide(ScreenSize.ToVector2(), 2f);
            return Vector2.Subtract(centerScreen, originText);
        }

        private bool _isOnMenu
        {
            get => this.Scene.GameManagement.CurrentStatus == UmbrellaToolKit.GameManagement.Status.MENU;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_isOnMenu)
            {
                BeginDraw(spriteBatch, false);
                Background.DrawSprite(spriteBatch);
                spriteBatch.DrawString(
                    this.Font,
                    Text,
                    CenterPosition(),
                    Color.White
                );
                EndDraw(spriteBatch);
            }
        }
    }
}
