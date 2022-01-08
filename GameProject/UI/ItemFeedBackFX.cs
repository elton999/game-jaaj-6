using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;

namespace game_jaaj_6.UI
{
    public class ItemFeedBackFX : GameObject
    {
        public enum Type { RELIC, KEY}
        public Type CurrentType;
        public override void Start()
        {
            base.Start();
            Sprite = Scene.Content.Load<Texture2D>("Sprites/tilemap");
        }

        float speed = 5f;
        float speedTransparent = 20f / 10000f;
        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Position.Y -= speed * deltaTime;
            Transparent = MathHelper.Clamp(Transparent - speedTransparent, 0.0f, 1f);
            if (Transparent == 0) Destroy();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Point location = CurrentType == Type.KEY ? new Point(88, 16) : new Point(88,0);
            Body = new Rectangle(location, new Point(8, 16));
            base.Draw(spriteBatch);
        }
    }
}
