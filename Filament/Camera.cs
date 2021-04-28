using System;
using Filament.CameraUtilities;
using OpenTK.Mathematics;

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

        public int Entity {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetEntity(NativePtr);
            }
        }

        public float Near {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetNear(NativePtr);
            }
        }

        public float CullingFar {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetCullingFar(NativePtr);
            }
        }

        public float Aperture {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetAperture(NativePtr);
            }
        }

        public float ShutterSpeed {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetShutterSpeed(NativePtr);
            }
        }

        public float Sensitivity {
            get {
                ThrowExceptionIfDisposed();

                return Native.Camera.GetSensitivity(NativePtr);
            }
        }

        public Matrix4 ModelMatrix {
            get {
                ThrowExceptionIfDisposed();

                var m = new float[16];

                Native.Camera.GetModelMatrix(NativePtr, m);

                return new Matrix4(
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

        public Matrix4 ProjectionMatrix {
            get {
                ThrowExceptionIfDisposed();

                var m = new float[16];

                Native.Camera.GetProjectionMatrix(NativePtr, m);

                return new Matrix4(
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

        public void SetProjection(Projection projection, float left, float right, float bottom, float top, float near, float far)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.SetProjection(NativePtr, (int) projection, left, right, bottom, top, near, far);
        }

        public void SetProjection(float fovInDegrees, float aspect, float near, float far, FieldOfView direction = FieldOfView.Vertical)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.SetProjectionFov(NativePtr, fovInDegrees, aspect, near, far, (int) direction);
        }

        public void SetLensProjection(float focalLength, float aspect, float near, float far)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.SetLensProjection(NativePtr, focalLength, aspect, near, far);
        }

        public void LookAt(Vector3 eye, Vector3 center)
        {
            ThrowExceptionIfDisposed();

            LookAt(eye, center, new Vector3(0, 1, 0));
        }

        public void LookAt(Vector3 eye, Vector3 center, Vector3 up)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.LookAt(NativePtr,
                eye.X, eye.Y, eye.Z,
                center.X, center.Y, center.Z,
                up.X, up.Y, up.Z);
        }

        public void SetExposure(float aperture, float shutterSpeed, float sensitivity)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.SetExposure(NativePtr, aperture, shutterSpeed, sensitivity);
        }

        public void SetCustomProjection(Matrix4 matrix, float near, float far)
        {
            ThrowExceptionIfDisposed();

            Native.Camera.SetCustomProjection(NativePtr, new[] {
                matrix.M11, matrix.M12, matrix.M13, matrix.M14,
                matrix.M21, matrix.M22, matrix.M23, matrix.M24,
                matrix.M31, matrix.M32, matrix.M33, matrix.M34,
                matrix.M41, matrix.M42, matrix.M43, matrix.M44,
            }, near, far);
        }

        #endregion
    }
}
