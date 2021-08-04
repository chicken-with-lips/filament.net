using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class IndirectLight
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndirectLight_nSetIntensity")]
        public static extern void SetIntensity(IntPtr nativeLight, float envIntensity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndirectLight_nGetIntensity")]
        public static extern float GetIntensity(IntPtr nativeLight);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndirectLight_nSetRotation")]
        public static extern void SetRotation(IntPtr nativeLight,
            float v0, float v1, float v2,
            float v3, float v4, float v5,
            float v6, float v7, float v8);
    }

    public static class IndirectLightBuilder
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndirectLight_nCreateBuilder")]
        public static extern IntPtr Create();

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndirectLight_nBuilderBuild")]
        public static extern IntPtr Build(IntPtr nativeBuilder, IntPtr nativeEngine);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndirectLight_nBuilderReflections")]
        public static extern void Reflections(IntPtr nativeBuilder, IntPtr nativeTexture);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndirectLight_nBuilderIntensity")]
        public static extern void Intensity(IntPtr nativeBuilder, float envIntensity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndirectLight_nBuilderIrradiance")]
        public static extern IntPtr Irradiance(IntPtr nativeBuilder, int bands, float[] sh);
    }
}
