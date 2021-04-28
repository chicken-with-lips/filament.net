using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class Camera
    {
        /// <summary>
        /// Returns the entity representing this camera.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetEntity")]
        public static extern int GetEntity(IntPtr nativeCamera);

        /// <summary>
        /// Sets the projection matrix from a frustum defined by six planes.
        /// </summary>
        /// <param name="nativeCamera"></param>
        /// <param name="projection">Type of projection to use.</param>
        /// <param name="left">Distance in world units from the camera to the left plane, at the near plane.</param>
        /// <param name="right">Distance in world units from the camera to the right plane, at the near plane.</param>
        /// <param name="bottom">Distance in world units from the camera to the bottom plane, at the near plane.</param>
        /// <param name="top">Distance in world units from the camera to the top plane, at the near plane.</param>
        /// <param name="near">Distance in world units from the camera to the near plane. The near plane's position in view space is z = -\p near.</param>
        /// <param name="far">Distance in world units from the camera to the far plane. The far plane's position in view space is z = -\p far.</param>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetProjection")]
        public static extern void SetProjection(IntPtr nativeCamera, int projection, float left, float right, float bottom, float top, float near, float far);

        /// <summary>
        /// Sets the projection matrix from the field-of-view.
        /// </summary>
        /// <param name="nativeCamera"></param>
        /// <param name="fovInDegrees">Full field-of-view in degrees. 0 < p fov < 180.</param>
        /// <param name="aspect">Aspect ratio \f$ \frac{width}{height} \f$. \p aspect > 0.</param>
        /// <param name="near">Distance in world units from the camera to the near plane. \p near > 0.</param>
        /// <param name="far">Distance in world units from the camera to the far plane. \p far > \p near.</param>
        /// <param name="direction">Direction of the \p fovInDegrees parameter.</param>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetProjectionFov")]
        public static extern void SetProjectionFov(IntPtr nativeCamera, float fovInDegrees, float aspect, float near, float far, int direction);

        /// <summary>
        /// Sets the projection matrix from the focal length.
        /// </summary>
        /// <param name="nativeCamera"></param>
        /// <param name="focalLength">Lens's focal length in millimeters. \p focalLength > 0.</param>
        /// <param name="aspect">Aspect ratio \f$ \frac{width}{height} \f$. \p aspect > 0.</param>
        /// <param name="near">Distance in world units from the camera to the near plane. \p near > 0.</param>
        /// <param name="far">Ddistance in world units from the camera to the far plane. \p far > \p near.</param>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetLensProjection")]
        public static extern void SetLensProjection(IntPtr nativeCamera, float focalLength, float aspect, float near, float far);

        /// <summary>
        /// Sets the camera's view matrix.
        /// </summary>
        /// <param name="nativeCamera"></param>
        /// <param name="eyeX">The position of the camera in world space.</param>
        /// <param name="eyeY">The position of the camera in world space.</param>
        /// <param name="eyeZ">The position of the camera in world space.</param>
        /// <param name="centerX">The point in world space the camera is looking at.</param>
        /// <param name="centerY">The point in world space the camera is looking at.</param>
        /// <param name="centerZ">The point in world space the camera is looking at.</param>
        /// <param name="upX">A unit vector denoting the camera's "up" direction.</param>
        /// <param name="upY">A unit vector denoting the camera's "up" direction.</param>
        /// <param name="upZ">A unit vector denoting the camera's "up" direction.</param>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nLookAt")]
        public static extern void LookAt(IntPtr nativeCamera,
            float eyeX, float eyeY, float eyeZ,
            float centerX, float centerY, float centerZ,
            float upX, float upY, float upZ);

        /// <summary>
        /// Returns the frustum's near plane.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetNear")]
        public static extern float GetNear(IntPtr nativeCamera);

        /// <summary>
        /// Returns the frustum's far plane used for culling.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetCullingFar")]
        public static extern float GetCullingFar(IntPtr nativeCamera);

        /// <summary>
        /// Sets this camera's exposure (default is f/16, 1/125s, 100 ISO)
        /// </summary>
        /// <remarks>
        /// The exposure ultimately controls the scene's brightness, just like with a real camera. The default values
        /// provide adequate exposure for a camera placed outdoors on a sunny day with the sun at the zenith.
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
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetExposure")]
        public static extern void SetExposure(IntPtr nativeCamera, float aperture, float shutterSpeed, float sensitivity);

        /// <summary>
        /// Returns this camera's aperture in f-stops.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetAperture")]
        public static extern float GetAperture(IntPtr nativeCamera);

        /// <summary>
        /// Returns this camera's shutter speed in seconds.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetShutterSpeed")]
        public static extern float GetShutterSpeed(IntPtr nativeCamera);

        /// <summary>
        /// Returns this camera's sensitivity in ISO.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetSensitivity")]
        public static extern float GetSensitivity(IntPtr nativeCamera);

        /// <summary>
        /// Sets the camera's view matrix.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetModelMatrix")]
        public static extern void SetModelMatrix(IntPtr nativeCamera, float[] matrix);

        /// <summary>
        /// Returns the camera's model matrix.
        /// </summary>
        /// <param name="matrix">
        /// The camera's pose in world space as a rigid transform. Parent transforms, if any, are taken into account.
        /// </param>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetModelMatrix")]
        public static extern void GetModelMatrix(IntPtr nativeCamera, float[] matrix);

        /// <summary>
        /// Returns the projection matrix used for rendering.
        /// </summary>
        /// <remarks>
        /// The projection matrix used for rendering always has its far plane set to infinity. This is why it may differ
        /// from the matrix set through setProjection() or setLensProjection().
        /// </remarks>
        /// <param name="nativeCamera"></param>
        /// <param name="matrix">The projection matrix used for rendering.</param>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetProjectionMatrix")]
        public static extern void GetProjectionMatrix(IntPtr nativeCamera, float[] matrix);

        /// <summary>
        /// Sets the projection matrix.
        /// </summary>
        /// <param name="nativeCamera"></param>
        /// <param name="projection">Custom projection matrix.</param>
        /// <param name="near">Distance in world units from the camera to the near plane. \p near > 0.</param>
        /// <param name="far">Distance in world units from the camera to the far plane. \p far > \p near.</param>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetCustomProjection")]
        public static extern void SetCustomProjection(IntPtr nativeCamera, float[] projection, float near, float far);
    }
}
