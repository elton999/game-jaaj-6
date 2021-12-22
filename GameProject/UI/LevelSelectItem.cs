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
            InputHelper = new Input();
            InputHelper.ResetStatus();
        }

        Input InputHelper;
        public override void Update(GameTime gameTime)
        {
            if (!_isOnLevelSelect) return;

            if (InputHelper.KeyPress(Input.Button.RIGHT) && LevelSelected < UnluckLevels)
                LevelSelected++;
            else if (InputHelper.KeyPress(Input.Button.LEFT) && LevelSelected > 0)
                LevelSelected--;
            else if (InputHelper.KeyPress(Input.Button.ESC))
            {
                Scene.GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.MENU;
                InputHelper.ResetStatus();
            }
            else if (InputHelper.KeyPress(Input.Button.CONFIRM))
            {
                Scene.GameManagement.restart();
                Scene.GameManagement.CurrentStatus = UmbrellaToolsKit.GameManagement.Status.LOADING;
            }
        }

        private List<Vector2> MenuMapPositions;
        private Rectangle bodySelected;
        private Rectangle bodylucked;
        private Rectangle bodyUnlucked;

        public int LevelSelected = 0;
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
