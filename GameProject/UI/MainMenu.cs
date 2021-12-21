using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using System.Collections.Generic;

namespace game_jaaj_6.UI
{
    class MainMenu : GameObject
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

            MenuItems = new List<string>();
            MenuItems.Add("Play");
            MenuItems.Add("Credits");
            MenuItems.Add("Settings");
            MenuItems.Add("Quit");

            SpriteSelect = Scene.Content.Load<Texture2D>("Sprites/tilemap");
            SpriteSelectBody = new Rectangle(new Point(56, 112), new Point(8, 8));
            SpriteSelecPosition = new Vector2(142, 171);

            Background = new Square();
            Background.Scene = this.Scene;
            Background.SquareColor = Color.Black;
            Background.size = ScreenSize;
            Background.Start();
        }
        private bool ShowMenu = false;
        private List<string> MenuItems;
        private Texture2D SpriteSelect;
        private Rectangle SpriteSelectBody;
        private Vector2 SpriteSelecPosition;

        public override void Update(GameTime gameTime)
        {
            Input();
            var keyboardState = Keyboard.GetState();
            if (keyboardState.GetPressedKeys().Length > 0)
                ShowMenu = true;
        }

        int itemSelected = 0;
        bool CUp = false;
        bool UpPressed = false;
        bool CDown = false;
        bool DownPressed = false;
        bool CComfirm = false;
        private void Input()
        {
            if (!_isOnMenu) return;
            var keyboardState = Keyboard.GetState();

            CUp = false;
            if (keyboardState.IsKeyDown(Keys.Up))
            {
                if (!UpPressed) CUp = true;
                UpPressed = true;
            }
            else if (keyboardState.IsKeyUp(Keys.Up))
                UpPressed = false;

            CDown = false;
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                if (!DownPressed) CDown = true;
                DownPressed = true;
            }
            else if (keyboardState.IsKeyUp(Keys.Down))
                DownPressed = false;

            if (!ShowMenu) return;

            if (CUp && itemSelected > 0) itemSelected -= 1;
            if (CDown && itemSelected < MenuItems.Count - 1) itemSelected += 1;

            if (keyboardState.IsKeyDown(Keys.Enter) && CComfirm)
            {
                switch (itemSelected) {
                    case 0:
                        Scene.GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.LEVEL_SELECT;
                        break;
                    case 3:
                        Scene.GameManagement.Game.Exit();
                        break;
                }
            }
            CComfirm = keyboardState.IsKeyUp(Keys.Enter);
        }

        private Vector2 CenterPosition()
        {
            var originText = Vector2.Divide(TextSize.ToVector2(), 2f);
            var centerScreen = Vector2.Divide(ScreenSize.ToVector2(), 2f);
            return Vector2.Subtract(centerScreen, originText);
        }

        private bool _isOnMenu
        {
            get => this.Scene.GameManagement.CurrentStatus == UmbrellaToolsKit.GameManagement.Status.MENU;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!_isOnMenu) return;
            BeginDraw(spriteBatch, false);
            Background.DrawSprite(spriteBatch);
            if(!ShowMenu)
                spriteBatch.DrawString(this.Font, Text, CenterPosition(), Color.White);
            else
            {
                spriteBatch.Draw(SpriteSelect, SpriteSelecPosition - ((MenuItems.Count - 1 - itemSelected) * new Vector2(0, 10)), SpriteSelectBody, Color.White);
                for(int i = 0; i < MenuItems.Count; i++)
                    spriteBatch.DrawString(this.Font, MenuItems[i], new Vector2(150, 140 + (i*10)), Color.White);
            }
            EndDraw(spriteBatch);
        }
    }
}
