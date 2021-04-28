namespace Filament.SampleData
{
    public class SampleAppDataLoader : AbstractDataLoader
    {
        #region Constants

        private const string Package = "generated/resources/filamentapp.bin";

#if DEBUG
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 568713;
        private const int AiDefaultTransOffset = 568713;
        private const int AiDefaultTransSize = 544291;
        private const int DepthVisualizerOffset = 1113004;
        private const int DepthVisualizerSize = 78284;
        private const int TransparentColorOffset = 1191288;
        private const int TransparentColorSize = 78450;
#elif RELEASE
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 386107;
        private const int AiDefaultTransOffset = 386107;
        private const int AiDefaultTransSize = 335584;
        private const int DepthVisualizerOffset = 721691;
        private const int DepthVisualizerSize = 22087;
        private const int TransparentColorOffset = 743778;
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
