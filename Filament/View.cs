using System;
using System.Numerics;

namespace Filament
{
    /// <summary>
    /// Viewport describes a view port in pixel coordinates.
    /// </summary>
    /// <remarks>A view port is represented by its left-bottom coordinate, width and height in pixels.</remarks>
    public readonly struct Viewport
    {
        /// <summary>
        /// Left coordinate in window space.
        /// </summary>
        public int Left { get; }

        /// <summary>
        /// Bottom coordinate in window space.
        /// </summary>
        public int Bottom { get; }

        /// <summary>
        /// Width in pixels.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Height in pixels.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// The right coordinate in window space of the viewport.
        /// </summary>
        public int Right => Left + Width;

        /// <summary>
        /// The top coordinate in window space of the viewport.
        /// </summary>
        public int Top => Bottom + Height;

        /// <summary>
        /// Creates a Viewport from its left-bottom coordinates, width and height in pixels.
        /// </summary>
        /// <param name="left">Left coordinate in pixel.</param>
        /// <param name="bottom">Bottom coordinate in pixel.</param>
        /// <param name="width">Width in pixel.</param>
        /// <param name="height">Height in pixel.</param>
        public Viewport(int left, int bottom, int width, int height)
        {
            Left = left;
            Bottom = bottom;
            Width = width;
            Height = height;
        }
    }

    /// <summary>
    /// A View encompasses all the state needed for rendering a Scene.
    /// </summary>
    /// <remarks>
    /// <para>Renderer::render() operates on View objects. These View objects specify important parameters such as:</para>
    /// <list>
    /// <item>The Scene.</item>
    /// <item>The Camera.</item>
    /// <item>The Viewport.</item>
    /// <item>Some rendering parameters.</item>
    /// </list>
    /// <para>Note: View instances are heavy objects that internally cache a lot of data needed for rendering. It is not
    /// advised for an application to use many View objects.</para>
    /// <para>For example, in a game, a View could be used for the main scene and another one for the game's user
    /// interface. More View instances could be used for creating special effects (e.g. a View is akin to a rendering
    /// pass).</para>
    /// </remarks>
    public class View : FilamentBase<View>
    {
        #region Properties

        /// <summary>
        /// The View's name.
        /// </summary>
        public string Name {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetName(NativePtr, value);
            }
        }

        /// <summary>
        /// <para>The rectangular region to render to.</para>
        /// <para>The viewport specifies where the content of the View (i.e. the Scene) is rendered in the render
        /// target. The Render target is automatically clipped to the Viewport.</para>
        /// </summary>
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

        /// <summary>
        /// <para>This View's Camera.</para>
        /// <para>Note: Make sure to dissociate a Camera from all Views before destroying it.</para>
        /// </summary>
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

