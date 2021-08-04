using System;
using System.Runtime.InteropServices;

namespace Filament.Native.Image
{
    public static class KtxBundle
    {
        /// <summary>
        /// Creates a new bundle by deserializing the given data.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Image_KtxBundle_CreateData")]
        public static extern IntPtr Create(byte[] data, int count);

        /// <summary>
        /// Parses the key="sh" metadata and returns 3 bands of data.
        /// </summary>
        /// <remarks>Assumes 3 bands for a total of 9 RGB coefficients.</remarks>
        /// <returns>True if successful.</returns>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Image_KtxBundle_GetSphericalHarmonics")]
        public static extern bool GetSphericalHarmonics(IntPtr nativeBundle, float[] data);
    }
}
