using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class Engine
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nCreateEngine", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr CreateEngine(int backend, IntPtr sharedContext);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyEngine")]
        public static extern IntPtr DestroyEngine(IntPtr nativeEngine);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nCreateSwapChain")]
        public static extern IntPtr CreateSwapChain(IntPtr nativeEngine, IntPtr nativeWindow, uint flags);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nCreateSwapChainHeadless")]
        public static extern IntPtr CreateSwapChainHeadless(IntPtr nativeEngine, int width, int height, uint flags);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nCreateView")]
        public static extern IntPtr CreateView(IntPtr nativeEngine);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nCreateRenderer")]
        public static extern IntPtr CreateRenderer(IntPtr nativeEngine);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nCreateCamera")]
        public static extern IntPtr CreateCamera(IntPtr nativeEngine, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyCameraComponent")]
        public static extern void DestroyCameraComponent(IntPtr nativeEngine, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nGetCameraComponent")]
        public static extern IntPtr GetCameraComponent(IntPtr nativeEngine, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nCreateScene")]
        public static extern IntPtr CreateScene(IntPtr nativeEngine);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyIndexBuffer")]
        public static extern bool DestroyIndexBuffer(IntPtr nativeEngine, IntPtr nativeIndexBuffer);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyVertexBuffer")]
        public static extern bool DestroyVertexBuffer(IntPtr nativeEngine, IntPtr nativeVertexBuffer);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyIndirectLight")]
        public static extern bool DestroyIndirectLight(IntPtr nativeEngine, IntPtr nativeIndirectLight);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyMaterial")]
        public static extern bool DestroyMaterial(IntPtr nativeEngine, IntPtr nativeMaterial);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyMaterialInstance")]
        public static extern bool DestroyMaterialInstance(IntPtr nativeEngine, IntPtr nativeMaterialInstance);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyTexture")]
        public static extern bool DestroyTexture(IntPtr nativeEngine, IntPtr nativeTexture);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyEntity")]
        public static extern void DestroyEntity(IntPtr nativeEngine, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroySwapChain")]
        public static extern bool DestroySwapChain(IntPtr nativeEngine, IntPtr nativeSwapChain);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyScene")]
        public static extern bool DestroyScene(IntPtr nativeEngine, IntPtr nativeScene);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyRenderer")]
        public static extern bool DestroyRenderer(IntPtr nativeEngine, IntPtr nativeRenderer);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyView")]
        public static extern bool DestroyView(IntPtr nativeEngine, IntPtr nativeView);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroySkybox")]
        public static extern bool DestroySkybox(IntPtr nativeEngine, IntPtr nativeSkybox);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nDestroyRenderTarget")]
        public static extern bool DestroyRenderTarget(IntPtr nativeEngine, IntPtr nativeRenderTarget);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nFlushAndWait")]
        public static extern bool FlushAndWait(IntPtr nativeEngine);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nGetTransformManager")]
        public static extern IntPtr GetTransformManager(IntPtr nativeEngine);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nGetRenderableManager")]
        public static extern IntPtr GetRenderableManager(IntPtr nativeEngine);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Engine_nGetDefaultMaterial")]
        public static extern IntPtr GetDefaultMaterial(IntPtr nativeEngine);
    }
}
