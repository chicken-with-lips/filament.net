using System;

namespace Filament
{
    /// <summary>
    /// TextureSampler defines how a texture is accessed.
    /// </summary>
    public class TextureSampler : FilamentBase<TextureSampler>
    {
        #region Methods

        private TextureSampler(IntPtr ptr) : base(ptr)
        {
        }

        public TextureSampler(SamplerMinFilter min, SamplerMagFilter mag, SamplerWrapMode wrapS = SamplerWrapMode.ClampToEdge, SamplerWrapMode wrapT = SamplerWrapMode.ClampToEdge, SamplerWrapMode wrapR = SamplerWrapMode.ClampToEdge, float anisotropy = 0)
            : this(Native.TextureSampler.CreateSampler((byte) min, (byte) mag, (byte) wrapS, (byte) wrapT, (byte) wrapR, anisotropy))
        {
            GetOrCreateCache(NativePtr);
        }

        internal static TextureSampler GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new TextureSampler(newPtr));
        }

        #endregion
    }
}
