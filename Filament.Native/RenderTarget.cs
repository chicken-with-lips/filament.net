using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class RenderTarget
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderTarget_nGetMipLevel")]
        public static extern int GetMipLevel(IntPtr nativeBuilder, int attachment);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderTarget_nGetFace")]
        public static extern byte GetFace(IntPtr nativeBuilder, int attachment);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderTarget_nGetLayer")]
        public static extern int GetLayer(IntPtr nativeBuilder, int attachment);
    }

    public static class RenderTargetBuilder
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderTarget_nCreateBuilder")]
        public static extern IntPtr Create();

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderTarget_nDestroyBuilder")]
        public static extern void Destroy(IntPtr nativeBuilder);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderTarget_nBuilderTexture")]
        public static extern void Texture(IntPtr nativeBuilder, int attachment, IntPtr nativeTexture);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderTarget_nBuilderMipLevel")]
        public static extern void MipLevel(IntPtr nativeBuilder, int attachment, int level);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderTarget_nBuilderFace")]
        public static extern void Face(IntPtr nativeBuilder, int attachment, byte face);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderTarget_nBuilderLayer")]
        public static extern void Layer(IntPtr nativeBuilder, int attachment, int layer);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderTarget_nBuilderBuild")]
        public static extern IntPtr Build(IntPtr nativeBuilder, IntPtr nativeEngine);
    }
}
