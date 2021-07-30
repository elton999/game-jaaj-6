using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_jaaj_6
{
    public class GameManagement : UmbrellaToolKit.GameManagement
    {
        public override void Start()
        {
            base.Start();
            this.SceneManagement.CurrentScene = 2;
            this.SceneManagement.Start();
            this.SceneManagement.MainScene.GameManagement = this;
            this.SceneManagement.MainScene.updateDataTime = 1f / 60f;

            this.SetValues();
        }

        public void SetValues()
        {
            this.Values.Add("key", true);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            this.SceneManagement.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.SceneManagement.Draw(spriteBatch);
            base.Draw(spriteBatch);
        }
    }
}