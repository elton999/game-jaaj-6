using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolsKit;
using UmbrellaToolsKit.Sprite;
using System.Collections.Generic;

namespace game_jaaj_6.UI
{
    public class Credits : GameObject
    {

        public class CreditsText
        {
            public SpriteFont SpriteFont;
            public string Text;
            public Vector2 Size;
            public int IncrementY = 0;

            public List<CreditsText> CreditList = new List<CreditsText>();

            public void Add(SpriteFont spriteFont, string text, int incrementY = 0)
            {
                var credit = new CreditsText();
                credit.Text = text;
                credit.SpriteFont = spriteFont;
                credit.Size = spriteFont.MeasureString(text);
                credit.IncrementY = incrementY;

                CreditList.Add(credit);
            }
        }

        SpriteFont BigFont;
        SpriteFont SmallFont;
        Square Background;
        private CreditsText _creditsList;

        public override void Start()
        {
            this.tag = "Display Level";
            base.Start();
            this.BigFont = this.Scene.Content.Load<SpriteFont>("Kenney_Rocket_Big");
            this.SmallFont = this.Scene.Content.Load<SpriteFont>("Kenney_Rocket");


            this.Position = new Vector2(-100, this.Scene.Sizes.Y / 2f);
            this.InitialPosition = this.Position;

            this.CreateBackground();

            this.SetAllCredits();
        }

        private void SetAllCredits()
        {
            _creditsList = new CreditsText();
            _creditsList.Add(BigFont, "Thanks For Playing");
            _creditsList.Add(SmallFont, "A GAME BY", 10);
            _creditsList.Add(SmallFont, "Elton Silva", 10);
            _creditsList.Add(SmallFont, "A game made for GAMEJAAJ 6", 20);
            _creditsList.Add(SmallFont, "SPECIAL THANKS", 30);
            _creditsList.Add(SmallFont, "Mom and Dad", 30);
            _creditsList.Add(SmallFont, "Josue Cortes", 30);
            _creditsList.Add(SmallFont, "Gustavo Albuquerque", 30);
        }

        public void CreateBackground()
        {
            this.Background = new Square();
            this.Background.Scene = this.Scene;
            this.Background.size = this.Scene.Sizes;
            this.Background.SquareColor = Color.Black;
            this.Background.Start();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            BeginDraw(spriteBatch, false);
            this.Background.DrawSprite(spriteBatch);
            for(int i = 0; i < this._creditsList.CreditList.Count; i++)
            {
                var credit = this._creditsList.CreditList[i];
                spriteBatch.DrawString(
                    credit.SpriteFont, credit.Text, new Vector2(426f / 2f, 10f * i + 70 + credit.IncrementY),
                    Color.White, 
                    0, 
                    Vector2.Divide(credit.Size, 2f).ToPoint().ToVector2(), 1.0f, 
                    SpriteEffects.None, 0
                    );
            }
            EndDraw(spriteBatch);
        }

    }
}
