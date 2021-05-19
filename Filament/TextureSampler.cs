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

        public TextureSampler(SamplerMinFilter min, SamplerMagFilter mag, SamplerWrapMode str = SamplerWrapMode.ClampToEdge)
            : this(Native.TextureSampler.CreateSampler((byte) min, (byte) mag, (byte) str, (byte) str, (byte) str))
        {
            ManuallyRegisterCache(NativePtr, this);
        }

        internal static TextureSampler GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new TextureSampler(newPtr));
        }

        #endregion
    }
}
