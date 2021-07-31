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

        private bool _open = false;
        public override void UpdateData(GameTime gameTime)
        {
            if (_open)
            {
                this.Anim.Play(gameTime, "open", AsepriteAnimation.AnimationDirection.FORWARD);
                if (this.Anim.lastFrame)
                    this.Destroy();
            }
            else
            {
                var currentPosition = this.Position;
                this.Position.X -= 5;

                var player = this.Scene.AllActors[0];
                if (overlapCheck(player) && this.Scene.GameManagement.Values["key"])
                {
                    _open = true;
                    this.Scene.GameManagement.CurrentStatus = UmbrellaToolKit.GameManagement.Status.PAUSE;
                }

                this.Position = currentPosition;

                this.Anim.Play(gameTime, "idle", AsepriteAnimation.AnimationDirection.LOOP);
            }

            base.UpdateData(gameTime);
        }

        public override void Destroy()
        {
            this.Scene.GameManagement.CurrentStatus = UmbrellaToolKit.GameManagement.Status.PLAYING;
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
