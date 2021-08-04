using System;
using System.Runtime.InteropServices;

namespace Filament.Native.Image
{
    public static class KtxUtility
    {

        /// <summary>
        /// Creates a Texture object from a KTX file and populates all of its faces and miplevels.
        /// </summary>
        /// <param name="engine">Used to create the Filament Texture.</param>
        /// <param name="bundle">In-memory representation of a KTX file.</param>
        /// <param name="forceSrgb">Forces the KTX-specified format into an SRGB format if possible.</param>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Image_KtxUtility_CreateTexture")]
        public static extern IntPtr CreateTexture(IntPtr engine, IntPtr bundle, bool forceSrgb);

    }
}
