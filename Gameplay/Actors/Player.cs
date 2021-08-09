using System;
using System.Threading;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
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
            this.InitialPosition = this.Position;
            this.tag = "Player";
            this.Scene.AllActors.Add(this);
            this.size = new Point(32, 32);

            this.Scene.Camera.MoveSpeed = 2.5f;

            this.Sprite = this.Scene.Content.Load<Texture2D>("Sprites/player");
            this.Anima = new AsepriteAnimation(this.Content.Load<AsepriteDefinitions>("Sprites/player_animation"));

            this.CreateBox();
            this.CreateCirclePath();
            this.CreateGhost();
            this.CreateDeathFx();
            this.CreateSmokeFX();

            DieSound = Content.Load<SoundEffect>("Sound/explosionCrunch_002");
            HitTheGroundSound = Content.Load<SoundEffect>("Sound/sfx_thouch_ground");
            DashSound = Content.Load<SoundEffect>("Sound/sfx_wpn_sword1");

            this.Scene.Camera.Position = this.Position;
        }

        SoundEffect DieSound;
        SoundEffect HitTheGroundSound;
        SoundEffect DashSound;

        private PlayerSmoke PlayerSmoke;
        private void CreateSmokeFX()
        {
            this.PlayerSmoke = new PlayerSmoke();
            this.PlayerSmoke.Scene = this.Scene;
            this.PlayerSmoke.Start();
        }

        private PlayerGhost Ghost = new PlayerGhost();
        private void CreateGhost()
        {
            Ghost = new PlayerGhost();
            Ghost.Scene = this.Scene;
            Ghost.Sprite = this.Sprite;
            this.Scene.Players.Add(Ghost);
            Ghost.Start();
        }

        private PlayerDeath DeathFX;
        private void CreateDeathFx()
        {
            DeathFX = new PlayerDeath();
            DeathFX.Scene = this.Scene;
            DeathFX.Sprite = this.Sprite;
            DeathFX.Position = this.Position;
            this.Scene.Players.Add(DeathFX);
            DeathFX.Start();
        }

        public void Restart()
        {
            this.Position = this.InitialPosition;
            this.Scene.Camera.Target = this.Position;
            this.RestartGravity();
            this.GroundCheck = new Vector2(0, 1);
            isDead = false;
        }

        bool isDead = false;
        public void Die()
        {
            if (!isDead)
            {
                isDead = true;
                DieSound.Play();
                this.DeathFX.Play(this.Position);
                wait(1f, () =>
                {
                    this.Scene.GameManagement.CurrentGameplayStatus = UmbrellaToolKit.GameManagement.GameplayStatus.DEATH;
                });
                wait(2f, () => { this.Restart(); });

            }
        }

        private void CreateCirclePath()
        {
            _circlePath = new CirclePath();
            _circlePath.Scene = this.Scene;
            this.Scene.Middleground.Add(_circlePath);
            _circlePath.Start();
            _circlePath.Player = this;
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
        public bool isMoving = false;
        public bool isPaused { get => this.Scene.GameManagement.Values["freeze"] || this.Scene.GameManagement.CurrentStatus == UmbrellaToolKit.GameManagement.Status.PAUSE; }
        public override void Update(GameTime gameTime)
        {
            if (!isPaused)
            {
                if (isGrounded)
                    this.LastPostionOnGround = Vector2.Subtract(this.Position, Vector2.Multiply(GroundCheck, -16f));

                if (this.isMoving)
                    this.Scene.Camera.Target = new Vector2(this.Position.X + this.size.X / 2, this.Position.Y + this.size.Y / 2);
                else
                    this.Scene.Camera.Target = new Vector2(this.LastPostionOnGround.X + this.size.X / 2, this.LastPostionOnGround.Y + this.size.Y / 2);

                this.Jump();

                this.Input();

                this.SetPositionCirclePath();

                this.PlayerSmoke.AnimationUpdate(gameTime);

                this.Animation(gameTime);
            }
        }

        private void SetPositionCirclePath()
        {
            this._circlePath.Position = Vector2.Add(this.LastPostionOnGround, new Vector2(-197, -197));
            this._circlePath.PostionOnGround = this.LastPostionOnGround;
        }

        #region input
        private bool cRight = false;
        private bool cLeft = false;
        private void Input()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.R))
                this.Die();
            if (Keyboard.GetState().IsKeyDown(Keys.Z))
                this.cJump = true;

            if (Keyboard.GetState().IsKeyUp(Keys.Z))
                this.cJump = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                this._moveDirection = new Vector2(1, 0);
                this.cRight = true;
                this.spriteEffect = SpriteEffects.None;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Right))
                this.cRight = false;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                this._moveDirection = new Vector2(-1, 0);
                this.cLeft = true;
                this.spriteEffect = SpriteEffects.FlipHorizontally;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Left))
                this.cLeft = false;

            if ((cRight || cLeft) && !isGrounded && this._circlePath.isDistanceEnough && !isMoving)
            {
                DashSound.Play();
                isMoving = true;
            }
        }
        #endregion

        #region UpdateData
        public Vector2 LastPostionOnGround = Vector2.Zero;
        float timer = 0;
        private Vector2 _moveDirection = new Vector2(1, 0);
        public override void UpdateData(GameTime gameTime)
        {

            if (!isPaused)
            {
                if (isMoving)
                {
                    this.cJump = false;
                    timer += (float)gameTime.ElapsedGameTime.TotalMilliseconds;

                    var correctPosition = new Vector2(this.Position.X - this.LastPostionOnGround.X, this.Position.Y - this.LastPostionOnGround.Y);


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

                    moveY(newPositionY - this.Position.Y, SetNewGravity);
                    if (isMoving)
                        moveX(newPositionX - this.Position.X, SetNewGravity);

                    this.RotateSprite(correctPosition);



                }

                if (!isMoving)
                {
                    this.Rotation = 0.0f;
                    this.Origin = new Vector2(16, 16);
                    this._box.Position = this.Position;
                    this.isGrounded = this.CheckGrounded(this.GroundCheck);
                    base.UpdateData(gameTime);

                    if (_distance > 200 && !isDead && !isGrounded)
                        this.Die();
                }

                this.SmashEfx();

                this.gravity2D = Vector2.Multiply(this.GroundCheck, this.GravityForce);
                this.SliderUpdate();
            }

        }

        private void SliderUpdate()
        {
            if (isGrounded && _gravityDown && this.GroundCheck.X != 0)
            {
                this.CheckWalls();
                this.gravity2D = new Vector2(this.gravity2D.X, this.GravityForce * 0.1f);
            }

            if (!isGrounded && !_gravityDown && this.GroundCheck.X != 0 && !isMoving)
                this.velocity.Y = 0;
        }

        private void SetNewGravity(string tag)
        {
            RestartGravity();
            CheckWalls();
        }

        private void RotateSprite(Vector2 correctPosition)
        {
            float angle = MathF.Atan2(correctPosition.Y - 16, correctPosition.X - 16) / 360 * MathF.PI;
            this.Rotation = angle * 360 / (MathF.PI * 2);
            this.Origin = new Vector2(16, 16);
        }

        #endregion

        private string[] _dangers = new string[4] {
            "demage area",
            "fire ball",
            "soldier",
            "spikes"
        };
        public override void OnCollision(string tag)
        {
            if (_dangers.Contains(tag))
                this.Die();
            base.OnCollision(tag);
        }

        private void RestartGravity()
        {
            isMoving = false;
            _gravityDown = false;
            this.Origin = new Vector2(0, 0);
            this.Rotation = 0.0f;
            this._box.Position = this.Position;
            timer = 0;
        }

        private float _distance
        {
            get => Vector2.Distance(this.Position, this.LastPostionOnGround);
        }

        private float _distanceSprite
        {
            get => Vector2.Distance(Vector2.Add(this.LastPostionOnGround, new Vector2(-32, -32)), this.Position);
        }

        private float _speed = 60f;
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
                this.PlayerSmoke.StartAnimation();
                this.PlayerSmoke.Rotation = this.Rotation;
                this.JumpPressedBtn = true;
                this.JumpPressed = true;
                this._Gravity2dTemp = this.GroundCheck;
            }

            this.JumpPressed = !this.cJump ? false : this.JumpPressed;


            if (this.JumpPressedBtn && this.JumpPressed && this.JumpPressedForce < 1)
            {
                this.gravityScale = 0.00003f;

                float g = (2 * this.JumpForce) / (MathF.Pow(6f, 2f));
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
                {
                    if (solid.tag.Equals("slider"))
                        _gravityDown = true;
                    return true;
                }

            if (this.Scene.Grid.checkOverlap(this.size, Vector2.Add(this.Position, groundCheck), this))
                return true;
            return false;
        }

        private Vector2 GroundCheck = new Vector2(0, 1);
        private bool _gravityDown = false;
        private float GravityForce = 8f;
        private void CheckWalls()
        {
            _gravityDown = false;

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

        #region animation
        AsepriteAnimation Anima;
        public void Animation(GameTime gameTime)
        {
            if (this.isMoving)
                this.Anima.Play(gameTime, "dash");
            else if (!this.isMoving & !this.isGrounded)
                this.Anima.Play(gameTime, "jump", AsepriteAnimation.AnimationDirection.LOOP);
            else if (_GroundHit)
                this.Anima.Play(gameTime, "landing", AsepriteAnimation.AnimationDirection.FORWARD);
            else
                this.Anima.Play(gameTime, "idle", AsepriteAnimation.AnimationDirection.LOOP);
        }

        #region smashEFX
        private Vector2 _PositionSmash;
        private Point _BobySmash;
        private bool _GroundHit = false;
        private bool _SmashEFX = false;
        private bool _last_isgrounded = false;
        private bool _isJumping { get => !this.isGrounded && !this.isMoving; }
        public void SmashEfx()
        {
            if (!_last_isgrounded && this.isGrounded && !_GroundHit)
            {
                _BobySmash.X = -15;
                _BobySmash.Y = 5;
                _PositionSmash.X = 3;
                _PositionSmash.Y = -2;
                _GroundHit = true;
                HitTheGroundSound.Play();
                wait(0.5f, () =>
                {
                    _BobySmash = new Point(0, 0);
                    _PositionSmash = new Vector2(0, 0);
                    _GroundHit = false;
                });
            }
            if (_isJumping && !_SmashEFX)
            {
                _SmashEFX = true;

                _BobySmash.X = 2;
                _BobySmash.Y = -2;
                _PositionSmash.X = -1;
                _PositionSmash.Y = 3;
                wait(0.4f, () =>
                {
                    _BobySmash = new Point(0, 0);
                    _PositionSmash = new Vector2(0, 0);
                    _SmashEFX = false;
                });
            }

            this._last_isgrounded = this.isGrounded;
        }
        #endregion
        #endregion

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (!isDead)
            {

                this._box.Scene = this.Scene;
                this.Body = this.Anima.Body;

                if (!isMoving)
                {
                    if (this.GroundCheck.X != 0)
                    {
                        this.Origin = this.GroundCheck.X == -1 ? new Vector2(16, 64 - 16) : this.Origin;
                        this.Origin = this.GroundCheck.X == 1 ? new Vector2(32, 16) : this.Origin;

                        this.Rotation = -1.55f * this.GroundCheck.X;
                    }

                    if (this.GroundCheck.Y == -1)
                    {
                        this.Origin = new Vector2(64 - 16, 64 - 16);
                        this.Rotation = -3.1f;
                    }
                }
                else if (!isPaused)
                {
                    this.Ghost.Frames.Add(this.Body);
                    this.Ghost.Positions.Add(this.Position);
                    this.Ghost.Rotations.Add(this.Rotation);
                    this.Ghost.Origins.Add(this.Origin);
                    this.Ghost.SpriteEffects.Add(this.spriteEffect);
                }

                if (!isGrounded)
                {
                    this.PlayerSmoke.Position = this.LastPostionOnGround;
                    this.PlayerSmoke.Origin = Vector2.Subtract(this.Origin, new Vector2(5f, -13f));
                    this.PlayerSmoke.Draw(spriteBatch);
                }

                BeginDraw(spriteBatch);
                spriteBatch.Draw(
                    this.Sprite,
                    new Rectangle(Vector2.Subtract(this.Position, _PositionSmash).ToPoint(),
                    new Point(this.Body.Width - _BobySmash.X, this.Body.Height - _BobySmash.Y)),
                    this.Body, this.SpriteColor * this.Transparent, this.Rotation, this.Origin, this.spriteEffect, 0);
                EndDraw(spriteBatch);
            }
        }
    }
}