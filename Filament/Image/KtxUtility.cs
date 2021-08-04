namespace Filament.Image
{
    public static class KtxUtility
    {
        #region Method

        public static Texture CreateTexture(Engine engine, KtxBundle bundle, bool forceSrgb)
        {
            return Texture.GetOrCreateCache(
                Native.Image.KtxUtility.CreateTexture(engine.NativePtr, bundle.NativePtr, forceSrgb)
            );
        }

        #endregion
    }
}
