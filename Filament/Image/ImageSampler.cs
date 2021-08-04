namespace Filament.Image
{
    public static class ImageSampler
    {
        #region Methods

        public static LinearImage ResampleImage(LinearImage source, int width, int height, ImageSamplerFilter filter)
        {
            return LinearImage.GetOrCreateCache(
                Native.Image.ImageSampler.ResampleImage(source.NativePtr, width, height, (int) filter)
            );
        }

        #endregion
    }

    public enum ImageSamplerFilter
    {
        /// <summary>Selects MITCHELL or LANCZOS dynamically.</summary>
        Default,

        /// <summary>Computes the un-weighted average over the filter radius.</summary>
        Box,

        /// <summary>Copies the source sample nearest to the center of the filter.</summary>
        Nearest,

        /// <summary>Also known as "smoothstep", has some nice properties.</summary>
        Hermite,

        /// <summary>Standard Gaussian filter with sigma = 0.5</summary>
        GaussianScalars, //

        /// <summary>Same as GAUSSIAN_SCALARS, but interpolates unitized vectors.</summary>
        GaussianNormals, //

        /// <summary>Cubic resampling per Mitchell-Netravali, default for magnification.</summary>
        Mitchell,

        /// <summary>Popular sinc-based filter, default for minification.</summary>
        Lanczos,

        /// <summary>Takes a min val rather than avg, perhaps useful for depth maps and SDF's.</summary>
        Minimum
    };
}
