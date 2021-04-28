using System;
using System.Runtime.InteropServices;

namespace Filament.Native
{
    public static class RenderableManager
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nSetLayerMask")]
        public static extern void SetLayerMask(IntPtr nativeRenderableManager, int instance, int select, int value);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nSetPriority")]
        public static extern void SetPriority(IntPtr nativeRenderableManager, int instance, int priority);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nSetCulling")]
        public static extern void SetCulling(IntPtr nativeRenderableManager, int instance, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nSetCastShadows")]
        public static extern void SetCastShadows(IntPtr nativeRenderableManager, int instance, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nSetReceiveShadows")]
        public static extern void SetReceiveShadows(IntPtr nativeRenderableManager, int instance, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nSetScreenSpaceContactShadows")]
        public static extern void SetScreenSpaceContactShadows(IntPtr nativeRenderableManager, int instance, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nIsShadowCaster")]
        public static extern bool IsShadowCaster(IntPtr nativeRenderableManager, int instance);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nIsShadowReceiver")]
        public static extern bool IsShadowReceiver(IntPtr nativeRenderableManager, int instance);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nGetInstance")]
        public static extern int GetInstance(IntPtr nativeRenderableManager, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nSetBlendOrderAt")]
        public static extern void SetBlenderOrderAt(IntPtr nativeRenderableManager, int instance, int primitiveIndex, int blendOrder);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nGetMaterialInstanceAt")]
        public static extern IntPtr GetMaterialInstanceAt(IntPtr nativeRenderableManager, int instance, int primitiveIndex);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nGetMaterialAt")]
        public static extern IntPtr GetMaterialAt(IntPtr nativeRenderableManager, int instance, int primitiveIndex);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nGetPrimitiveCount")]
        public static extern int GetPrimitiveCount(IntPtr nativeRenderableManager, int instance);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nSetMaterialInstanceAt")]
        public static extern void SetMaterialInstanceAt(IntPtr nativeRenderableManager, int instance, int primitiveIndex, IntPtr nativeMaterialInstance);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nHasComponent")]
        public static extern bool HasComponent(IntPtr nativeRenderableManager, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nDestroy")]
        public static extern void Destroy(IntPtr nativeRenderableManager, int entity);
    }

    public static class RenderableBuilder
    {
        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nCreateBuilder")]
        public static extern IntPtr CreateBuilder(int count);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nDestroyBuilder")]
        public static extern void DestroyBuilder(IntPtr nativeBuilder);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderBuild")]
        public static extern bool Build(IntPtr nativeBuilder, IntPtr nativeEngine, int entity);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderGeometry__JIIJJ")]
        public static extern void GeometryJIIJJ(IntPtr nativeBuilder, int index, uint primitiveType, IntPtr nativeVertexBuffer, IntPtr nativeIndexBuffer);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderGeometry__JIIJJII")]
        public static extern void GeometryJIIJJII(IntPtr nativeBuilder, int index, uint primitiveType, IntPtr nativeVertexBuffer, IntPtr nativeIndexBuffer, int offset, int count);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderGeometry__JIIJJIIII")]
        public static extern void GeometryJIIJJIIII(IntPtr nativeBuilder, int index, uint primitiveType, IntPtr nativeVertexBuffer, IntPtr nativeIndexBuffer, int offset, int minIndex, int maxIndex, int count);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderMaterial")]
        public static extern void Material(IntPtr nativeBuilder, int index, IntPtr nativeMaterialInstance);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderBlendOrder")]
        public static extern void BlendOrder(IntPtr nativeBuilder, int index, int blendOrder);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderBoundingBox")]
        public static extern void BoundingBox(IntPtr nativeBuilder, float cX, float cY, float cZ, float eX, float eY, float eZ);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderLayerMask")]
        public static extern void LayerMask(IntPtr nativeBuilder, int select, int value);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderPriority")]
        public static extern void Priority(IntPtr nativeBuilder, int priority);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderCulling")]
        public static extern void Culling(IntPtr nativeBuilder, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderCastShadows")]
        public static extern void CastShadows(IntPtr nativeBuilder, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderReceiveShadows")]
        public static extern void ReceiveShadows(IntPtr nativeBuilder, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderScreenSpaceContactShadows")]
        public static extern void ScreenSpaceContactShadows(IntPtr nativeBuilder, bool enabled);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderSkinning")]
        public static extern void Skinning(IntPtr nativeBuilder, int boneCount);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_RenderableManager_nBuilderMorphing")]
        public static extern void Morphing(IntPtr nativeBuilder, bool enabled);
    }
}
