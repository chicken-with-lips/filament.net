using System;
using System.Numerics;

namespace Filament.CameraUtilities
{
    /// <summary>
    /// Helper that enables camera interaction similar to sketchfab or Google Maps.
    /// </summary>
    /// <remarks>
    /// Clients notify the camera manipulator of various mouse or touch events, then periodically call
    /// its getLookAt() method so that they can adjust their camera(s). Three modes are supported: ORBIT,
    /// MAP, and FREE_FLIGHT. To construct a manipulator instance, the desired mode is passed into the
    /// create method.
    /// </remarks>
    public class ManipulatorBuilder
    {
        #region Members

        private Config _config;

        #endregion

        public ManipulatorBuilder()
        {
            _config.Viewport = new int[] {0, 0};
        }

        /// <summary>
        /// Width and height of the viewing area.
        /// </summary>
        public ManipulatorBuilder WithViewport(int width, int height)
        {
            _config.Viewport[0] = width;
            _config.Viewport[1] = height;

            return this;
        }

        /// <summary>
        /// World-space position of interest, defaults to (0,0,0).
        /// </summary>
        public ManipulatorBuilder WithTargetPosition(float x, float y, float z)
        {
            return WithTargetPosition(new Vector3(x, y, z));
        }

        /// <summary>
        /// World-space position of interest, defaults to (0,0,0).
        /// </summary>
        public ManipulatorBuilder WithTargetPosition(Vector3 targetPosition)
        {
            _config.TargetPosition = targetPosition;

            return this;
        }

        /// <summary>
        /// Orientation for the home position, defaults to (0,1,0).
        /// </summary>
        public ManipulatorBuilder WithUpVector(float x, float y, float z)
        {
            return WithUpVector(new Vector3(x, y, z));
        }

        /// <summary>
        /// Orientation for the home position, defaults to (0,1,0).
        /// </summary>
        public ManipulatorBuilder WithUpVector(Vector3 upVector)
        {
            _config.UpVector = upVector;

            return this;
        }

        /// <summary>
        /// Multiplied with scroll delta, defaults to 0.01.
        /// </summary>
        public ManipulatorBuilder WithZoomSpeed(float value)
        {
            _config.ZoomSpeed = value;

            return this;
        }

        /// <summary>
        /// Initial eye position in world space, defaults to (0,0,1).
        /// </summary>
        public ManipulatorBuilder WithOrbitHomePosition(float x, float y, float z)
        {
            return WithOrbitHomePosition(new Vector3(x, y, z));
        }

        /// <summary>
        /// Initial eye position in world space, defaults to (0,0,1).
        /// </summary>
        public ManipulatorBuilder WithOrbitHomePosition(Vector3 position)
        {
            _config.OrbitHomePosition = position;

            return this;
        }

        /// <summary>
        /// Multiplied with viewport delta, defaults to 0.01.
        /// </summary>
        public ManipulatorBuilder WithOrbitSpeed(float x, float y)
        {
            return WithOrbitSpeed(new Vector2(x, y));
        }

        /// <summary>
        /// Multiplied with viewport delta, defaults to 0.01.
        /// </summary>
        public ManipulatorBuilder WithOrbitSpeed(Vector2 speed)
        {
            _config.OrbitSpeed = speed;

            return this;
        }

        /// <summary>
        /// The axis that's held constant when viewport changes.
        /// </summary>
        public ManipulatorBuilder WithFieldOfViewDirection(FieldOfView direction)
        {
            _config.FovDirection = direction;

            return this;
        }

        /// <summary>
        /// The full FOV (not the half-angle).
        /// </summary>
        public ManipulatorBuilder WithFieldOfViewDegrees(float degrees)
        {
            _config.FovDegrees = degrees;

            return this;
        }

        /// <summary>
        /// The distance to the far plane.
        /// </summary>
        public ManipulatorBuilder WithFarPlane(float distance)
        {
            _config.FarPlane = distance;

            return this;
        }

        /// <summary>
        /// The ground size for computing home position.
        /// </summary>
        public ManipulatorBuilder WithMapExtent(float worldWidth, float worldHeight)
        {
            return WithMapExtent(new Vector2(worldWidth, worldHeight));
        }

