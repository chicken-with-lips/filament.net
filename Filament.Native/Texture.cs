using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class Texture
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nGetWidth")]
        public static extern int GetWidth(IntPtr nativeTexture, int level);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nGetHeight")]
        public static extern int GetHeight(IntPtr nativeTexture, int level);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nGetDepth")]
        public static extern int GetDepth(IntPtr nativeTexture, int level);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nGetLevels")]
        public static extern int GetLevels(IntPtr nativeTexture);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nGetTarget")]
        public static extern int GetTarget(IntPtr nativeTexture);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nGetInternalFormat")]
        public static extern ushort GetInternalFormat(IntPtr nativeTexture);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nGenerateMipmaps")]
        public static extern ushort GenerateMipmaps(IntPtr nativeTexture, IntPtr nativeEngine);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nSetImage")]
        public static extern ushort SetImage(IntPtr nativeTexture, IntPtr nativeEngine,
            int level, int xOffset, int yOffset,
            int width, int height, byte[] buffer, int bufferSizeInBytes,
            int left, int top, byte type, int alignment, int stride, byte format);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nSetImageLinear")]
        public static extern ushort SetImageLinear(IntPtr nativeTexture, IntPtr nativeEngine,
            int level, int xOffset, int yOffset,
            int width, int height, IntPtr nativeLinearImage,
            int left, int top, byte type, int alignment, int stride, byte format);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nSetImageCubemap")]
        public static extern ushort SetImageCubemap(IntPtr nativeTexture, IntPtr nativeEngine, int level,
            byte[] buffer, int bufferSizeInBytes,
            int left, int top, byte type, int alignment, int stride, byte format, int[] faceOffsets);
    }

    public static class TextureBuilder
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nCreateBuilder")]
        public static extern IntPtr CreateBuilder();

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nDestroyBuilder")]
        public static extern void DestroyBuilder(IntPtr nativeBuilder);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nBuilderWidth")]
        public static extern void Width(IntPtr nativeBuilder, int width);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nBuilderHeight")]
        public static extern void Height(IntPtr nativeBuilder, int height);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nBuilderDepth")]
        public static extern void Depth(IntPtr nativeBuilder, int depth);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nBuilderLevels")]
        public static extern void Levels(IntPtr nativeBuilder, int levels);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nBuilderSampler")]
        public static extern void Sampler(IntPtr nativeBuilder, int samplerType);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nBuilderFormat")]
        public static extern void Format(IntPtr nativeBuilder, ushort format);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nBuilderUsage")]
        public static extern void Usage(IntPtr nativeBuilder, byte format);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nBuilderSwizzle")]
        public static extern void Swizzle(IntPtr nativeBuilder, int r, int g, int b, int a);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Texture_nBuilderBuild")]
        public static extern IntPtr Build(IntPtr nativeBuilder, IntPtr nativeEngine);
    }
}
