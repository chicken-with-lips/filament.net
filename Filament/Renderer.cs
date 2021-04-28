using System;
using OpenTK.Mathematics;

namespace Filament
{
    public class Renderer : FilamentBase<Renderer>
    {
        #region Properties

        public Engine Engine {
            get {
                ThrowExceptionIfDisposed();

                return Engine.GetOrCreateCache(
                    Native.Renderer.GetEngine(NativePtr)
                );
            }
        }

        #endregion

        #region Methods

        private Renderer(IntPtr ptr) : base(ptr)
        {
        }

        internal static Renderer GetOrCreateCache(IntPtr ptr)
        {
            return GetOrCreateCache(ptr, newPtr => new Renderer(newPtr));
        }

        public bool BeginFrame(SwapChain swapChain, uint vsyncSteadyClockTimeNano = 0)
        {
            ThrowExceptionIfDisposed();

            return Native.Renderer.BeginFrame(NativePtr, swapChain.NativePtr, vsyncSteadyClockTimeNano);
        }

        public void EndFrame()
        {
            ThrowExceptionIfDisposed();

            Native.Renderer.EndFrame(NativePtr);
        }

        public void Render(View view)
        {
            ThrowExceptionIfDisposed();

            Native.Renderer.Render(NativePtr, view.NativePtr);
        }

        /// <summary>
        /// Set ClearOptions which are used at the beginning of a frame to clear or retain the SwapChain content.
        /// </summary>
        /// <param name="clearColor">Color to use to clear the SwapChain.</param>
        /// <param name="shouldClear">Whether the SwapChain should be cleared using the clearColor. Use this if translucent View will be drawn, for instance.</param>
        /// <param name="shouldDiscardContent">
        /// Whether the SwapChain content should be discarded. clear implies discard. Set this to false (along with
        /// clear to false as well) if the SwapChain already has content that needs to be preserved
        /// </param>
        public void SetClearOptions(Vector4 clearColor, bool shouldClear, bool shouldDiscardContent = true)
        {
            ThrowExceptionIfDisposed();

            Native.Renderer.SetClearOptions(NativePtr, clearColor.X, clearColor.Y, clearColor.Z, clearColor.W, shouldClear, shouldDiscardContent);
        }

        #endregion
    }
}
