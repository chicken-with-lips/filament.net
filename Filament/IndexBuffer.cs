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

        public static IndexBufferBuilder Create()
        {
            return GetOrCreateCache(
                Native.IndexBufferBuilder.CreateBuilder()
            );
        }

        public IndexBufferBuilder WithIndexCount(int indexCount)
        {
            ThrowExceptionIfDisposed();

            Native.IndexBufferBuilder.IndexCount(NativePtr, indexCount);

            return this;
        }

        public IndexBufferBuilder WithBufferType(IndexType indexType)
        {
            ThrowExceptionIfDisposed();

            Native.IndexBufferBuilder.BufferType(NativePtr, (uint) indexType);

            return this;
        }

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
