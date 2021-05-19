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

    /// <summary>
    /// A swap chain represents an Operating System's native renderable surface.
    /// </summary>
    /// <remarks>
    /// Typically it's a native window or a view. Because a SwapChain is initialized from a native object, it is given
    /// to filament as an IntPtr, which must be of the proper type for each platform filament is running on.
    /// </remarks>
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
