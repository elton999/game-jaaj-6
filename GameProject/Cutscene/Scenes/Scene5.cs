using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Cutscene.Scenes
{
    public class Scene5 : GameObject
    {
        public CutsceneManagement cutsceneManagement;
        AsepriteAnimation asepriteAnimation;

        public override void Start()
        {
            base.Start();
            Sprite = Scene.Content.Load<Texture2D>("Sprites/cutscenes/cutscene3");
            var animation = Scene.Content.Load<AsepriteDefinitions>("Sprites/cutscenes/cutscene3_animation");
            asepriteAnimation = new AsepriteAnimation(animation);

            Position = Scene.Sizes.ToVector2() / 2f -  new Vector2(213, 120) / 2f;
            Position -= Scene.Camera.Origin;
            Scene.Middleground.Add(this);
        }

        float timer = 0;
        public override void Update(GameTime gameTime)
        {
            if (cutsceneManagement.CurrentScene != 4) return; 
            base.Update(gameTime);
            asepriteAnimation.Play(gameTime, "explosion");

            if (!asepriteAnimation.lastFrame) return;
            timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            if (timer >= 10f * 60f)
                cutsceneManagement.CurrentScene = 5;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (cutsceneManagement.CurrentScene != 4) return;
            Body = asepriteAnimation.Body;
            base.Draw(spriteBatch);
        }
    }
}
