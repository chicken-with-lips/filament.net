using OpenTK.Mathematics;

namespace Filament.CameraUtilities
{
    public enum FieldOfView
    {
        Vertical,
        Horizontal
    }

    public enum Mode
    {
        Orbit,
        Map,
        FreeFlight
    }

    public enum Key
    {
        Forward,
        Left,
        Backward,
        Right,
        Up,
        Down
    }

    public delegate bool RayCallback(Vector3 origin, Vector3 direction, out float time, object userData);

    public abstract class Manipulator
    {
        #region Properties

        /// <summary>
        /// Gets the immutable mode of the manipulator.
        /// </summary>
        public Mode Mode => _mode;

        public Config Props => _props;

        public Vector3 Eye {
            get { return _eye; }
            protected set { _eye = value; }
        }

        public Vector3 Target {
            get { return _target; }
            protected set { _target = value; }
        }

        /// <summary>
        /// Gets a handle that can be used to reset the manipulator back to its current position.
        /// </summary>
        public abstract Bookmark CurrentBookmark { get; }

        /// <summary>
        /// Gets a handle that can be used to reset the manipulator back to its home position.
        /// </summary>
        public abstract Bookmark HomeBookmark { get; }

        #endregion

        #region Members

        private readonly Mode _mode;
        private Config _props;
        private Vector3 _eye;
        private Vector3 _target;

        #endregion

        #region Methods

        public Manipulator(Mode mode, Config props)
        {
            _mode = mode;

            SetProperties(props);
        }


        /// <summary>
        /// Sets the viewport dimensions. The manipulator uses this to process grab events and raycasts.
        /// </summary>
        public void SetViewport(int width, int height)
        {
            _props.Viewport[0] = width;
            _props.Viewport[1] = height;

            SetProperties(_props);
        }

        /// <summary>
        /// Gets the current orthonormal basis; this is usually called once per frame.
        /// </summary>
        public void GetLookAt(out Vector3 eyePosition, out Vector3 targetPosition, out Vector3 up)
        {
            var gaze = Vector3.Normalize(_target - _eye);
            var right = Vector3.Cross(gaze, _props.UpVector);

            targetPosition = _target;
            eyePosition = _eye;
            up = Vector3.Cross(right, gaze);
        }

        /// <summary>
        /// Given a viewport coordinate, picks a point in the ground plane, or in the actual scene if the
        /// raycast callback was provided.
        /// </summary>
        public bool Raycast(int x, int y, out Vector3 result)
        {
            result = Vector3.Zero;

            GetRay(x, y, out var origin, out var direction);

            // choose either the user's callback function or the plane intersector.
            var callback = _props.RaycastCallback;
            var fallback = new RayCallback(RaycastPlane);
            var userdata = _props.RaycastUserdata;

            if (callback == null) {
                callback = fallback;
                userdata = _props;
            }

            // if the ray misses, then try the fallback function.
            float t;

            if (!callback(_eye, direction, out t, userdata)) {
                if (callback == fallback || !fallback(_eye, direction, out t, _props)) {
                    return false;
                }
            }

            result = _eye + direction * t;

            return true;
        }

        /// <summary>
        /// Given a viewport coordinate, computes a picking ray (origin + direction).
        /// </summary>
        public void GetRay(int x, int y, out Vector3 origin, out Vector3 direction)
        {
            var gaze = Vector3.Normalize(_target - _eye);
            var right = Vector3.Normalize(Vector3.Cross(gaze, _props.UpVector));
            var upward = Vector3.Cross(right, gaze);
            float width = _props.Viewport[0];
            float height = _props.Viewport[1];
            var fov = _props.FovDegrees * MathHelper.Pi / 180.0f;

            // remap the grid coordinate into [-1, +1] and shift it to the pixel center.
            float u = 2.0f * (0.5f + x) / width - 1.0f;
            float v = 2.0f * (0.5f + y) / height - 1.0f;

            // compute the tangent of the field-of-view angle as well as the aspect ratio.
            float tangent = (float) MathHelper.Tan(fov / 2.0);
            float aspect = width / height;

            // adjust the gaze so it goes through the pixel of interest rather than the grid center.
            direction = gaze;

            if (_props.FovDirection == FieldOfView.Vertical) {
                direction += right * tangent * u * aspect;
                direction += upward * tangent * v;
            } else {
                direction += right * tangent * u;
                direction += upward * tangent * v / aspect;
            }

            direction = Vector3.Normalize(direction);
            origin = _eye;
        }

        /// <summary>
        /// Starts a grabbing session (i.e. the user is dragging around in the viewport).
        /// </summary>
        /// <remarks>
        /// In MAP mode, this starts a panning session.
        /// In ORBIT mode, this starts either rotating or strafing.
        /// In FREE_FLIGHT mode, this starts a nodal panning session.
        /// </remarks>
        /// <param name="x">X-coordinate for point of interest in viewport space</param>
        /// <param name="y">Y-coordinate for point of interest in viewport space</param>
        /// <param name="strafe">ORBIT mode only; if true, starts a translation rather than a rotation</param>
        public abstract void GrabBegin(int x, int y, bool strafe);

