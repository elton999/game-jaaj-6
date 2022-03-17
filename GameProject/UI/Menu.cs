using System.Collections.Generic;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace game_jaaj_6.UI
{
    public class Menu
    {
        Scene mainScene;
        public GameManagement GameManagement;
        public SoundEffect SelectedSound;

        public void Start()
        {
            var content = GameManagement.Game.Content;
            SelectedSound = content.Load<SoundEffect>("Sound/sfx_button_select");

            createScene();
            showLevelSelect();
            CreateFinalCredtis();
            ShowSettingsMenu();
            ShowMainMenu();
        }

        private void createScene()
        {
            mainScene = new Scene(GameManagement.Game.GraphicsDevice, GameManagement.Game.Content);
            mainScene.GameManagement = GameManagement;
        }

        private void showLevelSelect()
        {
            mainScene.SetLevelByName("map");
            mainScene.Camera.Position = mainScene.Camera.Origin;
            var levelSelectItem = new LevelSelectItem();
            levelSelectItem.Menu = this;
            levelSelectItem.Content = GameManagement.Content;
            levelSelectItem.Scene = mainScene;
            mainScene.Middleground.Add(levelSelectItem);
            levelSelectItem.Start();
        }

        private void ShowMainMenu()
        {
            var mainMenu = new MainMenu();
            mainMenu.Menu = this;
            mainMenu.Scene = mainScene;
            mainMenu.Start();
            mainScene.UI.Add(mainMenu);
            GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.MENU;
        }

        private void ShowSettingsMenu()
        {
            var settingsMenu = new MenuSettings();
            settingsMenu.Scene = mainScene;
            settingsMenu.Start();
            mainScene.UI.Add(settingsMenu);
            GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.SETTINGS;
        }

        private void CreateFinalCredtis()
        {
            var credits = new Credits();
            credits.Scene = mainScene;
            credits.Scene.UI.Add(credits);
            credits.Start();
        }

        public void Update(GameTime gameTime)
        {
            mainScene.Update(gameTime);   
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            mainScene.Draw(spriteBatch,
                GameManagement.Game.GraphicsDevice,
                new Vector2(
                    GameManagement.Game.GraphicsDevice.Viewport.Width,
                    GameManagement.Game.GraphicsDevice.Viewport.Height
                ));
        }
    }
}
