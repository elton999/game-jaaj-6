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
            CreateCutScene();
        }

        public override void restart()
        {
            // SceneManagement.CurrentScene = 3;
            SceneManagement.Start();
            SceneManagement.MainScene.GameManagement = this;
            SceneManagement.MainScene.updateDataTime = 1f / 60f;
            SceneManagement.MainScene.SetBackgroundColor = new Color(Vector3.Divide(new Vector3(65, 146, 195), 255.0f));

            CreateTransitionObject();
            CreateParallax();
            CreateLevelDisplay();
            CreateHUD();
            SetValues();
            
            CurrentStatus = Status.LOADING;

            if(SceneManagement.CurrentScene == 1)
                Cutscene.CurrentScene = 7;
        }

        UI.Menu Menu;
        private void CreateMenu()
        {
            Menu = new UI.Menu();
            Menu.GameManagement = this;
            Menu.Start();
        }

        Cutscene.CutsceneManagement Cutscene;
        private void CreateCutScene()
        {
            Cutscene = new Cutscene.CutsceneManagement();
            Cutscene.GameManagement = this;
            Cutscene.Start();
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
            var background = new Parallax.Background();
            background.Scene = SceneManagement.MainScene;
            SceneManagement.MainScene.Backgrounds.Add(background);
            background.Start();
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
            CurrentStatus == GameManagement.Status.LEVEL_SELECT ||
            CurrentStatus == GameManagement.Status.SETTINGS ||
            CurrentStatus == GameManagement.Status.CREDITS; 
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (Cutscene.isShowingCutscene){
                Cutscene.Update(gameTime);
                return;
            }else if (_canShowMenu)
                Menu.Update(gameTime);
                
            if (CurrentStatus != GameManagement.Status.LOADING)
            {
                if (CurrentGameplayStatus == GameManagement.GameplayStatus.DEATH)
                {
                    CurrentGameplayStatus = GameManagement.GameplayStatus.ALIVE;
                    transition.Close();
                    wait(1f, () => { transition.Open(); });
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SceneManagement.Draw(spriteBatch);

            if (Cutscene.isShowingCutscene)
                Cutscene.Draw(spriteBatch);
            else if (_canShowMenu) Menu.Draw(spriteBatch);
            
        }
    }
}