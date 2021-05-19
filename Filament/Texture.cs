using System;

namespace Filament
{
    /// <summary>
    /// Texture class supporting 2D and 3D textures, cube maps an mip-mapping.
    /// </summary>
    /// <remarks>A Texture is created by using <see cref="TextureBuilder"/>.
    public class Texture : FilamentBase<Texture>
    {
        #region Properties

        /// <summary>
        /// <para>Returns the maximum number of levels this texture can have.</para>
        /// <para>Attention: If this texture is using <see cref="TextureSamplerType.External"/>, the dimension of the
        /// texture are unknown and this method always returns whatever was set on the Builder.</para>
        /// </summary>
        /// <value>Maximum number of levels this texture can have.</value>
        public int Levels {
            get {
                ThrowExceptionIfDisposed();

                return Native.Texture.GetLevels(NativePtr);
            }
        }

        /// <summary>
        /// This texture Sampler as set by <see cref="TextureBuilder.WithSampler"/>.
        /// </summary>
        public TextureSamplerType Target {
            get {
                ThrowExceptionIfDisposed();

                return (TextureSamplerType) Native.Texture.GetTarget(NativePtr);
            }
        }

        /// <summary>
        /// Return this texture InternalFormat as set by <see cref="TextureBuilder.WithFormat"/>.
        /// </summary>
        public TextureFormat InternalFormat {
            get {
                ThrowExceptionIfDisposed();

                return (TextureFormat) Native.Texture.GetInternalFormat(NativePtr);
            }
        }

        #endregion

        #region Methods

        private Texture(IntPtr ptr) : base(ptr)
        {
        }