        /// <summary>
        /// The ground size for computing home position.
        /// </summary>
        public ManipulatorBuilder WithMapExtent(Vector2 extents)
        {
            _config.MapExtent = extents;

            return this;
        }

        /// <summary>
        /// Constrains the zoom-in level.
        /// </summary>
        public ManipulatorBuilder WithMapMinDistance(float distance)
        {
            _config.MapMinDistance = distance;

            return this;
        }

        /// <summary>
        /// Initial eye position in world space, defaults to (0,0,0).
        /// </summary>
        public ManipulatorBuilder WithFlightStartPosition(float x, float y, float z)
        {
            return WithFlightStartPosition(new Vector3(x, y, z));
        }

        /// <summary>
        /// Initial eye position in world space, defaults to (0,0,0).
        /// </summary>
        public ManipulatorBuilder WithFlightStartPosition(Vector3 position)
        {
            _config.FlightStartPosition = position;

            return this;
        }

        /// <summary>
        /// Initial orientation in pitch and yaw, defaults to (0,0).
        /// </summary>
        public ManipulatorBuilder WithFlightStartPosition(float pitch, float yaw)
        {
            _config.FlightStartPitch = pitch;
            _config.FlightStartYaw = yaw;

            return this;
        }

        /// <summary>
        /// The maximum camera speed in world units per second, defaults to 10.
        /// </summary>
        public ManipulatorBuilder WithFlightMaxMoveSpeed(float maxSpeed)
        {
            _config.FlightMaxSpeed = maxSpeed;

            return this;
        }

        /// <summary>
        /// The number of speed steps adjustable with scroll wheel, defaults to 80.
        /// </summary>
        public ManipulatorBuilder WithFlightSpeedSteps(int steps)
        {
            _config.FlightSpeedSteps = steps;

            return this;
        }

        /// <summary>
        /// Multiplied with viewport delta, defaults to 0.01,0.01.
        /// </summary>
        public ManipulatorBuilder WithFlightPanSpeed(float x, float y)
        {
            return WithFlightPanSpeed(new Vector2(x, y));
        }

        /// <summary>
        /// Multiplied with viewport delta, defaults to 0.01,0.01.
        /// </summary>
        public ManipulatorBuilder WithFlightPanSpeed(Vector2 speed)
        {
            _config.FlightPanSpeed = speed;

            return this;
        }

        /// <summary>
        /// Applies a deceleration to camera movement, defaults to 0 (no damping).
        /// </summary
        /// <remarks>
        /// Lower values give slower damping times, a good default is 15
        /// Too high a value may lead to instability
        /// </remarks>
        public ManipulatorBuilder WithFlightMoveDamping(float damping)
        {
            _config.FlightMoveDamping = damping;

            return this;
        }

        /// <summary>
        /// Plane equation used as a raycast fallback.
        /// </summary>
        public ManipulatorBuilder WithGroundPlane(float a, float b, float c, float d)
        {
            return WithGroundPlane(new Vector4(a, b, c, d));
        }

        /// <summary>
        /// Plane equation used as a raycast fallback.
        /// </summary>
        public ManipulatorBuilder WithGroundPlane(Vector4 plane)
        {
            _config.GroundPlane = plane;

            return this;
        }

        /// <summary>
        /// Raycast function for accurate grab-and-pan.
        /// </summary>
        public ManipulatorBuilder WithRaycastCallback(RayCallback callback, object userData)
        {
            _config.RaycastCallback = callback;
            _config.RaycastUserdata = userData;

            return this;
        }

        /// <summary>
        /// Creates a new camera manipulator, either ORBIT, MAP, or FREE_FLIGHT.
        /// </summary>
        public Manipulator Build(Mode mode)
        {
            switch (mode) {
                case Mode.FreeFlight:
                    return new FreeFlightManipulator(mode, _config);
                case Mode.Map:
                    return new MapManipulator(mode, _config);
                case Mode.Orbit:
                    return new OrbitManipulator(mode, _config);
            }

            throw new Exception("Unknown mode");
        }

        public static ManipulatorBuilder Create()
        {
            return new ManipulatorBuilder();
        }
    }
}
