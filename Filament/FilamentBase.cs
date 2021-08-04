using System;
using Filament.Managed;

namespace Filament
{
    public abstract class FilamentBase<T> : IDisposable
    {
        #region Members

        public IntPtr NativePtr { get; }

        private static InstanceCache<T> _cache = new();

        #endregion

        #region Methods

        protected FilamentBase(IntPtr ptr)
        {
            NativePtr = ptr;
        }

        ~FilamentBase()
        {
            Dispose(false);
        }

        protected static T GetOrCreateCache(IntPtr ptr, InstanceCache<T>.CreateInstance createInstance)
        {
            return _cache.GetOrCreate(ptr, createInstance);
        }

        protected static void ManuallyRegisterCache(IntPtr ptr, T instance)
        {
            _cache.ManuallyRegisterCache(ptr, instance);
        }

        #endregion

        #region IDisposable

        #region Properties

        public bool IsDisposed { get; private set; }

        #endregion

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) {
                return;
            }

            _cache.Remove(NativePtr);

            IsDisposed = true;
        }

        protected void ThrowExceptionIfDisposed()
        {
            if (IsDisposed) {
                throw new ObjectDisposedException(typeof(T).FullName);
            }
        }

        #endregion
    }
}
