using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class Skybox
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nSetLayerMask")]
        public static extern void SetLayerMask(IntPtr nativeSkyBox, int select, int value);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nGetLayerMask")]
        public static extern int GetLayerMask(IntPtr nativeSkyBox);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nGetIntensity")]
        public static extern float GetIntensity(IntPtr nativeSkyBox);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nSetColor")]
        public static extern void SetColor(IntPtr nativeSkyBox, float r, float g, float b, float a);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nGetTexture")]
        public static extern IntPtr GetTexture(IntPtr nativeSkyBox);
    }

    public static class SkyboxBuilder
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nCreateBuilder")]
        public static extern IntPtr CreateBuilder();

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nDestroyBuilder")]
        public static extern void DestroyBuilder(IntPtr nativeBuilder);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nBuilderEnvironment")]
        public static extern void Environment(IntPtr nativeBuilder, IntPtr nativeTexture);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nBuilderShowSun")]
        public static extern void ShowSun(IntPtr nativeBuilder, bool show);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nBuilderIntensity")]
        public static extern void Intensity(IntPtr nativeBuilder, float intensity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nBuilderColor")]
        public static extern void Color(IntPtr nativeBuilder, float r, float g, float b, float a);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Skybox_nBuilderBuild")]
        public static extern IntPtr Build(IntPtr nativeBuilder, IntPtr nativeEngine);
    }
}
