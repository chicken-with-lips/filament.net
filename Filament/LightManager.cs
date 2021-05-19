using System;
using OpenTK.Mathematics;

namespace Filament
{
    /// <summary>
    /// Used Builder to construct a Light instance.
    /// </summary>
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

        /// <summary>
        /// Creates a light builder and set the light's type.
        /// </summary>
        public static LightBuilder Create(LightType type)
        {
            return GetOrCreateCache(
                Native.LightBuilder.CreateBuilder((byte) type)
            );
        }

        /// <summary>
        /// <para>Whether this Light casts shadows (disabled by default).</para>
        /// <para>Warning: Only a <see cref="LightType.Directional"/>, <see cref="LightType.Sun"/>,
        /// <see cref="LightType.Spot"/>, or <see cref="LightType.FocusedSpot"/> light can cast shadows</para>
        /// </summary>
        /// <param name="enable">Enables or disables casting shadows from this Light.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithCastShadows(bool enable)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.CastShadows(NativePtr, enable);

            return this;
        }

        /// <summary>
        /// <para>Whether this light casts light (enabled by default).</para>
        /// <para>Note: In some situations it can be useful to have a light in the scene that doesn't actually emit
        /// light, but does cast shadows.</para>
        /// </summary>
        /// <param name="enable">Enables or disables lighting from this Light.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithCastLight(bool enable)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.CastLight(NativePtr, enable);

            return this;
        }

        /// <summary>
        /// <para>Sets the initial position of the light in world space.</para>
        /// <para>The Light's position is ignored for directional lights (Type.DIRECTIONAL or Type.SUN)</para>
        /// </summary>
        /// <param name="position">Light's position in world space. The default is at the origin.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithPosition(Vector3 position)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.Position(NativePtr, position.X, position.Y, position.Z);

            return this;
        }

        /// <summary>
        /// <para>Sets the initial direction of a light in world space.</para>
        /// <para>Note: The Light's direction is ignored for <see cref="LightType.Point"/> lights.</para>
        /// </summary>
        /// <param name="direction">
        /// Light's direction in world space. Should be a unit vector. The default is {0,-1,0}.
        /// </param>
        public LightBuilder WithDirection(Vector3 direction)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.Direction(NativePtr, direction.X, direction.Y, direction.Z);

            return this;
        }

        /// <summary>
        /// Sets the initial color of a light.
        /// </summary>
        /// </summary>
        /// <param name="linearColor">
        /// Color of the light specified in the linear sRGB color-space. The default is white {1,1,1}.
        /// </param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithColor(LinearColor linearColor)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.Color(NativePtr, linearColor.R, linearColor.G, linearColor.B);

            return this;
        }

        /// <summary>
        /// <para>Sets the initial intensity of a spot or point light in candela.</para>
        /// <para>
        /// Note: This method is equivalent to calling <see cref="WithIntensity(float)"/> for directional lights
        /// (Type.DIRECTIONAL or Type.SUN).
        /// </para>
        /// <para>This method overrides any prior calls to <see cref="WithIntensity(float)"/> or
        /// <see cref="WithIntensityCandela"/>.</para>
        /// </summary>
        /// <param name="intensity">Luminous intensity in candela.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithIntensityCandela(float intensity)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.IntensityCandela(NativePtr, intensity);

            return this;
        }

        /// <summary>
        /// <para>Sets the initial intensity of a light.</para>
        /// <param>For example, the sun's illuminance is about 100,000 lux.</para>
        /// <para>Note: This method overrides any prior calls to <see cref="WithIntensity(float)"/> or
        /// <see cref="WithIntensityCandela"/>.</para>
        /// </summary>
        /// <param name="intensity">
        /// <para>This parameter depends on the Light.Type:</para>
        /// <list>
        /// <item>- For directional lights, it specifies the illuminance in lux (or lumen/m^2).</item>
        /// <item>- For point lights and spot lights, it specifies the luminous power in lumen.</item>
        /// </list>
        /// </param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithIntensity(float intensity)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.IntensityJF(NativePtr, intensity);

            return this;
        }

        /// <summary>
        /// <para>Sets the initial intensity of a light in watts.</para>
        /// <para>Note: This call is equivalent to WithIntensity(efficiency * 683 * watts)</para>
        /// <para>Note: This method overrides any prior calls to <see cref="WithIntensity(float)"/> or
        /// <see cref="WithIntensityCandela"/>.</para>
        /// </summary>
        /// <param name="watts">Energy consumed by a lightbulb. It is related to the energy produced and ultimately the
        /// brightness by the \p efficiency parameter. This value is often available on the packaging of commercial
        /// lightbulbs.</param>
        /// <param name="efficiency">Efficiency in percent. This depends on the type of lightbulb used.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithIntensity(float watts, float efficiency)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.IntensityJFF(NativePtr, watts, efficiency);

            return this;
        }

        /// <summary>
        /// <para>Set the falloff distance for point lights and spot lights.</para>
        /// <para>At the falloff distance, the light has no more effect on objects.</para>
        /// <para>The falloff distance essentially defines a "sphere of influence" around the light, and therefore has
        /// an impact on performance. Larger falloffs might reduce performance significantly, especially when many
        /// lights are used.</para>
        /// <para>Try to avoid having a large number of light's spheres of influence overlap.</para>
        /// <para>Note:The Light's falloff is ignored for directional lights (Type.DIRECTIONAL or Type.SUN)</para>
        /// </summary>
        /// <param name="radius">Falloff distance in world units. Default is 1 meter.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithFalloff(float radius)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.Falloff(NativePtr, radius);

            return this;
        }

        /// <summary>
        /// <para>Defines a spot light's angular falloff attenuation.</para>
        /// <para>A spot light is defined by a position, a direction and two cones, <paramref name="inner"/> and
        /// <paramref name="outer"/>. These two cones are used to define the angular falloff attenuation of the spot
        /// light and are defined by the angle from the center axis to where the falloff begins (i.e. cones are defined
        /// by their half-angle).</para>
        /// <para>Note: The spot light cone is ignored for directional and point lights.</para>
        /// </summary>
        /// <param name="inner">Inner cone angle in radians between 0 and @f$ \pi/2 @f$</param>
        /// <param name="outer">Outer cone angle in radians between <param name="inner"/> and @f$ \pi/2 @f$</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithSpotLightCone(float inner, float outer)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.SpotLightCone(NativePtr, inner, outer);

            return this;
        }

        /// <summary>
        /// <para>Defines the angular radius of the sun, in degrees, between 0.25° and 20.0°</para>
        /// <para>The Sun as seen from Earth has an angular size of 0.526° to 0.545°</para>
        /// </summary>
        /// <param name="angularRadius">Sun's radius in degree. Default is 0.545°.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithSunAngularRadius(float angularRadius)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.SunAngularRadius(NativePtr, angularRadius);

            return this;
        }

        /// <summary>
        /// Defines the halo radius of the sun. The radius of the halo is defined as a multiplier of the sun angular
        /// radius.
        /// </summary>
        /// <param name="haloSize">Radius multiplier. Default is 10.0.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithSunHaloSize(float haloSize)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.SunHaloSize(NativePtr, haloSize);

            return this;
        }

        /// <summary>
        /// Defines the halo falloff of the sun. The falloff is a dimensionless number used as an exponent.
        /// </summary>
        /// <param name="haloFalloff">Hlo falloff. Default is 80.0.</param>
        /// <returns>This Builder, for chaining calls.</returns>
        public LightBuilder WithSunHaloFalloff(float haloFalloff)
        {
            ThrowExceptionIfDisposed();

            Native.LightBuilder.SunHaloFalloff(NativePtr, haloFalloff);

            return this;
        }

        /// <summary>
        /// <para>Adds the Light component to an entity.</para>
        /// <para>If this component already exists on the given entity, it is first destroyed as if
        /// <see cref="Engine.Destroy(int)"/> was called.</para>
        /// <para>Warning: Currently, only 2048 lights can be created on a given Engine.</para>
        /// </summary>
        /// <param name="engine">Reference to the <see cref="Engine"/> to associate this light with.</param>
        /// <param name="entity">Entity to add the light component to.</param>
        /// <returns>True if the component was created successfully, false otherwise.</returns>
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
