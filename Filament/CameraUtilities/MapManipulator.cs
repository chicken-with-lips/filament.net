using OpenTK.Mathematics;

namespace Filament.CameraUtilities
{
    public class MapManipulator : Manipulator
    {
        #region Members

        private bool _grabbing = false;
        private Vector3 _grabScene;
        private Vector3 _grabFar;
        private Vector3 _grabEye;
        private Vector3 _grabTarget;

        #endregion

        #region Methods

        public MapManipulator(Mode mode, Config props) : base(mode, props)
        {
            var width = Props.MapExtent.X;
            var height = Props.MapExtent.Y;
            var horiz = Props.FovDirection == FieldOfView.Horizontal;
            var targetToEye = Props.GroundPlane.Xyz;
            var halfExtent = (horiz ? width : height) / 2.0f;
            var fov = Props.FovDegrees * MathHelper.Pi / 180.0f;
            var distance = (float) (halfExtent / MathHelper.Tan(fov / 2.0f));

            Target = Props.TargetPosition;
            Eye = Target + distance * targetToEye;
        }

        private void MoveWithConstraints(Vector3 eye, Vector3 targetPosition)
        {
            Eye = eye;
            Target = targetPosition;
        }

        #endregion

        #region Manipulator

        #region Properties

        public override Bookmark CurrentBookmark {
            get {
                var direction = Vector3.Normalize(Target - Eye);

                float distance;
                RaycastPlane(Eye, direction, out distance, Props);

                float fov = Props.FovDegrees * MathHelper.Pi / 180.0f;
                float halfExtent = (float) (distance * MathHelper.Tan(fov / 2.0f));

                var targetPosition = Eye + direction * distance;

                var targetToEye = Props.GroundPlane.Xyz;
                var uvec = Vector3.Cross(Props.UpVector, targetToEye);
                var vvec = Vector3.Cross(targetToEye, uvec);
                var centerToTarget = targetPosition - Props.TargetPosition;

                Bookmark bookmark = new Bookmark();
                bookmark.Mode = Mode.Map;
                bookmark.Map.Extent = halfExtent * 2.0f;
                bookmark.Map.Center.X = Vector3.Dot(uvec, centerToTarget);
                bookmark.Map.Center.Y = Vector3.Dot(vvec, centerToTarget);

                bookmark.Orbit.Theta = 0;
                bookmark.Orbit.Phi = 0;
                bookmark.Orbit.Pivot = Props.TargetPosition +
                                       uvec * bookmark.Map.Center.X +
                                       vvec * bookmark.Map.Center.Y;
                bookmark.Orbit.Distance = (float) (halfExtent / MathHelper.Tan(fov / 2.0));

                return bookmark;
            }
        }

        public override Bookmark HomeBookmark {
            get {
                var fov = Props.FovDegrees * MathHelper.Pi / 180.0f;
                var width = Props.MapExtent.X;
                var height = Props.MapExtent.Y;
                var horiz = Props.FovDirection == FieldOfView.Horizontal;

                Bookmark bookmark = new Bookmark();
                bookmark.Mode = Mode.Map;
                bookmark.Map.Extent = horiz ? width : height;
                bookmark.Map.Center.X = 0;
                bookmark.Map.Center.Y = 0;

                bookmark.Orbit.Theta = 0;
                bookmark.Orbit.Phi = 0;
                bookmark.Orbit.Pivot = Target;
                bookmark.Orbit.Distance = (float) (0.5 * bookmark.Map.Extent / MathHelper.Tan(fov / 2.0));

                return bookmark;
            }
        }

        #endregion

        #region Methods

        public override void GrabBegin(int x, int y, bool strafe)
        {
            if (strafe || !Raycast(x, y, out _grabScene)) {
                return;
            }

            _grabFar = RaycastFarPlane(x, y);
            _grabEye = Eye;
            _grabTarget = Target;
            _grabbing = true;
        }

        public override void GrabUpdate(int x, int y)
        {
            if (_grabbing) {
                float ulen = Vector3.Distance(_grabScene, _grabEye);
                float vlen = Vector3.Distance(_grabFar, _grabScene);
                var translation = (_grabFar - RaycastFarPlane(x, y)) * ulen / vlen;
                var eyePosition = _grabEye + translation;
                var targetPosition = _grabTarget + translation;

                MoveWithConstraints(eyePosition, targetPosition);
            }
        }

        public override void GrabEnd()
        {
            _grabbing = false;
        }

        public override void Scroll(int x, int y, float scrollDelta)
        {
            Vector3 grabScene;
            if (!Raycast(x, y, out grabScene)) {
                return;
            }

            // Find the direction of travel for the dolly. We do not normalize since it
            // is desirable to move faster when further away from the targetPosition.
            var u = grabScene - Eye;

            // Prevent getting stuck when zooming in.
            if (scrollDelta < 0) {
                var distanceToSurface = u.Length;
                if (distanceToSurface < Props.ZoomSpeed) {
                    return;
                }
            }

            u *= -scrollDelta * Props.ZoomSpeed;

            var eyePosition = Eye + u;
            var targetPosition = Target + u;

            MoveWithConstraints(eyePosition, targetPosition);
        }


        public override void JumpToBookmark(Bookmark bookmark)
        {
            var targetToEye = Props.GroundPlane.Xyz;
            var halfExtent = bookmark.Map.Extent / 2.0f;
            var fov = Props.FovDegrees * MathHelper.Pi / 180.0f;
            var distance = (float) (halfExtent / MathHelper.Tan(fov / 2.0));
            var uvec = Vector3.Cross(Props.UpVector, targetToEye);
            var vvec = Vector3.Cross(targetToEye, uvec);

            uvec = Vector3.Normalize(uvec) * bookmark.Map.Center.X;
            vvec = Vector3.Normalize(vvec) * bookmark.Map.Center.Y;

            Target = Props.TargetPosition + uvec + vvec;
            Eye = Target + distance * targetToEye;
        }

        #endregion

        #endregion
    }
}
