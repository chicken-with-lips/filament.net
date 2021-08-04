using System;
using System.Numerics;

namespace Filament
{
    /// <summary>
    ///Factory and manager for renderables, which are entities that can be drawn.
    /// </summary>
    /// <remarks>
    /// <para>Renderables are bundles of primitives, each of which has its own geometry and material. All primitives in
    /// a particular renderable share a set of rendering attributes, such as whether they cast shadows or use vertex
    /// skinning.</para>
    /// <para>To modify the state of an existing renderable, clients should first use RenderableManager to get a
    /// temporary handle called an \em instance. The instance can then be used to get or set the renderable's state.
    /// Please note that instances are ephemeral; clients should store entities, not instances.
    /// </para>
    /// </remarks>
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

        /// <summary>
        /// Changes the visibility bits.
        /// </summary>
        /// <seealso cref="View.SetVisibleLayers"/>
        public void SetLayerMask(int instance, int select, int value)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetLayerMask(NativePtr, instance, select, value);
        }

        /// <summary>
        /// Changes the coarse-level draw ordering.
        /// </summary>
        public void SetPriority(int instance, int priority)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetPriority(NativePtr, instance, priority);
        }

        /// <summary>
        /// Changes whether or not frustum culling is on.
        /// </summary>
        public void SetCulling(int instance, bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetCulling(NativePtr, instance, enabled);
        }

        /// <summary>
        /// Changes whether or not the renderable casts shadows.
        /// </summary>
        public void SetCastShadows(int instance, bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetCastShadows(NativePtr, instance, enabled);
        }

        /// <summary>
        /// Changes whether or not the renderable can receive shadows.
        /// </summary>
        public void SetReceiveShadows(int instance, bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetReceiveShadows(NativePtr, instance, enabled);
        }

        /// <summary>
        /// Changes whether or not the renderable can use screen-space contact shadows.
        /// </summary>
        public void SetScreenSpaceContactShadows(int instance, bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetScreenSpaceContactShadows(NativePtr, instance, enabled);
        }

        /// <summary>
        /// Checks if the renderable can cast shadows.
        /// </summary>
        public bool IsShadowCaster(int instance)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableManager.IsShadowCaster(NativePtr, instance);
        }

        /// <summary>
        /// Checks if the renderable can receive shadows.
        /// </summary>
        public bool IsShadowReceiver(int instance)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableManager.IsShadowReceiver(NativePtr, instance);
        }

        /// <summary>
        /// Gets a temporary handle that can be used to access the renderable state.
        /// </summary>
        /// <param name="entity">An entity.</param>
        /// <returns>Non-zero handle if the entity has a renderable component, 0 otherwise.</returns>
        public int GetInstance(int entity)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableManager.GetInstance(NativePtr, entity);
        }

        /// <summary>
        /// Retrieves the material instance that is bound to the given primitive.
        /// </summary>
        public MaterialInstance GetMaterialInstanceAt(int instance, int primitiveIndex)
        {
            ThrowExceptionIfDisposed();

            return MaterialInstance.GetOrCreateCache(
                Native.RenderableManager.GetMaterialInstanceAt(NativePtr, instance, primitiveIndex)
            );
        }

        /// <summary>
        /// Changes the material instance binding for the given primitive.
        /// </summary>
        public void SetMaterialInstanceAt(int instance, int primitiveIndex, MaterialInstance materialInstance)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetMaterialInstanceAt(NativePtr, instance, primitiveIndex, materialInstance.NativePtr);
        }

        /// <summary>
        /// Checks if the given entity already has a renderable component.
        /// </summary>
        public bool HasComponent(int entity)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableManager.HasComponent(NativePtr, entity);
        }

        /// <summary>
        /// Gets the immutable number of primitives in the given renderable.
        /// </summary>
        public int GetPrimitiveCount(int instance)
        {
            ThrowExceptionIfDisposed();

            return Native.RenderableManager.GetPrimitiveCount(NativePtr, instance);
        }

        /// <summary>
        /// Changes the ordering index for blended primitives that all live at the same Z value.
        /// </summary>
        /// <param name="instance">The renderable of interest.</param>
        /// <param name="primitiveIndex">The primitive of interest.</param>
        /// <param name="blendOrder">Order draw order number (0 by default). Only the lowest 15 bits are used.</param>
        public void SetBlendOrderAt(int instance, int primitiveIndex, int blendOrder)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.SetBlenderOrderAt(NativePtr, instance, primitiveIndex, blendOrder);
        }

        public Box GetAxisAlignedBoundingBox(int instance)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.GetAxisAlignedBoundingBox(NativePtr, instance,
                out var centerX,
                out var centerY,
                out var centerZ,
                out var halfExtentX,
                out var halfExtentY,
                out var halfExtentZ
            );

            return new Box(
                new Vector3(centerX, centerY, centerZ),
                new Vector3(halfExtentX, halfExtentY, halfExtentZ)
            );
        }

        /// <summary>
        /// Destroys the renderable component in the given entity.
        /// </summary>
        /// <param name="entity">An entity.</param>
        public void Destroy(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableManager.Destroy(NativePtr, entity);
        }

        #endregion
    }

    /// <summary>
    /// Adds renderable components to entities using a builder pattern.
    /// </summary>
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

        /// <summary>
        /// <para>Creates a builder for renderable components.</para>
        /// <para>Note that builders typically do not have a long lifetime since clients should discard them after
        /// calling <see cref="Build"/>.</para>
        /// </summary>
        /// <param name="count">The number of primitives that will be supplied to the builder.</param>
        public static RenderableBuilder Create(int count = 1)
        {
            return GetOrCreateCache(
                Native.RenderableBuilder.CreateBuilder(count)
            );
        }

        /// <summary>
        /// <para>Specifies the geometry data for a primitive.</para>
        /// <para>Filament primitives must have an associated <see cref="VertexBuffer"/> and <see cref="IndexBuffer"/>.</para>
        /// </summary>
        /// <param name="index">zero-based index of the primitive, must be less than the count passed to Builder constructor.</param>
        /// <param name="primitiveType">Specifies the topology of the primitive.</param>
        /// <param name="vertexBuffer">Specifies the vertex buffer, which in turn specifies a set of attributes.</param>
        /// <param name="indexBuffer">Specifies the index buffer (either u16 or u32).</param>
        public RenderableBuilder WithGeometry(int index, PrimitiveType primitiveType, VertexBuffer vertexBuffer, IndexBuffer indexBuffer)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.GeometryJIIJJ(NativePtr, index, (uint) primitiveType, vertexBuffer.NativePtr, indexBuffer.NativePtr);

            return this;
        }

        /// <summary>
        /// <para>Specifies the geometry data for a primitive.</para>
        /// <para>Filament primitives must have an associated <see cref="VertexBuffer"/> and <see cref="IndexBuffer"/>.</para>
        /// </summary>
        /// <param name="index">zero-based index of the primitive, must be less than the count passed to Builder constructor.</param>
        /// <param name="primitiveType">Specifies the topology of the primitive.</param>
        /// <param name="vertexBuffer">Specifies the vertex buffer, which in turn specifies a set of attributes.</param>
        /// <param name="indexBuffer">Specifies the index buffer (either u16 or u32).</param>
        /// <param name="offset">Specifies where in the index buffer to start reading (expressed as a number of indices).</param>
        /// <param name="count">Number of indices to read (for triangles, this should be a multiple of 3).</param>
        public RenderableBuilder WithGeometry(int index, PrimitiveType primitiveType, VertexBuffer vertexBuffer, IndexBuffer indexBuffer, int offset, int count)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.GeometryJIIJJII(NativePtr, index, (uint) primitiveType, vertexBuffer.NativePtr, indexBuffer.NativePtr, offset, count);

            return this;
        }

        /// <summary>
        /// <para>Specifies the geometry data for a primitive.</para>
        /// <para>Filament primitives must have an associated <see cref="VertexBuffer"/> and <see cref="IndexBuffer"/>.</para>
        /// </summary>
        /// <param name="index">zero-based index of the primitive, must be less than the count passed to Builder constructor.</param>
        /// <param name="primitiveType">Specifies the topology of the primitive.</param>
        /// <param name="vertexBuffer">Specifies the vertex buffer, which in turn specifies a set of attributes.</param>
        /// <param name="indexBuffer">Specifies the index buffer (either u16 or u32).</param>
        /// <param name="offset">Specifies where in the index buffer to start reading (expressed as a number of indices).</param>
        /// <param name="minIndex">Specifies the minimum index contained in the index buffer.</param>
        /// <param name="maxIndex">Specifies the maximum index contained in the index buffer.</param>
        /// <param name="count">Number of indices to read (for triangles, this should be a multiple of 3).</param>
        public RenderableBuilder WithGeometry(int index, PrimitiveType primitiveType, VertexBuffer vertexBuffer, IndexBuffer indexBuffer, int offset, int minIndex, int maxIndex, int count)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.GeometryJIIJJIIII(NativePtr, index, (uint) primitiveType, vertexBuffer.NativePtr, indexBuffer.NativePtr, offset, minIndex, maxIndex, count);

            return this;
        }

        /// <summary>
        /// <para>Binds a material instance to the specified primitive.</para>
        /// <para>If no material is specified for a given primitive, Filament will fall back to a basic default material.</para>
        /// </summary>
        /// <param name="index">Zero-based index of the primitive, must be less than the count passed to Builder constructor.</param>
        /// <param name="materialInstance">The material to bind.</param>
        public RenderableBuilder WithMaterial(int index, MaterialInstance materialInstance)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.Material(NativePtr, index, materialInstance.NativePtr);

            return this;
        }

        /// <summary>
        /// Sets an ordering index for blended primitives that all live at the same Z value.
        /// </summary>
        /// <param name="index">The primitive of interest.</param>
        /// <param name="blendOrder">Draw order number (0 by default). Only the lowest 15 bits are used.</param>
        public RenderableBuilder WithBlendOrder(int index, int blendOrder)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.BlendOrder(NativePtr, index, blendOrder);

            return this;
        }

        /// <summary>
        /// <para>The axis-aligned bounding box of the renderable.</para>
        /// <para>This is an object-space AABB used for frustum culling. For skinning and morphing, this should
        /// encompass all possible vertex positions. It is mandatory unless culling is disabled for the renderable.</para>
        /// </summary>
        public RenderableBuilder WithBoundingBox(Box axisAlignedBoundingBox)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.BoundingBox(NativePtr,
                axisAlignedBoundingBox.Center.X, axisAlignedBoundingBox.Center.Y, axisAlignedBoundingBox.Center.Z,
                axisAlignedBoundingBox.HalfExtent.X, axisAlignedBoundingBox.HalfExtent.Y, axisAlignedBoundingBox.HalfExtent.Z
            );

            return this;
        }

        /// <summary>
        /// <para>Sets bits in a visibility mask. By default, this is 0x1.</para>
        /// <para>This feature provides a simple mechanism for hiding and showing groups of renderables in a Scene.
        /// See <see cref="View.SetVisibleLayers"/></para>
        /// <para>For example, to set bit 1 and reset bits 0 and 2 while leaving all other bits unaffected, use:
        /// WithLayerMask(7, 2)`.</para>
        /// <para>To change this at run time, see <see cref="RenderableManager.SetLayerMask"/>.</para>
        /// </summary>
        /// <param name="select">The set of bits to affect.</param>
        /// <param name="value">The replacement values for the affected bits.</param>
        public RenderableBuilder WithLayerMask(int select, int value)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.LayerMask(NativePtr, select, value);

            return this;
        }

        /// <summary>
        /// <para>Provides coarse-grained control over draw order.</para>
        /// <para>In general Filament reserves the right to re-order renderables to allow for efficient rendering.
        /// However clients can control ordering at a coarse level using priority.</para>
        /// <para>For example, this could be used to draw a semitransparent HUD, if a client wishes to avoid using a
        /// separate View for the HUD. Note that priority is completely orthogonal to <see cref="WithLayerMask"/>,
        /// which merely controls visibility.</para>
        /// </summary>
        /// <param name="priority">The priority is clamped to the range [0..7], defaults to 4; 7 is lowest priority
        /// (rendered last).</param>
        public RenderableBuilder WithPriority(int priority)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.Priority(NativePtr, priority);

            return this;
        }

        /// <summary>
        /// <para>Controls frustum culling, true by default.</para>
        /// <para>Note: Do not confuse frustum culling with backface culling. The latter is controlled via the material.</para>
        /// </summary>
        public RenderableBuilder WithCulling(bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.Culling(NativePtr, enabled);

            return this;
        }

        /// <summary>
        /// <para>Controls if this renderable casts shadows, false by default.</para>
        /// <para>If the View's shadow type is set to <see cref="ViewShadowType.Vsm"/>, castShadows should only be
        /// disabled if either is true:</para>
        /// <list>
        /// <item>receiveShadows is also disabled.</item>
        /// <item>The object is guaranteed to not cast shadows on itself or other objects (for example, a ground plane)</item>
        /// </list>
        /// </summary>
        public RenderableBuilder WithCastShadows(bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.CastShadows(NativePtr, enabled);

            return this;
        }

        /// <summary>
        /// Controls if this renderable receives shadows, true by default.
        /// </summary>
        public RenderableBuilder WithReceiveShadows(bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.ReceiveShadows(NativePtr, enabled);

            return this;
        }

        /// <summary>
        /// Controls if this renderable uses screen-space contact shadows. This is more expensive but can improve the
        /// quality of shadows, especially in large scenes. (off by default).
        /// </summary>
        public RenderableBuilder WithScreenSpaceContactShadows(bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.ScreenSpaceContactShadows(NativePtr, enabled);

            return this;
        }

        /// <summary>
        /// <para>nables GPU vertex skinning for up to 255 bones, 0 by default.</para>
        /// <para>Each vertex can be affected by up to 4 bones simultaneously. The attached VertexBuffer must provide
        /// data in the BONE_INDICES slot (uvec4) and the BONE_WEIGHTS slot (float4).</para>
        /// <para>See also <see cref="RenderableManager.SetBones"/>, which can be called on a per-frame basis to advance
        /// the animation.</para>
        /// </summary>
        /// <param name="boneCount">0 to disable, otherwise the number of bone transforms (up to 255)</param>
        public RenderableBuilder WithSkinning(int boneCount)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.Skinning(NativePtr, boneCount);

            return this;
        }

        /// <summary>
        /// <para>Controls if the renderable has vertex morphing targets, false by default.</para>
        /// <para>This is required to enable GPU morphing for up to 4 attributes. The attached VertexBuffer must
        /// provide data in the appropriate VertexAttribute slots (MORPH_POSITION_0 etc).</para>
        /// <para>See also <see cref="RenderableManager.SetMorphWeights"/>, which can be called on a per-frame basis to
        /// advance the animation.</para>
        /// </summary>
        public RenderableBuilder WithMorphing(bool enabled)
        {
            ThrowExceptionIfDisposed();

            Native.RenderableBuilder.Morphing(NativePtr, enabled);

            return this;
        }

        /// <summary>
        /// <para>Adds the Renderable component to an entity.</para>
        /// <para>If this component already exists on the given entity and the construction is successful, it is first
        /// destroyed as if destroy(utils::Entity e) was called. In case of error, the existing component is unmodified.
        /// </para>
        /// </summary>
        /// <param name="engine">Reference to the <see cref="Engine"/> to associate this Renderable with.</param>
        /// <param name="entity">Entity to add the Renderable component to.</param>
        /// <returns>Success if the component was created successfully, Error otherwise.</returns>
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
