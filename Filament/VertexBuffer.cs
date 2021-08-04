using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Filament
{
    /// <summary>
    /// <para>Holds a set of buffers that define the geometry of a Renderable.</para>
    /// <para>The geometry of the Renderable itself is defined by a set of vertex attributes such as position, color,
    /// normals, tangents, etc.</para>
    /// <para>There is no need to have a 1-to-1 mapping between attributes and buffer. A buffer can hold the data of
    /// several attributes -- attributes are then referred as being "interleaved".</para>
    /// <para>The buffers themselves are GPU resources, therefore mutating their data can be relatively slow. For this
    /// reason, it is best to separate the constant data from the dynamic data into multiple buffers.</para>
    /// <para>It is possible, and even encouraged, to use a single vertex buffer for several Renderables.</para>
    /// </summary>
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

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        public static VertexBufferBuilder Create()
        {
            return GetOrCreateCache(
                Native.VertexBufferBuilder.CreateBuilder()
            );
        }

        /// <summary>
        /// Size of each buffer in the set in vertex.
        /// </summary>
        /// <param name="vertexCount">Number of vertices in each buffer in this set.</param>
        /// <returns>This Builder for chaining calls.</returns>
        public VertexBufferBuilder WithVertexCount(int vertexCount)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBufferBuilder.VertexCount(NativePtr, vertexCount);

            return this;
        }

        /// <summary>
        /// <para>Defines how many buffers will be created in this vertex buffer set. These buffers are later referenced
        /// by index from 0 to \p bufferCount - 1.</para>
        /// <para>This call is mandatory. The default is 0.</para>
        /// </summary>
        /// <param name="bufferCount">Number of buffers in this vertex buffer set. The maximum value is 8.</param>
        /// <returns>This Builder for chaining calls.</returns>
        public VertexBufferBuilder WithBufferCount(int bufferCount)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBufferBuilder.BufferCount(NativePtr, (byte) bufferCount);

            return this;
        }

        /// <summary>
        /// <para>Sets up an attribute for this vertex buffer set.</para>
        /// <para>Using <param name="byteOffset"/> and <param name="byteStride"/>, attributes can be interleaved in the
        /// same buffer.</para>
        /// <para>Warning: <see cref="VertexAttribute.Tangents"/> must be specified as a quaternion and is how normals
        /// are specified.</para>
        /// </summary>
        /// <param name="attribute">The attribute to set up.</param>
        /// <param name="bufferIndex">The index of the buffer containing the data for this attribute. Must  be between
        /// 0 and bufferCount() - 1.</param>
        /// <param name="attributeType">The type of the attribute data (e.g. byte, float3, etc...).</param>
        /// <param name="byteOffset">Offset in *bytes* into the buffer <param name="bufferIndex"/>.</param>
        /// <param name="byteStride">Stride in *bytes* to the next element of this attribute. When set to  zero the
        /// attribute size, as defined by \p attributeType is used.</param>
        /// <returns>This Builder for chaining calls.</returns>
        public VertexBufferBuilder WithAttribute(VertexAttribute attribute, int bufferIndex, ElementType attributeType, int byteOffset = 0, int byteStride = 0)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBufferBuilder.Attribute(NativePtr, (int) attribute, bufferIndex, (uint) attributeType, byteOffset, byteStride);

            return this;
        }

        /// <summary>
        /// Sets whether a given attribute should be normalized. By default attributes are not normalized. A normalized
        /// attribute is mapped between 0 and 1 in the shader. This applies only to integer types.
        /// </summary>
        /// <param name="attribute">Enum of the attribute to set the normalization flag to.</param>
        /// <param name="normalized">True to automatically normalize the given attribute.</param>
        /// <returns>This Builder for chaining calls.</returns>
        public VertexBufferBuilder WithNormalized(VertexAttribute attribute, bool normalized = true)
        {
            ThrowExceptionIfDisposed();

            Native.VertexBufferBuilder.Normalized(NativePtr, (int) attribute, normalized);

            return this;
        }

        /// <summary>
        /// Creates the VertexBuffer object and returns a pointer to it.
        /// </summary>
        /// <param name="engine">Reference to the <see cref="Engine"/> to associate this VertexBuffer with.</param>
        /// <returns>The newly created object or nullptr if exceptions are disabled and an error occurred.</returns>
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
