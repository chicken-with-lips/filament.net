using System;
using System.Numerics;
using Filament.CameraUtilities;

namespace Filament
{
    public enum Projection
    {
        /// <summary>Objects get smaller as they are farther</summary>
        Perspective,

        /// <summary>Orthonormal projection, preserves distances</summary>
        Ortho
    }

    public class Camera : FilamentBase<Camera>
    {
        #region Properties

        /// <summary>
        /// Returns the entity representing this camera.
        /// </summary>
        public int Entity {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetEntity(NativePtr);
            }
        }

        /// <summary>
        /// Returns the frustum's near plane.
        /// </summary>
        public float Near {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetNear(NativePtr);
            }
        }

        /// <summary>
        /// Returns the frustum's far plane used for culling.
        /// </summary>
        public float CullingFar {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetCullingFar(NativePtr);
            }
        }

        /// <summary>
        /// Returns this camera's aperture in f-stops.
        /// </summary>
        public float Aperture {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetAperture(NativePtr);
            }
        }

        /// <summary>
        /// Returns this camera's shutter speed in seconds.
        /// </summary>
        public float ShutterSpeed {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetShutterSpeed(NativePtr);
            }
        }

        /// <summary>
        /// Returns this camera's sensitivity in ISO.
        /// </summary>
        public float Sensitivity {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetSensitivity(NativePtr);
            }
        }

        /// <summary>
        /// Returns or sets the camera's view matrix.
        /// </summary>
        public Matrix4x4 ModelMatrix {
            get {
                ThrowExceptionIfDisposed();

                var m = new float[16];

                Native.Camera.GetModelMatrix(NativePtr, m);

                return new Matrix4x4(
                    m[0], m[1], m[2], m[3],
                    m[4], m[5], m[6], m[7],
                    m[8], m[9], m[10], m[11],
                    m[12], m[13], m[14], m[15]
                );
            }
            set {
                ThrowExceptionIfDisposed();

                Native.Camera.SetModelMatrix(NativePtr, new[] {
                    value.M11, value.M12, value.M13, value.M14,
                    value.M21, value.M22, value.M23, value.M24,
                    value.M31, value.M32, value.M33, value.M34,
                    value.M41, value.M42, value.M43, value.M44
                });
            }
        }

        /// <summary>
        /// <para>Returns the projection matrix used for rendering.</para>
        /// <para>The projection matrix used for rendering always has its far plane set to infinity. This is why it may differ
        /// from the matrix set through setProjection() or setLensProjection().</para>
        /// </summary>
        public Matrix4x4 ProjectionMatrix {
            get {
                ThrowExceptionIfDisposed();

                var m = new float[16];

                Native.Camera.GetProjectionMatrix(NativePtr, m);

                return new Matrix4x4(
                    m[0], m[1], m[2], m[3],
                    m[4], m[5], m[6], m[7],
                    m[8], m[9], m[10], m[11],
                    m[12], m[13], m[14], m[15]
                );
            }
        }

        /// <summary>
        /// Returns the projection matrix used for culling (far plane is finite).
        /// </summary>
        public Matrix4x4 CullingProjectionMatrix {
            get {
                ThrowExceptionIfDisposed();

                var m = new float[16];

                Native.Camera.GetCullingProjectionMatrix(NativePtr, m);

                return new Matrix4x4(
                    m[0], m[1], m[2], m[3],
                    m[4], m[5], m[6], m[7],
                    m[8], m[9], m[10], m[11],
                    m[12], m[13], m[14], m[15]
                );
            }
        }

        #endregion

        #region Methods

        private Camera(IntPtr ptr) : base(ptr)
        {
        }

        internal static Camera GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new Camera(newPtr));
        }

        /// <summary>
        /// Sets the projection matrix from a frustum defined by six planes.
        /// </summary>
        /// <param name="projection">Type of projection to use.</param>
        /// <param name="left">Distance in world units from the camera to the left plane, at the near plane.</param>
        /// <param name="right">Distance in world units from the camera to the right plane, at the near plane.</param>
        /// <param name="bottom">Distance in world units from the camera to the bottom plane, at the near plane.</param>
        /// <param name="top">Distance in world units from the camera to the top plane, at the near plane.</param>
        /// <param name="near">Distance in world units from the camera to the near plane. The near plane's position in view space is z = -\p near.</param>
        /// <param name="far">Distance in world units from the camera to the far plane. The far plane's position in view space is z = -\p far.</param>
        public void SetProjection(Projection projection, float left, float right, float bottom, float top, float near, float far)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.SetProjection(NativePtr, (int) projection, left, right, bottom, top, near, far);
        }

        /// <summary>
        /// Sets the projection matrix from the field-of-view.
        /// </summary>
        /// <param name="fovInDegrees">Full field-of-view in degrees. 0 < p fov < 180.</param>
        /// <param name="aspect">Aspect ratio \f$ \frac{width}{height} \f$. \p aspect > 0.</param>
        /// <param name="near">Distance in world units from the camera to the near plane. \p near > 0.</param>
        /// <param name="far">Distance in world units from the camera to the far plane. \p far > \p near.</param>
        /// <param name="direction">Direction of the \p fovInDegrees parameter.</param>
        public void SetProjection(float fovInDegrees, float aspect, float near, float far, FieldOfView direction = FieldOfView.Vertical)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.SetProjectionFov(NativePtr, fovInDegrees, aspect, near, far, (int) direction);
        }

        /// <summary>
        /// Sets the projection matrix from the focal length.
        /// </summary>
        /// <param name="focalLength">Lens's focal length in millimeters. \p focalLength > 0.</param>
        /// <param name="aspect">Aspect ratio \f$ \frac{width}{height} \f$. \p aspect > 0.</param>
        /// <param name="near">Distance in world units from the camera to the near plane. \p near > 0.</param>
        /// <param name="far">Distance in world units from the camera to the far plane. \p far > \p near.</param>
        public void SetLensProjection(float focalLength, float aspect, float near, float far)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.SetLensProjection(NativePtr, focalLength, aspect, near, far);
        }

        /// <summary>
        /// Sets the camera's view matrix.
        /// </summary>
        /// <param name="eye">The position of the camera in world space.</param>
        /// <param name="center">The point in world space the camera is looking at.</param>
        public void LookAt(Vector3 eye, Vector3 center)
        {
            ThrowExceptionIfDisposed();

            LookAt(eye, center, new Vector3(0, 1, 0));
        }

        /// <summary>
        /// Sets the camera's view matrix.
        /// </summary>
        /// <param name="eye">The position of the camera in world space.</param>
        /// <param name="center">The point in world space the camera is looking at.</param>
        /// <param name="up">A unit vector denoting the camera's "up" direction.</param>
        public void LookAt(Vector3 eye, Vector3 center, Vector3 up)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.LookAt(NativePtr,
                eye.X, eye.Y, eye.Z,
                center.X, center.Y, center.Z,
                up.X, up.Y, up.Z);
        }

        /// <summary>
        /// <para>Sets this camera's exposure (default is f/16, 1/125s, 100 ISO)</para>
        /// <para>The exposure ultimately controls the scene's brightness, just like with a real camera. The default
        /// values provide adequate exposure for a camera placed outdoors on a sunny day with the sun at the zenith.</para>
        /// </remarks>
        /// <param name="nativeCamera"></param>
        /// <param name="aperture">
        /// Aperture in f-stops, clamped between 0.5 and 64. A lower \p aperture value *increases* the exposure, leading
        /// to a brighter scene. Realistic values are between 0.95 and 32.
        /// </param>
        /// <param name="shutterSpeed">
        /// Shutter speed in seconds, clamped between 1/25,000 and 60. A lower shutter speed increases the exposure.
        /// Realistic values are between 1/8000 and 30.
        /// </param>
        /// <param name="sensitivity">
        /// Sensitivity in ISO, clamped between 10 and 204,800. A higher \p sensitivity increases the exposure.
        /// Realistic values are between 50 and 25600.
        /// </param>
        public void SetExposure(float aperture, float shutterSpeed, float sensitivity)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.SetExposure(NativePtr, aperture, shutterSpeed, sensitivity);
        }

        /// <summary>
        /// Sets the projection matrix.
        /// </summary>
        /// <param name="projection">Custom projection matrix used for rendering and culling.</param>
        /// <param name="near">Distance in world units from the camera to the near plane. \p near > 0.</param>
        /// <param name="far">Distance in world units from the camera to the far plane. \p far > \p near.</param>
        public void SetCustomProjection(Matrix4x4 projection, float near, float far)
        {
            SetCustomProjection(projection, projection, near, far);
        }

        /// <summary>
        /// Sets the projection matrix.
        /// </summary>
        /// <param name="projection">Custom projection matrix.</param>
        /// <param name="projectionForCulling">Custom projection matrix used for culling.</param>
        /// <param name="near">Distance in world units from the camera to the near plane. \p near > 0.</param>
        /// <param name="far">Distance in world units from the camera to the far plane. \p far > \p near.</param>
        public void SetCustomProjection(Matrix4x4 projection, Matrix4x4 projectionForCulling, float near, float far)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.SetCustomProjection(NativePtr, new[] {
                    projection.M11, projection.M12, projection.M13, projection.M14,
                    projection.M21, projection.M22, projection.M23, projection.M24,
                    projection.M31, projection.M32, projection.M33, projection.M34,
                    projection.M41, projection.M42, projection.M43, projection.M44,
                },
                new[] {
                    projectionForCulling.M11, projectionForCulling.M12, projectionForCulling.M13, projectionForCulling.M14,
                    projectionForCulling.M21, projectionForCulling.M22, projectionForCulling.M23, projectionForCulling.M24,
                    projectionForCulling.M31, projectionForCulling.M32, projectionForCulling.M33, projectionForCulling.M34,
                    projectionForCulling.M41, projectionForCulling.M42, projectionForCulling.M43, projectionForCulling.M44,
                },
                near, far
            );
        }

        #endregion
    }
}
