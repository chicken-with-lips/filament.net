using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class Material
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nCreateInstance")]
        public static extern IntPtr CreateInstance(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetDefaultInstance")]
        public static extern IntPtr GetDefaultInstance(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetName")]
        public static extern string GetName(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetShading")]
        public static extern byte GetShading(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetInterpolation")]
        public static extern byte GetInterpolation(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetBlendingMode")]
        public static extern byte GetBlendingMode(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetRefractionMode")]
        public static extern byte GetRefractionMode(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetRefractionType")]
        public static extern byte GetRefractionType(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetVertexDomain")]
        public static extern byte GetVertexDomain(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetCullingMode")]
        public static extern byte GetCullingMode(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nIsColorWriteEnabled")]
        public static extern bool IsColorWriteEnabled(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nIsDepthWriteEnabled")]
        public static extern bool IsDepthWriteEnabled(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nIsDepthCullingEnabled")]
        public static extern bool IsDepthCullingEnabled(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nIsDoubleSided")]
        public static extern bool IsDoubleSided(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetMaskThreshold")]
        public static extern float GetMaskThreshold(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetSpecularAntiAliasingVariance")]
        public static extern float GetSpecularAntiAliasingVariance(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetSpecularAntiAliasingThreshold")]
        public static extern float GetSpecularAntiAliasingThreshold(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nGetParameterCount")]
        public static extern int GetParameterCount(IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nHasParameter")]
        public static extern bool HasParameter(IntPtr nativeMaterial, string name);
    }

    public static class MaterialBuilder
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Material_nBuilderBuild")]
        public static extern IntPtr Build(IntPtr nativeEngine, [MarshalAs(UnmanagedType.LPArray)] byte[] buffer, int bufferSizeInBytes);
    }
}
