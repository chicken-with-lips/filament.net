using System;
using OpenTK.Mathematics;

namespace Filament
{
    public readonly struct Viewport
    {
        public int Left { get; }
        public int Bottom { get; }
        public int Width { get; }
        public int Height { get; }

        public int Right => Left + Width;
        public int Top => Bottom + Height;

        public Viewport(int left, int bottom, int width, int height)
        {
            Left = left;
            Bottom = bottom;
            Width = width;
            Height = height;
        }
    }

    public class View : FilamentBase<View>
    {
        #region Properties

        public string Name {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetName(NativePtr, value);
            }
        }

        public Viewport Viewport {
            get {
                ThrowExceptionIfDisposed();

                Native.View.GetViewport(NativePtr, out var left, out var bottom, out var width, out var height);

                return new Viewport(left, bottom, width, height);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetViewport(NativePtr, value.Left, value.Bottom, value.Width, value.Height);
            }
        }

        public Camera Camera {
            get {
                ThrowExceptionIfDisposed();

                return Camera.GetOrCreateCache(
                    Native.View.GetCamera(NativePtr)
                );
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetCamera(NativePtr, value.NativePtr);
            }
        }

        public Camera DebugCamera {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetDebugCamera(NativePtr, value.NativePtr);
            }
        }

        public Camera DirectionalLightCamera {
            get {
                ThrowExceptionIfDisposed();

                return Camera.GetOrCreateCache(
                    Native.View.GetDirectionalLightCamera(NativePtr)
                );
            }
        }

        public Scene Scene {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetScene(NativePtr, value.NativePtr);
            }
        }

        public bool ShadowingEnabled {
            get {
                ThrowExceptionIfDisposed();

                return Native.View.IsShadowingEnabled(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetShadowingEnabled(NativePtr, value);
            }
        }

        public bool PostProcessingEnabled {
            get {
                ThrowExceptionIfDisposed();

                return Native.View.IsPostProcessingEnabled(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetPostProcessingEnabled(NativePtr, value);
            }
        }

        public RenderTarget RenderTarget {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetRenderTarget(NativePtr, value.NativePtr);
            }
        }

        public int SampleCount {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetSampleCount(NativePtr, value);
            }
        }

        public ViewAntiAliasing AntiAliasing {
            get {
                ThrowExceptionIfDisposed();

                return (ViewAntiAliasing) Native.View.GetAntiAliasing(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetAntiAliasing(NativePtr, (byte) value);
            }
        }

        public ViewDithering Dithering {
            get {
                ThrowExceptionIfDisposed();

                return (ViewDithering) Native.View.GetDithering(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetDithering(NativePtr, (byte) value);
            }
        }

        public ViewShadowType ShadowType {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetShadowType(NativePtr, (byte) value);
            }
        }

        public bool IsDynamicResolutionEnabled {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetDynamicResolutionOptions(NativePtr, value);
            }
        }

        public bool IsFrontFaceWindingInverted {
            get {
                ThrowExceptionIfDisposed();

                return Native.View.IsFrontFaceWindingInverted(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetFrontFaceWindingInverted(NativePtr, value);
            }
        }

        public ViewAmbientOcclusion AmbientOcclusion {
            get {
                ThrowExceptionIfDisposed();

                return (ViewAmbientOcclusion) Native.View.GetAmbientOcclusion(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetAmbientOcclusion(NativePtr, (byte) value);
            }
        }

        public ViewRenderQuality RenderQuality {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetRenderQuality(NativePtr, (byte) value.HdrColorBuffer);
            }
        }

        public VsmShadowOptions VsmShadowOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetVsmShadowOptions(NativePtr, value.Anisotropy);
            }
        }

        public ViewAmbientOcclusionOptions AmbientOcclusionOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetAmbientOcclusionOptions(NativePtr,
                    value.Radius, value.Bias, value.Power, value.Resolution, value.Intensity,
                    (byte) value.Quality, (byte) value.LowPassFilter, (byte) value.Upsampling, value.Enabled, value.MinHorizonAngleRad,
                    value.Ssct.LightConeRad, value.Ssct.ShadowDistance,
                    value.Ssct.ContactDistanceMax, value.Ssct.Intensity,
                    value.Ssct.LightDirection.X, value.Ssct.LightDirection.Y, value.Ssct.LightDirection.Z,
                    value.Ssct.DepthBias, value.Ssct.DepthSlopeBias, value.Ssct.SampleCount,
                    value.Ssct.RayCount, value.Ssct.Enabled);
            }
        }

        public ViewBloomOptions BloomOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetBloomOptions(NativePtr, value.Dirt?.NativePtr ?? IntPtr.Zero,
                    value.DirtStrength, value.Strength,
                    value.Resolution, value.Anamorphism, value.Levels, (byte) value.BlendMode,
                    value.Threshold, value.Enabled, value.Highlight);
            }
        }

        public ViewFogOptions FogOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetFogOptions(NativePtr, value.Distance, value.MaximumOpacity, value.Height, value.HeightFalloff,
                    value.Color.R, value.Color.G, value.Color.B, value.Density,
                    value.InScatteringStart, value.InScatteringSize, value.FogColorFromIbl, value.Enabled);
            }
        }

        public ViewBlendMode BlendMode {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetBlendMode(NativePtr, (byte) value);
            }
        }

        public ViewDepthOfFieldOptions DepthOfFieldOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetDepthOfFieldOptions(NativePtr, value.FocusDistance, value.CocScale, value.MaxApertureDiameter, value.Enabled);
            }
        }

        public ViewVignetteOptions VignetteOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetVignetteOptions(NativePtr,
                    value.MidPoint, value.Roundness, value.Feather,
                    value.Color.R, value.Color.G, value.Color.B, value.Color.A,
                    value.Enabled);
            }
        }

        public ViewTemporalAntiAliasingOptions TemporalAntiAliasingOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetTemporalAntiAliasingOptions(NativePtr,
                    value.Feedback, value.FilterWidth, value.Enabled);
            }
        }

        public bool IsScreenSpaceRefractionEnabled {
            get {
                ThrowExceptionIfDisposed();

                return Native.View.IsScreenSpaceRefractionEnabled(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetScreenSpaceRefractionEnabled(NativePtr, value);
            }
        }

        #endregion

        #region Methods

        private View(IntPtr ptr) : base(ptr)
        {
        }

        internal static View GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new View(newPtr));
        }

        public void SetVisibleLayers(int select, int value)
        {
            ThrowExceptionIfDisposed();

            Native.View.SetVisibleLayers(NativePtr, select, value);
        }

        public void SetDynamicLightingOptions(float zLightNear, float zLightFar)
        {
            ThrowExceptionIfDisposed();

            Native.View.SetDynamicLightingOptions(NativePtr, zLightNear, zLightFar);
        }

        #endregion
    }


    public enum ViewAntiAliasing : byte
    {
        /// <summary>No anti aliasing performed as part of post-processing</summary>
        None = 0,

        /// <summary>FXAA is a low-quality but very efficient type of anti-aliasing. (default).</summary>
        Fxaa = 1,
    }

    public enum ViewDithering : byte
    {
        /// <summary>No dithering</summary>
        None = 0,

        /// <summary>Temporal dithering (default)</summary>
        Temporal = 1,
    }

    public enum ViewQualityLevel : byte
    {
        Low,
        Medium,
        High,
        Ultra,
    }

    public enum ViewShadowType : byte
    {
        /// <summary>Percentage-closer filtered shadows (default)</summary>
        Pcf,

        /// <summary>Variance shadows</summary>
        Vsm,
    }

    public enum ViewAmbientOcclusion : byte
    {
        /// <summary>No Ambient Occlusion</summary>
        None,

        /// <summary>Basic, sampling SSAO</summary>
        Ssao,
    }

    public enum ViewBlendMode : byte
    {
        Opaque,
        Translucent,
    }

    /// <summary>
    /// Structure used to set the precision of the color buffer and related quality settings.
    /// </summary>
    public struct ViewRenderQuality
    {
        /// <summary>
        /// Sets the quality of the HDR color buffer.
        /// </summary>
        /// <remarks>
        /// A quality of HIGH or ULTRA means using an RGB16F or RGBA16F color buffer. This means
        /// colors in the LDR range (0..1) have a 10 bit precision. A quality of LOW or MEDIUM means
        /// using an R11G11B10F opaque color buffer or an RGBA16F transparent color buffer. With
        /// R11G11B10F colors in the LDR range have a precision of either 6 bits (red and green
        /// channels) or 5 bits (blue channel).
        /// </remarks>
        public ViewQualityLevel HdrColorBuffer;

        public static ViewRenderQuality Default => new() {
            HdrColorBuffer = ViewQualityLevel.High
        };
    }

    /// <summary>
    /// View-level options for VSM Shadowing.
    /// </summary>
    public struct VsmShadowOptions
    {
        /// <summary>
        /// Sets the number of anisotropic samples to use when sampling a VSM shadow map.
        /// </summary>
        /// <remarks>
        /// If greater than 0, mipmaps will automatically be generated each frame for all lights.
        /// The number of anisotropic samples = 2 ^ vsmAnisotropy.
        /// </remarks>
        public int Anisotropy;

        public static VsmShadowOptions Default => new();
    }

    /// <summary>
    /// Options for screen space Ambient Occlusion (SSAO) and Screen Space Cone Tracing (SSCT).
    /// </summary>
    public struct ViewAmbientOcclusionOptions
    {
        /// <summary> Ambient Occlusion radius in meters, between 0 and ~10.</summary>
        public float Radius;

        /// <summary>Controls ambient occlusion's contrast. Must be positive.</summary>
        public float Power;

        /// <summary>Self-occlusion bias in meters. Use to avoid self-occlusion. Between 0 and a few mm.</summary>
        public float Bias;

        /// <summary>How each dimension of the AO buffer is scaled. Must be either 0.5 or 1.0.</summary>
        public float Resolution;

        /// <summary>Strength of the Ambient Occlusion effect.</summary>
        public float Intensity;

        /// <summary>Affects # of samples used for AO.</summary>
        public ViewQualityLevel Quality;

        /// <summary>Affects AO smoothness.</summary>
        public ViewQualityLevel LowPassFilter;

        /// <summary>Affects AO buffer upsampling quality.</summary>
        public ViewQualityLevel Upsampling;

        /// <summary>Enables or disables screen-space ambient occlusion.</summary>
        public bool Enabled;

        /// <summary>Min angle in radian to consider.</summary>
        public float MinHorizonAngleRad;


        // ssct
        public ViewAmbientOcclusionSsctOptions Ssct;

        public static ViewAmbientOcclusionOptions Default => new() {
            Radius = 0.3f,
            Power = 1.0f,
            Bias = 0.0005f,
            Resolution = 0.5f,
            Intensity = 1.0f,
            Quality = ViewQualityLevel.Low,
            LowPassFilter = ViewQualityLevel.Medium,
            Upsampling = ViewQualityLevel.Low,
            Enabled = false,
            MinHorizonAngleRad = 0.0f,
            Ssct = ViewAmbientOcclusionSsctOptions.Default,
        };
    }

    /// <summary>
    /// Screen Space Cone Tracing (SSCT) options
    /// </summary>
    public struct ViewAmbientOcclusionSsctOptions
    {
        /// <summary>Full cone angle in radian, between 0 and pi/2</summary>
        public float LightConeRad;

        /// <summary>How far shadows can be cast</summary>
        public float ShadowDistance;

        /// <summary>Max distance for contact</summary>
        public float ContactDistanceMax;

        public float Intensity;
        public Vector3 LightDirection;

        /// <summary>Depth bias in world units (mitigate self shadowing)</summary>
        public float DepthBias;

        /// <summary>Depth slope bias (mitigate self shadowing)</summary>
        public float DepthSlopeBias;

        /// <summary>Tracing sample count, between 1 and 255</summary>
        public int SampleCount;

        /// <summary># of rays to trace, between 1 and 255</summary>
        public int RayCount;

        /// <summary>Enables or disables SSCT</summary>
        public bool Enabled;

        public static ViewAmbientOcclusionSsctOptions Default => new() {
            LightConeRad = 1.0f,
            ShadowDistance = 0.3f,
            ContactDistanceMax = 1.0f,
            Intensity = 0.8f,
            LightDirection = new(0, -1, 0),
            DepthBias = 0.01f,
            DepthSlopeBias = 0.01f,
            SampleCount = 4,
            RayCount = 1,
            Enabled = false
        };
    }

    public struct ViewBloomOptions
    {
        /// <summary>
        /// A dirt/scratch/smudges texture (that can be RGB), which gets added to the bloom effect.
        /// </summary>
        /// <remarks>
        /// Smudges are visible where bloom occurs. Threshold must be enabled for the dirt effect to work properly.
        /// </remarks>
        public Texture Dirt;

        /// <summary>
        /// Strength of the dirt texture.
        /// </summary>
        public float DirtStrength;

        /// <summary>
        /// How much of the bloom is added to the original image. Between 0 and 1.
        /// </summary>
        public float Strength;

        /// <summary>
        /// Resolution of bloom's minor axis.
        /// </summary>
        /// <remarks>
        /// The minimum value is 2^levels and the the maximum is lower of the original resolution and 4096. This
        /// parameter is silently clamped to the minimum and maximum. It is highly recommended that this value be
        /// smaller than the target resolution after dynamic resolution is applied (horizontally and vertically).
        /// </remarks>
        public int Resolution;

        /// <summary>
        /// Bloom's aspect ratio (x/y), for artistic purposes.
        /// </summary>
        public float Anamorphism;

        /// <summary>
        /// Number of successive blurs to achieve the blur effect, the minimum is 3 and the maximum is 12.
        /// </summary>
        /// <remarks>
        /// This value together with resolution influences the spread of the blur effect. This value can be silently
        /// reduced to accommodate the original image size.
        /// </summary>
        public int Levels;

        /// <summary>
        /// Whether the bloom effect is purely additive (false) or mixed with the original image (true).
        /// </summary>
        public ViewBloomBlendMode BlendMode;

        /// <summary>
        /// When enabled, a threshold at 1.0 is applied on the source image, this is useful for artistic reasons and
        /// is usually needed when a dirt texture is used.
        /// </summary>
        public bool Threshold;

        /// <summary>
        /// Enable or disable the bloom post-processing effect. Disabled by default.
        /// </summary>
        public bool Enabled;

        /// <summary>
        /// Limit highlights to this value before bloom [10, +inf].
        /// </summary>
        public float Highlight;

        public static ViewBloomOptions Default => new() {
            DirtStrength = 0.2f,
            Strength = 0.1f,
            Resolution = 360,
            Anamorphism = 1.0f,
            Levels = 6,
            BlendMode = ViewBloomBlendMode.Add,
            Threshold = true,
            Enabled = false,
            Highlight = 1000f,
        };
    }

    public enum ViewBloomBlendMode : byte
    {
        /// <summary>Bloom is modulated by the strength parameter and added to the scene</summary>
        Add,

        /// <summary>Bloom is interpolated with the scene using the strength parameter</summary>
        Interpolate,
    }

    public struct ViewFogOptions
    {
        /// <summary>Distance in world units from the camera where the fog starts ( >= 0.0 )</summary>
        public float Distance;

        /// <summary>Fog's maximum opacity between 0 and 1</summary>
        public float MaximumOpacity;

        /// <summary>Fog's floor in world units</summary>
        public float Height;

        /// <summary>How fast fog dissipates with altitude</summary>
        public float HeightFalloff;

        /// <summary>Fog's color (linear), see fogColorFromIbl</summary>
        public LinearColor Color;

        /// <summary>Fog's density at altitude given by 'height'</summary>
        public float Density;

        /// <summary>Distance in world units from the camera where in-scattering starts</summary>
        public float InScatteringStart;

        /// <summary>Size of in-scattering (>0 to activate). Good values are >> 1 (e.g. ~10 - 100).</summary>
        public float InScatteringSize;

        /// <summary>Fog color will be modulated by the IBL color in the view direction.</summary>
        public bool FogColorFromIbl;

        /// <summary>Enable or disable fog</summary>
        public bool Enabled;

        public static ViewFogOptions Default => new() {
            Distance = 0f,
            MaximumOpacity = 1.0f,
            Height = 0f,
            HeightFalloff = 1.0f,
            Color = new LinearColor(0.5f, 0.5f, 0.5f),
            Density = 0.1f,
            InScatteringStart = 0f,
            InScatteringSize = -1f,
            FogColorFromIbl = false,
            Enabled = false,
        };
    }

    /// <summary>
    /// Options to control Depth of Field (DoF) effect in the scene.
    /// </summary>
    /// <remarks>
    /// cocScale can be used to set the depth of field blur independently from the camera aperture, e.g. for artistic
    /// reasons. This can be achieved by setting: cocScale = cameraAperture / desiredDoFAperture
    /// </remarks>
    public struct ViewDepthOfFieldOptions
    {
        /// <summary>Focus distance in world units</summary>
        public float FocusDistance;

        /// <summary>Circle of confusion scale factor (amount of blur)</summary>
        public float CocScale;

        /// <summary>Maximum aperture diameter in meters (zero to disable rotation)</summary>
        public float MaxApertureDiameter;

        /// <summary>Enable or disable depth of field effect</summary>
        public bool Enabled;

        public static ViewDepthOfFieldOptions Default => new() {
            FocusDistance = 10f,
            CocScale = 1f,
            MaxApertureDiameter = 0.1f,
            Enabled = false,
        };
    }

    public struct ViewVignetteOptions
    {
        /// <summary>High values restrict the vignette closer to the corners, between 0 and 1</summary>
        public float MidPoint;

        /// <summary>Controls the shape of the vignette, from a rounded rectangle (0.0), to an oval (0.5), to a circle (1.0)</summary>
        public float Roundness;

        /// <summary>Softening amount of the vignette effect, between 0 and 1</summary>
        public float Feather;

        /// <summary>Color of the vignette effect, alpha is currently ignored</summary>
        public LinearColorA Color; //!< c

        /// <summary>Enables or disables the vignette effect</summary>
        public bool Enabled;

        public static ViewVignetteOptions Default => new() {
            MidPoint = 0.5f,
            Roundness = 0.5f,
            Feather = 0.5f,
            Color = new LinearColorA(0.0f, 0.0f, 0.0f, 1.0f),
            Enabled = false
        };
    }

    public struct ViewTemporalAntiAliasingOptions
    {
        /// <summary>Reconstruction filter width typically between 0 (sharper, aliased) and 1 (smoother)</summary>
        public float FilterWidth;

        /// <summary>History feedback, between 0 (maximum temporal AA) and 1 (no temporal AA).</summary>
        public float Feedback;

        /// <summary>Enables or disables temporal anti-aliasing</summary>
        public bool Enabled;

        public static ViewTemporalAntiAliasingOptions Default => new() {
            FilterWidth = 1f,
            Feedback = 0.04f,
            Enabled = false,
        };
    };
}
