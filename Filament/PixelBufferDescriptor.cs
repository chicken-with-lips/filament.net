using Filament.Image;

namespace Filament
{
    /// <summary>
    /// A descriptor to an image in main memory, typically used to transfer image data from the CPU to the GPU.
    /// </summary>
    public class PixelBufferDescriptor
    {
        #region Properties

        public int Left { get; private set; } = 0;
        public int Top { get; private set; }
        public int Stride { get; private set; }
        public PixelDataFormat Format { get; private set; }
        public int ImageSize { get; private set; }
        public CompressedPixelDataType CompressedFormat { get; private set; }
        public PixelDataType Type { get; private set; }
        public int Alignment { get; private set; }

        public byte[] Buffer { get; private set; }

        public LinearImage LinearImage { get; private set; }

        #endregion

        /// <summary>
        /// Creates a new PixelBufferDescriptor referencing an image in main memory
        /// </summary>
        /// <param name="buffer">Buffer containing the image</param>
        /// <param name="format">Format of the image pixels</param>
        /// <param name="type">Type of the image pixels</param>
        /// <param name="alignment">Alignment in bytes of pixel rows</param>
        /// <param name="left">Left coordinate in pixels</param>
        /// <param name="top">Top coordinate in pixels</param>
        /// <param name="stride">Stride of a row in pixels</param>
        public PixelBufferDescriptor(
            byte[] buffer,
            PixelDataFormat format, PixelDataType type, int alignment,
            int left, int top, int stride)
        {
            Buffer = buffer;
            Format = format;
            Type = type;

            Left = left;
            Top = top;
            Stride = stride;
            Alignment = alignment;
        }

        /// <summary>
        /// Creates a new PixelBufferDescriptor referencing an image in main memory
        /// </summary>
        /// <param name="linearImage">LinearImage to use for data</param>
        /// <param name="format">Format of the image pixels</param>
        /// <param name="type">Type of the image pixels</param>
        /// <param name="alignment">Alignment in bytes of pixel rows</param>
        /// <param name="left">Left coordinate in pixels</param>
        /// <param name="top">Top coordinate in pixels</param>
        /// <param name="stride">Stride of a row in pixels</param>
        public PixelBufferDescriptor(
            LinearImage linearImage,
            PixelDataFormat format, PixelDataType type, int alignment,
            int left, int top, int stride)
        {
            LinearImage = linearImage;
            Format = format;
            Type = type;

            Left = left;
            Top = top;
            Stride = stride;
            Alignment = alignment;
        }

        /// <summary>
        /// Creates a new PixelBufferDescriptor referencing an image in main memory
        /// </summary>
        /// <param name="buffer">Buffer containing the image</param>
        /// <param name="format">Format of the image pixels</param>
        /// <param name="type">Type of the image pixels</param>
        public PixelBufferDescriptor(
            byte[] buffer,
            PixelDataFormat format, PixelDataType type)
        {
            Buffer = buffer;
            Stride = 0;
            Format = format;
            Type = type;
            Alignment = 1;
        }

        /// <summary>
        /// Creates a new PixelBufferDescriptor referencing an image in main memory
        /// </summary>
        /// <param name="linearImage">LinearImage to use for data</param>
        /// <param name="format">Format of the image pixels</param>
        /// <param name="type">Type of the image pixels</param>
        public PixelBufferDescriptor(
            LinearImage linearImage,
            PixelDataFormat format, PixelDataType type)
        {
            LinearImage = linearImage;
            Stride = 0;
            Format = format;
            Type = type;
            Alignment = 1;
        }

        /// <summary>
        /// Creates a new PixelBufferDescriptor referencing a compressed image in main memory
        /// </summary>
        /// <param name="buffer">Buffer containing the image</param>
        /// <param name="format">Compressed format of the image</param>
        /// <param name="imageSize">Compressed size of the image</param>
        public PixelBufferDescriptor(
            byte[] buffer,
            CompressedPixelDataType format, int imageSize)
        {
            Buffer = buffer;
            ImageSize = imageSize;
            CompressedFormat = format;
            Type = PixelDataType.Compressed;
            Alignment = 1;
        }

        /// <summary>
        /// Creates a new PixelBufferDescriptor referencing a compressed image in main memory
        /// </summary>
        /// <param name="linearImage">LinearImage to use for data</param>
        /// <param name="format">Compressed format of the image</param>
        /// <param name="imageSize">Compressed size of the image</param>
        public PixelBufferDescriptor(
            LinearImage linearImage,
            CompressedPixelDataType format, int imageSize)
        {
            LinearImage = linearImage;
            ImageSize = imageSize;
            CompressedFormat = format;
            Type = PixelDataType.Compressed;
            Alignment = 1;
        }
    }
}
