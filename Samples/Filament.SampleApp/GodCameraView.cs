namespace Filament.DemoApp
{
    public class GodCameraView : CameraView
    {
        #region Properties

        public Camera GodCamera {
            set => View.DebugCamera = value;
        }

        #endregion

        #region Methods

        public GodCameraView(Renderer renderer, string name) : base(renderer, name)
        {
        }

        #endregion
    }
}
