using UmbrellaToolsKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace game_jaaj_6.Gameplay
{
    public class LightOfEnd : GameObject
    {
        private Point _lightOn = new Point(0, 56);
        private Point _lightOff = new Point(8, 56);

        public override void Start()
        {
            Sprite = Scene.Content.Load<Texture2D>("Sprites/tilemap");
            Body = new Rectangle(_lightOff, new Point(8, 8));
            base.Start();
        }

        public override void Update(GameTime gameTime)
        {
            if (Scene.GameManagement.CurrentStatus == GameManagement.Status.PAUSE)
                Body.Location = _lightOn;
            else
                Body.Location = _lightOff;
        }
    }
}
