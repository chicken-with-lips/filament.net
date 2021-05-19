using System;

namespace Filament
{
    public enum AttachmentPoint
    {
        Color = 0,
        Depth = 1,
    }

    /// <summary>
    /// An offscreen render target that can be associated with a View and contains weak references to a set of attached
    /// Texture objects.
    /// </summary>
    /// <remarks>Clients are responsible for the lifetime of all associated Texture attachments.</remarks>
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

        /// <summary>
        /// Returns the mipmap level set on the given attachment point.
        /// </summary>
        /// <param name="attachment">Attachment point.</param>
        /// <returns>The mipmap level set on the given attachment point.</returns>
        public int GetMipLevel(AttachmentPoint attachment)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderTarget.GetMipLevel(NativePtr, (int) attachment);
        }

        /// <summary>
        /// Returns the face of a cubemap set on the given attachment point.
        /// </summary>
        /// <param name="attachment">Attachment point.</param>
        /// <returns>A cubemap face identifier. This is only relevant if the attachment's texture is a cubemap.</returns>
        public TextureCubemapFace GetFace(AttachmentPoint attachment)
        {
            ThrowExceptionIfDisposed();

            return (TextureCubemapFace) Native.RenderTarget.GetFace(NativePtr, (int) attachment);
        }

        /// <summary>
        /// Returns the texture-layer set on the given attachment point.
        /// </summary>
        /// <param name="attachment">Attachment point.</param>
        /// <returns>A texture layer. This is only relevant if the attachment's texture is a 3D texture.</returns>
        public int GetLayer(AttachmentPoint attachment)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderTarget.GetLayer(NativePtr, (int) attachment);
        }

        #endregion
    }

    /// <summary>
    /// Use Builder to construct a RenderTarget objects.
    /// </summary>
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

        /// <summary>
        /// Sets a texture to a given attachment point. All RenderTargets must have a non-null COLOR attachment.
        /// </summary>
        /// <param name="attachment">The attachment point of the texture.</param>
        /// <param name="texture">The associated texture object.</param>
        /// <returns>A reference to this Builder for chaining calls.</returns>
        public RenderTargetBuilder WithTexture(AttachmentPoint attachment, Texture texture)
        {
            ThrowExceptionIfDisposed();

            Native.RenderTargetBuilder.Texture(NativePtr, (int) attachment, texture.NativePtr);

            return this;
        }

        /// <summary>
        /// Sets the mipmap level for a given attachment point.
        /// </summary>
        /// <param name="attachment">The attachment point of the texture.</param>
        /// <param name="level">The associated mipmap level, 0 by default.</param>
        /// <returns>A reference to this Builder for chaining calls.</returns>
        public RenderTargetBuilder WithMipLevel(AttachmentPoint attachment, int level = 0)
        {
            ThrowExceptionIfDisposed();

            Native.RenderTargetBuilder.MipLevel(NativePtr, (int) attachment, level);

            return this;
        }

        /// <summary>
        /// Sets the cubemap face for a given attachment point.
        /// </summary>
        /// <param name="attachment">The attachment point.</param>
        /// <param name="face">The associated cubemap face.</param>
        /// <returns>A reference to this Builder for chaining calls.</returns>
        public RenderTargetBuilder WithFace(AttachmentPoint attachment, TextureCubemapFace face)
        {
            ThrowExceptionIfDisposed();

            Native.RenderTargetBuilder.Face(NativePtr, (int) attachment, (byte) face);

            return this;
        }

        /// <summary>
        /// Sets the layer for a given attachment point (for 3D textures).
        /// </summary>
        /// <param name="attachment">The attachment point.</param>
        /// <param name="layer">The associated cubemap layer.</param>
        /// <returns>A reference to this Builder for chaining calls.</returns>
        public RenderTargetBuilder WithLayer(AttachmentPoint attachment, int layer)
        {
            ThrowExceptionIfDisposed();

            Native.RenderTargetBuilder.Layer(NativePtr, (int) attachment, layer);

            return this;
        }

        /// <summary>
        /// Creates the RenderTarget object.
        /// </summary>
        /// <returns>The newly created object or null if exceptions are disabled and an error occurred.</returns>
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
