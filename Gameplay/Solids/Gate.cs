using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolKit.Collision;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay.Solids
{
    public class Gate : Solid
    {
        public override void Start()
        {
            base.Start();
            this.Scene.AllSolids.Add(this);
            this.tag = "gate";
            this.size = new Point(16, 88);
            this.Sprite = this.Scene.Content.Load<Texture2D>("Sprites/gate");
            Anim = new AsepriteAnimation(this.Scene.Content.Load<AsepriteDefinitions>("Sprites/gate_animation"));
        }

        public override void UpdateData(GameTime gameTime)
        {
            this.Anim.Play(gameTime, "idle", AsepriteAnimation.AnimationDirection.LOOP);
            base.UpdateData(gameTime);
        }

        public override void Destroy()
        {
            base.Destroy();
            this.Scene.AllSolids.Remove(this);
        }

        AsepriteAnimation Anim;
        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Body = this.Anim.Body;
            this.Origin = new Vector2(9, 0);
            base.Draw(spriteBatch);
        }
    }
}
