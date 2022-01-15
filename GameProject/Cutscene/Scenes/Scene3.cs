using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_jaaj_6.Cutscene.Scenes
{
    public class Scene3 : Space
    {
        public override void Start()
        {
            base.Start();
            ship1InitialPosition = ship1Position;
            ships2Positions.CopyTo(ships2InitialPositions, 0);
            ships3Positions.CopyTo(ships3InitialPositions, 0);
        }

        float timer = 0;
        float speed = 5f;
        Vector2 ship1InitialPosition;
        Vector2[] ships2InitialPositions = new Vector2[2];
        Vector2[] ships3InitialPositions = new Vector2[3];

        public override void Update(GameTime gameTime)
        {
            if (cutsceneManagement.CurrentScene != 2) return;
            base.Update(gameTime);
            timer += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if (timer > 2)
            {
                ship1Animation.Play(gameTime, "attack", UmbrellaToolsKit.Sprite.AsepriteAnimation.AnimationDirection.FORWARD);
                if (ship1Animation.lastFrame) cutsceneManagement.CurrentScene = 3;
            }
            else
            {
                ship1Animation.Play(gameTime, "idle", UmbrellaToolsKit.Sprite.AsepriteAnimation.AnimationDirection.FORWARD);
            }

            for (int i = 0; i < ships2Positions.Length; i++)
                ships2Positions[i] = ships2InitialPositions[i] + (float)Math.Cos(timer * speed) * 5f * Vector2.UnitY;
            for (int i = 0; i < ships3Positions.Length; i++)
                ships3Positions[i] = ships3InitialPositions[i] - (float)Math.Cos(timer * speed) * 4f * Vector2.UnitY;

            ship1Position = ship1InitialPosition + (float)Math.Cos(timer * speed) * 3f * Vector2.UnitY;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (cutsceneManagement.CurrentScene != 2) return;
            base.Draw(spriteBatch);
        }
    }
}
