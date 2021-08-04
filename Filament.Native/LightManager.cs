using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class LightBuilder
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nCreateBuilder")]
        public static extern IntPtr CreateBuilder(byte lightType);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nDestroyBuilder")]
        public static extern void DestroyBuilder(IntPtr nativeBuilder);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderCastShadows")]
        public static extern void CastShadows(IntPtr nativeBuilder, bool enable);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderCastLight")]
        public static extern void CastLight(IntPtr nativeBuilder, bool enable);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderPosition")]
        public static extern void Position(IntPtr nativeBuilder, float x, float y, float z);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderDirection")]
        public static extern void Direction(IntPtr nativeBuilder, float x, float y, float z);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderColor")]
        public static extern void Color(IntPtr nativeBuilder, float linearR, float linearG, float linearB);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderIntensityCandela")]
        public static extern void IntensityCandela(IntPtr nativeBuilder, float intensity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderIntensity__JF")]
        public static extern void IntensityJF(IntPtr nativeBuilder, float intensity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderIntensity__JFF")]
        public static extern void IntensityJFF(IntPtr nativeBuilder, float watts, float efficiency);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderFalloff")]
        public static extern void Falloff(IntPtr nativeBuilder, float radius);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderSpotLightCone")]
        public static extern void SpotLightCone(IntPtr nativeBuilder, float inner, float outer);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderAngularRadius")]
        public static extern void SunAngularRadius(IntPtr nativeBuilder, float angularRadius);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderHaloSize")]
        public static extern void SunHaloSize(IntPtr nativeBuilder, float haloSize);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderHaloFalloff")]
        public static extern void SunHaloFalloff(IntPtr nativeBuilder, float haloFalloff);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_LightManager_nBuilderBuild")]
        public static extern bool Build(IntPtr nativeBuilder, IntPtr nativeEngine, int entity);
    }
}
