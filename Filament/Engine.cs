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

        #endregion

        #region Methods

        private Engine(IntPtr ptr) : base(ptr)
        {
        }

        internal static Engine GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new Engine(newPtr));
        }

        public SwapChain CreateSwapChain(IntPtr nativeWindow, SwapChainConfig flags = SwapChainConfig.None)
        {
            ThrowExceptionIfDisposed();

            return SwapChain.GetOrCreateCache(
                Native.Engine.CreateSwapChain(NativePtr, nativeWindow, (uint) flags)
            );
        }

        public SwapChain CreateSwapChain(int width, int height, SwapChainConfig flags = SwapChainConfig.None)
        {
            ThrowExceptionIfDisposed();

            return SwapChain.GetOrCreateCache(
                Native.Engine.CreateSwapChainHeadless(NativePtr, width, height, (uint) flags)
            );
        }

        public Renderer CreateRenderer()
        {
            ThrowExceptionIfDisposed();

            return Renderer.GetOrCreateCache(
                Native.Engine.CreateRenderer(NativePtr)
            );
        }

        public View CreateView()
        {
            ThrowExceptionIfDisposed();

            return View.GetOrCreateCache(
                Native.Engine.CreateView(NativePtr)
            );
        }

        public Scene CreateScene()
        {
            ThrowExceptionIfDisposed();

            return Scene.GetOrCreateCache(
                Native.Engine.CreateScene(NativePtr)
            );
        }

        public Camera CreateCameraWithEntity(int entity)
        {
            ThrowExceptionIfDisposed();

            return Camera.GetOrCreateCache(
                Native.Engine.CreateCameraWithEntity(NativePtr, entity)
            );
        }

        public void DestroyCameraComponent(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.Engine.DestroyCameraComponent(NativePtr, entity);
        }

        public void Destroy(int entity)
        {
            ThrowExceptionIfDisposed();

            Native.Engine.DestroyEntity(NativePtr, entity);
        }

        public bool Destroy(SwapChain swapChain)
        {
            ThrowExceptionIfDisposed();

            swapChain.Dispose();

            return Native.Engine.DestroySwapChain(NativePtr, swapChain.NativePtr);
        }

        public bool Destroy(Scene scene)
        {
            ThrowExceptionIfDisposed();

            scene.Dispose();

            return Native.Engine.DestroyScene(NativePtr, scene.NativePtr);
        }

        public bool Destroy(Renderer renderer)
        {
            ThrowExceptionIfDisposed();

            renderer.Dispose();

            return Native.Engine.DestroyRenderer(NativePtr, renderer.NativePtr);
        }

        public bool Destroy(View view)
        {
            ThrowExceptionIfDisposed();

            view.Dispose();

            return Native.Engine.DestroyView(NativePtr, view.NativePtr);
        }

        public bool Destroy(Skybox skybox)
        {
            ThrowExceptionIfDisposed();

            skybox.Dispose();

            return Native.Engine.DestroySkybox(NativePtr, skybox.NativePtr);
        }

        public bool Destroy(Texture texture)
        {
            ThrowExceptionIfDisposed();

            texture.Dispose();

            return Native.Engine.DestroyTexture(NativePtr, texture.NativePtr);
        }

        public bool Destroy(VertexBuffer vertexBuffer)
        {
            ThrowExceptionIfDisposed();

            vertexBuffer.Dispose();

            return Native.Engine.DestroyVertexBuffer(NativePtr, vertexBuffer.NativePtr);
        }

        public bool Destroy(IndexBuffer indexBuffer)
        {
            ThrowExceptionIfDisposed();

            indexBuffer.Dispose();

            return Native.Engine.DestroyIndexBuffer(NativePtr, indexBuffer.NativePtr);
        }

        public bool Destroy(Material material)
        {
            ThrowExceptionIfDisposed();

            material.Dispose();

            return Native.Engine.DestroyMaterial(NativePtr, material.NativePtr);
        }

        public bool Destroy(IndirectLight light)
        {
            ThrowExceptionIfDisposed();

            light.Dispose();

            return Native.Engine.DestroyIndirectLight(NativePtr, light.NativePtr);
        }

        public bool Destroy(MaterialInstance materialInstance)
        {
            ThrowExceptionIfDisposed();

            materialInstance.Dispose();

            return Native.Engine.DestroyMaterialInstance(NativePtr, materialInstance.NativePtr);
        }

        public bool Destroy(RenderTarget renderTarget)
        {
            ThrowExceptionIfDisposed();

            renderTarget.Dispose();

            return Native.Engine.DestroyRenderTarget(NativePtr, renderTarget.NativePtr);
        }

        public void FlushAndWait()
        {
            ThrowExceptionIfDisposed();

            Native.Engine.FlushAndWait(NativePtr);
        }

        public static Engine Create(Backend backend = Backend.Default)
        {
            return GetOrCreateCache(
                Native.Engine.CreateEngine((int) backend, IntPtr.Zero)
            );
        }

        #endregion

        #region FilamentBase

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            Native.Engine.DestroyEngine(NativePtr);
        }

        #endregion
    }
}
