using System;
using System.Numerics;

namespace Filament.Image
{
    public class KtxBundle : FilamentBase<KtxBundle>
    {
        #region Methods

        private KtxBundle(IntPtr ptr) : base(ptr)
        {
        }

        public KtxBundle(byte[] buffer) : this(buffer, buffer.Length)
        {
        }

        public KtxBundle(byte[] buffer, int count) :
            base(Native.Image.KtxBundle.Create(buffer, count))
        {
            ManuallyRegisterCache(NativePtr, this);
        }

        public bool GetSphericalHarmonics(out Vector3[] bands)
        {
            var parts = new float[9 * 3];
            bands = new Vector3[9];

            if (!Native.Image.KtxBundle.GetSphericalHarmonics(NativePtr, parts)) {
                return false;
            }

            for (var i = 0; i < 9; i++) {
                bands[i] = new Vector3(
                    parts[i * 3],
                    parts[i * 3 + 1],
                    parts[i * 3 + 2]
                );
            }

            return true;
        }

        internal static KtxBundle GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new KtxBundle(newPtr));
        }

        #endregion
    }
}
