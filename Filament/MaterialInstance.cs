using System;

namespace Filament
{
    public class MaterialInstance : FilamentBase<MaterialInstance>
    {
        #region Methods

        private MaterialInstance(IntPtr ptr) : base(ptr)
        {
        }

        internal static MaterialInstance GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new MaterialInstance(newPtr));
        }

        public void SetParameter(string name, bool x)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterBool(NativePtr, name, x);
        }

        public void SetParameter(string name, bool x, bool y)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterBool2(NativePtr, name, x, y);
        }

        public void SetParameter(string name, bool x, bool y, bool z)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterBool3(NativePtr, name, x, y, z);
        }

        public void SetParameter(string name, bool x, bool y, bool z, bool w)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterBool4(NativePtr, name, x, y, z, w);
        }

        public void SetParameter(string name, int x)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterInt(NativePtr, name, x);
        }

        public void SetParameter(string name, int x, int y)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterInt2(NativePtr, name, x, y);
        }

        public void SetParameter(string name, int x, int y, int z)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterInt3(NativePtr, name, x, y, z);
        }

        public void SetParameter(string name, int x, int y, int z, int w)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterInt4(NativePtr, name, x, y, z, w);
        }

        public void SetParameter(string name, float x)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterFloat(NativePtr, name, x);
        }

        public void SetParameter(string name, float x, float y)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterFloat2(NativePtr, name, x, y);
        }

        public void SetParameter(string name, float x, float y, float z)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterFloat3(NativePtr, name, x, y, z);
        }

        public void SetParameter(string name, float x, float y, float z, float w)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterFloat4(NativePtr, name, x, y, z, w);
        }

        public void SetParameter(string name, RgbType type, Color color)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterRgbColor(NativePtr, name, (byte) type, color.R, color.G, color.B);
        }

        public void SetParameter(string name, RgbaType type, Color color)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterRgbaColor(NativePtr, name, (byte) type, color.R, color.G, color.B, color.A);
        }

        public void SetParameter(string name, Texture texture, TextureSampler sampler)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterTexture(NativePtr, texture.NativePtr, name, sampler.NativePtr);
        }

        #endregion
    }
}
