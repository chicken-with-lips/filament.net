using System;
using System.Runtime.InteropServices;

namespace Filament.Native.Image
{
    public static class LinearImage
    {
        /// <summary>
        /// Allocates a zeroed-out image.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Image_LinearImage_Create")]
        public static extern IntPtr Create(int width, int height, int channels);

        /// <summary>
        /// Deallocates an image.
        /// </summary>
        /// <param name="nativeImage"></param>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Image_LinearImage_Destroy")]
        public static extern void Destroy(IntPtr nativeImage);

        /// <summary>
        /// Sets a specifix pixel.
        /// </summary>
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Image_LinearImage_SetPixelData")]
        public static extern void SetPixelData(IntPtr nativeImage, int column, int row, float r, float g, float b, float a);
    }
}
