using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
namespace UmbrellaToolsKit
{
    public class ScreenController
    {
        private GraphicsDeviceManager graphics;
        public static ScreenController instance;

        public ScreenController(GraphicsDeviceManager graphics, int resolution = 0)
        {
            instance = this;
            this.graphics = graphics;
            this.SetResolutions();
            this.Resolution = resolution;
            Update();
        }

        public List<Vector2> Resolutions = new List<Vector2>();
        public int Resolution { get; set; }
        public bool ShowMouse = true;
        private void SetResolutions()
        {
            this.Resolutions.Add(new Vector2(426, 240));
            this.Resolutions.Add(new Vector2(768, 432));
            this.Resolutions.Add(new Vector2(1152, 648));
            this.Resolutions.Add(new Vector2(1176, 664));
            this.Resolutions.Add(new Vector2(1280, 720));
            this.Resolutions.Add(new Vector2(1360, 768));
            this.Resolutions.Add(new Vector2(1366, 768));
            this.Resolutions.Add(new Vector2(1600, 900));
            this.Resolutions.Add(new Vector2(1768, 992));
            this.Resolutions.Add(new Vector2(1920, 1080));
        }

        public Vector2 getCurrentResolutionSize { get => this.Resolutions[this.Resolution]; }
        public Vector2 getCenterScreem
        {
            get => new Vector2(this.Resolutions[this.Resolution].X / 2f, this.Resolutions[this.Resolution].Y / 2f);
        }

        public bool fullScreen = false;
        public void Update()
        {
            graphics.PreferredBackBufferHeight = (int)this.Resolutions[this.Resolution].Y;
            graphics.PreferredBackBufferWidth = (int)this.Resolutions[this.Resolution].X;
            if (fullScreen != graphics.IsFullScreen)
                graphics.ToggleFullScreen();
            graphics.ApplyChanges();
        }
    }
}
