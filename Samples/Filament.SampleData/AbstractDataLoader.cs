using System;
using System.IO;

namespace Filament.SampleData
{
    public abstract class AbstractDataLoader
    {
        #region Methods

        protected byte[] Load(string relativePath, int offset, int size)
        {
            var packFile = Path.Combine(Path.GetDirectoryName(typeof(AbstractDataLoader).Assembly.Location), relativePath);

            using (var stream = File.OpenRead(packFile)) {
                using (BinaryReader memoryStream = new BinaryReader(stream)) {
                    memoryStream.BaseStream.Seek(offset, SeekOrigin.Begin);

                    return memoryStream.ReadBytes(size);
                }
            }
        }

        #endregion
    }
}
