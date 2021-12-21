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
            CreateMenu();
            this.restart();
        }

        public override void restart()
        {
            this.SceneManagement.Start();
            this.SceneManagement.MainScene.GameManagement = this;
            this.SceneManagement.MainScene.updateDataTime = 1f / 60f;
            this.SceneManagement.MainScene.SetBackgroundColor = new Color(Vector3.Divide(new Vector3(65, 146, 195), 255.0f));

            this.CreateTransitionObject();
            this.CreateParallax();
            this.CreateLevelDisplay();
            this.CreateHUD();
            this.SetValues();
        }

        UI.Menu Menu;
        private void CreateMenu()
        {
            Menu = new UI.Menu();
            Menu.GameManagement = this;
            Menu.Start();
        }

        private void CreateFinalCredtis()
        {
            SceneManagement.MainScene.UI.Clear();
            var credits = new UI.Credits();
            credits.Scene = SceneManagement.MainScene;
            credits.Scene.UI.Add(credits);
            credits.Start();
            CurrentStatus = GameManagement.Status.PAUSE;
        }

        private void CreateLevelDisplay()
        {
            var levelTitle = new UI.TitleLevelDisplay();
            levelTitle.Scene = SceneManagement.MainScene;
            levelTitle.Scene.UI.Add(levelTitle);
            levelTitle.Start();
        }

        private void CreateHUD()
        {
            var hud = new UI.HUD();
            hud.Scene = SceneManagement.MainScene;
            hud.Scene.UI.Add(hud);
            hud.Start();
        }

        private void CreateParallax()
        {
            var clouds = new Parallax.Clouds();
            clouds.Scene = SceneManagement.MainScene;
            SceneManagement.MainScene.Backgrounds.Add(clouds);
            clouds.Start();
        }

        UI.Transition transition;
        private void CreateTransitionObject()
        {
            transition = new UI.Transition();
            transition.Scene = SceneManagement.MainScene;
            SceneManagement.MainScene.UI.Add(transition);
            transition.Start();
        }

        public void SetValues()
        {
            if (!Values.ContainsKey("key"))
            {
                Values.Add("key", false);
                Values.Add("freeze", false);
            }
            else
            {
                Values["key"] = false;
                Values["freeze"] = false;
            }
        }

        private bool _canShowMenu { get => 
                CurrentStatus == GameManagement.Status.MENU || 
                CurrentStatus == GameManagement.Status.LEVEL_SELECT; 
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (CurrentStatus != GameManagement.Status.LOADING)
            {
                
                if (this.CurrentGameplayStatus == GameManagement.GameplayStatus.DEATH)
                {
                    this.CurrentGameplayStatus = GameManagement.GameplayStatus.ALIVE;
                    transition.Close();
                    wait(1f, () => { transition.Open(); });
                }
            }
            if(_canShowMenu) Menu.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.CurrentStatus != GameManagement.Status.LOADING)
                this.SceneManagement.Draw(spriteBatch);

            if(_canShowMenu) Menu.Draw(spriteBatch);
        }
    }
}