        /// <summary>
        /// Debugging: The Camera used for rendering. It may be different from the culling camera.
        /// </summary>
        public Camera DebugCamera {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetDebugCamera(NativePtr, value.NativePtr);
            }
        }

        /// <summary>
        /// Debugging: Returns a Camera from the point of view of the dominant directional light used for shadowing.
        /// </summary>
        public Camera DirectionalLightCamera {
            get {
                ThrowExceptionIfDisposed();

                return Camera.GetOrCreateCache(
                    Native.View.GetDirectionalLightCamera(NativePtr)
                );
            }
        }

        /// <summary>
        /// <para>Set this View instance's Scene.</para>
        /// <para>Note: Make sure to dissociate a Scene from all Views before destroying it.</para>
        /// </summary>
        public Scene Scene {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetScene(NativePtr, value.NativePtr);
            }
        }

        /// <summary>
        /// Enables or disables shadow mapping. Enabled by default.
        /// </summary>
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

        /// <summary>
        /// <para>Enables or disables post processing. Enabled by default.</para>
        /// <para>Post-processing includes:</para>
        /// <list>
        /// <item>Bloom</item>
        /// <item>Tone-mapping & gamma encoding</item>
        /// <item>Dithering</item>
        /// <item>MSAA</item>
        /// <item>FXAA</item>
        /// <item>Dynamic scaling</item>
        /// </list>
        /// <para>Disabling post-processing forgoes color correctness as well as anti-aliasing and should only be used
        /// experimentally (e.g. for UI overlays).</para>
        /// </summary>
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

        /// <summary>
        /// <para>Specifies an offscreen render target to render into.</para>
        /// <para>By default, the view's associated render target is null, which corresponds to the SwapChain associated
        /// with the engine.</para>
        /// <para>A view with a custom render target cannot rely on <see cref="Renderer.SetClearOptions"/>, which only
        /// apply to the SwapChain. Such view can use a Skybox instead.</para>
        /// </summary>
        public RenderTarget RenderTarget {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetRenderTarget(NativePtr, value.NativePtr);
            }
        }

        /// <summary>
        /// <para>Sets how many samples are to be used for MSAA in the post-process stage.  Default is 1 and disables
        /// MSAA.</para>
        /// <para>Note: Anti-aliasing can also be performed in the post-processing stage, generally at lower cost.</para>
        /// </summary>
        public int SampleCount {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetSampleCount(NativePtr, value);
            }
        }

        /// <summary>
        /// <para>Enables or disables anti-aliasing in the post-processing stage. Enabled by default.</para>
        /// <para>MSAA can be enabled in addition, see <see cref="SampleCount"/>.</para>
        /// </summary>
        public ViewAntiAliasing AntiAliasing {
            get {
                ThrowExceptionIfDisposed();

                return (ViewAntiAliasing)Native.View.GetAntiAliasing(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetAntiAliasing(NativePtr, (byte)value);
            }
        }

        /// <summary>
        /// Enables or disables dithering in the post-processing stage. Enabled by default.
        /// </summary>
        public ViewDithering Dithering {
            get {
                ThrowExceptionIfDisposed();

                return (ViewDithering)Native.View.GetDithering(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetDithering(NativePtr, (byte)value);
            }
        }

        /// <summary>
        /// <para>Set the shadow mapping technique this View uses.</para>
        /// <para>The ShadowType affects all the shadows seen within the View.</para>
        /// <para><see cref="ViewShadowType.Vsm"/> imposes a restriction on marking renderables as only shadow receivers
        /// (but not casters). To ensure correct shadowing with VSM, all shadow participant renderables should be marked
        /// as both receivers and casters. Objects that are guaranteed to not cast shadows on themselves or other
        /// objects (such as flat ground planes) can be set to not cast shadows, which might improve shadow quality.</para>
        /// <para>Warning: This API is still experimental and subject to change.</para>
        /// </summary>
        public ViewShadowType ShadowType {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetShadowType(NativePtr, (byte)value);
            }
        }

        /// <summary>
        /// The dynamic resolution options for this view. Dynamic resolution options controls whether dynamic resolution
        /// is enabled, and if it is, how it behaves.
        /// </summary>
        public bool IsDynamicResolutionEnabled {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetDynamicResolutionOptions(NativePtr, value);
            }
        }

        /// <summary>
        /// <para>Inverts the winding order of front faces. By default front faces use a counter-clockwise winding
        /// order. When the winding order is inverted, front faces are faces with a clockwise winding order.</para>
        /// <para>Changing the winding order will directly affect the culling mode in materials. Inverting the winding
        /// order of front faces is useful when rendering mirrored reflections (water, mirror surfaces, front camera in
        /// AR, etc.).</para>
        /// </summary>
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

        /// <summary>
        /// Activates or deactivates ambient occlusion.
        /// </summary>
        public ViewAmbientOcclusion AmbientOcclusion {
            get {
                ThrowExceptionIfDisposed();

                return (ViewAmbientOcclusion)Native.View.GetAmbientOcclusion(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetAmbientOcclusion(NativePtr, (byte)value);
            }
        }

        /// <summary>
        /// The rendering quality for this view. Refer to RenderQuality for more information about the different
        /// settings available.
        /// </summary>
        public ViewRenderQuality RenderQuality {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetRenderQuality(NativePtr, (byte)value.HdrColorBuffer);
            }
        }

        /// <summary>
        /// <para>Sets VSM shadowing options that apply across the entire View.</para>
        /// <para>Additional light-specific VSM options can be set with <see cref="LightManager.ShadowOptions"/>.</para>
        /// <para>Note: Only applicable when shadow type is set to <see cref="ViewShadowType.Vsm"/>.</para>
        /// <para>Warning: This API is still experimental and subject to change.</para>
        /// </summary>
        public VsmShadowOptions VsmShadowOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetVsmShadowOptions(
                    NativePtr,
                    value.Anisotropy,
                    value.Mipmapping,
                    value.Exponent,
                    value.MinVarianceScale,
                    value.LightBleedReduction
                );
            }
        }

        /// <summary>
        /// Ambient occlusion options.
        /// </summary>
        public ViewAmbientOcclusionOptions AmbientOcclusionOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetAmbientOcclusionOptions(NativePtr,
                    value.Radius, value.Bias, value.Power, value.Resolution, value.Intensity, value.BilateralThreshold,
                    (byte)value.Quality, (byte)value.LowPassFilter, (byte)value.Upsampling, value.Enabled, value.MinHorizonAngleRad,
                    value.Ssct.LightConeRad, value.Ssct.ShadowDistance,
                    value.Ssct.ContactDistanceMax, value.Ssct.Intensity,
                    value.Ssct.LightDirection.X, value.Ssct.LightDirection.Y, value.Ssct.LightDirection.Z,
                    value.Ssct.DepthBias, value.Ssct.DepthSlopeBias, value.Ssct.SampleCount,
                    value.Ssct.RayCount, value.Ssct.Enabled);
            }
        }

        /// <summary>
        /// Enables or disables bloom in the post-processing stage. Disabled by default.
        /// </summary>
        public ViewBloomOptions BloomOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetBloomOptions(NativePtr, value.Dirt?.NativePtr ?? IntPtr.Zero,
                    value.DirtStrength, value.Strength,
                    value.Resolution, value.Anamorphism, value.Levels, (byte)value.BlendMode,
                    value.Threshold, value.Enabled, value.Highlight, value.LensFlare, value.Starburst,
                    value.ChromaticAberration, value.GhostCount, value.GhostSpacing, value.GhostThreshold,
                    value.HaloThickness, value.HaloRadius, value.HaloThreshold
                );
            }
        }

        /// <summary>
        /// Enables or disables fog. Disabled by default.
        /// </summary>
        public ViewFogOptions FogOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetFogOptions(NativePtr, value.Distance, value.MaximumOpacity, value.Height, value.HeightFalloff,
                    value.Color.R, value.Color.G, value.Color.B, value.Density,
                    value.InScatteringStart, value.InScatteringSize, value.FogColorFromIbl, value.Enabled);
            }
        }

        /// <summary>
        /// The blending mode used to draw the view into the SwapChain.
        /// </summary>
        public ViewBlendMode BlendMode {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetBlendMode(NativePtr, (byte)value);
            }
        }

        /// <summary>
        /// Enables or disables Depth of Field. Disabled by default.
        /// </summary>
        public ViewDepthOfFieldOptions DepthOfFieldOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetDepthOfFieldOptions(
                    NativePtr, value.CocScale,
                    value.MaxApertureDiameter, value.Enabled, (int)value.Filter, value.NativeResolution,
                    value.ForegroundRingCount, value.BackgroundRingCount, value.FastGatherRingCount,
                    value.MaxForegroundCOC, value.MaxBackgroundCOC
                );
            }
        }

        /// <summary>
        /// Enables or disables the vignetted effect in the post-processing stage. Disabled by default.
        /// </summary>
        public ViewVignetteOptions VignetteOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetVignetteOptions(NativePtr,
                    value.MidPoint, value.Roundness, value.Feather,
                    value.Color.R, value.Color.G, value.Color.B, value.Color.A,
                    value.Enabled);
            }
        }

        /// <summary>
        /// Enables or disable temporal anti-aliasing (TAA). Disabled by default.
        /// </summary>
        public ViewTemporalAntiAliasingOptions TemporalAntiAliasingOptions {
            set {
                ThrowExceptionIfDisposed();

                Native.View.SetTemporalAntiAliasingOptions(NativePtr,
                    value.Feedback, value.FilterWidth, value.Enabled);
            }
        }

        /// <summary>
        /// Enables or disables screen space refraction. Enabled by default.
        /// </summary>
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

        /// <summary>
        /// <para>Sets which layers are visible.</para>
        /// <para>Renderable objects can have one or several layers associated to them. Layers are represented with an
        /// 8-bits bitmask, where each bit corresponds to a layer.</para>
        /// <para>This call sets which of those layers are visible. Renderables in invisible layers won't be rendered.</para>
        /// <para>Note: By default all layers are visible.</para>
        /// </summary>
        /// <param name="select">A bitmask specifying which layer to set or clear using <param name="value"/>.</param>
        /// <param name="value">A bitmask where each bit sets the visibility of the corresponding layer (1: visible,
        /// 0: invisible), only layers in <param name="select"/> are affected.</param>
        public void SetVisibleLayers(int select, int value)
        {
            ThrowExceptionIfDisposed();

            Native.View.SetVisibleLayers(NativePtr, select, value);
        }

        /// <summary>
        /// <para>Sets options relative to dynamic lighting for this view.</para>
        /// <para>Together zLightNear and zLightFar must be chosen so that the visible influence of lights is spread
        /// between these two values.</para>
        /// </summary>
        /// <param name="zLightNear">Distance from the camera where the lights are expected to shine. This parameter can
        /// affect performance and is useful because depending on the scene, lights that shine close to the camera may
        /// not be visible -- in this case, using a larger value can improve performance. e.g. when standing and looking
        /// straight, several meters of the ground isn't visible and if lights are expected to shine there, there is no
        /// point using a short zLightNear. (DefaultL 5m).</param>
        /// <param name="zLightFar">Distance from the camera after which lights are not expected to be visible.
        /// Similarly to zLightNear, setting this value properly can improve performance. (Default: 100m).</param>
        public void SetDynamicLightingOptions(float zLightNear, float zLightFar)
        {
            ThrowExceptionIfDisposed();

            Native.View.SetDynamicLightingOptions(NativePtr, zLightNear, zLightFar);
        }

        #endregion
    }


    /// <summary>
    /// List of available post-processing anti-aliasing techniques.
    /// </summary>
    public enum ViewAntiAliasing : byte
    {
        /// <summary>No anti aliasing performed as part of post-processing</summary>
        None = 0,

        /// <summary>FXAA is a low-quality but very efficient type of anti-aliasing. (default).</summary>
        Fxaa = 1,
    }

    /// <summary>
    /// List of available post-processing dithering techniques.
    /// </summary>
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

    /// <summary>
    /// List of available shadow mapping techniques.
    /// </summary>
    public enum ViewShadowType : byte
    {
        /// <summary>Percentage-closer filtered shadows (default)</summary>
        Pcf,

        /// <summary>Variance shadows</summary>
        Vsm,
    }

    /// <summary>
    /// List of available ambient occlusion techniques.
    /// </summary>
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

        /// <summary>Whether to generate mipmaps for all VSM shadow maps.</summary>
        public bool Mipmapping;

        /// <summary>EVSM exponent.  The maximum value permissible is 5.54 for a shadow map in fp16, or 42.0 for a
        /// shadow map in fp32. Currently the shadow map bit depth is always fp16.
        /// </summary>
        public float Exponent;

        /// <summary>VSM minimum variance scale, must be positive.</summary>
        public float MinVarianceScale;

        /// <summary>VSM light bleeding reduction amount, between 0 and 1.</summary>
        public float LightBleedReduction;

        public static VsmShadowOptions Default => new() {
            Anisotropy = 0,
            Mipmapping = false,
            Exponent = 5.54f,
            MinVarianceScale = 1.0f,
            LightBleedReduction = 0.2f,
        };
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

        /// <summary>
        /// Depth distance that constitute an edge for filtering. Must be positive. Default is 5cm.
        /// </summary>
        /// <remarks>
        /// This must be adjusted with the scene's scale and/or units. A value too low will result in high frequency
        /// noise, while a value too high will result in the loss of geometry edges. For AO, it is generally better to
        /// be too blurry than not enough.
        /// </remarks>
        public float BilateralThreshold;

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
            BilateralThreshold = 0.05f,
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

    /// <summary>
    /// Options to control the bloom effect.
    /// </summary>
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
        /// Resolution of bloom's vertical axis.
        /// </summary>
        /// <remarks>
        /// The minimum value is 2^levels and the the maximum is lower of the original resolution and 2048. This
        /// parameter is silently clamped to the minimum and maximum. It is highly recommended that this value be
        /// smaller than the target resolution after dynamic resolution is applied (horizontally and vertically).
        /// </remarks>
        public int Resolution;

        /// <summary>
        /// Bloom's aspect ratio (x/y), for artistic purposes.
        /// </summary>
        public float Anamorphism;

        /// <summary>
        /// Number of successive blurs to achieve the blur effect, the minimum is 3 and the maximum is 11.
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

        /// <summary>
        /// Enable screen-space lens flare.
        /// </summary>
        public bool LensFlare;

        /// <summary>
        /// Enable starburst effect on lens flare.
        /// </summary>
        public bool Starburst;

        /// <summary>
        /// Amount of chromatic aberration.
        /// </summary>
        public float ChromaticAberration;

        /// <summary>
        /// Number of flare "ghosts".
        /// </summary>
        public int GhostCount;

        /// <summary>
        /// Spacing of the ghost in screen units [0, 1[.
        /// </summary>
        public float GhostSpacing;

        /// <summary>
        /// HDR threshold for the ghosts.
        /// </summary>
        public float GhostThreshold;

        /// <summary>
        /// thickness of halo in vertical screen units, 0 to disable.
        /// </summary>
        public float HaloThickness;

        /// <summary>
        /// radius of halo in vertical screen units [0, 0.5].
        /// </summary>
        public float HaloRadius;

        /// <summary>
        /// HDR threshold for the halo.
        /// </summary>
        public float HaloThreshold;

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
            Starburst = true,
            ChromaticAberration = 0.005f,
            GhostCount = 4,
            GhostSpacing = 0.6f,
            GhostThreshold = 10.0f,
            HaloThickness = 0.1f,
            HaloRadius = 0.4f,
            HaloThreshold = 10.0f,
        };
    }

    public enum ViewBloomBlendMode : byte
    {
        /// <summary>Bloom is modulated by the strength parameter and added to the scene</summary>
        Add,

        /// <summary>Bloom is interpolated with the scene using the strength parameter</summary>
        Interpolate,
    }

    /// <summary>
    /// Options to control fog in the scene.
    /// </summary>
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
        public enum KernelFilter
        {
            None,
            Median,
        }

        /// <summary>Focus distance in world units</summary>
        public float FocusDistance;

        /// <summary>Circle of confusion scale factor (amount of blur)</summary>
        public float CocScale;

        /// <summary>Maximum aperture diameter in meters (zero to disable rotation)</summary>
        public float MaxApertureDiameter;

        /// <summary>Enable or disable depth of field effect</summary>
        public bool Enabled;

        /// <summary>
        /// Filter to use for filling gaps in the kernel.
        /// </summary>
        public KernelFilter Filter;

        /// <summary>
        /// Perform DoF processing at native resolution
        /// </summary>
        public bool NativeResolution;

        /// <summary>
        /// <para>Number of of rings used by the foreground kernel. The number of rings affects quality and performance.
        /// The actual number of sample per pixel is defined as (ringCount * 2 - 1)^2. Here are a few commonly used
        /// values:</para>
        /// <list>
        /// <item>3 rings :   25 ( 5x 5 grid)</item>
        /// <item>4 rings :   49 ( 7x 7 grid)</item>
        /// <item>5 rings :   81 ( 9x 9 grid)</item>
        /// <item>17 rings : 1089 (33x33 grid)</item>
        /// </list>
        /// <para>With a maximum circle-of-confusion of 32, it is never necessary to use more than 17 rings.</para>
        /// <para>Usually all three settings below are set to the same value, however, it is often acceptable to use a
        /// lower ring count for the "fast tiles", which improves performance. Fast tiles are regions of the screen
        /// where every pixels have a similar circle-of-confusion radius.</para>
        /// <para>A value of 0 means default, which is 5 on desktop and 3 on mobile.</para>
        /// </summary>
        public int ForegroundRingCount;

        /// <summary>
        /// Number of of rings used by the background kernel. The number of rings affects quality and performance.
        /// </summary>
        public int BackgroundRingCount;

        /// <summary>
        /// Number of of rings used by the fast gather kernel. The number of rings affects quality and performance.
        /// </summary>
        public int FastGatherRingCount;

        /// <summary>
        /// Maximum circle-of-confusion in pixels for the foreground, must be in [0, 32] range. A value of 0 means
        /// default, which is 32 on desktop and 24 on mobile.
        /// </summary>
        public int MaxForegroundCOC;

        /// <summary>
        /// Maximum circle-of-confusion in pixels for the background, must be in [0, 32] range. A value of 0 means
        /// default, which is 32 on desktop and 24 on mobile.
        /// </summary>
        public int MaxBackgroundCOC;

        public static ViewDepthOfFieldOptions Default => new() {
            FocusDistance = 10f,
            CocScale = 1f,
            MaxApertureDiameter = 0.1f,
            Enabled = false,
            Filter = KernelFilter.Median,
        };
    }

    /// <summary>
    /// Options to control the vignetting effect.
    /// </summary>
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

    /// <summary>
    /// Options for Temporal Anti-aliasing (TAA).
    /// </summary>
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
