using System;
using OpenTK.Mathematics;

namespace Filament
{
    public class IndirectLight : FilamentBase<IndirectLight>
    {
        #region Properties

        public float Intensity {
            get {
                ThrowExceptionIfDisposed();

                return Native.IndirectLight.GetIntensity(NativePtr);
            }
            set {
                ThrowExceptionIfDisposed();

                Native.IndirectLight.SetIntensity(NativePtr, value);
            }
        }

        public Matrix3 Rotation {
            set {
                ThrowExceptionIfDisposed();

                Native.IndirectLight.SetRotation(
                    NativePtr,
                    value.M11, value.M12, value.M13,
                    value.M21, value.M22, value.M23,
                    value.M31, value.M32, value.M33
                );
            }
        }

        #endregion

        #region Methods

        private IndirectLight(IntPtr ptr) : base(ptr)
        {
        }

        internal static IndirectLight GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new IndirectLight(newPtr));
        }

        #endregion
    }

    public class IndirectLightBuilder : FilamentBase<IndirectLightBuilder>
    {
        #region Methods

        private IndirectLightBuilder(IntPtr ptr) : base(ptr)
        {
        }

        internal static IndirectLightBuilder GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new IndirectLightBuilder(newPtr));
        }

        public IndirectLightBuilder WithReflections(Texture cubemap)
        {
            ThrowExceptionIfDisposed();

            Native.IndirectLightBuilder.Reflections(NativePtr, cubemap.NativePtr);

            return this;
        }

        public IndirectLightBuilder WithIntensity(float envIntensity)
        {
            ThrowExceptionIfDisposed();

            Native.IndirectLightBuilder.Intensity(NativePtr, envIntensity);

            return this;
        }

        public IndirectLightBuilder WithIrradiance(int bands, Vector3[] sh)
        {
            ThrowExceptionIfDisposed();

            float[] values = new float[sh.Length * 3];

            for (var i = 0; i < sh.Length; i++) {
                values[i * 3] = sh[i].X;
                values[i * 3 + 1] = sh[i].Y;
                values[i * 3 + 2] = sh[i].Z;
            }

            Native.IndirectLightBuilder.Irradiance(NativePtr, bands, values);

            return this;
        }

        public IndirectLight Build(Engine engine)
        {
            ThrowExceptionIfDisposed();

            return IndirectLight.GetOrCreateCache(
                Native.IndirectLightBuilder.Build(NativePtr, engine.NativePtr)
            );
        }

        public static IndirectLightBuilder Create()
        {
            return GetOrCreateCache(
                Native.IndirectLightBuilder.Create()
            );
        }

        #endregion
    }
}
