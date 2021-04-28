using System;

namespace Filament
{
    public class Material : FilamentBase<Material>
    {
        #region Properties

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

        public MaterialShading ShadingModel {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialShading) Native.Material.GetShading(NativePtr);
            }
        }

        public MaterialInterpolation InterpolationMode {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialInterpolation) Native.Material.GetInterpolation(NativePtr);
            }
        }

        public MaterialBlendingMode BlendingMode {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialBlendingMode) Native.Material.GetBlendingMode(NativePtr);
            }
        }

        public MaterialRefractionMode RefractionMode {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialRefractionMode) Native.Material.GetRefractionMode(NativePtr);
            }
        }

        public MaterialRefractionType RefractionType {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialRefractionType) Native.Material.GetRefractionType(NativePtr);
            }
        }

        public MaterialVertexDomain VertexDomain {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialVertexDomain) Native.Material.GetVertexDomain(NativePtr);
            }
        }

        public MaterialCullingMode CullingMode {
            get {
                ThrowExceptionIfDisposed();

                return (MaterialCullingMode) Native.Material.GetCullingMode(NativePtr);
            }
        }

        public bool IsColorWriteEnabled {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.IsColorWriteEnabled(NativePtr);
            }
        }

        public bool IsDepthWriteEnabled {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.IsDepthWriteEnabled(NativePtr);
            }
        }

        public bool IsDepthCullingEnabled {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.IsDepthCullingEnabled(NativePtr);
            }
        }

        public bool IsDoubleSided {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.IsDoubleSided(NativePtr);
            }
        }

        public float MaskThreshold {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.GetMaskThreshold(NativePtr);
            }
        }

        public float SpecularAntiAliasingVariance {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.GetSpecularAntiAliasingVariance(NativePtr);
            }
        }

        public float SpecularAntiAliasingThreshold {
            get {
                ThrowExceptionIfDisposed();

                return Native.Material.GetSpecularAntiAliasingThreshold(NativePtr);
            }
        }

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

        public MaterialInstance CreateInstance()
        {
            ThrowExceptionIfDisposed();

            return MaterialInstance.GetOrCreateCache(
                Native.Material.CreateInstance(NativePtr)
            );
        }

        public bool HasParameter(string name)
        {
            ThrowExceptionIfDisposed();

            return Native.Material.HasParameter(NativePtr, name);
        }

        #endregion
    }

    public class MaterialBuilder
    {
        #region Members

        private byte[] _data;

        #endregion

        #region Methods

        private MaterialBuilder()
        {
        }

        public MaterialBuilder WithPackage(byte[] buffer, int offset, int size)
        {
            _data = new byte[size];
            Array.Copy(buffer, offset, _data, 0, size);

            return this;
        }

        public MaterialBuilder WithPackage(byte[] data)
        {
            _data = data;

            return this;
        }

        public Material Build(Engine engine)
        {
            if (null == _data) {
                throw new Exception("WithPackage must be called before Build");
            }

            return Material.GetOrCreateCache(
                Native.MaterialBuilder.Build(engine.NativePtr, _data, _data.Length)
            );
        }

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
