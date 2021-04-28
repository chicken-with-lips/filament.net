using System;

namespace Filament
{
    public class RenderableManager : FilamentBase<RenderableManager>
    {
        #region Methods

        private RenderableManager(IntPtr ptr) : base(ptr)
        {
        }

        internal static RenderableManager GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new RenderableManager(newPtr));
        }

        public void SetLayerMask(int instance, int select, int value)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetLayerMask(NativePtr, instance, select, value);
        }

        public void SetPriority(int instance, int priority)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetPriority(NativePtr, instance, priority);
        }

        public void SetCulling(int instance, bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetCulling(NativePtr, instance, enabled);
        }

        public void SetCastShadows(int instance, bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetCastShadows(NativePtr, instance, enabled);
        }

        public void SetReceiveShadows(int instance, bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetReceiveShadows(NativePtr, instance, enabled);
        }

        public void SetScreenSpaceContactShadows(int instance, bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetScreenSpaceContactShadows(NativePtr, instance, enabled);
        }

        public bool IsShadowCaster(int instance)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableManager.IsShadowCaster(NativePtr, instance);
        }

        public bool IsShadowReceiver(int instance)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableManager.IsShadowReceiver(NativePtr, instance);
        }

        public int GetInstance(int entity)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableManager.GetInstance(NativePtr, entity);
        }

        public MaterialInstance GetMaterialInstanceAt(int instance, int primitiveIndex)
        {
            ThrowExceptionIfDisposed();

            return MaterialInstance.GetOrCreateCache(
                Native.RenderableManager.GetMaterialInstanceAt(NativePtr, instance, primitiveIndex)
            );
        }

        public void SetMaterialInstanceAt(int instance, int primitiveIndex, MaterialInstance materialInstance)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetMaterialInstanceAt(NativePtr, instance, primitiveIndex, materialInstance.NativePtr);
        }

        public bool HasComponent(int entity)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableManager.HasComponent(NativePtr, entity);
        }

        public int GetPrimitiveCount(int instance)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableManager.GetPrimitiveCount(NativePtr, instance);
        }

        public void SetBlendOrderAt(int instance, int primitiveIndex, int blendOrder)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetBlenderOrderAt(NativePtr, instance, primitiveIndex, blendOrder);
        }

        public void Destroy(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.Destroy(NativePtr, entity);
        }

        #endregion
    }

    public class RenderableBuilder : FilamentBase<RenderableBuilder>
    {
        #region Methods

        private RenderableBuilder(IntPtr ptr) : base(ptr)
        {
        }

        internal static RenderableBuilder GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new RenderableBuilder(newPtr));
        }

        public static RenderableBuilder Create(int count = 1)
        {
            return GetOrCreateCache(
                Native.RenderableBuilder.CreateBuilder(count)
            );
        }

        public RenderableBuilder WithGeometry(int index, PrimitiveType primitiveType, VertexBuffer vertexBuffer, IndexBuffer indexBuffer)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.GeometryJIIJJ(NativePtr, index, (uint) primitiveType, vertexBuffer.NativePtr, indexBuffer.NativePtr);

            return this;
        }

        public RenderableBuilder WithGeometry(int index, PrimitiveType primitiveType, VertexBuffer vertexBuffer, IndexBuffer indexBuffer, int offset, int count)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.GeometryJIIJJII(NativePtr, index, (uint) primitiveType, vertexBuffer.NativePtr, indexBuffer.NativePtr, offset, count);

            return this;
        }

        public RenderableBuilder WithGeometry(int index, PrimitiveType primitiveType, VertexBuffer vertexBuffer, IndexBuffer indexBuffer, int offset, int minIndex, int maxIndex, int count)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.GeometryJIIJJIIII(NativePtr, index, (uint) primitiveType, vertexBuffer.NativePtr, indexBuffer.NativePtr, offset, minIndex, maxIndex, count);

            return this;
        }

        public RenderableBuilder WithMaterial(int index, MaterialInstance materialInstance)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.Material(NativePtr, index, materialInstance.NativePtr);

            return this;
        }

        public RenderableBuilder WithBlendOrder(int index, int blendOrder)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.BlendOrder(NativePtr, index, blendOrder);

            return this;
        }

        public RenderableBuilder WithBoundingBox(Box axisAlignedBoundingBox)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.BoundingBox(NativePtr,
                axisAlignedBoundingBox.Center.X, axisAlignedBoundingBox.Center.Y, axisAlignedBoundingBox.Center.Z,
                axisAlignedBoundingBox.HalfExtent.X, axisAlignedBoundingBox.HalfExtent.Y, axisAlignedBoundingBox.HalfExtent.Z
            );

            return this;
        }

        public RenderableBuilder WithLayerMask(int select, int value)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.LayerMask(NativePtr, select, value);

            return this;
        }

        public RenderableBuilder WithPriority(int priority)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.Priority(NativePtr, priority);

            return this;
        }

        public RenderableBuilder WithCulling(bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.Culling(NativePtr, enabled);

            return this;
        }

        public RenderableBuilder WithCastShadows(bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.CastShadows(NativePtr, enabled);

            return this;
        }

        public RenderableBuilder WithReceiveShadows(bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.ReceiveShadows(NativePtr, enabled);

            return this;
        }

        public RenderableBuilder WithScreenSpaceContactShadows(bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.ScreenSpaceContactShadows(NativePtr, enabled);

            return this;
        }

        public RenderableBuilder WithNativeSkinning(int boneCount)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.Skinning(NativePtr, boneCount);

            return this;
        }

        public RenderableBuilder WithMorphing(bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.Morphing(NativePtr, enabled);

            return this;
        }

        public bool Build(Engine engine, int entity)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableBuilder.Build(NativePtr, engine.NativePtr, entity);
        }

        #endregion

        #region FilamentBase

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.RenderableBuilder.DestroyBuilder(NativePtr);
        }

        #endregion
    }
}
