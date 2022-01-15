using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UmbrellaToolsKit;

namespace game_jaaj_6.Cutscene.Scenes
{
    public class Scene4 : Cockpit
    {
        public override void Start()
        {
            base.Start();
            PilotPosition += Vector2.UnitY * 40f;
            HandsPosition -= Vector2.UnitY * 40f;
            PilotCurrentPosition = PilotPosition;
            HandsCurrentPosition = HandsPosition;
        }

        Vector2 PilotCurrentPosition;
        Vector2 HandsCurrentPosition;

        float speed = 8f / 1000f;
        bool red = false;
        float timer = 0;
        public override void Update(GameTime gameTime)
        {
            if (cutsceneManagement.CurrentScene != 3) return;
            base.Update(gameTime);

            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            PilotCurrentPosition -= Vector2.UnitY * delta * speed;
            HandsCurrentPosition += Vector2.UnitY * delta * speed;

            Random rnd = new Random();
            HandsPosition = HandsCurrentPosition + new Vector2(rnd.Next(-2, 2), rnd.Next(-2, 2));
            PilotPosition = PilotCurrentPosition + new Vector2(rnd.Next(-2, 2), rnd.Next(-2, 2));

            timer += delta;
            if (timer >= 3f * 60f) {
                red = !red;
                timer = 0;
                cutsceneManagement.Background.ToggleColor();
            }

            if (red)
            {
                BoxHands = new Rectangle(HandsSpriteLocations[1], new Point(159, 108));
                BoxPilot = new Rectangle(PilotSpriteLocations[1], new Point(65, 120));
                
            } else
            {
                BoxHands = new Rectangle(HandsSpriteLocations[0], new Point(159, 108));
                BoxPilot = new Rectangle(PilotSpriteLocations[0], new Point(65, 120));
            }

            if (PilotCurrentPosition.Y < -70f) cutsceneManagement.CurrentScene = 4;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (cutsceneManagement.CurrentScene != 3) return;
            base.Draw(spriteBatch);
        }
    }
}
