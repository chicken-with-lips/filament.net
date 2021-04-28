using Filament.CameraUtilities;

namespace Filament.DemoApp
{
    public class CameraView
    {
        #region Properties

        public Manipulator CameraManipulator {
            get => _cameraManipulator;
            set => _cameraManipulator = value;
        }

        public Camera Camera {
            set => _view.Camera = value;
        }

        public Viewport Viewport {
            get => _viewport;
            set {
                _viewport = value;
                _view.Viewport = _viewport;

                _cameraManipulator?.SetViewport(_viewport.Width, _viewport.Height);
            }
        }

        public View View => _view;

        #endregion

        #region Members

        private string _name;
        private Viewport _viewport;
        private View _view;
        private Manipulator _cameraManipulator;
        private Engine _engine;

        #endregion

        #region Methods

        public CameraView(Renderer renderer, string name)
        {
            _name = name;

            _engine = renderer.Engine;
            _view = _engine.CreateView();
            _view.Name = _name;
        }

        ~CameraView()
        {
            _engine.Destroy(_view);
        }

        public bool Intersects(int x, int y)
        {
            if (x >= _viewport.Left && x < _viewport.Left + _viewport.Width) {
                if (y >= _viewport.Bottom && y < _viewport.Bottom + _viewport.Height) {
                    return true;
                }
            }

            return false;
        }

        public void MouseDown(int button, int x, int y)
        {
            _cameraManipulator?.GrabBegin(x, y, button == 3);
        }

        public void MouseUp(int x, int y)
        {
            _cameraManipulator?.GrabEnd();
        }

        public void MouseMoved(int x, int y)
        {
            _cameraManipulator?.GrabUpdate(x, y);
        }

        public void MouseWheel(int x)
        {
            _cameraManipulator?.Scroll(0, 0, x);
        }

        #endregion

        public enum Mode
        {
            None,
            Rotate,
            Track
        }
    }
}
