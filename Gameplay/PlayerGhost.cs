using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolKit;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public class PlayerGhost : GameObject
    {
        public List<Rectangle> Frames = new List<Rectangle>();
        public List<Vector2> Positions = new List<Vector2>();
        public List<float> Rotations = new List<float>();
        public List<Vector2> Origins = new List<Vector2>();
        public List<SpriteEffects> SpriteEffects = new List<SpriteEffects>();
        public override void Start()
        {
            this.Effect = this.Scene.Content.Load<Effect>("Shaders/SpriteColor");
            base.Start();
        }

        public override void UpdateData(GameTime gameTime)
        {
            float timer = (float)gameTime.TotalGameTime.TotalMilliseconds;
            if (this.Frames.Count > 0 && timer % 4.0f > 2.5f)
            {
                this.Frames.Remove(this.Frames[0]);
                this.Positions.Remove(this.Positions[0]);
                this.Rotations.Remove(this.Rotations[0]);
                this.Origins.Remove(this.Origins[0]);
                this.SpriteEffects.Remove(this.SpriteEffects[0]);
            }

            base.UpdateData(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this.Effect.Parameters["ColorFill"].SetValue(Color.White.ToVector4());
            BeginDraw(spriteBatch);
            for (var i = 0; i < Frames.Count; i++)
            {
                this.Transparent = (float)i / (float)Frames.Count;
                this.Position = this.Positions[i];
                this.Body = this.Frames[i];
                this.Origin = this.Origins[i];
                this.Rotation = this.Rotations[i];
                this.spriteEffect = this.SpriteEffects[i];

                this.Effect.Parameters["transparent"].SetValue(this.Transparent);
                DrawSprite(spriteBatch);
            }
            EndDraw(spriteBatch);
        }
    }
}