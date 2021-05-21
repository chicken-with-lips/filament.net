using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class View
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetName")]
        public static extern void SetName(IntPtr nativeView, string name);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetScene")]
        public static extern void SetScene(IntPtr nativeView, IntPtr nativeScene);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetCamera")]
        public static extern void SetCamera(IntPtr nativeView, IntPtr nativeCamera);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nGetCamera")]
        public static extern IntPtr GetCamera(IntPtr nativeView);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetDebugCamera")]
        public static extern void SetDebugCamera(IntPtr nativeView, IntPtr nativeCamera);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nGetDirectionalLightCamera")]
        public static extern IntPtr GetDirectionalLightCamera(IntPtr nativeView);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetViewport")]
        public static extern void SetViewport(IntPtr nativeView, int left, int bottom, int width, int height);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetVisibleLayers")]
        public static extern void SetVisibleLayers(IntPtr nativeView, int select, int value);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetShadowingEnabled")]
        public static extern void SetShadowingEnabled(IntPtr nativeView, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nIsShadowingEnabled")]
        public static extern bool IsShadowingEnabled(IntPtr nativeView);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetPostProcessingEnabled")]
        public static extern void SetPostProcessingEnabled(IntPtr nativeView, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nIsPostProcessingEnabled")]
        public static extern bool IsPostProcessingEnabled(IntPtr nativeView);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nGetViewport")]
        public static extern bool GetViewport(IntPtr nativeView, out int left, out int bottom, out int width, out int height);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetRenderTarget")]
        public static extern void SetRenderTarget(IntPtr nativeView, IntPtr nativeTarget);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetSampleCount")]
        public static extern void SetSampleCount(IntPtr nativeView, int count);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetAntiAliasing")]
        public static extern void SetAntiAliasing(IntPtr nativeView, byte type);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nGetAntiAliasing")]
        public static extern byte GetAntiAliasing(IntPtr nativeView);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetDithering")]
        public static extern void SetDithering(IntPtr nativeView, byte dithering);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nGetDithering")]
        public static extern byte GetDithering(IntPtr nativeView);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetDynamicResolutionOptions")]
        public static extern void SetDynamicResolutionOptions(IntPtr nativeView, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetDynamicResolutionOptions")]
        public static extern void SetShadowType(IntPtr nativeView, byte type);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetDynamicResolutionOptions")]
        public static extern void SetVsmShadowOptions(IntPtr nativeView, int anistropy);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetRenderQuality")]
        public static extern void SetRenderQuality(IntPtr nativeView, byte hdrColorBufferQuality);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetDynamicLightingOptions")]
        public static extern void SetDynamicLightingOptions(IntPtr nativeView, float zLightNear, float zLightFar);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetFrontFaceWindingInverted")]
        public static extern void SetFrontFaceWindingInverted(IntPtr nativeView, bool inverted);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nIsFrontFaceWindingInverted")]
        public static extern bool IsFrontFaceWindingInverted(IntPtr nativeView);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetAmbientOcclusion")]
        public static extern void SetAmbientOcclusion(IntPtr nativeView, byte mode);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nGetAmbientOcclusion")]
        public static extern byte GetAmbientOcclusion(IntPtr nativeView);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetAmbientOcclusionOptions")]
        public static extern void SetAmbientOcclusionOptions(IntPtr nativeView, float radius, float bias, float power,
            float resolution, float intensity,
            byte quality, byte lowPassFilter, byte upsampling, bool enabled, float minHorizonAngleRad,
            float ssctLightConeRad, float ssctStartTraceDistance,
            float ssctContactDistanceMax,
            float ssctIntensity, float ssctLightDirX, float ssctLightDirY,
            float ssctLightDirZ,
            float ssctDepthBias, float ssctDepthSlopeBias, int ssctSampleCount,
            int ssctRayCount, bool ssctEnabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetBloomOptions")]
        public static extern void SetBloomOptions(IntPtr nativeView, IntPtr nativeTexture,
            float dirtStrength, float strength, int resolution, float anamorphism, int levels,
            byte blendMode, bool threshold, bool enabled, float highlight, bool lensFlare, bool starburst,
            float chromaticAberration, int ghostCount, float ghostSpacing, float ghostThreshold, float haloThickness,
            float haloRadius, float haloThreshold);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetFogOptions")]
        public static extern void SetFogOptions(IntPtr nativeView,
            float distance, float maximumOpacity, float height,
            float heightFalloff, float r,
            float g, float b, float density, float inScatteringStart,
            float inScatteringSize, bool fogColorFromIbl, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetBlendMode")]
        public static extern void SetBlendMode(IntPtr nativeView, byte blendMode);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetDepthOfFieldOptions")]
        public static extern void SetDepthOfFieldOptions(IntPtr nativeView, float focusDistance, float cocScale,
            float maxApertureDiameter, bool enabled, int filter, bool nativeResolution, int foregroundRingCount,
            int backgroundRingCount, int fastGatherRingCount, int maxForegroundCOC, int maxBackgroundCOC);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetVignetteOptions")]
        public static extern void SetVignetteOptions(IntPtr nativeView, float midPoint, float roundness,
            float feather, float r, float g, float b, float a, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetTemporalAntiAliasingOptions")]
        public static extern void SetTemporalAntiAliasingOptions(IntPtr nativeView, float feedback, float filterWidth,
            bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nSetScreenSpaceRefractionEnabled")]
        public static extern void SetScreenSpaceRefractionEnabled(IntPtr nativeView, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_View_nIsScreenSpaceRefractionEnabled")]
        public static extern bool IsScreenSpaceRefractionEnabled(IntPtr nativeView);
    }
}