        /// <summary>
        /// Updates a grabbing session.
        /// </summary>
        /// <remarks>
        /// This must be called at least once between grabBegin / grabEnd to dirty the camera.
        /// </remarks>
        public abstract void GrabUpdate(int x, int y);

        /// <summary>
        /// Ends a grabbing session.
        /// </summary>
        public abstract void GrabEnd();

        /// <summary>
        /// Signals that a key is now in the down state.
        /// </summary>
        /// <remarks>
        /// In FREE_FLIGHT mode, the camera is translated forward and backward and strafed left and right
        /// depending on the depressed keys. This allows WASD-style movement.
        /// </remarks>
        public virtual void KeyDown(Key key)
        {
        }

        /// <summary>
        /// Signals that a key is now in the up state.
        /// </summary>
        public virtual void KeyUp(Key key)
        {
        }

        /// <summary>
        /// In MAP and ORBIT modes, dollys the camera along the viewing direction.
        /// In FREE_FLIGHT mode, adjusts the move speed of the camera.
        /// </summary>
        /// <param name="x">X-coordinate for point of interest in viewport space, ignored in FREE_FLIGHT mode.</param>
        /// <param name="y">Y-coordinate for point of interest in viewport space, ignored in FREE_FLIGHT mode.</param>
        /// <param name="scrollDelta">
        /// In MAP and ORBIT modes, negative means "zoom in", positive means "zoom out".
        /// In FREE_FLIGHT mode, negative means "slower", positive means "faster"
        /// </param>
        public abstract void Scroll(int x, int y, float scrollDelta);

        /// <summary>
        /// Processes input and updates internal state.
        /// </summary>
        public virtual void Update(float deltaTime)
        {
        }

        /// <summary>
        /// Sets the manipulator position and orientation back to a stashed state.
        /// </summary>
        public abstract void JumpToBookmark(Bookmark bookmark);

        protected virtual void SetProperties(Config props)
        {
            _props = props;

            if (_props.ZoomSpeed == 0) {
                _props.ZoomSpeed = 0.01f;
            }

            if (_props.UpVector == Vector3.Zero) {
                _props.UpVector = new Vector3(0, 1, 0);
            }

            if (_props.FovDegrees == 0) {
                _props.FovDegrees = 33f;
            }

            if (_props.FarPlane == 0) {
                _props.FarPlane = 5000f;
            }

            if (_props.MapExtent == Vector2.Zero) {
                _props.MapExtent = new Vector2(512, 512);
            }
        }

        protected Vector3 RaycastFarPlane(int x, int y)
        {
            var gaze = Vector3.Normalize(_target - _eye);
            var right = Vector3.Cross(gaze, _props.UpVector);
            var upward = Vector3.Cross(right, gaze);
            float width = _props.Viewport[0];
            float height = _props.Viewport[1];
            var fov = _props.FovDegrees * MathHelper.Pi / 180.0f;

            // remap the grid coordinate into [-1, +1] and shift it to the pixel center.
            float u = 2.0f * (0.5f + x) / width - 1.0f;
            float v = 2.0f * (0.5f + y) / height - 1.0f;

            // compute the tangent of the field-of-view angle as well as the aspect ratio.
            var tangent = (float) MathHelper.Tan(fov / 2.0);
            var aspect = width / height;

            // Adjust the gaze so it goes through the pixel of interest rather than the grid center.
            var direction = gaze;

            if (_props.FovDirection == FieldOfView.Vertical) {
                direction += right * tangent * u * aspect;
                direction += upward * tangent * v;
            } else {
                direction += right * tangent * u;
                direction += upward * tangent * v / aspect;
            }

            return _eye + direction * _props.FarPlane;
        }

        protected bool RaycastPlane(Vector3 origin, Vector3 direction, out float t, object userData)
        {
            var plane = ((Config) userData).GroundPlane;
            var n = new Vector3(plane[0], plane[1], plane[2]);
            var p0 = n * plane[3];
            var denom = -Vector3.Dot(n, direction);

            if (denom > 1e-6) {
                var p0l0 = p0 - origin;
                t = Vector3.Dot(p0l0, n) / -denom;
                return t >= 0;
            } else {
                t = 0;
            }

            return false;
        }

        #endregion
    }

    public struct Config
    {
        public int[] Viewport;

        public Vector3 TargetPosition;
        public Vector3 UpVector;
        public float ZoomSpeed;

        public Vector3 OrbitHomePosition;
        public Vector2 OrbitSpeed;

        public FieldOfView FovDirection;
        public float FovDegrees;
        public float FarPlane;
        public Vector4 GroundPlane;

        public Vector2 MapExtent;
        public float MapMinDistance;

        public Vector3 FlightStartPosition;
        public float FlightStartPitch;
        public float FlightStartYaw;
        public float FlightMaxSpeed;
        public float FlightSpeedSteps;
        public Vector2 FlightPanSpeed;
        public float FlightMoveDamping;


        public RayCallback RaycastCallback;
        public object RaycastUserdata;
    }
}
