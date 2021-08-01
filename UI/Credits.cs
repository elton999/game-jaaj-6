using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using UmbrellaToolKit;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.UI
{
    public class Credits : GameObject
    {
        SpriteFont BigFont;
        SpriteFont SmallFont;
        Square Background;
        public override void Start()
        {
            this.tag = "Display Level";
            base.Start();
            this.BigFont = this.Scene.Content.Load<SpriteFont>("Kenney_Rocket_Big");
            this.SmallFont = this.Scene.Content.Load<SpriteFont>("Kenney_Rocket");

            this.Position = new Vector2(-100, this.Scene.Sizes.Y / 2f);
            this.InitialPosition = this.Position;

            this.CreateBackground();
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
            spriteBatch.DrawString(this.BigFont, "Thanks For Playing", new Vector2(85, 30), Color.White);
            spriteBatch.DrawString(this.SmallFont, "A GAME BY", new Vector2(170, 70), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(this.SmallFont, "Elton Silva", new Vector2(175, 90), Color.White);

            spriteBatch.DrawString(this.SmallFont, "A game made for GAMEJAAJ 6", new Vector2(125, 120), Color.White);

            spriteBatch.DrawString(this.SmallFont, "SPECIAL THANKS", new Vector2(140, 150), Color.White, 0, new Vector2(0, 0), 1.5f, SpriteEffects.None, 0);
            spriteBatch.DrawString(this.SmallFont, "Mom and Dad", new Vector2(180, 170), Color.White);
            spriteBatch.DrawString(this.SmallFont, "Josue Cortes", new Vector2(170, 180), Color.White);
            spriteBatch.DrawString(this.SmallFont, "Gustavo Albuquerque", new Vector2(142, 190), Color.White);
            EndDraw(spriteBatch);
        }

    }
}
