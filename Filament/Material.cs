using System;

namespace Filament
{
    public class Material : FilamentBase<Material>
    {
        #region Properties

        /// <summary>
        /// Returns the name of this material.
        /// </summary>
        public string Name {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.GetName(NativePtr);
            }
        }

        public MaterialInstance DefaultInstance {
            get {
                ThrowExceptionIfDisposed();

                return MaterialInstance.GetOrCreateCache(
                    Native.Material.GetDefaultInstance(NativePtr)
                );
            }
        }

        /// <summary>
        /// Returns the shading model of this material.
        /// </summary>
        public MaterialShading ShadingModel {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialShading) Native.Material.GetShading(NativePtr);
            }
        }

        /// <summary>
        /// Returns the interpolation mode of this material. This affects how variables are interpolated.
        /// </summary>
        public MaterialInterpolation InterpolationMode {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialInterpolation) Native.Material.GetInterpolation(NativePtr);
            }
        }

        /// <summary>
        /// Returns the blending mode of this material.
        /// </summary>
        public MaterialBlendingMode BlendingMode {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialBlendingMode) Native.Material.GetBlendingMode(NativePtr);
            }
        }

        /// <summary>
        /// Returns the refraction mode used by this material.
        /// </summary>
        public MaterialRefractionMode RefractionMode {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialRefractionMode) Native.Material.GetRefractionMode(NativePtr);
            }
        }

        /// <summary>
        /// Return the refraction type used by this material.
        /// </summary>
        public MaterialRefractionType RefractionType {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialRefractionType) Native.Material.GetRefractionType(NativePtr);
            }
        }

        /// <summary>
        /// Returns the vertex domain of this material.
        /// </summary>
        public MaterialVertexDomain VertexDomain {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialVertexDomain) Native.Material.GetVertexDomain(NativePtr);
            }
        }

        /// <summary>
        /// Returns the default culling mode of this material.
        /// </summary>
        public MaterialCullingMode CullingMode {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialCullingMode) Native.Material.GetCullingMode(NativePtr);
            }
        }

        /// <summary>
        /// Indicates whether instances of this material will, by default, write to the color buffer.
        /// </summary>
        public bool IsColorWriteEnabled {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.IsColorWriteEnabled(NativePtr);
            }
        }

        /// <summary>
        /// Indicates whether instances of this material will, by default, write to the depth buffer.
        /// </summary>
        public bool IsDepthWriteEnabled {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.IsDepthWriteEnabled(NativePtr);
            }
        }

        /// <summary>
        /// Indicates whether instances of this material will, by default, use depth testing.
        /// </summary>
        public bool IsDepthCullingEnabled {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.IsDepthCullingEnabled(NativePtr);
            }
        }

        /// <summary>
        /// Indicates whether this material is double-sided.
        /// </summary>
        public bool IsDoubleSided {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.IsDoubleSided(NativePtr);
            }
        }

        /// <summary>
        /// Returns the alpha mask threshold used when the blending mode is set to masked.
        /// </summary>
        public float MaskThreshold {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.GetMaskThreshold(NativePtr);
            }
        }

        /// <summary>
        /// Returns the screen-space variance for specular-antialiasing, this value is between 0 and 1.
        /// </summary>
        public float SpecularAntiAliasingVariance {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.GetSpecularAntiAliasingVariance(NativePtr);
            }
        }

        /// <summary>
        /// Returns the clamping threshold for specular-antialiasing, this value is between 0 and 1.
        /// </summary>
        public float SpecularAntiAliasingThreshold {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.GetSpecularAntiAliasingThreshold(NativePtr);
            }
        }

        /// <summary>
        /// Returns the number of parameters declared by this material.
        /// </summary>
        public int ParameterCount {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.GetParameterCount(NativePtr);
            }
        }

        #endregion

        #region Methods

        private Material(IntPtr ptr) : base(ptr)
        {
        }

        internal static Material GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new Material(newPtr));
        }

        /// <summary>
        /// Creates a new instance of this material. Material instances should be freed using
        /// <see cref="Engine.Destroy(MaterialInstance)"/>.
        /// </summary>
        /// <returns>The newly created instance.</returns>
        public MaterialInstance CreateInstance()
        {
            ThrowExceptionIfDisposed();

            return MaterialInstance.GetOrCreateCache(
                Native.Material.CreateInstance(NativePtr)
            );
        }

        /// <summary>
        /// Indicates whether a parameter of the given name exists on this material.
        /// </summary>
        /// <param name="name">Name of the parameter.</param>
        /// <returns>True if the material has a parameter with the given name, false if not.</returns>
        public bool HasParameter(string name)
        {
            ThrowExceptionIfDisposed();

            return Native.Material.HasParameter(NativePtr, name);
        }

        #endregion
    }

    /// <summary>
    /// Used to setup and create a Material.
    /// </summary>
    public class MaterialBuilder
    {
        #region Members

        private byte[] _data;

        #endregion

        #region Methods

        private MaterialBuilder()
        {
        }

        /// <summary>
        /// Specifies the material data. The material data is a binary blob produced by libfilamat or by matc.
        /// </summary>
        /// <param name="buffer">The material data.</param>
        /// <param name="offset">Offset in to the material data to start reading.</param>
        /// <param name="size">Size of the material data in bytes.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public MaterialBuilder WithPackage(byte[] buffer, int offset, int size)
        {
            _data = new byte[size];
            Array.Copy(buffer, offset, _data, 0, size);

            return this;
        }

        /// <summary>
        /// Specifies the material data. The material data is a binary blob produced by libfilamat or by matc.
        /// </summary>
        /// <param name="data">The material data.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public MaterialBuilder WithPackage(byte[] data)
        {
            _data = data;

            return this;
        }

        /// <summary>
        /// Creates the Material object.
        /// </summary>
        /// <param name="engine">Reference to the <see cref="Engine"/> to associate this Material with.</param>
        /// <returns>The newly created object or null if exceptions are disabled and an error occurred.</returns>
        /// <exception cref="Exception"></exception>
        public Material Build(Engine engine)
        {
            if (null == _data) {
                throw new Exception("WithPackage must be called before Build");
            }

            return Material.GetOrCreateCache(
                Native.MaterialBuilder.Build(engine.NativePtr, _data, _data.Length)
            );
        }

        /// <summary>
        /// Creates a new material builder.
        /// </summary>
        public static MaterialBuilder Create()
        {
            return new();
        }

        #endregion
    }

    public enum VertexAttribute
    {
        /// <summary>XYZ position (float3)</summary>
        Position = 0,

        /// <summary>Tangent, bitangent and normal, encoded as a quaternion (float4)</summary>
        Tangents = 1,

        /// <summary>Vertex color (float4)</summary>
        Color = 2,

        /// <summary>Texture coordinates (float2)</summary>
        Uv0 = 3,

        /// <summary>Texture coordinates (float2)</summary>
        Uv1 = 4,

        /// <summary>Indices of 4 bones, as unsigned integers (uvec4)</summary>
        BoneIndices = 5,

        /// <summary>Weights of the 4 bones (normalized float4)</summary>
        BoneWeights = 6,

        // -- we have 1 unused slot here --
        Custom0 = 8,
        Custom1 = 9,
        Custom2 = 10,
        Custom3 = 11,
        Custom4 = 12,
        Custom5 = 13,
        Custom6 = 14,
        Custom7 = 15,

        // Aliases for vertex morphing.
        MorphPosition0 = Custom0,
        MorphPosition1 = Custom1,
        MorphPosition2 = Custom2,
        MorphPosition3 = Custom3,
        MorphTangents0 = Custom4,
        MorphTangents1 = Custom5,
        MorphTangents2 = Custom6,
        MorphTangents3 = Custom7,

        // this is limited by driver::MAX_VERTEX_ATTRIBUTE_COUNT
    }

    public enum ElementType : uint
    {
        Byte,
        Byte2,
        Byte3,
        Byte4,
        UByte,
        UByte2,
        UByte3,
        UByte4,
        Short,
        Short2,
        Short3,
        Short4,
        UShort,
        UShort2,
        UShort3,
        UShort4,
        Int,
        UInt,
        Float,
        Float2,
        Float3,
        Float4,
        Half,
        Half2,
        Half3,
        Half4,
    }

    public enum PrimitiveType : uint
    {
        Points = 0,
        Lines = 1,
        Triangles = 4,
        None = 0xFF,
    }

    public enum MaterialShading : byte
    {
        /// <summary>No lighting applied, emissive possible</summary>
        Unlit,

        /// <summary>Default, standard lighting</summary>
        Lit,

        /// <summary>Subsurface lighting model</summary>
        Subsurface,

        /// <summary>Cloth lighting model</summary>
        Cloth,

        /// <summary>Legacy lighting model</summary>
        SpecularGlossiness,
    }

    /// <summary>
    /// Attribute interpolation types in the fragment shader.
    /// </summary>
    public enum MaterialInterpolation : byte
    {
        /// <summary>Default, smooth interpolation</summary>
        Smooth,

        /// <summary>Flat interpolation</summary>
        Flat,
    }

    public enum MaterialBlendingMode : byte
    {
        /// <summary>Material is opaque</summary>
        Opaque,

        /// <summary>Material is transparent and color is alpha-pre-multiplied, affects diffuse lighting only</summary>
        Transparent,

        /// <summary>Material is additive (e.g.: hologram)</summary>
        Add,

        /// <summary>Material is masked (i.e. alpha tested)</summary>
        Masked,

        /// <summary>Material is transparent and color is alpha-pre-multiplied, affects specular lighting</summary>
        Fade,

        /// <summary>Material darkens what's behind it</summary>
        Multiply,

        /// <summary>Material brightens what's behind it</summary>
        Screen,
    }

    public enum MaterialRefractionMode : byte
    {
        /// <summary>No refraction</summary>
        None = 0,

        /// <summary>Refracted rays go to the ibl cubemap</summary>
        CubeMap = 1,

        /// <summary>Refracted rays go to screen space</summary>
        ScreenSpace = 2,
    }

    public enum MaterialRefractionType : byte
    {
        /// <summary>Refraction through solid objects (e.g. a sphere)</summary>
        Solid = 0,

        /// <summary>Refraction through thin objects (e.g. window)</summary>
        Thin = 1,
    }

    public enum MaterialVertexDomain : byte
    {
        /// <summary>Vertices are in object space, default</summary>
        Object,

        /// <summary>Vertices are in world space</summary>
        World,

        /// <summary>Vertices are in view space</summary>
        View,

        /// <summary>Vertices are in normalized device space</summary>
        Device,
    }

    public enum MaterialCullingMode : byte
    {
        /// <summary>No culling, front and back faces are visible</summary>
        None,

        /// <summary>Front face culling, only back faces are visible</summary>
        Front,

        /// <summary>Back face culling, only front faces are visible</summary>
        Back,

        /// <summary>Front and Back, geometry is not visible</summary>
        FrontAndBack,
    }
}
