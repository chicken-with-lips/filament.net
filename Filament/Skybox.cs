using System;

namespace Filament
{
    public class Skybox : FilamentBase<Skybox>
    {
        #region Properties

        public int LayerMask {
            get {
                ThrowExceptionIfDisposed();

                return Native.Skybox.GetLayerMask(NativePtr);
            }
        }

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

        public void SetLayerMask(int select, int value)
        {
            ThrowExceptionIfDisposed();

            Native.Skybox.SetLayerMask(NativePtr, select, value);
        }

        #endregion
    }

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

        public SkyboxBuilder WithEnvironment(Texture texture)
        {
            ThrowExceptionIfDisposed();

            Native.SkyboxBuilder.Environment(NativePtr, texture.NativePtr);

            return this;
        }

        public SkyboxBuilder WithSun(bool shouldShow)
        {
            ThrowExceptionIfDisposed();

            Native.SkyboxBuilder.ShowSun(NativePtr, shouldShow);

            return this;
        }

        public SkyboxBuilder WithIntensity(float intensity)
        {
            ThrowExceptionIfDisposed();

            Native.SkyboxBuilder.Intensity(NativePtr, intensity);

            return this;
        }

        public SkyboxBuilder WithColor(Color color)
        {
            ThrowExceptionIfDisposed();

            Native.SkyboxBuilder.Color(NativePtr, color.R, color.G, color.B, color.A);

            return this;
        }

        public Skybox Build(Engine engine)
        {
            ThrowExceptionIfDisposed();

            return Skybox.GetOrCreateCache(
                Native.SkyboxBuilder.Build(NativePtr, engine.NativePtr)
            );
        }

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
