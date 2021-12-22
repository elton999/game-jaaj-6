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
            CreateFinalCredtis();
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
            GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.MENU;
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
