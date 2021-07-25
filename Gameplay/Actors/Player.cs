using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using UmbrellaToolKit;
using UmbrellaToolKit.Collision;
using UmbrellaToolKit.Sprite;

namespace game_jaaj_6.Gameplay.Actors
{
    public class Player : Actor
    {
        private Square _box;
        public override void Start()
        {
            base.Start();
            this.tag = "Player";
            this.Scene.AllActors.Add(this);
            this.size = new Point(32, 32);

            this._box = new Square();
            this._box.Scene = this.Scene;
            this._box.size = new Point(32, 32);
            this._box.SquareColor = Color.Red;
            this._box.Position = this.Position;
            this._box.Start();

            circlePath = new CirclePath();
            circlePath.Scene = this.Scene;
            this.Scene.Middleground.Add(circlePath);
            circlePath.Start();
        }

        CirclePath circlePath;
        public override void Update(GameTime gameTime)
        {
            this.Scene.Camera.Target = new Vector2(this.Position.X + this.size.X / 2, this.Position.Y + this.size.Y / 2);
            if (isGrounded)
                this.LastPostionOnGround = this.Position;

            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                this.cJump = true;

            if (Keyboard.GetState().IsKeyUp(Keys.Z))
                this.cJump = false;

            this.Jump();

            this.SetPositionCirclePath();

            base.Update(gameTime);
        }

        private void SetPositionCirclePath()
        {
            this.circlePath.Position = Vector2.Add(this.LastPostionOnGround, new Vector2(-197, -197));
            this.circlePath.PostionOnGround = this.LastPostionOnGround;
        }

        public Vector2 LastPostionOnGround = Vector2.Zero;
        public override void UpdateData(GameTime gameTime)
        {
            this.CheckGrounded();
            base.UpdateData(gameTime);
        }

        private int JumpPressedForce = 0;
        private bool JumpPressed = false;
        private bool JumpPressedBtn = false;
        private float JumpForce = 500.0f;
        private bool isGrounded = false;
        private bool cJump = false;

        public void Jump()
        {
            if (this.isGrounded && this.cJump)
            {
                this.JumpPressedBtn = true;
                this.JumpPressed = true;
            }

            this.JumpPressed = !this.cJump ? false : this.JumpPressed;

            if (this.JumpPressedBtn && this.JumpPressed && this.JumpPressedForce < 17)
            {
                this.velocity = new Vector2(this.velocity.X, this.JumpForce);
                this.JumpPressedForce += 1;
            }

            if (this.isGrounded && !this.JumpPressed)
            {
                this.JumpPressed = false;
                this.JumpPressedForce = 0;
            }
        }

        public override bool isRiding(Solid solid)
        {
            if (solid.check(this.size, new Vector2(this.Position.X, this.Position.Y + 1)))
                return true;
            return false;
        }

        private void CheckGrounded()
        {
            this.isGrounded = false;
            foreach (Solid solid in this.Scene.AllSolids)
                if (solid.check(this.size, new Vector2(this.Position.X, this.Position.Y + 1)))
                    this.isGrounded = true;

            if (this.Scene.Grid.checkOverlap(this.size, new Vector2(this.Position.X, this.Position.Y + 1), this))
                this.isGrounded = true;

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            this._box.Position = this.Position;
            this._box.Scene = this.Scene;
            this._box.Draw(spriteBatch);
            //base.Draw(spriteBatch);
        }
    }
}