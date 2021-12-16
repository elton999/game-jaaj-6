using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;

namespace game_jaaj_6
{
    public class GameManagement : UmbrellaToolsKit.GameManagement
    {
        public override void Start()
        {
            base.Start();
            this.restart();
        }

        public override void restart()
        {
            this.SceneManagement.Start();
            this.SceneManagement.MainScene.GameManagement = this;
            this.SceneManagement.MainScene.updateDataTime = 1f / 60f;
            this.SceneManagement.MainScene.SetBackgroundColor = new Color(Vector3.Divide(new Vector3(65, 146, 195), 255.0f));

            if(this.SceneManagement.CurrentScene == 1)
                this.CreateMenu();

            this.CreateTransitionObject();
            this.CreateParallax();
            this.CreateLevelDisplay();
            this.CreateHUD();
            this.SetValues();
        }

        public void CreateMenu()
        {
            var menu = new UI.Menu();
            menu.Scene = this.SceneManagement.MainScene;
            this.CurrentStatus = Status.MENU;
            menu.Start();
        }

        private void CreateFinalCredtis()
        {
            this.SceneManagement.MainScene.UI.Clear();
            var credits = new UI.Credits();
            credits.Scene = this.SceneManagement.MainScene;
            credits.Scene.UI.Add(credits);
            credits.Start();
            this.CurrentStatus = GameManagement.Status.PAUSE;
        }

        private void CreateLevelDisplay()
        {
            var levelDisplay = new UI.LevelDisplay();
            levelDisplay.Scene = this.SceneManagement.MainScene;
            levelDisplay.Scene.UI.Add(levelDisplay);
            levelDisplay.Start();

        }

        private void CreateHUD()
        {
            var hud = new UI.HUD();
            hud.Scene = this.SceneManagement.MainScene;
            hud.Scene.UI.Add(hud);
            hud.Start();
        }

        private void CreateParallax()
        {
            var clouds = new Parallax.Clouds();
            clouds.Scene = this.SceneManagement.MainScene;
            this.SceneManagement.MainScene.Backgrounds.Add(clouds);
            clouds.Start();
        }

        UI.Transition transition;
        private void CreateTransitionObject()
        {
            transition = new UI.Transition();
            transition.Scene = this.SceneManagement.MainScene;
            this.SceneManagement.MainScene.UI.Add(transition);
            transition.Start();
        }

        public void SetValues()
        {
            if (!this.Values.ContainsKey("key"))
            {
                this.Values.Add("key", false);
                this.Values.Add("freeze", false);
            }
            else
            {
                this.Values["key"] = false;
                this.Values["freeze"] = false;
            }
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (this.CurrentGameplayStatus == GameManagement.GameplayStatus.DEATH)
            {
                this.CurrentGameplayStatus = GameManagement.GameplayStatus.ALIVE;
                transition.Close();
                wait(1f, () => { transition.Open(); });
            }

            this.SceneManagement.Update(gameTime);

            if (this.CurrentStatus == GameManagement.Status.CREDITS)
                this.CreateFinalCredtis();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.CurrentStatus != GameManagement.Status.LOADING)
                this.SceneManagement.Draw(spriteBatch);
            //base.Draw(spriteBatch);
        }
    }
}