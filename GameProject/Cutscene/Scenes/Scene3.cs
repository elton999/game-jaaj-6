using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_jaaj_6.Cutscene.Scenes
{
    public class Scene3 : Space
    {
        float timer = 0;
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
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (cutsceneManagement.CurrentScene != 2) return;
            base.Draw(spriteBatch);
        }
    }
}
