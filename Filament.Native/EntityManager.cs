using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class EntityManager
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_EntityManager_nGetEntityManager")]
        public static extern IntPtr GetEntityManager();

        [DllImport("libfilament-dotnet", EntryPoint = "filament_EntityManager_nCreate")]
        public static extern int Create(IntPtr nativeEntityManager);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_EntityManager_nDestroy")]
        public static extern int Destroy(IntPtr nativeEntityManager, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_EntityManager_nIsAlive")]
        public static extern bool IsAlive(IntPtr nativeEntityManager, int entity);
    }
}
