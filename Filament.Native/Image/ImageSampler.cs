using System;
using System.Runtime.InteropServices;

namespace Filament.Native.Image
{
    public static class ImageSampler
    {
        /// <summary>
        /// Resizes or blurs the given linear image, producing a new linear image with the given dimensions.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Image_ImageSampler_ResampleImage")]
        public static extern IntPtr ResampleImage(IntPtr nativeImage, int width, int height, int filter);
    }
}
