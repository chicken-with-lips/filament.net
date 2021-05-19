using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class TransformManager
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_TransformManager_nGetInstance")]
        public static extern int GetInstance(IntPtr nativeTransformManager, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_TransformManager_nGetWorldTransform")]
        public static extern void GetWorldTransform(IntPtr nativeTransformManager, int instance, float[] result);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_TransformManager_nSetTransform")]
        public static extern void SetTransform(IntPtr nativeTransformManager, int instance, float[] result);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_TransformManager_nOpenLocalTransformTransaction")]
        public static extern void OpenLocalTransformTransaction(IntPtr nativeTransformManager);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_TransformManager_nCommitLocalTransformTransaction")]
        public static extern void CommitLocalTransformTransaction(IntPtr nativeTransformManager);
    }
}
