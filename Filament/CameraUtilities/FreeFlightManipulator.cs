using System;
using System.Numerics;

namespace Filament.CameraUtilities
{
    public class FreeFlightManipulator : Manipulator
    {
        #region Members

        private Vector2 _grabWin;
        private Vector2 _targetEuler; // (pitch, yaw)
        private Vector2 _grabEuler; // (pitch, yaw)
        private bool[] _keyDown = new bool[6];
        private bool _grabbing;
        private float _scrollWheel;
        private float _scrollPositionNormalized;
        private float _moveSpeed = 1.0f;
        private Vector3 _eyeVelocity;

        #endregion

        #region Methods

        public FreeFlightManipulator(Mode mode, Config props) : base(mode, props)
        {
            Eye = Props.FlightStartPosition;
            var pitch = Props.FlightStartPitch;
            var yaw = Props.FlightStartYaw;

            _targetEuler = new Vector2(pitch, yaw);

            UpdateTarget(pitch, yaw);
        }


        public void UpdateTarget(float pitch, float yaw)
        {
            var mat = Matrix4x4.CreateFromQuaternion(
                Quaternion.CreateFromYawPitchRoll(yaw, pitch, 0)
            );

            Target = Eye + Vector3.Transform(-Vector3.UnitZ, mat);
        }

        #endregion

        #region Manipulator

        #region Properties

        public override Bookmark CurrentBookmark {
            get {
                Bookmark bookmark = new Bookmark();
                bookmark.Flight.Position = Eye;
                bookmark.Flight.Pitch = _targetEuler.X;
                bookmark.Flight.Yaw = _targetEuler.Y;
                return bookmark;
            }
        }

        public override Bookmark HomeBookmark {
            get {
                Bookmark bookmark = new Bookmark();
                bookmark.Flight.Position = Props.FlightStartPosition;
                bookmark.Flight.Pitch = Props.FlightStartPitch;
                bookmark.Flight.Yaw = Props.FlightStartYaw;

                return bookmark;
            }
        }

        #endregion

        #region Methods

        protected override void SetProperties(Config props)
        {
            var resolved = props;

            if (resolved.FlightPanSpeed == Vector2.Zero) {
                resolved.FlightPanSpeed = new Vector2(0.01f, 0.01f);
            }

            if (resolved.FlightMaxSpeed == 0.0f) {
                resolved.FlightMaxSpeed = 10.0f;
            }

            if (resolved.FlightSpeedSteps == 0) {
                resolved.FlightSpeedSteps = 80;
            }

            base.SetProperties(resolved);
        }

        public override void GrabBegin(int x, int y, bool strafe)
        {
            _grabWin = new Vector2(x, y);
            _grabbing = true;
            _grabEuler = _targetEuler;
        }

        public override void GrabUpdate(int x, int y)
        {
            if (!_grabbing) {
                return;
            }

            var del = _grabWin - new Vector2(x, y);

            var grabPitch = _grabEuler.X;
            var grabYaw = _grabEuler.Y;

            double EPSILON = 0.001;

            var panSpeed = Props.FlightPanSpeed;
            float minPitch = (float) (-MathF.PI*2 + EPSILON);
            float maxPitch = (float) (MathF.PI*2 - EPSILON);

            var pitch = Math.Clamp(grabPitch + del.Y * -panSpeed.Y, minPitch, maxPitch);
            var yaw = grabYaw + del.X * panSpeed.X % 2.0f * MathF.PI;

            UpdateTarget(pitch, yaw);
        }

        public override void GrabEnd()
        {
            _grabbing = false;
        }

        public override void KeyDown(Key key)
        {
            _keyDown[(int) key] = true;
        }

        public override void KeyUp(Key key)
        {
            _keyDown[(int) key] = false;
        }

        public override void Scroll(int x, int y, float scrollDelta)
        {
            var halfSpeedSteps = Props.FlightSpeedSteps / 2.0f;

            _scrollWheel = Math.Clamp(_scrollWheel + scrollDelta, -halfSpeedSteps, halfSpeedSteps);

            // normalize the scroll position from -1 to 1 and calculate the move speed, in world units per second.
            _scrollPositionNormalized = (_scrollWheel + halfSpeedSteps) / halfSpeedSteps - 1.0f;
            _moveSpeed = MathF.Pow(Props.FlightMaxSpeed, _scrollPositionNormalized);
        }

        public override void Update(float deltaTime)
        {
            var forceLocal = Vector3.Zero;

            if (_keyDown[(int) Key.Forward]) {
                forceLocal += new Vector3(0, 0, -1.0f);
            }

            if (_keyDown[(int) Key.Left]) {
                forceLocal += new Vector3(-1.0f, 0, 0);
            }

            if (_keyDown[(int) Key.Backward]) {
                forceLocal += new Vector3(0, 0, 1.0f);
            }

            if (_keyDown[(int) Key.Right]) {
                forceLocal += new Vector3(1.0f, 0, 0);
            }

            var orientation = Matrix4x4.CreateLookAt(Eye, Target, Props.UpVector);
            var forceWorld4 = Vector4.Transform(new Vector4(forceLocal, 0), orientation);
            Vector3 forceWorld = new Vector3(forceWorld4.X, forceLocal.Y, forceLocal.Z);

            if (_keyDown[(int) Key.Up]) {
                forceWorld += new Vector3(0, 1.0f, 0);
            }

            if (_keyDown[(int) Key.Down]) {
                forceWorld += new Vector3(0, -1.0f, 0);
            }

            forceWorld *= _moveSpeed;

            var dampingFactor = Props.FlightMoveDamping;

            if (dampingFactor == 0.0f) {
                // Without damping, we simply treat the force as our velocity.
                _eyeVelocity = forceWorld;
            } else {
                // The dampingFactor acts as "friction", which acts upon the camera in the direction
                // opposite its velocity.
                // Force is also multiplied by the dampingFactor, to "make up" for the friction.
                // This ensures that the max velocity still approaches mMoveSpeed;
                var velocityDelta = (forceWorld - _eyeVelocity) * dampingFactor;
                _eyeVelocity += velocityDelta * deltaTime;
            }

            var positionDelta = _eyeVelocity * deltaTime;

            Eye += positionDelta;
            Target += positionDelta;
        }

        public override void JumpToBookmark(Bookmark bookmark)
        {
            Eye = bookmark.Flight.Position;
            UpdateTarget(bookmark.Flight.Pitch, bookmark.Flight.Yaw);
        }

        #endregion

        #endregion
    }
}
