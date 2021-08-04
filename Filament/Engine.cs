using System;

namespace Filament
{
    public enum Backend
    {
        /// <summary>Automatically selects an appropriate driver for the platform.</summary>
        Default,

        /// <summary>Selects the OpenGL driver (which supports OpenGL ES as well).</summary>
        OpenGL,

        /// <summary>Selects the Vulkan driver if the platform supports it.</summary>
        Vulkan,

        /// <summary>Selects the Metal driver if the platform supports it.</summary>
        Metal,

        /// <summary>Selects the no-op driver for testing purposes.</summary>
        Noop
    }

    /// <summary>
    /// Engine is filament's main entry-point.
    /// </summary>
    /// <remarks>
    /// <para>An Engine instance main function is to keep track of all resources created by the user and manage the rendering
    /// thread as well as the hardware renderer.</para>
    /// <para>Resource Tracking</para>
    /// =================
    /// <para>Each Engine instance keeps track of all objects created by the user, such as vertex and index buffers, lights,
    /// cameras, etc.</para>
    /// <para>The user is expected to free those resources, however, leaked resources are freed when the engine instance is
    /// destroyed and a warning is emitted in the console.</para>
    /// <para>Thread safety</para>
    /// =============
    /// <para>An Engine instance is not thread-safe. The implementation makes no attempt to synchronize calls to an Engine
    /// instance methods. If multi-threading is needed, synchronization must be external.</para>
    /// <para>Multi-threading</para>
    /// ===============
    /// <para>When created, the Engine instance starts a render thread as well as multiple worker threads, these threads have
    /// an elevated priority appropriate for rendering, based on the platform's best practices. The number of worker
    /// threads depends on the platform and is automatically chosen for best performance.</para>
    /// <para>On platforms with asymmetric cores (e.g. ARM's Big.Little), Engine makes some educated guesses as to which cores
    /// to use for the render thread and worker threads. For example, it'll try to keep an OpenGL ES thread on a Big
    /// core.</para>
    /// <para>Swap Chains</para>
    /// ===========
    /// <para>A swap chain represents an Operating System's *native* renderable surface. Typically it's a window or a view.
    /// Because a SwapChain is initialized from a native object, it is given to filament as an <see cref="IntPtr"/>,
    /// which must be of the proper type for each platform filament is running on.</para>
    /// </remarks>
    public sealed class Engine : FilamentBase<Engine>
    {
        #region Properties

        public RenderableManager RenderableManager {
            get {
                ThrowExceptionIfDisposed();

                return RenderableManager.GetOrCreateCache(
                    Native.Engine.GetRenderableManager(NativePtr)
                );
            }
        }

        public TransformManager TransformManager {
            get {
                ThrowExceptionIfDisposed();

                return TransformManager.GetOrCreateCache(
                    Native.Engine.GetTransformManager(NativePtr)
                );
            }
        }

        public Material DefaultMaterial {
            get {
                ThrowExceptionIfDisposed();

                return Material.GetOrCreateCache(
                    Native.Engine.GetDefaultMaterial(NativePtr)
                );
            }
        }

        #endregion

        #region Methods

        private Engine(IntPtr ptr) : base(ptr)
        {
        }

