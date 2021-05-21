using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class Camera
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetEntity")]
        public static extern int GetEntity(IntPtr nativeCamera);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetProjection")]
        public static extern void SetProjection(IntPtr nativeCamera, int projection, float left, float right, float bottom, float top, float near, float far);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetProjectionFov")]
        public static extern void SetProjectionFov(IntPtr nativeCamera, float fovInDegrees, float aspect, float near, float far, int direction);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetLensProjection")]
        public static extern void SetLensProjection(IntPtr nativeCamera, float focalLength, float aspect, float near, float far);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nLookAt")]
        public static extern void LookAt(IntPtr nativeCamera,
            float eyeX, float eyeY, float eyeZ,
            float centerX, float centerY, float centerZ,
            float upX, float upY, float upZ);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetNear")]
        public static extern float GetNear(IntPtr nativeCamera);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetCullingFar")]
        public static extern float GetCullingFar(IntPtr nativeCamera);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetExposure")]
        public static extern void SetExposure(IntPtr nativeCamera, float aperture, float shutterSpeed, float sensitivity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetAperture")]
        public static extern float GetAperture(IntPtr nativeCamera);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetShutterSpeed")]
        public static extern float GetShutterSpeed(IntPtr nativeCamera);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetSensitivity")]
        public static extern float GetSensitivity(IntPtr nativeCamera);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetModelMatrix")]
        public static extern void SetModelMatrix(IntPtr nativeCamera, float[] matrix);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetModelMatrix")]
        public static extern void GetModelMatrix(IntPtr nativeCamera, float[] matrix);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetProjectionMatrix")]
        public static extern void GetProjectionMatrix(IntPtr nativeCamera, float[] matrix);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nGetCullingProjectionMatrix")]
        public static extern void GetCullingProjectionMatrix(IntPtr nativeCamera, float[] matrix);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Camera_nSetCustomProjection")]
        public static extern void SetCustomProjection(IntPtr nativeCamera, float[] projection, float[] projectionForCulling, float near, float far);
    }
}
