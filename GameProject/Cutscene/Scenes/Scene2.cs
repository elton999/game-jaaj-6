using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UmbrellaToolsKit;

namespace game_jaaj_6.Cutscene.Scenes
{
    public class Scene2 : Cockpit
    {
        float speed = 8f / 1000f;
        public override void Update(GameTime gameTime)
        {
            if (cutsceneManagement.CurrentScene != 1) return;
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            HandsPosition -= Vector2.UnitY * delta * speed;
            PilotPosition += Vector2.UnitY * delta * speed;
            if (PilotPosition.Y > -60f) cutsceneManagement.CurrentScene = 2;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (cutsceneManagement.CurrentScene != 1) return;
            base.Draw(spriteBatch);
        }
    }
}
