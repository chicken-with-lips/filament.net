using System;
using System.Runtime.InteropServices;

namespace Filament
{
    public enum IndexType : uint
    {
        /// <summary>16-bit indices</summary>
        UShort = ElementType.UShort,

        /// <summary>32-bit indices</summary>
        UInt = ElementType.UInt
    };

    /// <summary>
    /// A buffer containing vertex indices into a VertexBuffer.
    /// </summary>
    /// <remarks>
    /// <para>Indices can be 16 or 32 bit.</para>
    /// <para>The buffer itself is a GPU resource, therefore mutating the data can be relatively slow. Typically these
    /// buffers are constant.</para>
    ///<para>It is possible, and even encouraged, to use a single index buffer for several Renderables.</para>
    /// </remarks>
    public class IndexBuffer : FilamentBase<IndexBuffer>
    {
        #region Methods

        private IndexBuffer(IntPtr ptr) : base(ptr)
        {
        }

        internal static IndexBuffer GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new IndexBuffer(newPtr));
        }

        public void SetBuffer(Engine engine, uint[] buffer)
        {
            ThrowExceptionIfDisposed();

            Native.IndexBuffer.SetBufferUInt(NativePtr, engine.NativePtr, buffer, buffer.Length * Marshal.SizeOf<uint>(), 0);
        }

        public void SetBuffer(Engine engine, ushort[] buffer)
        {
            ThrowExceptionIfDisposed();

            Native.IndexBuffer.SetBufferUShort(NativePtr, engine.NativePtr, buffer, buffer.Length * Marshal.SizeOf<ushort>(), 0);
        }

        #endregion
    }

    /// <summary>
    /// Builder for building <see cref="IndexBuffer"/>.
    /// </summary>
    public class IndexBufferBuilder : FilamentBase<IndexBufferBuilder>
    {
        #region Methods

        private IndexBufferBuilder(IntPtr ptr) : base(ptr)
        {
        }

        internal static IndexBufferBuilder GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new IndexBufferBuilder(newPtr));
        }

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        /// <returns>A new builder.</returns>
        public static IndexBufferBuilder Create()
        {
            return GetOrCreateCache(
                Native.IndexBufferBuilder.CreateBuilder()
            );
        }

        /// <summary>
        /// Size of the index buffer in elements.
        /// </summary>
        /// <param name="indexCount">Number of indices the IndexBuffer can hold.</param>
        /// <returns>Reference to this builder for chaining calls.</returns>
        public IndexBufferBuilder WithIndexCount(int indexCount)
        {
            ThrowExceptionIfDisposed();

            Native.IndexBufferBuilder.IndexCount(NativePtr, indexCount);

            return this;
        }

        /// <summary>
        /// Type of the index buffer, 16-bit or 32-bit.
        /// </summary>
        /// <param name="indexType">Type of indices stored in the IndexBuffer.</param>
        /// <returns>Reference to this builder for chaining calls.</returns>
        public IndexBufferBuilder WithBufferType(IndexType indexType)
        {
            ThrowExceptionIfDisposed();

            Native.IndexBufferBuilder.BufferType(NativePtr, (uint) indexType);

            return this;
        }

        /// <summary>
        /// Creates the IndexBuffer object and returns a <see cref="IndexBuffer"> to it. After creation, the index
        /// buffer is uninitialized. Use IndexBuffer::setBuffer() to initialized the IndexBuffer.
        /// </summary>
        /// <param name="engine">Reference to the <see cref="Engine"/> to associate this IndexBuffer with.</param>
        /// <returns>The newly created object or null if exceptions are disabled and an error occurred.</returns>
        public IndexBuffer Build(Engine engine)
        {
            ThrowExceptionIfDisposed();

            return IndexBuffer.GetOrCreateCache(
                Native.IndexBufferBuilder.Build(NativePtr, engine.NativePtr)
            );
        }

        #endregion

        #region FilamentBase

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.IndexBufferBuilder.DestroyBuilder(NativePtr);
        }

        #endregion
    }
}
