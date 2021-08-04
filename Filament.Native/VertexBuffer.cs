using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class VertexBuffer
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nSetBufferAt")]
        public static extern void SetBufferAtByte(IntPtr nativeVertexBuffer, IntPtr nativeEngine, int bufferIndex, byte[] buffer, int bufferSizeInBytes, int destOffsetInBytes);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nSetBufferAt")]
        public static extern void SetBufferAtFloat(IntPtr nativeVertexBuffer, IntPtr nativeEngine, int bufferIndex, float[] buffer, int bufferSizeInBytes, int destOffsetInBytes);
    }

    public static class VertexBufferBuilder
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nCreateBuilder")]
        public static extern IntPtr CreateBuilder();

        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nDestroyBuilder")]
        public static extern void DestroyBuilder(IntPtr nativeBuilder);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nBuilderVertexCount")]
        public static extern void VertexCount(IntPtr nativeBuilder, int vertexCount);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nBuilderBufferCount")]
        public static extern void BufferCount(IntPtr nativeBuilder, int bufferCount);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nBuilderAttribute")]
        public static extern void Attribute(IntPtr nativeBuilder, int attribute, int bufferIndex, uint attributeType, int byteOffset, int byteStride);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nBuilderNormalized")]
        public static extern void Normalized(IntPtr nativeBuilder, int attribute, bool normalized);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nBuilderBuild")]
        public static extern IntPtr Build(IntPtr nativeBuilder, IntPtr nativeEngine);
    }
}
