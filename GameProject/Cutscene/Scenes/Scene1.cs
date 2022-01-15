using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using Microsoft.Xna.Framework.Input;

namespace game_jaaj_6.Cutscene.Scenes
{
    public class Scene1 : Space
    {
        public override void Start()
        {
            base.Start();
            ship1Position -= Vector2.UnitY * 50f;
            setShip2PositionIncrement(50f);
            setShip3PositionIncrement(50f);
        }

        float speedShip1 = 10f / 1000f;
        float speedShip2 = 10f / 1000f;
        float speedShip3 = 5f / 1000f;
        bool canstart = false;
        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter)) canstart = true;
            if (cutsceneManagement.CurrentScene != 0 || !canstart) return;
            base.Update(gameTime);
            float delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            ship1Animation.Play(gameTime, "idle", AsepriteAnimation.AnimationDirection.FORWARD);

            ship1Position += Vector2.UnitY * speedShip1 * delta;
            setShip2PositionIncrement(-(speedShip2 * delta));
            setShip3PositionIncrement(-(speedShip3 * delta));

            if (ship1Position.Y > - 20f) cutsceneManagement.CurrentScene = 1;
        } 

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (cutsceneManagement.CurrentScene != 0) return;
            base.Draw(spriteBatch);
        }
    }
}
