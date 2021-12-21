using System.Collections.Generic;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_jaaj_6.UI
{
    public class Menu
    {
        Scene mainScene;
        public GameManagement GameManagement;
        public void Start()
        {
            createScene();
            showLevelSelect();
            ShowMainMenu();
            GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.MENU;
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
            levelSelectItem.Content = GameManagement.Content;
            levelSelectItem.Scene = mainScene;
            mainScene.Middleground.Add(levelSelectItem);
            levelSelectItem.Start();
        }

        private void ShowMainMenu()
        {
            var mainMenu = new MainMenu();
            mainMenu.Scene = mainScene;
            mainMenu.Start();
            mainScene.UI.Add(mainMenu);
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
