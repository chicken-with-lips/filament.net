using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class Renderer
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_Renderer_nBeginFrame")]
        public static extern bool BeginFrame(IntPtr nativeRenderer, IntPtr nativeSwapChain, uint frameTimeNanos);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Renderer_nEndFrame")]
        public static extern void EndFrame(IntPtr nativeRenderer);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Renderer_nRender")]
        public static extern void Render(IntPtr nativeRenderer, IntPtr nativeView);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Renderer_nSetClearOptions")]
        public static extern void SetClearOptions(IntPtr nativeRenderer, float r, float g, float b, float a, bool clear, bool discard);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Renderer_nGetEngine")]
        public static extern IntPtr GetEngine(IntPtr nativeRenderer);
    }
}
