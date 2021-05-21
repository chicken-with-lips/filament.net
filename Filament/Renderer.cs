using System;
using System.Numerics;

namespace Filament
{
    /// <summary>
    /// <para>A Renderer instance represents an operating system's window.</para>
    /// <para>Typically, applications create a Renderer per window. The Renderer generates drawing commands for the
    /// render thread and manages frame latency.</para>
    /// <para>A Renderer generates drawing commands from a View, itself containing a Scene description.</para>
    /// </summary>
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

        /// <summary>
        /// <para>Set-up a frame for this Renderer.</para>
        /// <para>BeginFrame manages frame pacing, and returns whether or not a frame should be drawn. The goal of this is to skip
        /// frames when the GPU falls behind in order to keep the frame latency low.</para>
        /// <para>If a given frame takes too much time in the GPU, the CPU will get ahead of the GPU. The display will
        /// draw the same frame twice producing a stutter. At this point, the CPU is ahead of the GPU and depending on
        /// how many frames are buffered, latency increases.</para>
        /// <para>BeginFrame attempts to detect this situation and returns false in that case, indicating to the caller
        /// to skip the current frame.</para>
        /// <para>When BeginFrame returns true, it is mandatory to render the frame and call <see cref="EndFrame"/>.
        /// However, when BeginFrame returns false, the caller has the choice to either skip the frame and not call
        /// <see cref="EndFrame"/>, or proceed as though true was returned.</para>
        /// </summary>
        /// <param name="swapChain">The <see cref="SwapChain"/> instance to use.</param>
        /// <param name="vsyncSteadyClockTimeNano">The time in nanosecond of when the current frame started, or 0 if
        /// unknown. This value should be the timestamp of the last h/w vsync.</param>
        /// <returns>False if the current frame should be skipped, true if the current must be drawn and
        /// <see cref="EndFrame"/> must be called.</returns>
        public bool BeginFrame(SwapChain swapChain, uint vsyncSteadyClockTimeNano = 0)
        {
            ThrowExceptionIfDisposed();

            return Native.Renderer.BeginFrame(NativePtr, swapChain.NativePtr, vsyncSteadyClockTimeNano);
        }

        /// <summary>
        /// <para>Finishes the current frame and schedules it for display.</para>
        /// <para>EndFrame schedules the current frame to be displayed on the Renderer's window.</para>
        /// <para>Note: All calls to <see cref="Render"/> must happen *before* EndFrame. EndFrame must be called if
        /// BeginFrame returned true, otherwise, EndFrame must not be called unless the caller ignored BeginFrame's
        /// return value.</para>
        /// </summary>
        public void EndFrame()
        {
            ThrowExceptionIfDisposed();

            Native.Renderer.EndFrame(NativePtr);
        }

        /// <summary>
        /// <para>Render a View into this renderer's window.</para>
        /// <para>This is filament main rendering method, most of the CPU-side heavy lifting is performed here. Render
        /// main function is to generate render commands which are asynchronously executed by the Engine's render
        /// thread.</para>
        /// <para>Render generates commands for each of the following stages:</para>
        /// <list>
        /// <item>1. Shadow map pass, if needed (currently only a single shadow map is supported).</item>
        /// <item>2. Depth pre-pass.</item>
        /// <item>3. Color pass.</item>
        /// <item>4. Post-processing pass.</item>
        /// </list>
        /// <para>Render must be called after BeginFrame and before EndFrame.</para>
        /// <para>Note: Render must be called from the Engine's main thread (or external synchronization must be
        /// provided). In particular, calls to Render on different Renderer instances MUST be synchronized.</para>
        /// <para>Note: Render performs potentially heavy computations and cannot be multi-threaded. However,
        /// internally, render() is highly multi-threaded to both improve performance in mitigate the call's latency.</para>
        /// </summary>
        /// <param name="view">The view to render.</param>
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
