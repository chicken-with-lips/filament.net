using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class Scene
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Scene_nSetSkybox")]
        public static extern void SetSkybox(IntPtr nativeBuilder, IntPtr nativeSkybox);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Scene_nSetIndirectLight")]
        public static extern void SetIndirectLight(IntPtr nativeBuilder, IntPtr nativeIndirectLight);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Scene_nAddEntity")]
        public static extern void AddEntity(IntPtr nativeBuilder, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Scene_nRemove")]
        public static extern void RemoveEntity(IntPtr nativeBuilder, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Scene_nGetRenderableCount")]
        public static extern int GetRenderableCount(IntPtr nativeBuilder);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Scene_nGetLightCount")]
        public static extern int GetLightCount(IntPtr nativeBuilder);
    }
}
