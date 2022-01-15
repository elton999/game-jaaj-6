using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_jaaj_6.Cutscene
{
    public abstract class Space : GameObject
    {
        public CutsceneManagement cutsceneManagement;
        Texture2D background;
        public AsepriteAnimation ship1Animation;
        AsepriteAnimation ship2Animation;
        AsepriteAnimation ship3Animation;

        public Vector2 ship1Position;
        public Vector2[] ships2Positions;
        public Vector2[] ships3Positions;

        public override void Start()
        {
            string path = "Sprites/cutscenes/";
            background = Scene.Content.Load<Texture2D>(path + "cutscene2");
            Sprite = Scene.Content.Load<Texture2D>(path + "cutscene2_1");
            SetAnimationsSettings(path);
            setAllPositions();
            Scene.Middleground.Add(this);
        }

        public Vector2 screemCenter { get => Scene.Sizes.ToVector2() / 2f - Scene.Camera.Origin; }
        private void setAllPositions()
        {
            Vector2 origin = Vector2.One * 55f / 2f;
            ships2Positions = new Vector2[] { 
                screemCenter + new Vector2(40f, -10f) - origin, 
                screemCenter - new Vector2(40f, 10f) - origin 
            };

            ships3Positions = new Vector2[] {
                screemCenter + new Vector2(80f, 10f) - origin,
                screemCenter + new Vector2(0, 10f)  - origin,
                screemCenter - new Vector2(80f, -10f)  - origin
            };

            ship1Position = screemCenter - origin + Vector2.UnitY * 30f;
        }

        private void SetAnimationsSettings(string path)
        {
            var animationSettings = Scene.Content.Load<AsepriteDefinitions>(path + "cutscene2_1_animation");
            ship1Animation = new AsepriteAnimation(animationSettings);
            ship2Animation = new AsepriteAnimation(animationSettings);
            ship3Animation = new AsepriteAnimation(animationSettings);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            ship2Animation.Play(gameTime, "ship1", AsepriteAnimation.AnimationDirection.FORWARD);
            ship3Animation.Play(gameTime, "ship2", AsepriteAnimation.AnimationDirection.FORWARD);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            
            spriteBatch.Draw(background, screemCenter - (background.Bounds.Size.ToVector2() / 2f).ToPoint().ToVector2(), SpriteColor);

            foreach (Vector2 shipPosition in ships3Positions)
            {
                Position = shipPosition;
                Body = ship3Animation.Body;
                DrawSprite(spriteBatch);
            }

            foreach (Vector2 shipPosition in ships2Positions)
            {
                Position = shipPosition;
                Body = ship2Animation.Body;
                DrawSprite(spriteBatch);
            }

            Position = ship1Position;
            Body = ship1Animation.Body;
            DrawSprite(spriteBatch);

            EndDraw(spriteBatch);
        }
    }
}
