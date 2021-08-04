namespace Filament.SampleData
{
    public class SampleAppDataLoader : AbstractDataLoader
    {
        #region Constants

        private const string Package = "generated/resources/filamentapp.bin";

#if DEBUG
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 907932;
        private const int AiDefaultTransOffset = 907932;
        private const int AiDefaultTransSize = 874821;
        private const int DepthVisualizerOffset = 1782753;
        private const int DepthVisualizerSize = 111224;
        private const int TransparentColorOffset = 1893977;
        private const int TransparentColorSize = 111911;
#elif RELEASE
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 365086;
        private const int AiDefaultTransOffset = 365086;
        private const int AiDefaultTransSize = 316034;
        private const int DepthVisualizerOffset = 681120;
        private const int DepthVisualizerSize = 22191;
        private const int TransparentColorOffset = 703311;
        private const int TransparentColorSize = 21899;
#endif

        #endregion

        #region Methods

        public byte[] LoadAiDefaultMat()
        {
            return Load(Package, AiDefaultMatOffset, AiDefaultMatSize);
        }

        public byte[] LoadAiDefaultTrans()
        {
            return Load(Package, AiDefaultTransOffset, AiDefaultTransSize);
        }

        public byte[] LoadDepthVisualizer()
        {
            return Load(Package, DepthVisualizerOffset, DepthVisualizerSize);
        }

        public byte[] LoadTransparentColor()
        {
            return Load(Package, TransparentColorOffset, TransparentColorSize);
        }

        #endregion
    }
}
