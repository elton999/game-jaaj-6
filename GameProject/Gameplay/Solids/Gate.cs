using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using UmbrellaToolsKit.Collision;
using UmbrellaToolsKit.Sprite;

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
            OpenDoorSound = Content.Load<SoundEffect>("Sound/door");
        }

        SoundEffect OpenDoorSound;

        private bool _open = false;
        private bool PlaySound = false;
        public override void UpdateData(GameTime gameTime)
        {
            if (_open)
            {
                if (!PlaySound)
                {
                    OpenDoorSound.Play();
                    PlaySound = true;
                }
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
                    this.Scene.GameManagement.Values["freeze"] = true;
                }

                this.Position = currentPosition;

                this.Anim.Play(gameTime, "idle", AsepriteAnimation.AnimationDirection.LOOP);
            }
        }

        public override void Destroy()
        {
            this.Scene.GameManagement.Values["freeze"] = false;
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
