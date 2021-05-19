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

        /// <summary>
        /// Set a uniform by name.
        /// </summary>
        /// <param name="name">Name of the parameter as defined by Material.</param>
        /// <param name="x">Value of the parameter to set.</param>
        public void SetParameter(string name, bool x)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterBool(NativePtr, name, x);
        }

        /// <summary>
        /// Set a uniform array by name.
        /// </summary>
        /// <param name="name">Name of the parameter array as defined by Material.</param>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        public void SetParameter(string name, bool x, bool y)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterBool2(NativePtr, name, x, y);
        }

        /// <summary>
        /// Set a uniform array by name.
        /// </summary>
        /// <param name="name">Name of the parameter array as defined by Material.</param>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        /// <param name="z">Third value.</param>
        public void SetParameter(string name, bool x, bool y, bool z)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterBool3(NativePtr, name, x, y, z);
        }

        /// <summary>
        /// Set a uniform array by name.
        /// </summary>
        /// <param name="name">Name of the parameter array as defined by Material.</param>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        /// <param name="z">Third value.</param>
        /// <param name="w">Fourth value.</param>
        public void SetParameter(string name, bool x, bool y, bool z, bool w)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterBool4(NativePtr, name, x, y, z, w);
        }

        /// <summary>
        /// Set a uniform by name.
        /// </summary>
        /// <param name="name">Name of the parameter as defined by Material.</param>
        /// <param name="x">Value of the parameter to set.</param>
        public void SetParameter(string name, int x)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterInt(NativePtr, name, x);
        }

        /// <summary>
        /// Set a uniform array by name.
        /// </summary>
        /// <param name="name">Name of the parameter array as defined by Material.</param>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        public void SetParameter(string name, int x, int y)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterInt2(NativePtr, name, x, y);
        }

        /// <summary>
        /// Set a uniform array by name.
        /// </summary>
        /// <param name="name">Name of the parameter array as defined by Material.</param>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        /// <param name="z">Third value.</param>
        public void SetParameter(string name, int x, int y, int z)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterInt3(NativePtr, name, x, y, z);
        }

        /// <summary>
        /// Set a uniform array by name.
        /// </summary>
        /// <param name="name">Name of the parameter array as defined by Material.</param>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        /// <param name="z">Third value.</param>
        /// <param name="w">Fourth value.</param>
        public void SetParameter(string name, int x, int y, int z, int w)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterInt4(NativePtr, name, x, y, z, w);
        }

        /// <summary>
        /// Set a uniform by name.
        /// </summary>
        /// <param name="name">Name of the parameter as defined by Material.</param>
        /// <param name="x">Value of the parameter to set.</param>
        public void SetParameter(string name, float x)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterFloat(NativePtr, name, x);
        }

        /// <summary>
        /// Set a uniform array by name.
        /// </summary>
        /// <param name="name">Name of the parameter array as defined by Material.</param>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        public void SetParameter(string name, float x, float y)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterFloat2(NativePtr, name, x, y);
        }

        /// <summary>
        /// Set a uniform array by name.
        /// </summary>
        /// <param name="name">Name of the parameter array as defined by Material.</param>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        /// <param name="z">Third value.</param>
        public void SetParameter(string name, float x, float y, float z)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterFloat3(NativePtr, name, x, y, z);
        }

        /// <summary>
        /// Set a uniform array by name.
        /// </summary>
        /// <param name="name">Name of the parameter array as defined by Material.</param>
        /// <param name="x">First value.</param>
        /// <param name="y">Second value.</param>
        /// <param name="z">Third value.</param>
        /// <param name="w">Fourth value.</param>
        public void SetParameter(string name, float x, float y, float z, float w)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterFloat4(NativePtr, name, x, y, z, w);
        }

        /// <summary>
        /// Set an RGB color as the named parameter. A conversion might occur depending on the specified type.
        /// </summary>
        /// <param name="name">Name of the parameter as defined by Material.</param>
        /// <param name="type">Whether the color value is encoded as Linear or sRGB.</param>
        /// <param name="color">Color value.</param>
        public void SetParameter(string name, RgbType type, Color color)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterRgbColor(NativePtr, name, (byte) type, color.R, color.G, color.B);
        }

        /// <summary>
        /// Set an RGBA color as the named parameter. A conversion might occur depending on the specified type.
        /// </summary>
        /// <param name="name">Name of the parameter as defined by Material.</param>
        /// <param name="type">Whether the color value is encoded as Linear or sRGB/A.</param>
        /// <param name="color">Color value.</param>
        public void SetParameter(string name, RgbaType type, Color color)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterRgbaColor(NativePtr, name, (byte) type, color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Set a texture as the named parameter.
        /// </summary>
        /// <param name="name">Name of the parameter as defined by Material.</param>
        /// <param name="texture">Texture object.</param>
        /// <param name="sampler">Sampler parameters.</param>
        public void SetParameter(string name, Texture texture, TextureSampler sampler)
        {
            ThrowExceptionIfDisposed();

            Native.MaterialInstance.SetParameterTexture(NativePtr, texture.NativePtr, name, sampler.NativePtr);
        }

        #endregion
    }
}
