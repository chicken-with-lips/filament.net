namespace Filament.SampleData
{
    public class MonkeyDataLoader : AbstractDataLoader
    {
        #region Constants

        private const string Package = "generated/resources/monkey.bin";


        private const int SuzanneOffset = 0;
        private const int SuzanneSize = 272768;
        private const int AlbedoS3tcOffset = 272768;
        private const int AlbedoS3tcSize = 1398236;
        private const int RoughnessOffset = 1671004;
        private const int RoughnessSize = 1398236;
        private const int MetallicOffset = 3069240;
        private const int MetallicSize = 1398236;
        private const int AoOffset = 4467476;
        private const int AoSize = 1398236;
        private const int NormalOffset = 5865712;
        private const int NormalSize = 1065180;

        #endregion

        #region Methods

        public byte[] LoadSuzanne()
        {
            return Load(Package, SuzanneOffset, SuzanneSize);
        }

        public byte[] LoadAlbedoS3tc()
        {
            return Load(Package, AlbedoS3tcOffset, AlbedoS3tcSize);
        }

        public byte[] LoadRougness()
        {
            return Load(Package, RoughnessOffset, RoughnessSize);
        }

        public byte[] LoadMetallic()
        {
            return Load(Package, MetallicOffset, MetallicSize);
        }

        public byte[] LoadAo()
        {
            return Load(Package, AoOffset, AoSize);
        }

        public byte[] LoadNormal()
        {
            return Load(Package, NormalOffset, NormalSize);
        }
    }

    #endregion
}
