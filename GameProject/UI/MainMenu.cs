using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using System.Collections.Generic;

namespace game_jaaj_6.UI
{
    class MainMenu : GameObject
    {
        SpriteFont Font;
        Point TextSize;
        string Text = "- press any key to start -";
        Point ScreenSize = new Point(426, 240);
        public override void Start()
        {
            base.Start();
            this.tag = "menu";

            this.Scene.UI.Add(this);
            this.Font = this.Scene.Content.Load<SpriteFont>("Kenney_Rocket");
            TextSize = this.Font.MeasureString(Text).ToPoint();
            
            CreateMenuItems();
            CreateLogo();

            InputHelper = new Input();
        }

        private void CreateMenuItems()
        {
            MenuItems = new List<string>();
            MenuItems.Add("Play");
            MenuItems.Add("Credits");
            MenuItems.Add("Settings");
            MenuItems.Add("Quit");

            SpriteArrow = Scene.Content.Load<Texture2D>("Sprites/tilemap");
            SpriteArrowBody = new Rectangle(new Point(56, 112), new Point(8, 8));
            SpriteArrowPosition = new Vector2(142, 171);
        }

        private Vector2 positionLogo;
        private Texture2D spriteLogo;
        private void CreateLogo()
        {
            spriteLogo = Scene.Content.Load<Texture2D>("Sprites/logo");
            float positionX = Scene.Sizes.X / 2f - spriteLogo.Width / 2f;
            positionLogo = new Vector2(positionX, 30);
        }

        private bool ShowMenu = false;
        private List<string> MenuItems;
        private Texture2D SpriteArrow;
        private Rectangle SpriteArrowBody;
        private Vector2 SpriteArrowPosition;

        public override void Update(GameTime gameTime)
        {
            InputCheck();

            if (InputHelper.PressAnyButton())
            {
                ShowMenu = true;
                InputHelper.ResetStatus();
            }
        }

        int itemSelected = 0;
        Input InputHelper;
        private void InputCheck()
        {
            if (!ShowMenu || !_isOnMenu) return;

            if (InputHelper.KeyPress(Input.Button.UP) && itemSelected > 0) itemSelected -= 1;
            if (InputHelper.KeyPress(Input.Button.DOWN) && itemSelected < MenuItems.Count - 1) itemSelected += 1;

            if (InputHelper.KeyPress(Input.Button.CONFIRM)) executeSelectedItem();
        }

        private void executeSelectedItem()
        {
            switch (itemSelected)
            {
                case 0:
                    Scene.GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.LEVEL_SELECT;
                    break;
                case 1:
                    Scene.GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.CREDITS;
                    break;
                case 2:
                    Scene.GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.SETTINGS;
                    break;
                case 3:
                    Scene.GameManagement.Game.Exit();
                    break;
            }
        }

        private Vector2 CenterPosition()
        {
            var originText = Vector2.Divide(TextSize.ToVector2(), 2f);
            var centerScreen = Vector2.Divide(ScreenSize.ToVector2(), 2f);
            centerScreen.Y += 50f;
            return Vector2.Subtract(centerScreen, originText);
        }

        private bool _isOnMenu
        {
            get => Scene.GameManagement.CurrentStatus == UmbrellaToolsKit.GameManagement.Status.MENU;
        }

        private Vector2 GetArrowPosition()
        {
            return SpriteArrowPosition - ((MenuItems.Count - 1 - itemSelected) * new Vector2(0, 10));
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!_isOnMenu) return;
            Scene.ScreemGraphicsDevice.Clear(Color.Black);
            BeginDraw(spriteBatch, false);
            DrawLogo(spriteBatch);
            if(!ShowMenu)
                spriteBatch.DrawString(this.Font, Text, CenterPosition(), Color.White);
            else
                DrawMenuItems(spriteBatch);
            EndDraw(spriteBatch);
        }

        private void DrawLogo(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(spriteLogo, positionLogo, Color.White);
        }

        private void DrawMenuItems(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteArrow, GetArrowPosition(), SpriteArrowBody, Color.White);
            for (int i = 0; i < MenuItems.Count; i++)
                spriteBatch.DrawString(this.Font, MenuItems[i], new Vector2(150, 140 + (i * 10)), Color.White);
        }


    }
}
