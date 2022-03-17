using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Collision;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public class EndLevel : Actor
    {
        private int MaxNumberOfLevels = 10;
        public override void Start()
        {
            System.Console.WriteLine("start");
            base.Start();
            FinishLevelSound = Content.Load<SoundEffect>("Sound/finish level");
        }
        SoundEffect FinishLevelSound;

        bool _isChangingLevel = false;
        public override void UpdateData(GameTime gameTime)
        {
            var player = this.Scene.AllActors[0];
            if (this.overlapCheck(player) && !_isChangingLevel)
            {
                FinishLevelSound.Play();
                _isChangingLevel = true;
                
                Scene.GameManagement.UnluckNextLevel();

                int nextScene = Scene.GameManagement.SceneManagement.CurrentScene + 1;
                float updateDataTime = Scene.GameManagement.SceneManagement.MainScene.updateDataTime;

                if (nextScene < MaxNumberOfLevels + 1)
                {
                    Scene.GameManagement.CurrentStatus = GameManagement.Status.PAUSE;
                    wait(2.0f, () =>
                    {
                        Scene.GameManagement.SceneManagement.MainScene.Dispose();
                        Scene.GameManagement.CurrentStatus = GameManagement.Status.LEVEL_SELECT;
                    });
                }
                else
                {
                    Scene.GameManagement.CurrentStatus = GameManagement.Status.PAUSE;
                    wait(2.0f, () =>
                    {
                        Scene.GameManagement.CurrentStatus = GameManagement.Status.CREDITS;
                    });
                }
            }
        }

    }
}