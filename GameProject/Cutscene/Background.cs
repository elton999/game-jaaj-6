using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;

namespace game_jaaj_6.Cutscene
{
    public class Background : Square
    {
        public override void Start()
        {
            size = new Point(213, 120);
            SquareColor = Color.White;
            base.Start();
            Scene.Backgrounds.Add(this);
            backgroundColor = colors[0];
        }

        Color[] colors = new Color[] { 
            new Color((new Vector3(32, 0, 178)) / 255f), 
            new Color((new Vector3(178, 16, 48)) / 255f) 
        };
        Color backgroundColor;
        Vector2 backgroundPosition { get => size.ToVector2() / 2f - Scene.Sizes.ToVector2() / 2f; }

        int currentColor = 0;
        public void ToggleColor()
        {
            currentColor = currentColor == 0 ? 1 : 0;
            backgroundColor = colors[currentColor];
        }

        public void SetColor(int color){
            backgroundColor = colors[color];
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            Position = backgroundPosition;
            SpriteColor = backgroundColor;
        }
    }
}