        internal static Texture GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new Texture(newPtr));
        }

        /// <summary>
        /// <para>Returns the width of a 2D or 3D texture level.</para>
        /// <para>Attention: If this texture is using <see cref="TextureSamplerType.External"/>, the dimension of the
        /// texture are unknown and this method always returns whatever was set on the Builder.</para>
        /// </summary>
        /// <param name="level">Texture level.</param>
        /// <returns>Width in texel of the specified level, clamped to 1.</returns>
        public int GetWidth(int level)
        {
            ThrowExceptionIfDisposed();

            return Native.Texture.GetWidth(NativePtr, level);
        }

        /// <summary>
        /// <para>Returns the height of a 2D or 3D texture level.</para>
        /// <para>Attention: If this texture is using <see cref="TextureSamplerType.External"/>, the dimension of the
        /// texture are unknown and this method always returns whatever was set on the Builder.</para>
        /// </summary>
        /// <param name="level">Texture level.</param>
        /// <returns>Height in texel of the specified level, clamped to 1.</returns>
        public int GetHeight(int level)
        {
            ThrowExceptionIfDisposed();

            return Native.Texture.GetHeight(NativePtr, level);
        }

        /// <summary>
        /// <para>Returns the depth of a 3D texture level.</para>
        /// <para>Attention: If this texture is using <see cref="TextureSamplerType.External"/>, the dimension of the
        /// texture are unknown and this method always returns whatever was set on the Builder.</para>
        /// </summary>
        /// <param name="level">Texture level.</param>
        /// <returns>Depth in texel of the specified level, clamped to 1.</returns>
        public int GetDepth(int level)
        {
            ThrowExceptionIfDisposed();

            return Native.Texture.GetDepth(NativePtr, level);
        }

        public void SetImage(Engine engine, int level, PixelBufferDescriptor buffer)
        {
            ThrowExceptionIfDisposed();

            SetImage(engine, level, 0, 0, GetWidth(level), GetHeight(level), buffer);
        }

        /// <summary>
        /// <para>Updates a sub-image of a 2D texture for a level.</para>
        /// <para>Attention: Buffer's <see cref="TextureFormat"/> must match that of <see cref="InternalFormat"/>.</para>
        /// <para>This Texture instance must use <see cref="TextureSamplerType.Texture2d"/> or
        /// <see cref="TextureSamplerType.External"/>. IF the later is specified and external textures are supported by
        /// the driver implementation, this method will have no effect, otherwise it will behave as if the texture was
        /// specified with <see cref="TextureSamplerType.Texture2d"/>.</para>
        /// </summary>
        /// <param name="engine">Engine this texture is associated to.</param>
        /// <param name="level">Level to set the image for.</param>
        /// <param name="xOffset">Left offset of the sub-region to update.</param>
        /// <param name="yOffset">Bottom offset of the sub-region to update.</param>
        /// <param name="width">Width of the sub-region to update.</param>
        /// <param name="height">Height of the sub-region to update.</param>
        /// <param name="descriptor">Client-side buffer containing the image to set.</param>
        public void SetImage(Engine engine, int level, int xOffset, int yOffset, int width, int height, PixelBufferDescriptor descriptor)
        {
            ThrowExceptionIfDisposed();

            if (descriptor.Type == PixelDataType.Compressed) {
                throw new NotSupportedException();
            }

            if (null != descriptor.LinearImage) {
                Native.Texture.SetImageLinear(NativePtr, engine.NativePtr, level,
                    xOffset, yOffset, width, height,
                    descriptor.LinearImage.NativePtr,
                    descriptor.Left, descriptor.Top, (byte) descriptor.Type, descriptor.Alignment,
                    descriptor.Stride, (byte) descriptor.Format);
            } else {
                Native.Texture.SetImage(NativePtr, engine.NativePtr, level,
                    xOffset, yOffset, width, height,
                    descriptor.Buffer, descriptor.Buffer.Length,
                    descriptor.Left, descriptor.Top, (byte) descriptor.Type, descriptor.Alignment,
                    descriptor.Stride, (byte) descriptor.Format);
            }
        }

        /// <summary>
        /// <para>Updates a sub-image of a 3D texture or 2D texture array for a level.</para>
        /// <para>Attention: Buffer's <see cref="TextureFormat"/> must match that of <see cref="InternalFormat"/>.</para>
        /// <para>This Texture instance must use <see cref="TextureSamplerType.Texture3d"/> or
        /// <see cref="TextureSamplerType.External"/>.</para>
        /// </summary>
        /// <param name="engine">Engine this texture is associated to.</param>
        /// <param name="level">Level to set the image for.</param>
        /// <param name="xOffset">Left offset of the sub-region to update.</param>
        /// <param name="yOffset">Bottom offset of the sub-region to update.</param>
        /// <param name="zOffset">Depth offset of the sub-region to update.</param>
        /// <param name="width">Width of the sub-region to update.</param>
        /// <param name="height">Height of the sub-region to update.</param>
        /// <param name="descriptor">Client-side buffer containing the image to set.</param>
        public void SetImage(Engine engine, int level, int xOffset, int yOffset, int zOffset, int width, int height, int depth, PixelBufferDescriptor descriptor)
        {
            ThrowExceptionIfDisposed();

            if (descriptor.Type == PixelDataType.Compressed) {
                throw new NotSupportedException();
            } else {
                throw new NotSupportedException();
            }
        }

        /// <summary>
        /// <para>Specify all six images of a cube map level.</para>
        /// <para>This method follows exactly the OpenGL conventions.</para>
        /// <para>Attention: Buffer's <see cref="TextureFormat"/> must match that of <see cref="InternalFormat"/>.</para>
        /// <para>This Texture instance must use <see cref="TextureSamplerType.Cubemap"/> or it has no effect</para>
        /// </summary>
        /// <param name="engine">Engine this texture is associated to.</param>
        /// <param name="level">Level to set the image for.</param>
        /// <param name="descriptor">Client-side buffer containing the images to set.</param>
        /// <param name="faceOffsets">Offsets in bytes into \p buffer for all six images. The offsets are specified in
        /// the following order: +x, -x, +y, -y, +z, -z</param>
        public void SetImage(Engine engine, int level, PixelBufferDescriptor descriptor, FaceOffsets faceOffsets)
        {
            ThrowExceptionIfDisposed();

            if (null != descriptor.LinearImage) {
                throw new NotImplementedException();
            }

            if (descriptor.Type == PixelDataType.Compressed) {
                throw new NotImplementedException();
            }

            Native.Texture.SetImageCubemap(NativePtr, engine.NativePtr, level,
                descriptor.Buffer, descriptor.Buffer.Length,
                descriptor.Left, descriptor.Top, (byte) descriptor.Type, descriptor.Alignment,
                descriptor.Stride, (byte) descriptor.Format,
                faceOffsets.ToArray());
        }

        /// <summary>
        /// <para>Generates all the mipmap levels automatically. This requires the texture to have a color-renderable
        /// format.</para>
        /// <para>Attention:This Texture instance must NOT use <see cref="TextureSamplerType.Cubemap"/> or it has no
        /// effect</para>
        /// </summary>
        /// <param name="engine">Engine this texture is associated to.</param>
        public void GenerateMipmaps(Engine engine)
        {
            ThrowExceptionIfDisposed();

            Native.Texture.GenerateMipmaps(NativePtr, engine.NativePtr);
        }

        #endregion
    }

    /// <summary>
    /// Use Builder to construct a Texture object instance.
    /// </summary>
    public class TextureBuilder : FilamentBase<TextureBuilder>
    {
        #region Methods

        private TextureBuilder(IntPtr ptr) : base(ptr)
        {
        }

        internal static TextureBuilder GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new TextureBuilder(newPtr));
        }

        /// <summary>
        /// Specifies the width in texels of the texture. Doesn't need to be a power-of-two.
        /// </summary>
        /// <param name="width">Width of the texture in texels (default: 1).</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public TextureBuilder WithWidth(int width)
        {
            ThrowExceptionIfDisposed();

            Native.TextureBuilder.Width(NativePtr, width);

            return this;
        }

        /// <summary>
        ///Specifies the height in texels of the texture. Doesn't need to be a power-of-two.
        /// </summary>
        /// <param name="height">Height of the texture in texels (default: 1).</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public TextureBuilder WithHeight(int height)
        {
            ThrowExceptionIfDisposed();

            Native.TextureBuilder.Height(NativePtr, height);

            return this;
        }

        /// <summary>
        /// <para>Specifies the depth in texels of the texture. Doesn't need to be a power-of-two.</para>
        /// <para>The depth controls the number of layers in a 2D array texture. Values greater than 1 effectively
        /// create a 3D texture.</para>
        /// <para>Attention: This Texture instance must use <see cref="TextureSamplerType.Texture3d"/> or
        /// <see cref="TextureSamplerType.Texture2dArray"/> or it has no effect.</para>
        /// </summary>
        /// <param name="depth">Depth of the texture in texels (default: 1).</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public TextureBuilder WithDepth(int depth)
        {
            ThrowExceptionIfDisposed();

            Native.TextureBuilder.Depth(NativePtr, depth);

            return this;
        }

        /// <summary>
        /// <para>Specifies the numbers of mip map levels.</para>
        /// <para>This creates a mip-map pyramid. The maximum number of levels a texture can have is such that
        /// max(width, height, level) / 2^MAX_LEVELS = 1.</para>
        /// </summary>
        /// <param name="levels">Number of mipmap levels for this texture.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public TextureBuilder WithLevels(int levels)
        {
            ThrowExceptionIfDisposed();

            Native.TextureBuilder.Levels(NativePtr, levels);

            return this;
        }

        /// <summary>
        /// Specifies the type of sampler to use.
        /// </summary>
        /// <param name="samplerType">Sampler type.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public TextureBuilder WithSampler(TextureSamplerType samplerType)
        {
            ThrowExceptionIfDisposed();

            Native.TextureBuilder.Sampler(NativePtr, (int) samplerType);

            return this;
        }

        /// <summary>
        /// <para>Specifies the internal format of this texture.</para>
        /// <para>The internal format specifies how texels are stored (which may be different from how they'r
        /// e specified in SetImage). InternalFormat specifies both the color components and the data type used.</para>
        /// </summary>
        /// </summary>
        /// <param name="format">Format of the texture's texel.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public TextureBuilder WithFormat(TextureFormat format)
        {
            ThrowExceptionIfDisposed();

            Native.TextureBuilder.Format(NativePtr, (byte) format);

            return this;
        }

        /// <summary>
        /// <para>Specifies if the texture will be used as a render target attachment.</para>
        /// <para>If the texture is potentially rendered into, it may require a different memory layout, which needs to
        /// be known during construction.</para>
        /// </summary>
        /// <param name="usage">Defaults to <see cref="TextureUsage.Default"/>; c.f.
        /// <see cref="TextureUsage.ColorAttachment"/>.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public TextureBuilder WithUsage(TextureUsage usage)
        {
            ThrowExceptionIfDisposed();

            Native.TextureBuilder.Usage(NativePtr, (byte) usage);

            return this;
        }

        /// <summary>
        /// Specifies how a texture's channels map to color components.
        /// </summary>
        /// <param name="r">Channel for red component.</param>
        /// <param name="g">Channel for green component.</param>
        /// <param name="b">Channel for blue component.</param>
        /// <param name="a">Channel for alpha component.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public TextureBuilder WithSwizzle(TextureSwizzle r, TextureSwizzle g, TextureSwizzle b, TextureSwizzle a)
        {
            ThrowExceptionIfDisposed();

            Native.TextureBuilder.Swizzle(NativePtr, (int) r, (int) g, (int) b, (int) a);

            return this;
        }

        /// <summary>
        /// Creates the Texture object and returns a pointer to it.
        /// </summary>
        /// <param name="engine">Reference to the <see cref="Engine"/> to associate this Texture with.</param>
        /// <returns>The newly created object or nullptr if exceptions are disabled and an error occurred.</returns>
        public Texture Build(Engine engine)
        {
            ThrowExceptionIfDisposed();

            return Texture.GetOrCreateCache(
                Native.TextureBuilder.Build(NativePtr, engine.NativePtr)
            );
        }

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        public static TextureBuilder Create()
        {
            return GetOrCreateCache(
                Native.TextureBuilder.CreateBuilder()
            );
        }

        #endregion

        #region FilamentBase

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.TextureBuilder.DestroyBuilder(NativePtr);
        }

        #endregion
    }

    public enum TextureSamplerType
    {
        /// <summary>2D texture</summary>
        Texture2d,

        /// <summary>2D array texture</summary>
        Texture2dArray,

        /// <summary>Cube map texture</summary>
        Cubemap,

        /// <summary>External texture</summary>
        External,

        /// <summary>3D texture</summary>
        Texture3d
    }

    public enum TextureFormat : ushort
    {
        // 8-bits per element
        R8,
        R8_SNorm,
        R8Ui,
        R8I,
        Stencil8,

        // 16-bits per element
        R16F,
        R16Ui,
        R16I,
        Rg8,
        Rg8_SNorm,
        Rg8Ui,
        RG8I,
        Rgb565,
        Rgb9_E5, // 9995 is actually 32 bpp but it's here for historical reasons.
        Rgb5_A1,
        Rgba4,
        Depth16,

        // 24-bits per element
        Rgb8,
        SRgb8,
        Rgb8_SNorm,
        Rgb8Ui,
        Rgb8I,
        Depth24,

        // 32-bits per element
        R32F,
        R32Ui,
        R32I,
        RG16F,
        RG16Ui,
        RG16I,
        R11F_G11F_B10F,
        Rgba8,
        SRgb8_A8,
        Rgba8SNorm,
        Unused, // used to be rgbm
        Rgb10_A2,
        Rgba8Ui,
        Rgba8I,
        Depth32F,
        Depth24_Stencil8,
        Depth32F_Stencil8,

        // 48-bits per element
        Rgb16F,
        Rgb16Ui,
        Rgb16I,

        // 64-bits per element
        Rg32F,
        Rg32Ui,
        Rg32I,
        Rgba16F,
        Rgba16Ui,
        Rgba16I,

        // 96-bits per element
        Rgb32F,
        Rgb32Ui,
        Rgb32I,

        // 128-bits per element
        Rgba32F,
        Rgba32Ui,
        Rgba32I,

        // compressed formats

        // Mandatory in GLES 3.0 and GL 4.3
        Eac_R11,
        Eac_R11_Signed,
        Eac_Rg11,
        Eac_Rg11_Signed,
        Etc2_Rgb8,
        Etc2_SRgb8,
        Etc2_Rgb8_A1,
        Etc2_SRgb8_A1,
        Etc2_Eac_Rgba8,
        Etc2_Eac_SRgba8,

        // Available everywhere except Android/iOS
        Dxt1_Rgb,
        Dxt1_Rgba,
        Dxt3_Rgba,
        Dxt5_Rgba,
        Dxt1_SRgb,
        Dxt1_SRgba,
        Dxt3_SRgba,
        Dxt5_SRgba,

        // Astc formats are available with a GLES extension
        Rgba_Astc_4x4,
        Rgba_Astc_5x4,
        Rgba_Astc_5x5,
        Rgba_Astc_6x5,
        Rgba_Astc_6x6,
        Rgba_Astc_8x5,
        Rgba_Astc_8x6,
        Rgba_Astc_8x8,
        Rgba_Astc_10x5,
        Rgba_Astc_10x6,
        Rgba_Astc_10x8,
        Rgba_Astc_10x10,
        Rgba_Astc_12x10,
        Rgba_Astc_12x12,
        SRgb8_Alpha8_Astc_4x4,
        SRgb8_Alpha8_Astc_5x4,
        SRgb8_Alpha8_Astc_5x5,
        SRgb8_Alpha8_Astc_6x5,
        SRgb8_Alpha8_Astc_6x6,
        SRgb8_Alpha8_Astc_8x5,
        SRgb8_Alpha8_Astc_8x6,
        SRgb8_Alpha8_Astc_8x8,
        SRgb8_Alpha8_Astc_10x5,
        SRgb8_Alpha8_Astc_10x6,
        SRgb8_Alpha8_Astc_10x8,
        SRgb8_Alpha8_Astc_10x10,
        SRgb8_Alpha8_Astc_12x10,
        SRgb8_Alpha8_Astc_12x12,
    }

    /// <summary>
    /// Bitmask describing the intended Texture Usage
    /// </summary>
    [Flags]
    public enum TextureUsage : byte
    {
        /// <summary>Texture can be used as a color attachment</summary>
        ColorAttachment = 0x1,

        /// <summary>Texture can be used as a depth attachment</summary>
        DepthAttachment = 0x2,

        /// <summary>Texture can be used as a stencil attachment</summary>
        StencilAttachment = 0x4,

        /// <summary>Data can be uploaded into this texture (default)</summary>
        Uploadable = 0x8,

        /// <summary>Texture can be sampled (default)</summary>
        Sampleable = 0x10,

        /// <summary>Texture can be used as a subpass input</summary>
        SubpassInput = 0x20,

        /// <summary>Default texture usage</summary>
        Default = Uploadable | Sampleable
    }

    public enum TextureSwizzle
    {
        SubstituteZero,
        SubstituteOne,
        Channel0,
        Channel1,
        Channel2,
        Channel3
    }

    /// <summary>
    /// Sampler minification filter.
    /// </summary>
    public enum SamplerMinFilter : byte
    {
        /// <summary>No filtering. Nearest neighbor is used.</summary>
        Nearest = 0,

        /// <summary>Box filtering. Weighted average of 4 neighbors is used.</summary>
        Linear = 1,

        /// <summary>Mip-mapping is activated. But no filtering occurs.</summary>
        NearestMipmapNearest = 2,

        /// <summary>Box filtering within a mip-map level.</summary>
        LinearMipmapNearest = 3,

        /// <summary>Mip-map levels are interpolated, but no other filtering occurs.</summary>
        NearestMipmapLinear = 4,

        /// <summary>Both interpolated Mip-mapping and linear filtering are used.</summary>
        LinearMipmapLinear = 5
    }

    /// <summary>
    /// Sampler magnification filter
    /// </summary>
    public enum SamplerMagFilter : byte
    {
        /// <summary>No filtering. Nearest neighbor is used.</summary>
        Nearest = 0,

        /// <summary>Box filtering. Weighted average of 4 neighbors is used.</summary>
        Linear = 1,
    }

    public enum SamplerWrapMode : byte
    {
        /// <summary>The edge of the texture extends to infinity</summary>
        ClampToEdge,

        /// <summary>The texture infinitely repeats in the wrap direction</summary>
        Repeat,

        /// <summary>The texture infinitely repeats and mirrors in the wrap direction</summary>
        MirroredRepeat
    };

    public enum PixelDataFormat : byte
    {
        /// <summary>One Red channel, float</summary>
        R,

        /// <summary>One Red channel, integer</summary>
        RInteger,

        /// <summary>Two Red and Green channels, float</summary>
        Rg,

        /// <summary>Two Red and Green channels, integer</summary>
        RgInteger,

        /// <summary>Three Red, Green and Blue channels, float</summary>
        Rgb,

        /// <summary>Three Red, Green and Blue channels, integer</summary>
        RgbInteger,

        /// <summary>Four Red, Green, Blue and Alpha channels, float</summary>
        Rgba,

        /// <summary>Four Red, Green, Blue and Alpha channels, integer</summary>
        RgbaInteger,
        Unused,

        /// <summary>Depth, 16-bit or 24-bits usually</summary>
        DepthComponent,

        /// <summary>Two Depth (24-bits) + Stencil (8-bits) channels</summary>
        DepthStencil,

        /// <summary>One Alpha channel, float</summary>
        Alpha
    }

    public enum PixelDataType : byte
    {
        /// <summary>unsigned byte</summary>
        UByte,

        /// <summary>signed byte</summary>
        Byte,

        /// <summary>unsigned short (16-bit)</summary>
        UShort,

        /// <summary>signed short (16-bit)</summary>
        Short,

        /// <summary>unsigned int (16-bit)</summary>
        UInt,

        /// <summary>signed int (32-bit)</summary>
        Int,

        /// <summary>half-float (16-bit float)</summary>
        Half,

        /// <summary>float (32-bits float)</summary>
        Float,

        /// <summary>compressed pixels, @see CompressedPixelDataType</summary>
        Compressed,

        /// <summary>three low precision floating-point numbers</summary>
        UInt_10F_11F_11F_Rev,

        /// <summary>unsigned int (16-bit), encodes 3 RGB channels</summary>
        UShort565,

        /// <summary>unsigned normalized 10 bits RGB, 2 bits alpha</summary>
        UInt_2_10_10_10_Rev,
    }

    public enum CompressedPixelDataType : ushort
    {
        // Mandatory in GLES 3.0 and GL 4.3
        Eac_R11,
        Eac_R11_Signed,
        Eac_Rg11,
        Eac_Rg11_Signed,
        Etc2_Rgb8,
        Etc2_Srgb8,
        Etc2_Rgb8_A1,
        Etc2_Srgb8_A1,
        Etc2_Eac_Rgba8,
        Etc2_Eac_SRgba8,

        // Available everywhere except Android/iOS
        DXT1_Rgb,
        DXT1_Rgba,
        DXT3_Rgba,
        DXT5_Rgba,
        DXT1_Srgb,
        DXT1_SRgba,
        DXT3_SRgba,
        DXT5_SRgba,

        // ASTC formats are available with a GLES extension
        Rgba_Astc_4x4,
        Rgba_Astc_5x4,
        Rgba_Astc_5x5,
        Rgba_Astc_6x5,
        Rgba_Astc_6x6,
        Rgba_Astc_8x5,
        Rgba_Astc_8x6,
        Rgba_Astc_8x8,
        Rgba_Astc_10x5,
        Rgba_Astc_10x6,
        Rgba_Astc_10x8,
        Rgba_Astc_10x10,
        Rgba_Astc_12x10,
        Rgba_Astc_12x12,
        Srgb8_Alpha8_Astc_4x4,
        Srgb8_Alpha8_Astc_5x4,
        Srgb8_Alpha8_Astc_5x5,
        Srgb8_Alpha8_Astc_6x5,
        Srgb8_Alpha8_Astc_6x6,
        Srgb8_Alpha8_Astc_8x5,
        Srgb8_Alpha8_Astc_8x6,
        Srgb8_Alpha8_Astc_8x8,
        Srgb8_Alpha8_Astc_10x5,
        Srgb8_Alpha8_Astc_10x6,
        Srgb8_Alpha8_Astc_10x8,
        Srgb8_Alpha8_Astc_10x10,
        Srgb8_Alpha8_Astc_12x10,
        Srgb8_Alpha8_Astc_12x12,
    }

    public struct FaceOffsets
    {
        #region Properties

        public int this[int index] {
            get {
                switch (index) {
                    case 0: return Px;
                    case 1: return Nx;
                    case 2: return Py;
                    case 3: return Ny;
                    case 4: return Pz;
                    case 5: return Nz;
                }

                throw new IndexOutOfRangeException();
            }
            set {
                switch (index) {
                    case 0:
                        Px = value;
                        break;
                    case 1:
                        Nx = value;
                        break;
                    case 2:
                        Py = value;
                        break;
                    case 3:
                        Ny = value;
                        break;
                    case 4:
                        Pz = value;
                        break;
                    case 5:
                        Nz = value;
                        break;
                }

                throw new IndexOutOfRangeException();
            }
        }

        #endregion

        #region Members

        /// <summary>+x face offset in bytes</summary>
        public int Px;

        /// <summary>-x face offset in bytes</summary>
        public int Nx;

        /// <summary>+y face offset in bytes</summary>
        public int Py;

        /// <summary>-y face offset in bytes</summary>
        public int Ny;

        /// <summary>+z face offset in bytes</summary>
        public int Pz;

        /// <summary>-z face offset in bytes</summary>
        public int Nz;

        #endregion

        #region Methods

        public int[] ToArray()
        {
            return new[] {Px, Nx, Py, Ny, Pz, Nz};
        }

        #endregion
    }

    public enum TextureCubemapFace : byte
    {
        PositiveX = 0,
        NegativeX = 1,
        PositiveY = 2,
        NegativeY = 3,
        PositiveZ = 4,
        NegativeZ = 5,
    }
}
