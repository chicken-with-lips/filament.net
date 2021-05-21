namespace Filament.SampleData
{
    public class SampleAppDataLoader : AbstractDataLoader
    {
        #region Constants

        private const string Package = "generated/resources/filamentapp.bin";

#if DEBUG
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 569878;
        private const int AiDefaultTransOffset = 569878;
        private const int AiDefaultTransSize = 545456;
        private const int DepthVisualizerOffset = 1115334;
        private const int DepthVisualizerSize = 78862;
        private const int TransparentColorOffset = 1194196;
        private const int TransparentColorSize = 79028;
#elif RELEASE
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 379289;
        private const int AiDefaultTransOffset = 379289;
        private const int AiDefaultTransSize = 328966;
        private const int DepthVisualizerOffset = 708255;
        private const int DepthVisualizerSize = 22087;
        private const int TransparentColorOffset = 730342;
        private const int TransparentColorSize = 21794;
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
