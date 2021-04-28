using System;
using OpenTK.Mathematics;

namespace Filament.CameraUtilities
{
    public class OrbitManipulator : Manipulator
    {
        #region Members

        private GrabState _grabState = GrabState.Inactive;
        private bool _flipped = false;
        private Vector3 _grabPivot;
        private Vector3 _grabScene;
        private Vector3 _grabFar;
        private Vector3 _grabEye;
        private Vector3 _grabTarget;
        private Bookmark _grabBookmark;
        private int _grabWinX;
        private int _grabWinY;
        private Vector3 _pivot;

        #endregion

        #region Methods

        public OrbitManipulator(Mode mode, Config props) : base(mode, props)
        {
            Eye = Props.OrbitHomePosition;


            _pivot = Target = Props.TargetPosition;
        }

        #endregion

        #region Manipulator

        #region Properties

        public override Bookmark CurrentBookmark {
            get {
                Bookmark bookmark = new Bookmark();
                bookmark.Mode = Mode.Orbit;

                var pivotToEye = Eye - _pivot;
                var d = pivotToEye.Length;
                var x = pivotToEye.X / d;
                var y = pivotToEye.Y / d;
                var z = pivotToEye.Z / d;

                bookmark.Orbit.Phi = (float) MathHelper.Asin(y);
                bookmark.Orbit.Theta = (float) MathHelper.Atan2(x, z);
                bookmark.Orbit.Distance = _flipped ? -d : d;
                bookmark.Orbit.Pivot = _pivot;

                var fov = Props.FovDegrees * MathHelper.Pi / 180.0f;
                var halfExtent = d * MathHelper.Tan(fov / 2.0);
                var targetToEye = Props.GroundPlane.Xyz;
                var uvec = Vector3.Cross(Props.UpVector, targetToEye);
                var vvec = Vector3.Cross(targetToEye, uvec);
                var centerToTarget = _pivot - Props.TargetPosition;

                bookmark.Map.Extent = (float) (halfExtent * 2.0f);
                bookmark.Map.Center.X = Vector3.Dot(uvec, centerToTarget);
                bookmark.Map.Center.Y = Vector3.Dot(vvec, centerToTarget);

                return bookmark;
            }
        }

        public override Bookmark HomeBookmark {
            get {
                Bookmark bookmark = new Bookmark();
                bookmark.Mode = Mode.Orbit;
                bookmark.Orbit.Phi = 0;
                bookmark.Orbit.Theta = 0;
                bookmark.Orbit.Pivot = Props.TargetPosition;
                bookmark.Orbit.Distance = Vector3.Distance(Props.TargetPosition, Props.OrbitHomePosition);

                var fov = Props.FovDegrees * MathHelper.Pi / 180.0f;
                var halfExtent = (float) (bookmark.Orbit.Distance * MathHelper.Tan(fov / 2.0));

                bookmark.Map.Extent = halfExtent * 2f;
                bookmark.Map.Center.X = 0;
                bookmark.Map.Center.Y = 0;

                return bookmark;
            }
        }

        #endregion

        #region Methods

        protected override void SetProperties(Config props)
        {
            Config resolved = props;

            if (resolved.OrbitHomePosition == Vector3.Zero) {
                resolved.OrbitHomePosition = new Vector3(0, 0, 1.0f);
            }

            if (resolved.OrbitSpeed == Vector2.Zero) {
                resolved.OrbitSpeed = new Vector2(0.01f, 0.01f);
            }

            // By default, place the ground plane so that it aligns with the targetPosition position.
            // This is used only when PANNING.
            if (resolved.GroundPlane == Vector4.Zero) {
                var d = resolved.TargetPosition.Length;
                var n = Vector3.Normalize(resolved.OrbitHomePosition - resolved.TargetPosition);
                resolved.GroundPlane = new Vector4(n, -d);
            }

            base.SetProperties(resolved);
        }

        public override void GrabBegin(int x, int y, bool strafe)
        {
            _grabState = strafe ? GrabState.Panning : GrabState.Orbiting;
            _grabPivot = _pivot;
            _grabEye = Eye;
            _grabTarget = Target;
            _grabBookmark = CurrentBookmark;
            _grabWinX = x;
            _grabWinY = y;
            _grabFar = RaycastFarPlane(x, y);

            Raycast(x, y, out _grabScene);
        }

        public override void GrabUpdate(int x, int y)
        {
            var delx = _grabWinX - x;
            var dely = _grabWinY - y;

            if (_grabState == GrabState.Orbiting) {
                Bookmark bookmark = CurrentBookmark;

                var theta = delx * Props.OrbitSpeed.X;
                var phi = dely * Props.OrbitSpeed.Y;
                var maxPhi = MathHelper.TwoPi - 0.001f;

                bookmark.Orbit.Phi = MathHelper.Clamp(_grabBookmark.Orbit.Phi + phi, -maxPhi, +maxPhi);
                bookmark.Orbit.Theta = _grabBookmark.Orbit.Theta + theta;

                JumpToBookmark(bookmark);
            }

            if (_grabState == GrabState.Panning) {
                var ulen = Vector3.Distance(_grabScene, _grabEye);
                var vlen = Vector3.Distance(_grabFar, _grabScene);
                var translation = (_grabFar - RaycastFarPlane(x, y)) * ulen / vlen;

                _pivot = _grabPivot + translation;
                Eye = _grabEye + translation;
                Target = _grabTarget + translation;
            }
        }

        public override void GrabEnd()
        {
            _grabState = GrabState.Inactive;
        }

        public override void Scroll(int x, int y, float scrollDelta)
        {
            var gaze = Vector3.Normalize(Target - Eye);
            var movement = gaze * Props.ZoomSpeed * -scrollDelta;
            var v0 = _pivot - Eye;

            Eye += movement;
            Target += movement;

            var v1 = _pivot - Eye;

            // Check if the camera has moved past the point of interest.
            if (Vector3.Dot(v0, v1) < 0) {
                _flipped = !_flipped;
            }
        }

        public override void JumpToBookmark(Bookmark bookmark)
        {
            _pivot = bookmark.Orbit.Pivot;
            var x = (float) (MathHelper.Sin(bookmark.Orbit.Theta) * MathHelper.Cos(bookmark.Orbit.Phi));
            var y = (float) MathHelper.Sin(bookmark.Orbit.Phi);
            var z = (float) (MathHelper.Cos(bookmark.Orbit.Theta) * MathHelper.Cos(bookmark.Orbit.Phi));

            Eye = _pivot + new Vector3(x, y, z) * MathHelper.Abs(bookmark.Orbit.Distance);
            _flipped = bookmark.Orbit.Distance < 0;
            Target = Eye + new Vector3(x, y, z) * (_flipped ? 1.0f : -1.0f);
        }

        #endregion

        #endregion

        public enum GrabState
        {
            Inactive,
            Orbiting,
            Panning
        }
    }
}
