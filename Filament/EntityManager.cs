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

        /// <summary>
        /// Create an entity. Thread safe.
        /// </summary>
        /// <returns>An entity.</returns>
        public static int Create()
        {
            return Native.EntityManager.Create(_ptr);
        }

        /// <summary>
        /// Destroys an entity. Thread safe.
        /// </summary>
        /// <returns>An entity.</returns>
        public static int Destroy(int entity)
        {
            return Native.EntityManager.Destroy(_ptr, entity);
        }

        /// <summary>
        /// Return whether the given Entity has been destroyed or not. Thread safe.
        /// </summary>
        /// <param name="entity">An entity</param>
        /// <returns>False if destroyed, true otherwise.</returns>
        public static bool IsAlive(int entity)
        {
            return Native.EntityManager.IsAlive(_ptr, entity);
        }
    }
}
