namespace Filament.SampleData
{
    public class MonkeyDataLoader : AbstractDataLoader
    {
        #region Constants

        private const string Package = "generated/resources/monkey.bin";


        private const int SuzanneOffset = 0;
        private const int SuzanneSize = 272768;
        private const int AlbedoS3tcOffset = 272768;
        private const int AlbedoS3tcSize = 699172;
        private const int RoughnessOffset = 971940;
        private const int RoughnessSize = 1398209;
        private const int MetallicOffset = 2370149;
        private const int MetallicSize = 1398209;
        private const int AoOffset = 3768358;
        private const int AoSize = 1398209;
        private const int NormalOffset = 5166567;
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