        internal static Engine GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new Engine(newPtr));
        }

        /// <summary>
        /// Creates a SwapChain from the given Operating System's native window handle.
        /// </summary>
        /// <param name="nativeWindow">An opaque native window handle. e.g.: on Android this is an `ANativeWindow*`.</param>
        /// <param name="flags">One or more configuration flags.</param>
        /// <returns>The newly created SwapChain or null if it couldn't be created.</returns>
        public SwapChain CreateSwapChain(IntPtr nativeWindow, SwapChainConfig flags = SwapChainConfig.None)
        {
            ThrowExceptionIfDisposed();

            return SwapChain.GetOrCreateCache(
                Native.Engine.CreateSwapChain(NativePtr, nativeWindow, (uint) flags)
            );
        }

        /// <summary>
        /// Creates a headless SwapChain.
        /// </summary>
        /// <param name="width">Width of the drawing buffer in pixels.</param>
        /// <param name="height">Height of the drawing buffer in pixels.</param>
        /// <param name="flags">One or more configuration flags.</param>
        /// <returns>The newly created SwapChain or null if it couldn't be created.</returns>
        public SwapChain CreateSwapChain(int width, int height, SwapChainConfig flags = SwapChainConfig.None)
        {
            ThrowExceptionIfDisposed();

            return SwapChain.GetOrCreateCache(
                Native.Engine.CreateSwapChainHeadless(NativePtr, width, height, (uint) flags)
            );
        }

        /// <summary>
        /// Creates a renderer associated to this engine.
        /// </summary>
        /// <remarks>A Renderer is intended to map to a *window* on screen.</remarks>
        /// <returns>The newly created Renderer or null if it couldn't be created.</returns>
        public Renderer CreateRenderer()
        {
            ThrowExceptionIfDisposed();

            return Renderer.GetOrCreateCache(
                Native.Engine.CreateRenderer(NativePtr)
            );
        }

        /// <summary>
        /// Creates a View.
        /// </summary>
        /// <returns>The newly created View or null if it couldn't be created.</returns>
        public View CreateView()
        {
            ThrowExceptionIfDisposed();

            return View.GetOrCreateCache(
                Native.Engine.CreateView(NativePtr)
            );
        }


        /// <summary>
        /// Creates a Scene.
        /// </summary>
        /// <returns>The newly created Scene or null if it couldn't be created.</returns>
        public Scene CreateScene()
        {
            ThrowExceptionIfDisposed();

            return Scene.GetOrCreateCache(
                Native.Engine.CreateScene(NativePtr)
            );
        }

        /// <summary>
        /// Creates a Camera component.
        /// </summary>
        /// <param name="entity">Entity to add the camera component to.</param>
        /// <returns>The newly created Camera or null if it couldn't be created.</returns>
        public Camera CreateCamera(int entity)
        {
            ThrowExceptionIfDisposed();

            return Camera.GetOrCreateCache(
                Native.Engine.CreateCamera(NativePtr, entity)
            );
        }

        /// <summary>
        /// Destroys the Camera component associated with the given entity.
        /// </summary>
        /// <param name="entity">An entity.</param>
        public void DestroyCameraComponent(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.Engine.DestroyCameraComponent(NativePtr, entity);
        }

        /// <summary>
        /// Returns the Camera component of the given entity.
        /// </summary>
        /// <param name="entity">An entity.</param>
        /// <returns>The Camera component for this entity or null if the entity didn't have a Camera component.</returns>
        public Camera GetCameraComponent(int entity)
        {
            ThrowExceptionIfDisposed();

            return Camera.GetOrCreateCache(
                Native.Engine.GetCameraComponent(NativePtr, entity)
            );
        }

        /// <summary>
        /// Destroys all filament-known components from this entity.
        /// </summary>
        /// <param name="entity">An entity.</param>
        public void Destroy(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.Engine.DestroyEntity(NativePtr, entity);
        }

        /// <summary>
        /// Destroys a SwapChain.
        /// </summary>
        /// <param name="swapChain">A SwapChain.</param>
        public bool Destroy(SwapChain swapChain)
        {
            ThrowExceptionIfDisposed();

            swapChain.Dispose();

            return Native.Engine.DestroySwapChain(NativePtr, swapChain.NativePtr);
        }

        /// <summary>
        /// Destroys a Scene.
        /// </summary>
        /// <param name="scene">A Scene.</param>
        public bool Destroy(Scene scene)
        {
            ThrowExceptionIfDisposed();

            scene.Dispose();

            return Native.Engine.DestroyScene(NativePtr, scene.NativePtr);
        }

        /// <summary>
        /// Destroys a Renderer.
        /// </summary>
        /// <param name="renderer">A Renderer.</param>
        public bool Destroy(Renderer renderer)
        {
            ThrowExceptionIfDisposed();

            renderer.Dispose();

            return Native.Engine.DestroyRenderer(NativePtr, renderer.NativePtr);
        }

        /// <summary>
        /// Destroys a View.
        /// </summary>
        /// <param name="view">A View.</param>
        public bool Destroy(View view)
        {
            ThrowExceptionIfDisposed();

            view.Dispose();

            return Native.Engine.DestroyView(NativePtr, view.NativePtr);
        }

        /// <summary>
        /// Destroys a Skybox.
        /// </summary>
        /// <param name="skybox">A Skybox.</param>
        public bool Destroy(Skybox skybox)
        {
            ThrowExceptionIfDisposed();

            skybox.Dispose();

            return Native.Engine.DestroySkybox(NativePtr, skybox.NativePtr);
        }

        /// <summary>
        /// Destroys a Skybox.
        /// </summary>
        /// <param name="skybox">A Skybox.</param>
        public bool Destroy(Texture texture)
        {
            ThrowExceptionIfDisposed();

            texture.Dispose();

            return Native.Engine.DestroyTexture(NativePtr, texture.NativePtr);
        }

        /// <summary>
        /// Destroys a VertexBuffer.
        /// </summary>
        /// <param name="vertexBuffer">A VertexBuffer.</param>
        public bool Destroy(VertexBuffer vertexBuffer)
        {
            ThrowExceptionIfDisposed();

            vertexBuffer.Dispose();

            return Native.Engine.DestroyVertexBuffer(NativePtr, vertexBuffer.NativePtr);
        }

        /// <summary>
        /// Destroys a IndexBuffer.
        /// </summary>
        /// <param name="indexBuffer">A IndexBuffer.</param>
        public bool Destroy(IndexBuffer indexBuffer)
        {
            ThrowExceptionIfDisposed();

            indexBuffer.Dispose();

            return Native.Engine.DestroyIndexBuffer(NativePtr, indexBuffer.NativePtr);
        }

        /// <summary>
        /// Destroys a Material.
        /// </summary>
        /// <param name="material">A Material.</param>
        /// <remarks>All MaterialInstance of the specified material must be destroyed before destroying it.</remarks>
        public bool Destroy(Material material)
        {
            ThrowExceptionIfDisposed();

            material.Dispose();

            return Native.Engine.DestroyMaterial(NativePtr, material.NativePtr);
        }

        /// <summary>
        /// Destroys a IndirectLight.
        /// </summary>
        /// <param name="light">A IndirectLight.</param>
        public bool Destroy(IndirectLight light)
        {
            ThrowExceptionIfDisposed();

            light.Dispose();

            return Native.Engine.DestroyIndirectLight(NativePtr, light.NativePtr);
        }

        /// <summary>
        /// Destroys a MaterialInstance.
        /// </summary>
        /// <param name="materialInstance">A MaterialInstance.</param>
        public bool Destroy(MaterialInstance materialInstance)
        {
            ThrowExceptionIfDisposed();

            materialInstance.Dispose();

            return Native.Engine.DestroyMaterialInstance(NativePtr, materialInstance.NativePtr);
        }

        /// <summary>
        /// Destroys a RenderTarget.
        /// </summary>
        /// <param name="renderTarget">A RenderTarget.</param>
        public bool Destroy(RenderTarget renderTarget)
        {
            ThrowExceptionIfDisposed();

            renderTarget.Dispose();

            return Native.Engine.DestroyRenderTarget(NativePtr, renderTarget.NativePtr);
        }

        /// <summary>
        /// <para>Kicks the hardware thread (e.g. the OpenGL, Vulkan or Metal thread) and blocks until all commands to
        /// this point are executed. Note that this doesn't guarantee that the hardware is actually finished.</para>
        /// <para>This is typically used right after destroying the <see cref="SwapChain"/>, in cases where a guarantee about
        /// the <see cref="SwapChain"/> destruction is needed in a timely fashion, such as when responding to Android's
        /// <code>android.view.SurfaceHolder.Callback.surfaceDestroyed</code></para>
        /// </summary>
        public void FlushAndWait()
        {
            ThrowExceptionIfDisposed();

            Native.Engine.FlushAndWait(NativePtr);
        }

        /// <summary>
        /// Creates an instance of Engine.
        /// </summary>
        /// <param name="backend">Which driver backend to use.</param>
        /// <returns>The newly created Engine, or null if the Engine couldn't be created.</returns>
        public static Engine Create(Backend backend = Backend.Default)
        {
            return GetOrCreateCache(
                Native.Engine.CreateEngine((int) backend, IntPtr.Zero)
            );
        }

        #endregion

        #region FilamentBase

        /// <summary>
        /// <para>Dispose of the Engine instance and all associated resources.</para>
        /// <para>Should be called last and after all other resources have been destroyed, it ensures all filament
        /// resources are freed.</para>
        /// <para>Destroy performs the following tasks:</para>
        /// <list>
        /// <item><term>1. Destroy all internal software and hardware resources.</term></item>
        /// <item><term>2. Free all user allocated resources that are not already destroyed and logs a warning. This
        /// indicates a "leak" in the user's code.</term></item>
        /// <item><term>3. Terminate the rendering engine's thread.</term></item>
        /// </list>
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.Engine.DestroyEngine(NativePtr);
        }

        #endregion
    }
}
