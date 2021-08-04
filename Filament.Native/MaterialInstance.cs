using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class MaterialInstance
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterBool")]
        public static extern void SetParameterBool(IntPtr nativeMaterialInstance, string name, bool x);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterBool2")]
        public static extern void SetParameterBool2(IntPtr nativeMaterialInstance, string name, bool x, bool y);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterBool3")]
        public static extern void SetParameterBool3(IntPtr nativeMaterialInstance, string name, bool x, bool y, bool z);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterBool4")]
        public static extern void SetParameterBool4(IntPtr nativeMaterialInstance, string name, bool x, bool y, bool z, bool w);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterInt")]
        public static extern void SetParameterInt(IntPtr nativeMaterialInstance, string name, int x);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterInt2")]
        public static extern void SetParameterInt2(IntPtr nativeMaterialInstance, string name, int x, int y);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterInt3")]
        public static extern void SetParameterInt3(IntPtr nativeMaterialInstance, string name, int x, int y, int z);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterInt4")]
        public static extern void SetParameterInt4(IntPtr nativeMaterialInstance, string name, int x, int y, int z, int w);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterFloat")]
        public static extern void SetParameterFloat(IntPtr nativeMaterialInstance, string name, float x);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterFloat2")]
        public static extern void SetParameterFloat2(IntPtr nativeMaterialInstance, string name, float x, float y);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterFloat3")]
        public static extern void SetParameterFloat3(IntPtr nativeMaterialInstance, string name, float x, float y, float z);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterFloat4")]
        public static extern void SetParameterFloat4(IntPtr nativeMaterialInstance, string name, float x, float y, float z, float w);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterRgbColor")]
        public static extern void SetParameterRgbColor(IntPtr nativeMaterialInstance, string name, byte type, float r, float g, float b);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterRgbaColor")]
        public static extern void SetParameterRgbaColor(IntPtr nativeMaterialInstance, string name, byte type, float r, float g, float b, float a);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_MaterialInstance_nSetParameterTexture")]
        public static extern void SetParameterTexture(IntPtr nativeMaterialInstance, IntPtr nativeTexture, string name, IntPtr sampler);
    }
}
