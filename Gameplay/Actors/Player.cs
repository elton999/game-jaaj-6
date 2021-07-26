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

            _circlePath = new CirclePath();
            _circlePath.Scene = this.Scene;
            this.Scene.Middleground.Add(_circlePath);
            _circlePath.Start();
        }

        private CirclePath _circlePath;
        private bool isMoving = false;
        public override void Update(GameTime gameTime)
        {
            this.Scene.Camera.Target = new Vector2(this.Position.X + this.size.X / 2, this.Position.Y + this.size.Y / 2);
            if (isGrounded)
                this.LastPostionOnGround = Vector2.Subtract(this.Position, Vector2.Multiply(GroundCheck, -16f));

            this.Jump();

            this.Input();

            this.SetPositionCirclePath();
        }

        private bool cRight = false;
        private bool cLeft = false;
        private void Input()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                this.cJump = true;

            if (Keyboard.GetState().IsKeyUp(Keys.Z))
                this.cJump = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this._moveDirection = new Vector2(1, 0);
                this.cRight = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Right))
                this.cRight = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this._moveDirection = new Vector2(-1, 0);
                this.cLeft = true;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Left))
                this.cLeft = false;

            if ((cRight || cLeft) && !isGrounded)
            {
                isMoving = true;
            }
        }

        private void SetPositionCirclePath()
        {
            this._circlePath.Position = Vector2.Add(this.LastPostionOnGround, new Vector2(-197, -197));
            this._circlePath.PostionOnGround = this.LastPostionOnGround;
        }

        public Vector2 LastPostionOnGround = Vector2.Zero;
        float timer = 0;
        private Vector2 _moveDirection = new Vector2(1, 0);
        public override void UpdateData(GameTime gameTime)
        {
            if (isMoving)
            {
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                var correctPosition = new Vector2(this.Position.X - this.LastPostionOnGround.X, this.Position.Y - this.LastPostionOnGround.Y);
                float angle = System.MathF.Atan2(correctPosition.Y - 16, correctPosition.X - 16) / 180 * System.MathF.PI;

                float newPositionY = this.LastPostionOnGround.Y + _getCosPosition * _distance;
                float newPositionX = this.LastPostionOnGround.X + _getSinPosition * this._moveDirection.X * _distance;
                this._box.Rotation = angle * 360 / (System.MathF.PI * 2);

                moveY(newPositionY - this.Position.Y, (_) =>
                {
                    RestartGravity();
                    CheckWalls();
                });
                moveX(newPositionX - this.Position.X, (_) =>
                {
                    RestartGravity();
                    CheckWalls();
                });

                this._box.Origin = new Vector2(16, 16);
                this._box.Position.X = (this.LastPostionOnGround.X + _getSinPosition * this._moveDirection.X * _distanceSprite);
                this._box.Position.Y = (this.LastPostionOnGround.Y + _getCosPosition * _distanceSprite);
            }

            if (!isMoving)
            {
                this.gravity2D = Vector2.Multiply(this.GroundCheck, this.GravityForce);
                this._box.Rotation = 0;
                this._box.Origin = new Vector2(0, 0);
                this._box.Position = this.Position;
                this.isGrounded = this.CheckGrounded(this.GroundCheck);
                base.UpdateData(gameTime);
            }
        }

        private void RestartGravity()
        {
            isMoving = false;
            this._box.Origin = new Vector2(0, 0);
            this._box.Rotation = 0;
            this._box.Position = this.Position;
            timer = 0;
        }

        private float _distance
        {
            get => Vector2.Distance(this.Position, this.LastPostionOnGround);
        }

        private float _distanceSprite
        {
            get => Vector2.Distance(Vector2.Add(this.LastPostionOnGround, new Vector2(-16, -16)), this.Position);
        }

        private float _speed = 150f;
        private float _getCosPosition
        {
            get => System.MathF.Cos(timer * 0.0001f * _speed);
        }

        private float _getSinPosition
        {
            get => System.MathF.Sin(timer * 0.0001f * _speed);
        }

        private int JumpPressedForce = 0;
        private bool JumpPressed = false;
        private bool JumpPressedBtn = false;
        private float JumpForce = 150.0f;
        private bool isGrounded = false;
        private bool cJump = false;

        public void Jump()
        {
            this.velocityDecrecentY = 0;
            if (this.isGrounded && this.cJump)
            {
                this.JumpPressedBtn = true;
                this.JumpPressed = true;
            }

            this.JumpPressed = !this.cJump ? false : this.JumpPressed;


            if (this.JumpPressedBtn && this.JumpPressed && this.JumpPressedForce < 30)
            {
                float g = (2 * this.JumpForce) / (System.MathF.Pow(0.5f, 2f));
                float initJumpVelocity = System.MathF.Sqrt(2f * g * this.JumpForce);
                this.velocity = Vector2.Add(velocity, Vector2.Multiply(this.GroundCheck, -initJumpVelocity));
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
            if (solid.check(this.size, Vector2.Add(this.Position, GroundCheck)))
                return true;
            return false;
        }

        private bool CheckGrounded(Vector2 groundCheck)
        {
            foreach (Solid solid in this.Scene.AllSolids)
                if (solid.check(this.size, Vector2.Add(this.Position, groundCheck)))
                    return true;

            if (this.Scene.Grid.checkOverlap(this.size, Vector2.Add(this.Position, groundCheck), this))
                return true;
            return false;
        }

        private Vector2 GroundCheck = new Vector2(0, -1);
        private float GravityForce = 8f;
        private void CheckWalls()
        {
            var directionWall = new Vector2(0, 1);
            if (CheckGrounded(directionWall))
                GroundCheck = directionWall;

            directionWall = new Vector2(0, -1);
            if (CheckGrounded(directionWall))
                GroundCheck = directionWall;

            directionWall = new Vector2(1, 0);
            if (CheckGrounded(directionWall))
                GroundCheck = directionWall;

            directionWall = new Vector2(-1, 0);
            if (CheckGrounded(directionWall))
                GroundCheck = directionWall;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            this._box.Scene = this.Scene;
            this._box.Draw(spriteBatch);
            //base.Draw(spriteBatch);
        }
    }
}