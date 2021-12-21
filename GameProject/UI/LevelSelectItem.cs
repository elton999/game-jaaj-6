using System.Collections.Generic;
using UmbrellaToolsKit;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game_jaaj_6.UI
{
    public class LevelSelectItem : GameObject
    {
        public override void Start()
        {
            base.Start();
            Sprite = Scene.Content.Load<Texture2D>("Sprites/tilemap");
            bodySelected = new Rectangle(new Point(48, 104), new Point(8,8));
            bodylucked = new Rectangle(new Point(56, 104), new Point(8, 8));
            bodyUnlucked = new Rectangle(new Point(48, 112), new Point(8,8));

            SetPositionsOfMapLevelSelect();
        }

        bool RightPressed = false;
        bool LeftPressed = false;
        bool CRight = false;
        bool CLeft = false;
        bool CComfirm = false;
        public override void Update(GameTime gameTime)
        {
            if (!_isOnLevelSelect) return;

            var keyboardState = Keyboard.GetState();

            CRight = false;
            if (keyboardState.IsKeyDown(Keys.Right))
            {
                if(!RightPressed) CRight = true;
                RightPressed = true;
            }
            else if (keyboardState.IsKeyUp(Keys.Right))
                RightPressed = false;

            CLeft = false;
            if (keyboardState.IsKeyDown(Keys.Left))
            {
                if (!LeftPressed) CLeft = true;
                LeftPressed = true;
            }
            else if (keyboardState.IsKeyUp(Keys.Left))
                LeftPressed = false;


            if (CRight && LevelSelected < UnluckLevels)
                LevelSelected += 1;
            if (CLeft && LevelSelected > 0)
                LevelSelected -= 1;

            if (keyboardState.IsKeyDown(Keys.Enter) && CComfirm)
                Scene.GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.PLAYING;
            CComfirm = keyboardState.IsKeyUp(Keys.Enter);

        }

        private List<Vector2> MenuMapPositions;
        private Rectangle bodySelected;
        private Rectangle bodylucked;
        private Rectangle bodyUnlucked;

        public int LevelSelected = 1;
        public int UnluckLevels = 4;
        
        private void SetPositionsOfMapLevelSelect()
        {
            MenuMapPositions = new List<Vector2>();
            MenuMapPositions.Add(new Vector2(13 * 8, 9 * 8));
            MenuMapPositions.Add(new Vector2(19 * 8, 4 * 8));
            MenuMapPositions.Add(new Vector2(24 * 8, 4 * 8));
            MenuMapPositions.Add(new Vector2(24 * 8, 13 * 8));
            MenuMapPositions.Add(new Vector2(24 * 8, 21 * 8));
            MenuMapPositions.Add(new Vector2(31 * 8, 21 * 8));
            MenuMapPositions.Add(new Vector2(37 * 8, 21 * 8));
            MenuMapPositions.Add(new Vector2(37 * 8, 16 * 8));
            MenuMapPositions.Add(new Vector2(37 * 8, 10 * 8));
            MenuMapPositions.Add(new Vector2(37 * 8, 4 * 8));
        }

        private bool _isOnLevelSelect
        {
            get => this.Scene.GameManagement.CurrentStatus == UmbrellaToolsKit.GameManagement.Status.LEVEL_SELECT;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!_isOnLevelSelect) return;
            BeginDraw(spriteBatch);
            for (int i = 0; i < MenuMapPositions.Count; i++)
            {
                Position = MenuMapPositions[i];
                Body = i <= UnluckLevels ? bodyUnlucked : bodylucked;
                Body = LevelSelected == i ? bodySelected : Body;
                DrawSprite(spriteBatch);
            }
            EndDraw(spriteBatch);
        }
    }
}
