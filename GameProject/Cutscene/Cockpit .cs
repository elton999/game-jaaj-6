using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using UmbrellaToolsKit;

namespace game_jaaj_6.Cutscene
{
    public abstract class Cockpit : GameObject
    {
        public CutsceneManagement cutsceneManagement;
        Texture2D SpriteHands;
        Texture2D SpritePilot;

        public Vector2 HandsPosition;
        public Vector2 PilotPosition;

        public Rectangle BoxPilot;
        public Rectangle BoxHands;

        public Point[] PilotSpriteLocations = new Point[] { new Point(0), new Point(65, 0) };
        public Point[] HandsSpriteLocations = new Point[] { new Point(0), new Point(159, 0) };

        public override void Start()
        {
            base.Start();
            SetSpriteSettings();
            SetInitialPosition();
            Scene.Middleground.Add(this);
        }

        private void SetSpriteSettings()
        {
            SpriteHands = Scene.Content.Load<Texture2D>("Sprites/cutscenes/cutscene1_2");
            SpritePilot = Scene.Content.Load<Texture2D>("Sprites/cutscenes/cutscene1");

            BoxHands = new Rectangle(HandsSpriteLocations[0], new Point(159, 108));
            BoxPilot = new Rectangle(PilotSpriteLocations[0], new Point(65, 120));
        }

        private void SetInitialPosition()
        {
            HandsPosition = Scene.Sizes.ToVector2() / 2f - BoxHands.Size.ToVector2() / 2f;
            PilotPosition = Scene.Sizes.ToVector2() / 2f - BoxPilot.Size.ToVector2() / 2f;
            HandsPosition -= Scene.Camera.Origin - Vector2.UnitY * 50f;
            PilotPosition -= Scene.Camera.Origin + Vector2.UnitY * 20f;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch);
            spriteBatch.Draw(SpritePilot, PilotPosition, BoxPilot, SpriteColor);
            spriteBatch.Draw(SpriteHands, HandsPosition, BoxHands, SpriteColor);
            EndDraw(spriteBatch);
        }
    }
}
