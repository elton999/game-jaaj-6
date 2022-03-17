using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UmbrellaToolsKit;

namespace game_jaaj_6.UI
{
    struct ItemsSetting
    {
        public string Name;
        public List<string> options;
    }

    public class MenuSettings : GameObject
    {
        private SpriteFont Font;
        private Texture2D SpriteSelect;
        private Rectangle SpriteSelectBody;
        private Vector2 SpriteSelecPosition;
        private List<ItemsSetting> MenuItems;
        private int[] currentItemOption = new int[] { 0, 5 };

        public override void Start()
        {
            base.Start();

            Scene.UI.Add(this);
            Font = Scene.Content.Load<SpriteFont>("Kenney_Rocket");

            SpriteSelect = Scene.Content.Load<Texture2D>("Sprites/tilemap");
            SpriteSelectBody = new Rectangle(new Point(56, 112), new Point(8, 8));
            SpriteSelecPosition = new Vector2(142, 151);

            setAllMenuItems();

            UpdateScreenSettings();

            InputHelper = new Input();
        }

        private void setAllMenuItems()
        {
            MenuItems = new List<ItemsSetting>();
            var item = new ItemsSetting();
            item.Name = "Window mode";
            item.options = new List<string>();
            item.options.AddRange(new string[] { "windowed", "fullscreen" });
            MenuItems.Add(item);

            item = new ItemsSetting();
            item.Name = "Resolution";
            item.options = new List<string>();
            foreach (Vector2 resolution in ScreenController.instance.Resolutions)
                item.options.Add($"{resolution.X} x {resolution.Y}");
            MenuItems.Add(item);
        }

        int itemSelected = 0;
        Input InputHelper;
        public override void Update(GameTime gameTime)
        {
            if (!_isOnSettings) return;

            if (InputHelper.KeyPress(Input.Button.UP) && itemSelected > 0) itemSelected -= 1;
            if (InputHelper.KeyPress(Input.Button.DOWN) && itemSelected < MenuItems.Count - 1) itemSelected += 1;
            
            changeOptions();

            if (InputHelper.KeyPress(Input.Button.ESC))
            { 
                Scene.GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.MENU;
                InputHelper.ResetStatus();
            }
        }

        private void changeOptions()
        {
            int maxItems = MenuItems[itemSelected].options.Count - 1;
            if (InputHelper.KeyPress(Input.Button.LEFT))
            {
                if (currentItemOption[itemSelected] == 0) currentItemOption[itemSelected] = maxItems;
                else currentItemOption[itemSelected]--;
                UpdateScreenSettings();
            }

            if (InputHelper.KeyPress(Input.Button.RIGHT))
            {
                if (currentItemOption[itemSelected] == maxItems) currentItemOption[itemSelected] = 0;
                else currentItemOption[itemSelected]++;
                UpdateScreenSettings();
            }
        }

        private void UpdateScreenSettings()
        {
            ScreenController.instance.fullScreen = currentItemOption[0] == 1;
            ScreenController.instance.Resolution = currentItemOption[1];
            ScreenController.instance.Update();
        }

        private bool _isOnSettings
        {
            get => Scene.GameManagement.CurrentStatus == UmbrellaToolsKit.GameManagement.Status.SETTINGS; 
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!_isOnSettings) return;
            Scene.ScreemGraphicsDevice.Clear(Color.Black);

            BeginDraw(spriteBatch);
            DrawPointer(spriteBatch);
            DrawItems(spriteBatch);
            EndDraw(spriteBatch);
        }

        private void DrawItems(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < MenuItems.Count; i++)
            {
                int currentOption = currentItemOption[i];
                spriteBatch.DrawString(Font, MenuItems[i].Name + ":", new Vector2(150, 140 + (i * 10)), Color.White);
                spriteBatch.DrawString(Font, MenuItems[i].options[currentOption], new Vector2(240, 140 + (i * 10)), Color.Gray);
            }
        }

        private void DrawPointer(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                SpriteSelect, 
                SpriteSelecPosition - ((MenuItems.Count - 1 - itemSelected) * new Vector2(0, 10)), 
                SpriteSelectBody, 
                Color.White
            );
        }
    }
}
