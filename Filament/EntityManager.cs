using System;

namespace Filament
{
    public static class EntityManager
    {
        private static readonly IntPtr _ptr;

        static EntityManager()
        {
            _ptr = Native.EntityManager.GetEntityManager();
        }

        public static int Create()
        {
            return Native.EntityManager.Create(_ptr);
        }

        public static int Destroy(int entity)
        {
            return Native.EntityManager.Destroy(_ptr, entity);
        }

        public static bool IsAlive(int entity)
        {
            return Native.EntityManager.IsAlive(_ptr, entity);
        }
    }
}
