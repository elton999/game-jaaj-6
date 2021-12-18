using UmbrellaToolsKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_jaaj_6.UI
{
    public class LevelSelect
    {
        Scene mainScene;
        public GameManagement GameManagement;
        public void Start()
        {
            createScene();
        }

        private void createScene() 
        {
            mainScene = new Scene(GameManagement.Game.GraphicsDevice, GameManagement.Game.Content);
            mainScene.GameManagement = GameManagement;
            mainScene.SetLevelByName("map");
            mainScene.Camera.Position = mainScene.Camera.Origin;
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
