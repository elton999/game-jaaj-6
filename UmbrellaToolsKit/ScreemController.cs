using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UmbrellaToolsKit
{
    public class ScreemController
    {
        public GraphicsDeviceManager graphics;

        public ScreemController(GraphicsDeviceManager graphics, int resolution = 0)
        {
            this.graphics = graphics;
            this.SetResolutions();
            this.Resolution = resolution;
            Update();
        }

        public List<Vector2> Resolutions = new List<Vector2>();
        public int Resolution { get; set; }
        private void SetResolutions()
        {
            this.Resolutions.Add(new Vector2(384, 216));
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

        public void Update()
        {
            graphics.PreferredBackBufferHeight = (int)this.Resolutions[this.Resolution].Y;
            graphics.PreferredBackBufferWidth = (int)this.Resolutions[this.Resolution].X;
            graphics.ApplyChanges();
        }
    }
}
