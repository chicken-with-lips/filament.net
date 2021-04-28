using System;

namespace Filament
{
    public enum AttachmentPoint
    {
        Color = 0,
        Depth = 1,
    }

    public class RenderTarget : FilamentBase<RenderTarget>
    {
        #region Methods

        private RenderTarget(IntPtr ptr) : base(ptr)
        {
        }

        internal static RenderTarget GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new RenderTarget(newPtr));
        }

        public int GetMipLevel(AttachmentPoint attachment)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderTarget.GetMipLevel(NativePtr, (int) attachment);
        }

        public TextureCubemapFace GetFace(AttachmentPoint attachment)
        {
            ThrowExceptionIfDisposed();

            return (TextureCubemapFace) Native.RenderTarget.GetFace(NativePtr, (int) attachment);
        }

        public int GetLayer(AttachmentPoint attachment)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderTarget.GetLayer(NativePtr, (int) attachment);
        }

        #endregion
    }

    public class RenderTargetBuilder : FilamentBase<RenderTargetBuilder>
    {
        #region Methods

        private RenderTargetBuilder(IntPtr ptr) : base(ptr)
        {
        }

        internal static RenderTargetBuilder GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new RenderTargetBuilder(newPtr));
        }

        public static RenderTargetBuilder Create()
        {
            return GetOrCreateCache(
                Native.RenderTargetBuilder.Create()
            );
        }

        public RenderTargetBuilder WithTexture(AttachmentPoint attachment, Texture texture)
        {
            ThrowExceptionIfDisposed();

            Native.RenderTargetBuilder.Texture(NativePtr, (int) attachment, texture.NativePtr);

            return this;
        }

        public RenderTargetBuilder WithMipLevel(AttachmentPoint attachment, int level)
        {
            ThrowExceptionIfDisposed();

            Native.RenderTargetBuilder.MipLevel(NativePtr, (int) attachment, level);

            return this;
        }

        public RenderTargetBuilder WithFace(AttachmentPoint attachment, TextureCubemapFace face)
        {
            ThrowExceptionIfDisposed();

            Native.RenderTargetBuilder.Face(NativePtr, (int) attachment, (byte) face);

            return this;
        }

        public RenderTargetBuilder WithLayer(AttachmentPoint attachment, int layer)
        {
            ThrowExceptionIfDisposed();

            Native.RenderTargetBuilder.Layer(NativePtr, (int) attachment, layer);

            return this;
        }

        public RenderTarget Build(Engine engine)
        {
            ThrowExceptionIfDisposed();

            return RenderTarget.GetOrCreateCache(
                Native.RenderTargetBuilder.Build(NativePtr, engine.NativePtr)
            );
        }

        #endregion

        #region FilamentBase

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.RenderTargetBuilder.Destroy(NativePtr);
        }

        #endregion
    }
}
