using System;
using OpenTK.Mathematics;

namespace Filament
{
    public class LightBuilder : FilamentBase<LightBuilder>
    {
        #region Methods

        private LightBuilder(IntPtr ptr) : base(ptr)
        {
        }

        internal static LightBuilder GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new LightBuilder(newPtr));
        }

        public static LightBuilder Create(LightType type)
        {
            return GetOrCreateCache(
                Native.LightBuilder.CreateBuilder((byte) type)
            );
        }

        public LightBuilder WithCastShadows(bool enable)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.CastShadows(NativePtr, enable);

            return this;
        }

        public LightBuilder WithCastLight(bool enable)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.CastLight(NativePtr, enable);

            return this;
        }

        public LightBuilder WithPosition(Vector3 position)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.Position(NativePtr, position.X, position.Y, position.Z);

            return this;
        }

        public LightBuilder WithDirection(Vector3 direction)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.Direction(NativePtr, direction.X, direction.Y, direction.Z);

            return this;
        }

        public LightBuilder WithColor(LinearColor linearColor)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.Color(NativePtr, linearColor.R, linearColor.G, linearColor.B);

            return this;
        }

        public LightBuilder WithIntensityCandela(float intensity)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.IntensityCandela(NativePtr, intensity);

            return this;
        }

        public LightBuilder WithIntensity(float intensity)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.IntensityJF(NativePtr, intensity);

            return this;
        }

        public LightBuilder WithIntensity(float watts, float efficiency)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.IntensityJFF(NativePtr, watts, efficiency);

            return this;
        }

        public LightBuilder WithFalloff(float radius)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.Falloff(NativePtr, radius);

            return this;
        }

        public LightBuilder WithSpotLightCone(float inner, float outer)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.SpotLightCone(NativePtr, inner, outer);

            return this;
        }

        public LightBuilder WithSunAngularRadius(float angularRadius)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.SunAngularRadius(NativePtr, angularRadius);

            return this;
        }

        public LightBuilder WithSunHaloSize(float haloSize)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.SunHaloSize(NativePtr, haloSize);

            return this;
        }

        public LightBuilder WithSunHaloFalloff(float haloFalloff)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.SunHaloFalloff(NativePtr, haloFalloff);

            return this;
        }

        public bool Build(Engine engine, int entity)
        {
            ThrowExceptionIfDisposed();

            return Native.LightBuilder.Build(NativePtr, engine.NativePtr, entity);
        }

        #endregion

        #region FilamentBase

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.LightBuilder.DestroyBuilder(NativePtr);
        }

        #endregion
    }

    public enum LightType : byte
    {
        /// <summary>Directional light that also draws a sun's disk in the sky.</summary>
        Sun,

        /// <summary>Directional light, emits light in a given direction.</summary>
        Directional,

        /// <summary>Point light, emits light from a position, in all directions.</summary>
        Point,

        /// <summary>Physically correct spot light.</summary>
        FocusedSpot,

        /// <summary>Spot light with coupling of outer cone and illumination disabled.</summary>
        Spot,
    }
}
