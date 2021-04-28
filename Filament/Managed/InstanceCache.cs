using System;
using System.Collections.Generic;

namespace Filament.Managed
{
    public class InstanceCache<T>
    {
        #region Members

        private readonly Dictionary<IntPtr, T> _cache = new();

        #endregion

        #region Methods

        public T GetOrCreate(IntPtr ptr, CreateInstance createInstance)
        {
            if (ptr == IntPtr.Zero) {
                return default;
            }

            if (!_cache.ContainsKey(ptr)) {
                _cache.Add(ptr, createInstance(ptr));
            }

            return _cache[ptr];
        }

        public void ManuallyRegisterCache(IntPtr ptr, T instance)
        {
            if (_cache.ContainsKey(ptr)) {
                throw new InvalidOperationException("Instance already registered");
            }

            _cache.Add(ptr, instance);
        }

        public void Remove(IntPtr cameraPtr)
        {
            if (_cache.ContainsKey(cameraPtr)) {
                _cache.Remove(cameraPtr);
            }
        }

        #endregion

        public delegate T CreateInstance(IntPtr ptr);
    }
}
