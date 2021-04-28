using System;

namespace Filament
{
    [Flags]
    public enum SwapChainConfig : uint
    {
        None = 0x0,
        Transparent = 0x1,
        Readable = 0x2,
        EnableXcb = 0x4,
        AppleCvPixelBuffer = 0x8
    }

    public class SwapChain : FilamentBase<SwapChain>
    {
        #region Methods

        private SwapChain(IntPtr ptr) : base(ptr)
        {
        }

        internal static SwapChain GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new SwapChain(newPtr));
        }

        #endregion
    }
}
