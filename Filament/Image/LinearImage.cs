using System;

namespace Filament.Image
{
    public class LinearImage : FilamentBase<LinearImage>
    {
        #region Methods

        private LinearImage(IntPtr ptr) : base(ptr)
        {
        }

        public LinearImage(int width, int height, int channels) : this(Native.Image.LinearImage.Create(width, height, channels))
        {
            ManuallyRegisterCache(NativePtr, this);
        }

        internal static LinearImage GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new LinearImage(newPtr));
        }

        public void SetPixelData(int column, int row, float r, float g = 0, float b = 0, float a = 0)
        {
            Native.Image.LinearImage.SetPixelData(NativePtr, column, row, r, g, b, a);
        }

        #endregion

        #region FilamentBase

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.Image.LinearImage.Destroy(NativePtr);
        }

        #endregion
    }
}
