using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class IndexBuffer
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndexBuffer_nSetBuffer")]
        public static extern void SetBufferUShort(IntPtr nativeVertexBuffer, IntPtr nativeEngine, ushort[] buffer, int bufferSizeInBytes, int destOffsetInBytes);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndexBuffer_nSetBuffer")]
        public static extern void SetBufferUInt(IntPtr nativeVertexBuffer, IntPtr nativeEngine, uint[] buffer, int bufferSizeInBytes, int destOffsetInBytes);
    }

    public static class IndexBufferBuilder
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndexBuffer_nCreateBuilder")]
        public static extern IntPtr CreateBuilder();

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndexBuffer_nDestroyBuilder")]
        public static extern void DestroyBuilder(IntPtr nativeBuilder);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndexBuffer_nBuilderIndexCount")]
        public static extern void IndexCount(IntPtr nativeBuilder, int indexCount);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndexBuffer_nBuilderBufferType")]
        public static extern void BufferType(IntPtr nativeBuilder, uint indexType);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_IndexBuffer_nBuilderBuild")]
        public static extern IntPtr Build(IntPtr nativeBuilder, IntPtr nativeEngine);
    }
}
