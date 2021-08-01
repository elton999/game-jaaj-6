using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolKit;
using UmbrellaToolKit.Collision;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public class EndLevel : Actor
    {

        public override void UpdateData(GameTime gameTime)
        {
            var player = this.Scene.AllActors[0];
            if (this.overlapCheck(player))
            {
                int nextScene = this.Scene.GameManagement.SceneManagement.CurrentScene + 1;
                float updateDataTime = this.Scene.GameManagement.SceneManagement.MainScene.updateDataTime;
                if (nextScene <= 4)
                {
                    System.Console.WriteLine(nextScene);
                    this.Scene.GameManagement.SceneManagement.CurrentScene = nextScene;
                    this.Scene.GameManagement.restart();
                }
            }
        }

    }
}