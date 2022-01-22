using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;

namespace game_jaaj_6.Cutscene
{
    public class CutsceneManagement
    {
        public Scene mainScene;
        public GameManagement GameManagement;
        public int CurrentScene = 0;
        public bool isShowingCutscene { get => CurrentScene < 5 || CurrentScene == 7; }

        private Input Input;

        public void Start()
        {
            CreateScene();
            CreateCutscene6();
            CreateCutscene5();
            CreateCutscene4();
            CreateCutscene3();
            CreateCutscene2();
            CreateCutscene1();
            Input = new Input();
        }

        void CreateScene()
        {
            mainScene = new Scene(GameManagement.Game.GraphicsDevice, GameManagement.Game.Content);
            mainScene.SetBackgroundColor = Color.Black;
            mainScene.GameManagement = GameManagement;
            mainScene.CreateBackBuffer();
            mainScene.CreateCamera();
            mainScene.LevelReady = true;
        }

        public Background Background;
        void CreateBackground()
        {
            Background = new Background();
            Background.Scene = mainScene;
            Background.Start();
        }

        void CreateCutscene1()
        {
            var scene = new Scenes.Scene1();
            scene.Scene = mainScene;
            scene.cutsceneManagement = this;
            scene.Start();
        }

        void CreateCutscene2()
        {
            var scene = new Scenes.Scene2();
            scene.cutsceneManagement = this;
            scene.Scene = mainScene;
            scene.Start();
        }

        void CreateCutscene3()
        {
            var scene = new Scenes.Scene3();
            scene.Scene = mainScene;
            scene.cutsceneManagement = this;
            scene.Start();
        }

        void CreateCutscene4()
        {
            CreateBackground();
            var scene = new Scenes.Scene4();
            scene.cutsceneManagement = this;
            scene.Scene = mainScene;
            scene.Start();
        }

        void CreateCutscene5()
        {
            var scene = new Scenes.Scene5();
            scene.cutsceneManagement = this;
            scene.Scene = mainScene;
            scene.Start();
        }

        void CreateCutscene6()
        {
            var scene = new Scenes.Scene6();
            scene.cutsceneManagement = this;
            scene.Scene = mainScene;
            scene.Start();
        }

        public void Update(GameTime gameTime)
        {
            if(CurrentScene < 4 && Input.KeyPress(Input.Button.CONFIRM)) CurrentScene++;
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
