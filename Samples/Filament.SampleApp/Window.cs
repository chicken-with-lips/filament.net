using System;
using System.Numerics;
using static GLFWDotNet.GLFW;

namespace Filament.DemoApp
{
    public class Window : IDisposable
    {
        #region Properties

        public IntPtr Ptr => _windowPtr;

        public IntPtr NativeWindow => glfwGetNativeWindow(_windowPtr);

        public int Width {
            get {
                glfwGetWindowSize(_windowPtr, out int width, out int height);
                return width;
            }
        }

        public int Height {
            get {
                glfwGetWindowSize(_windowPtr, out int width, out int height);
                return height;
            }
        }

        public Vector2 ContentScale {
            get {
                glfwGetWindowContentScale(_windowPtr, out var xScale, out var yScale);

                return new Vector2((float) xScale, (float) yScale);
            }
        }

        #endregion

        #region Members

        private IntPtr _windowPtr;

        #endregion

        #region Methods

        public Window(string title, int width, int height, bool isResizable, bool isHeadless)
        {
            glfwInit();

            if (isResizable) {
                glfwWindowHint(GLFW_RESIZABLE, 1);
            }

            if (isHeadless) {
                glfwWindowHint(GLFW_VISIBLE, 0);
            }

            _windowPtr = glfwCreateWindow(width, height, title, IntPtr.Zero, IntPtr.Zero);
        }

        ~Window()
        {
            Dispose(false);
        }

        #endregion

        #region IDisposable

        private bool _isDisposed;


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed) {
                return;
            }

            glfwTerminate();

            _isDisposed = true;
        }

        #endregion
    }
}
