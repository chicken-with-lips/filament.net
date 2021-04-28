using System;
using System.Runtime.InteropServices;
using OpenTK.Mathematics;
using Half = OpenTK.Mathematics.Half;

namespace Filament
{
    public class VertexBuffer : FilamentBase<VertexBuffer>
    {
        #region Methods

        private VertexBuffer(IntPtr ptr) : base(ptr)
        {
        }

        internal static VertexBuffer GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new VertexBuffer(newPtr));
        }

        public void SetBufferAt(Engine engine, int bufferIndex, VertexBufferObject buffer)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBuffer.SetBufferAtByte(NativePtr, engine.NativePtr, bufferIndex, buffer.ToArray(), (int) buffer.Length, 0);
        }

        public void SetBufferAt(Engine engine, int bufferIndex, Vector2[] buffer)
        {
            ThrowExceptionIfDisposed();

            NativeSetBufferAtVector2(NativePtr, engine.NativePtr, bufferIndex, buffer, buffer.Length * 2 * Marshal.SizeOf<float>(), 0);
        }

        public void SetBufferAt(Engine engine, int bufferIndex, Vector3[] buffer)
        {
            ThrowExceptionIfDisposed();

            NativeSetBufferAtVector3(NativePtr, engine.NativePtr, bufferIndex, buffer, buffer.Length * 3 * Marshal.SizeOf<float>(), 0);
        }

        public void SetBufferAt(Engine engine, int bufferIndex, Vector4h[] buffer)
        {
            ThrowExceptionIfDisposed();

            NativeSetBufferAtVector4h(NativePtr, engine.NativePtr, bufferIndex, buffer, buffer.Length * 4 * Half.SizeInBytes, 0);
        }

        public void SetBufferAt(Engine engine, int bufferIndex, byte[] buffer)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBuffer.SetBufferAtByte(NativePtr, engine.NativePtr, bufferIndex, buffer, buffer.Length, 0);
        }

        public void SetBufferAt(Engine engine, int bufferIndex, float[] buffer)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBuffer.SetBufferAtFloat(NativePtr, engine.NativePtr, bufferIndex, buffer, buffer.Length, 0);
        }

        #endregion

        #region P/Invoke

        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nSetBufferAt", CallingConvention = CallingConvention.Cdecl)]
        private static extern void NativeSetBufferAtVector4h(IntPtr nativeVertexBuffer, IntPtr nativeEngine, int bufferIndex, Vector4h[] buffer, int bufferSizeInBytes, int destOffsetInBytes);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nSetBufferAt", CallingConvention = CallingConvention.Cdecl)]
        private static extern void NativeSetBufferAtVector3(IntPtr nativeVertexBuffer, IntPtr nativeEngine, int bufferIndex, Vector3[] buffer, int bufferSizeInBytes, int destOffsetInBytes);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_VertexBuffer_nSetBufferAt", CallingConvention = CallingConvention.Cdecl)]
        private static extern void NativeSetBufferAtVector2(IntPtr nativeVertexBuffer, IntPtr nativeEngine, int bufferIndex, Vector2[] buffer, int bufferSizeInBytes, int destOffsetInBytes);

        #endregion
    }

    public class VertexBufferBuilder : FilamentBase<VertexBufferBuilder>
    {
        #region Methods

        private VertexBufferBuilder(IntPtr ptr) : base(ptr)
        {
        }

        internal static VertexBufferBuilder GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new VertexBufferBuilder(newPtr));
        }

        public static VertexBufferBuilder Create()
        {
            return GetOrCreateCache(
                Native.VertexBufferBuilder.CreateBuilder()
            );
        }

        public VertexBufferBuilder WithVertexCount(int vertexCount)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBufferBuilder.VertexCount(NativePtr, vertexCount);

            return this;
        }

        public VertexBufferBuilder WithBufferCount(int bufferCount)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBufferBuilder.BufferCount(NativePtr, (byte) bufferCount);

            return this;
        }

        public VertexBufferBuilder WithAttribute(VertexAttribute attribute, int bufferIndex, ElementType attributeType, int byteOffset = 0, int byteStride = 0)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBufferBuilder.Attribute(NativePtr, (int) attribute, bufferIndex, (uint) attributeType, byteOffset, byteStride);

            return this;
        }

        public VertexBufferBuilder WithNormalized(VertexAttribute attribute, bool normalized = true)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBufferBuilder.Normalized(NativePtr, (int) attribute, normalized);

            return this;
        }

        public VertexBuffer Build(Engine engine)
        {
            ThrowExceptionIfDisposed();

            return VertexBuffer.GetOrCreateCache(
                Native.VertexBufferBuilder.Build(NativePtr, engine.NativePtr)
            );
        }

        #endregion

        #region FilamentBase

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.VertexBufferBuilder.DestroyBuilder(NativePtr);
        }

        #endregion
    }
}
