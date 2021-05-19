using System;

namespace Filament
{
    /// <summary>
    /// When added to a Scene, the Skybox fills all untouched pixels.
    /// </summary>
    /// <remarks>
    /// Currently only Texture based sky boxes are supported.
    /// </remarks>
    public class Skybox : FilamentBase<Skybox>
    {
        #region Properties

        /// <summary>
        /// The visibility mask bits.
        /// </summary>
        public int LayerMask {
            get {
                ThrowExceptionIfDisposed();

                return Native.Skybox.GetLayerMask(NativePtr);
            }
        }

        /// <summary>
        /// The skybox's intensity in cd/m^2.
        /// </summary>
        public float Intensity {
            get {
                ThrowExceptionIfDisposed();

                return Native.Skybox.GetIntensity(NativePtr);
            }
        }

        public Color Color {
            set {
                ThrowExceptionIfDisposed();

                Native.Skybox.SetColor(NativePtr, value.R, value.G, value.B, value.A);
            }
        }

        /// <summary>
        /// The associated texture, or null if it does not exist.
        /// </summary>
        public Texture Texture {
            get {
                ThrowExceptionIfDisposed();

                return Texture.GetOrCreateCache(
                    Native.Skybox.GetTexture(NativePtr)
                );
            }
        }

        #endregion

        #region Methods

        private Skybox(IntPtr ptr) : base(ptr)
        {
        }

        internal static Skybox GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new Skybox(newPtr));
        }

        /// <summary>
        /// <para>Sets bits in a visibility mask. By default, this is 0x1.</para>
        /// <para>This provides a simple mechanism for hiding or showing this Skybox in a Scene.</para>
        /// <para>For example, to set bit 1 and reset bits 0 and 2 while leaving all other bits unaffected, call:
        /// SetLayerMask(7, 2).
        /// </para>
        /// </summary>
        /// <param name="select">The set of bits to affect.</param>
        /// <param name="value">The replacement values for the affected bits.</param>
        public void SetLayerMask(int select, int value)
        {
            ThrowExceptionIfDisposed();

            Native.Skybox.SetLayerMask(NativePtr, select, value);
        }

        #endregion
    }

    /// <summary>
    /// Use Builder to construct an Skybox object.
    /// </summary>
    public class SkyboxBuilder : FilamentBase<SkyboxBuilder>
    {
        #region Methods

        private SkyboxBuilder(IntPtr ptr) : base(ptr)
        {
        }

        internal static SkyboxBuilder GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new SkyboxBuilder(newPtr));
        }

        /// <summary>
        /// <para>Set the environment map (i.e. the skybox content).</para>
        /// <para>The Skybox is rendered as though it were an infinitely large cube with the camera inside it. This
        /// means that the cubemap which is mapped onto the cube's exterior will appear mirrored. This follows the
        /// OpenGL conventions.</para>
        /// <para>The cmgen tool generates reflection maps by default which are therefore ideal to use as skyboxes.</para>
        /// </summary>
        /// <param name="texture">This Texture must be a cube map.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public SkyboxBuilder WithEnvironment(Texture texture)
        {
            ThrowExceptionIfDisposed();

            Native.SkyboxBuilder.Environment(NativePtr, texture.NativePtr);

            return this;
        }

        /// <summary>
        /// Indicates whether the sun should be rendered. The sun can only be rendered if there is at least one light of
        /// type SUN in the scene. The default value is false.
        /// </summary>
        /// <param name="shouldShow">True if the sun should be rendered, false otherwise.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public SkyboxBuilder WithSun(bool shouldShow)
        {
            ThrowExceptionIfDisposed();

            Native.SkyboxBuilder.ShowSun(NativePtr, shouldShow);

            return this;
        }

        /// <summary>
        /// <para>Skybox intensity when no IndirectLight is set.</para>
        /// <para>This call is ignored when an IndirectLight is set, otherwise it is used in its place.</para>
        /// </summary>
        /// <param name="intensity">Scale factor applied to the skybox texel values such that the result is in cd/m^2
        /// (lux) units (default = 30000).</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public SkyboxBuilder WithIntensity(float intensity)
        {
            ThrowExceptionIfDisposed();

            Native.SkyboxBuilder.Intensity(NativePtr, intensity);

            return this;
        }

        /// <summary>
        /// Sets the skybox to a constant color. Default is opaque black. Ignored if an environment is set.
        /// </summary>
        /// <param name="color">This Builder, for chaining calls.</param>
        public SkyboxBuilder WithColor(Color color)
        {
            ThrowExceptionIfDisposed();

            Native.SkyboxBuilder.Color(NativePtr, color.R, color.G, color.B, color.A);

            return this;
        }

        /// <summary>
        /// Creates the Skybox object and returns a pointer to it.
        /// </summary>
        /// <param name="engine">Reference to the <see cref="Engine"/> to associate this Skybox with.</param>
        /// <returns>The newly created object, or nullptr if the light couldn't be created.</returns>
        public Skybox Build(Engine engine)
        {
            ThrowExceptionIfDisposed();

            return Skybox.GetOrCreateCache(
                Native.SkyboxBuilder.Build(NativePtr, engine.NativePtr)
            );
        }

        /// <summary>
        /// Creates a new builder.
        /// </summary>
        /// <returns>The newly created builder.</returns>
        public static SkyboxBuilder Create()
        {
            return GetOrCreateCache(
                Native.SkyboxBuilder.CreateBuilder()
            );
        }

        #endregion

        #region FilamentBase

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.SkyboxBuilder.DestroyBuilder(NativePtr);
        }

        #endregion
    }
}
