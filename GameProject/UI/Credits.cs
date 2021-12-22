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
        private CreditsText _creditsList;

        public override void Start()
        {
            tag = "Display Level";
            base.Start();
            BigFont = Scene.Content.Load<SpriteFont>("Kenney_Rocket_Big");
            SmallFont = Scene.Content.Load<SpriteFont>("Kenney_Rocket");


            Position = new Vector2(-100, Scene.Sizes.Y / 2f);
            InitialPosition = this.Position;

            SetAllCredits();
            InputHelper = new Input();
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

        private bool _isOnCredits { get => Scene.GameManagement.CurrentStatus == UmbrellaToolsKit.GameManagement.Status.CREDITS; }

        Input InputHelper;
        public override void Update(GameTime gameTime)
        {
            
            if (!_isOnCredits) return;
            if (InputHelper.KeyDown(Input.Button.ESC))
                Scene.GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.MENU;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!_isOnCredits) return;

            Scene.ScreemGraphicsDevice.Clear(Color.Black);
            BeginDraw(spriteBatch, false);
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
