using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using UmbrellaToolsKit;

namespace game_jaaj_6
{
    public class GameManagement : UmbrellaToolsKit.GameManagement
    {
        private Song _backgroundMusic;
        public override void Start()
        {
            base.Start();
            _backgroundMusic = Game.Content.Load<Song>("Sound/music");
            MediaPlayer.Play(_backgroundMusic, new System.TimeSpan(0, 0, 11));
            MediaPlayer.IsRepeating = true;
            CreateMenu();
            CreateCutScene();
        }

        public override void restart()
        {
            SetValues();
            SceneManagement.Start();
            SceneManagement.MainScene.GameManagement = this;
            SceneManagement.MainScene.updateDataTime = 1f / 60f;
            SceneManagement.MainScene.SetBackgroundColor = new Color(Vector3.Divide(new Vector3(65, 146, 195), 255.0f));

            CreateTransitionObject();
            CreateParallax();
            CreateLevelDisplay();
            CreateHUD();
            
            CurrentStatus = Status.LOADING;

            if(SceneManagement.CurrentScene == 1)
                Cutscene.CurrentScene = 7;
        }

        UI.Menu Menu;
        public void CreateMenu()
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

        UI.TitleLevelDisplay LevelTitle;
        private void CreateLevelDisplay()
        {
            if (LevelTitle != null)
                LevelTitle.Dispose();
            LevelTitle = new UI.TitleLevelDisplay();
            LevelTitle.Scene = SceneManagement.MainScene;
            LevelTitle.Scene.UI.Add(LevelTitle);
            LevelTitle.Start();
        }

        UI.HUD HUD;
        private void CreateHUD()
        {
            if(HUD != null)
                HUD.Dispose();
            HUD = new UI.HUD();
            HUD.Scene = SceneManagement.MainScene;
            HUD.Scene.UI.Add(HUD);
            HUD.Start();
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
                Values.Add("AllRelicsOnLevel", 0);
                Values.Add("AllRelicsCollected", 0);
            }
            Values["key"] = false;
            Values["freeze"] = false;
            Values["AllRelicsOnLevel"] = 0;
            Values["AllRelicsCollected"] = 0;
        }

        private bool _canShowMenu { get => 
            CurrentStatus == GameManagement.Status.MENU || 
            CurrentStatus == GameManagement.Status.LEVEL_SELECT ||
            CurrentStatus == GameManagement.Status.SETTINGS ||
            CurrentStatus == GameManagement.Status.CREDITS; 
        }

        public override void Update(GameTime gameTime)
        {
            if (Cutscene.isShowingCutscene){
                Cutscene.Update(gameTime);
                return;
            }else if (_canShowMenu)
                Menu.Update(gameTime);

            base.Update(gameTime);
                
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