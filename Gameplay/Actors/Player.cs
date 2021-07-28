using System;
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
        # region start
        private Square _box;
        public override void Start()
        {
            base.Start();
            this.tag = "Player";
            this.Scene.AllActors.Add(this);
            this.size = new Point(32, 32);

            this.CreateBox();
            this.CreateCirclePath();
        }

        private void CreateCirclePath()
        {
            _circlePath = new CirclePath();
            _circlePath.Scene = this.Scene;
            this.Scene.Middleground.Add(_circlePath);
            _circlePath.Start();
        }

        private void CreateBox()
        {
            _box = new Square();
            _box.Scene = this.Scene;
            _box.size = new Point(32, 32);
            _box.SquareColor = Color.Red;
            _box.Position = this.Position;
            _box.Start();
        }
        #endregion

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

        #region input
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

            if ((cRight || cLeft) && !isGrounded && this._circlePath.isDistanceEnough)
                isMoving = true;
        }
        #endregion

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
                this.cJump = false;
                timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                var correctPosition = new Vector2(this.Position.X - this.LastPostionOnGround.X, this.Position.Y - this.LastPostionOnGround.Y);
                float angle = MathF.Atan2(correctPosition.Y - 16, correctPosition.X - 16) / 180 * MathF.PI;

                float newPositionX = 0;
                float newPositionY = 0;

                if (this.GroundCheck.Y == 1)
                {
                    newPositionX = this.LastPostionOnGround.X - _getSinPosition * -this._moveDirection.X * _distance;
                    newPositionY = this.LastPostionOnGround.Y - _getCosPosition * _distance;
                }

                if (this.GroundCheck.Y == -1)
                {
                    newPositionX = this.LastPostionOnGround.X + _getSinPosition * this._moveDirection.X * _distance;
                    newPositionY = this.LastPostionOnGround.Y + _getCosPosition * _distance;
                }

                if (this.GroundCheck.X == 1)
                {
                    newPositionX = this.LastPostionOnGround.X - _getCosPosition * _distance;
                    newPositionY = this.LastPostionOnGround.Y - _getSinPosition * this._moveDirection.X * _distance;
                }

                if (this.GroundCheck.X == -1)
                {
                    newPositionX = this.LastPostionOnGround.X + _getCosPosition * _distance;
                    newPositionY = this.LastPostionOnGround.Y + _getSinPosition * this._moveDirection.X * _distance;
                }

                moveY(newPositionY - this.Position.Y, (_) =>
                {
                    RestartGravity();
                    CheckWalls();
                });
                if (isMoving)
                {
                    moveX(newPositionX - this.Position.X, (_) =>
                    {
                        RestartGravity();
                        CheckWalls();
                    });
                }

                this._box.Rotation = angle * 360 / (MathF.PI * 2);
                this._box.Origin = new Vector2(16, 16);

                if (this.GroundCheck.Y != 0)
                {
                    float processedDirection = this._moveDirection.X * -this.GroundCheck.Y;
                    this._box.Position.X = this.LastPostionOnGround.X - _getSinPosition * processedDirection * _distanceSprite * this.GroundCheck.Y;
                    this._box.Position.Y = this.LastPostionOnGround.Y - _getCosPosition * _distanceSprite * this.GroundCheck.Y;
                }

                if (this.GroundCheck.X != 0)
                {
                    this._box.Position.X = this.LastPostionOnGround.X - _getCosPosition * _distanceSprite * this.GroundCheck.X;
                    this._box.Position.Y = this.LastPostionOnGround.Y - _getSinPosition * this._moveDirection.X * _distanceSprite * this.GroundCheck.X;
                }

            }

            if (!isMoving)
            {
                this._box.Rotation = 0;
                this._box.Origin = new Vector2(0, 0);
                this._box.Position = this.Position;
                this.isGrounded = this.CheckGrounded(this.GroundCheck);
                base.UpdateData(gameTime);
            }
            this.gravity2D = Vector2.Multiply(this.GroundCheck, this.GravityForce);
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

        private float _speed = 80f;
        private float _getCosPosition
        {
            get => MathF.Cos(timer * 0.0001f * _speed);
        }

        private float _getSinPosition
        {
            get => MathF.Sin(timer * 0.0001f * _speed);
        }

        #region jump
        private int JumpPressedForce = 0;
        private bool JumpPressed = false;
        private bool JumpPressedBtn = false;
        private float JumpForce = 1.0f;
        private bool isGrounded = false;
        private bool cJump = false;
        private Vector2 _Gravity2dTemp;

        public void Jump()
        {
            this.velocityDecrecentY = 0;
            if (this.isGrounded && this.cJump)
            {
                this.JumpPressedBtn = true;
                this.JumpPressed = true;
                this._Gravity2dTemp = this.GroundCheck;
            }

            this.JumpPressed = !this.cJump ? false : this.JumpPressed;


            if (this.JumpPressedBtn && this.JumpPressed && this.JumpPressedForce < 1)
            {
                this.gravityScale = 0.0001f;
                float g = (2 * this.JumpForce) / (MathF.Pow(5f, 2f));
                float initJumpVelocity = MathF.Sqrt(8f * g * this.JumpForce);
                this.velocity = Vector2.Add(velocity, Vector2.Multiply(this.GroundCheck, -initJumpVelocity));
                this.JumpPressedForce += 1;
            }

            if (this.isGrounded && !this.JumpPressed && !this.isMoving)
            {
                this.JumpPressed = false;
                this.JumpPressedForce = 0;
                this.gravityScale = 1f;
            }
        }
        #endregion

        #region ground check
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

        private Vector2 GroundCheck = new Vector2(0, 1);
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
        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {

            this._box.Scene = this.Scene;
            this._box.Draw(spriteBatch);
        }
    }
}