using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using Filament.CameraUtilities;
using Filament.SampleData;
using GLFWDotNet;
using OpenTK.Mathematics;
using StbImageSharp;


namespace Filament.DemoApp
{
    public class WindowConfig
    {
        public string Title = "Filament";
        public int Width = 1024;
        public int Height = 640;
        public bool IsResizable = false;
    }

    public class ApplicationConfig
    {
        public bool IsHeadless = false;
        public bool EnableSplitView = false;
        public Backend Backend = Backend.OpenGL;
        public Mode CameraMode = Mode.Orbit;
        public string IblDirectory = null;
        public string DirtTexture = null;
    }

    public class Application
    {
        #region Properties

        public string RootAssetPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        public SetupCallback Setup { get; set; }

        public CleanupCallback Cleanup { get; set; }

        public PreRenderCallback PreRender { get; set; }

        public PostRenderCallback PostRender { get; set; }

        public AnimateCallback Animate { get; set; }

        public Ibl Ibl { get; private set; }

        #endregion

        #region Members

        private WindowConfig _windowConfig;
        private ApplicationConfig _appConfig;
        private Window _window;
        private Engine _engine;
        private SwapChain _swapChain;
        private Renderer _renderer;
        private Scene _scene;

        private Manipulator _mainCameraManipulator;
        private Manipulator _debugCameraManipulator;

        private Camera[] _cameras = new Camera[4];
        private Camera _mainCamera;
        private Camera _debugCamera;
        private Camera _orthoCamera;
        private Camera _uiCamera;

        private List<View> _offscreenViews = new();
        private List<CameraView> _views = new();
        private CameraView _mainView;
        private CameraView _uiView;
        private CameraView _depthView;
        private CameraView _orthoView;
        private GodCameraView _godView;

        private Material _defaultMaterial;
        private Material _transparentMaterial;
        private Material _depthMaterial;
        private MaterialInstance _depthMaterialInstance;

        private float _cameraFocalLength = 28f;
        private int _sidebarWidth = 0;
        private bool _isClosed;
        // private ulong _time;
        private float _time;
        private ulong _skippedFrames;

        private Texture _dirt;

        private int _lastX;
        private int _lastY;

        #endregion

        #region Methods

        public Application(WindowConfig windowConfig, ApplicationConfig appConfig)
        {
            _windowConfig = windowConfig;
            _appConfig = appConfig;
        }

        public void AddOffscreenView(View view)
        {
            _offscreenViews.Add(view);
        }

        public void Run()
        {
            _window = new Window(_windowConfig.Title, _windowConfig.Width, _windowConfig.Height, _windowConfig.IsResizable, _appConfig.IsHeadless);
            _engine = Engine.Create(_appConfig.Backend);

            if (_appConfig.IsHeadless) {
                _swapChain = _engine.CreateSwapChain(_windowConfig.Width, _windowConfig.Height);
            } else {
                _swapChain = _engine.CreateSwapChain(_window.NativeWindow);
            }

            _renderer = _engine.CreateRenderer();
            _scene = _engine.CreateScene();

            InitCameraAndViews();
            InitMaterials();

            LoadDirt(_appConfig);
            LoadIbl(_appConfig);

            if (Ibl != null) {
                Ibl.Skybox.SetLayerMask(0x7, 0x4);

                _scene.Skybox = Ibl.Skybox;
                _scene.IndirectLight = Ibl.IndirectLight;
            }

            foreach (var cameraView in _views) {
                if (cameraView != _uiView) {
                    cameraView.View.Scene = _scene;
                }
            }

            Setup?.Invoke(_engine, _mainView.View, _scene);

            // bool mousePressed[3] = { false };

            var sidebarWidth = _sidebarWidth;
            var cameraFocalLength = _cameraFocalLength;

            // SDL_EventState(SDL_DROPFILE, SDL_ENABLE);
            // SDL_Window* sdlWindow = window->getSDLWindow();

            while (!_isClosed) {
                if (_sidebarWidth != sidebarWidth || _cameraFocalLength != cameraFocalLength) {
                    ConfigureCamerasForWindow();

                    sidebarWidth = _sidebarWidth;
                    cameraFocalLength = _cameraFocalLength;
                }

                // Loop over fresh events twice: first stash them and let ImGui process them, then allow
                // the app to process the stashed events. This is done because ImGui might wish to block
                // certain events from the app (e.g., when dragging the mouse over an obscuring window).
                /*constexpr int kMaxEvents = 16;
                SDL_Event events[kMaxEvents];
                int nevents = 0;
                */

                GLFW.glfwPollEvents();


                /*
                while (nevents < kMaxEvents && SDL_PollEvent(&events[nevents]) != 0) {
                    if (mImGuiHelper) {
                        ImGuiIO& io = ImGui::GetIO();
                        SDL_Event* event = &events[nevents];
                        switch (event->type) {
                            case SDL_MOUSEWHEEL: {
                                if (event->wheel.x > 0) io.MouseWheelH += 1;
                                if (event->wheel.x < 0) io.MouseWheelH -= 1;
                                if (event->wheel.y > 0) io.MouseWheel += 1;
                                if (event->wheel.y < 0) io.MouseWheel -= 1;
                                break;
                            }
                            case SDL_MOUSEBUTTONDOWN: {
                                if (event->button.button == SDL_BUTTON_LEFT) mousePressed[0] = true;
                                if (event->button.button == SDL_BUTTON_RIGHT) mousePressed[1] = true;
                                if (event->button.button == SDL_BUTTON_MIDDLE) mousePressed[2] = true;
                                break;
                            }
                            case SDL_TEXTINPUT: {
                                io.AddInputCharactersUTF8(event->text.text);
                                break;
                            }
                            case SDL_KEYDOWN:
                            case SDL_KEYUP: {
                                int key = event->key.keysym.scancode;
                                IM_ASSERT(key >= 0 && key < IM_ARRAYSIZE(io.KeysDown));
                                io.KeysDown[key] = (event->type == SDL_KEYDOWN);
                                io.KeyShift = ((SDL_GetModState() & KMOD_SHIFT) != 0);
                                io.KeyAlt = ((SDL_GetModState() & KMOD_ALT) != 0);
                                io.KeyCtrl = ((SDL_GetModState() & KMOD_CTRL) != 0);
                                io.KeySuper = ((SDL_GetModState() & KMOD_GUI) != 0);
                                break;
                            }
                        }
                    }
                    nevents++;
                }*/

                if (GLFW.glfwWindowShouldClose(_window.Ptr) != 0) {
                    _isClosed = true;
                }

                GLFW.glfwSetCursorPosCallback(_window.Ptr, (window, xpos, ypos) => {
                    // if (!io || !io->WantCaptureMouse)
                    _lastX = (int) xpos;
                    _lastY = (int) ypos;

                    foreach (var cameraView in _views) {
                        if (cameraView.Intersects(_lastX, _lastY)) {
                            cameraView.MouseMoved(_lastX, _lastY);
                        }
                    }
                });

                GLFW.glfwSetScrollCallback(_window.Ptr, (window, xoffset, yoffset) => {
                    // if (!io || !io->WantCaptureMouse)
                    foreach (var cameraView in _views) {
                        if (cameraView.Intersects(_lastX, _lastY)) {
                            cameraView.MouseWheel((int) yoffset);
                        }
                    }
                });

                GLFW.glfwSetMouseButtonCallback(_window.Ptr, (window, button, action, mods) => {
                    // if (!io || !io->WantCaptureMouse)
                    foreach (var cameraView in _views) {
                        if (action == GLFW.GLFW_PRESS) {
                            cameraView.MouseDown(button, _lastX, _lastY);
                        } else if (action == GLFW.GLFW_RELEASE) {
                            cameraView.MouseUp(_lastX, _lastY);
                        }
                    }
                });

                GLFW.glfwSetWindowSizeCallback(_window.Ptr, (window, width, height) => {
                    ConfigureCamerasForWindow();
                });

                // Now, loop over the events a second time for app-side processing.
                /*for (int i = 0; i < nevents; i++) {
                    const SDL_Event& event = events[i];
                    ImGuiIO* io = mImGuiHelper ? &ImGui::GetIO() : nullptr;
                    switch (event.type) {
                        case SDL_QUIT:
                            mClosed = true;
                            break;
                        case SDL_KEYDOWN:
                            if (event.key.keysym.scancode == SDL_SCANCODE_ESCAPE) {
                                mClosed = true;
                            }
                            window->keyDown(event.key.keysym.scancode);
                            break;
                        case SDL_KEYUP:
                            window->keyUp(event.key.keysym.scancode);
                            break;
                        case SDL_DROPFILE:
                            if (mDropHandler) {
                                mDropHandler(event.drop.file);
                            }
                            SDL_free(event.drop.file);
                            break;
                        case SDL_WINDOWEVENT:
                            switch (event.window.event) {
                                case SDL_WINDOWEVENT_RESIZED:
                                    window->resize();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                }*/

                // Calculate the time step.
                // var frequency = GLFW.glfwGetTimerFrequency();
                // var now = GLFW.glfwGetTimerValue();
                // var timeStep = _time > 0 ? (float) ((double) (now - _time) / frequency) : (float) (1.0f / 60.0f);

                // _time = now;

                var timeStep = (float) GLFW.glfwGetTime() - _time;
                _time = (float) GLFW.glfwGetTime();

                // Allow the app to animate the scene if desired.
                Animate?.Invoke(_engine, _mainView.View, (float) GLFW.glfwGetTimerValue() / (float) GLFW.glfwGetTimerFrequency());

                // Populate the UI scene, regardless of whether Filament wants to a skip frame. We should
                // always let ImGui generate a command list; if it skips a frame it'll destroy its widgets.
                /*if (mImGuiHelper) {

                    // Inform ImGui of the current window size in case it was resized.
                    if (config.headless) {
                        mImGuiHelper->setDisplaySize(window->mWidth, window->mHeight);
                    } else {
                        int windowWidth, windowHeight;
                        int displayWidth, displayHeight;
                        SDL_GetWindowSize(window->mWindow, &windowWidth, &windowHeight);
                        SDL_GL_GetDrawableSize(window->mWindow, &displayWidth, &displayHeight);
                        mImGuiHelper->setDisplaySize(windowWidth, windowHeight,
                                windowWidth > 0 ? ((float)displayWidth / windowWidth) : 0,
                                displayHeight > 0 ? ((float)displayHeight / windowHeight) : 0);
                    }

                    // Setup mouse inputs (we already got mouse wheel, keyboard keys & characters
                    // from our event handler)
                    ImGuiIO& io = ImGui::GetIO();
                    int mx, my;
                    Uint32 buttons = SDL_GetMouseState(&mx, &my);
                    io.MousePos = ImVec2(-FLT_MAX, -FLT_MAX);
                    io.MouseDown[0] = mousePressed[0] || (buttons & SDL_BUTTON(SDL_BUTTON_LEFT)) != 0;
                    io.MouseDown[1] = mousePressed[1] || (buttons & SDL_BUTTON(SDL_BUTTON_RIGHT)) != 0;
                    io.MouseDown[2] = mousePressed[2] || (buttons & SDL_BUTTON(SDL_BUTTON_MIDDLE)) != 0;
                    mousePressed[0] = mousePressed[1] = mousePressed[2] = false;

                    // TODO: Update to a newer SDL and use SDL_CaptureMouse() to retrieve mouse coordinates
                    // outside of the client area; see the imgui SDL example.
                    if ((SDL_GetWindowFlags(window->mWindow) & SDL_WINDOW_INPUT_FOCUS) != 0) {
                        io.MousePos = ImVec2((float)mx, (float)my);
                    }

                    // Populate the UI Scene.
                    mImGuiHelper->render(timeStep, imguiCallback);
                }*/

                // Update the camera manipulators for each view.
                foreach (var cameraView in _views) {
                    cameraView.CameraManipulator?.Update(timeStep);
                }

                // Update the position and orientation of the two cameras.
                Vector3 eyePosition, center, up;

                _mainCameraManipulator.GetLookAt(out eyePosition, out center, out up);
                _mainCamera.LookAt(eyePosition, center, up);

                _debugCameraManipulator.GetLookAt(out eyePosition, out center, out up);
                _debugCamera.LookAt(eyePosition, center, up);

                // Update the cube distortion matrix used for frustum visualization.
                var lightmapCamera = _mainView.View.DirectionalLightCamera;

                // lightmapCube->mapFrustum(_engine, lightmapCamera);
                // cameraCube->mapFrustum(_engine, _mainCamera);

                // TODO: we need better timing or use SDL_GL_SetSwapInterval
                Thread.Sleep(16);

                if (null != PreRender) {
                    foreach (var cameraView in _views) {
                        if (cameraView != _uiView) {
                            PreRender(_engine, cameraView.View, _scene, _renderer);
                        }
                    }
                }

                if (_renderer.BeginFrame(_swapChain)) {
                    foreach (var offscreenView in _offscreenViews) {
                        _renderer.Render(offscreenView);
                    }

                    foreach (var cameraView in _views) {
                        _renderer.Render(cameraView.View);
                    }

                    _renderer.EndFrame();

                    // We call PostRender only when the frame has not been skipped. It might be used
                    // for taking screenshots under the assumption that a state change has taken effect.
                    if (null != PostRender) {
                        foreach (var cameraView in _views) {
                            if (cameraView != _uiView) {
                                PostRender(_engine, cameraView.View, _scene, _renderer);
                            }
                        }
                    }
                } else {
                    ++_skippedFrames;
                }
            }

            /*if (mImGuiHelper) {
                mImGuiHelper.reset();
            }*/

            Cleanup?.Invoke(_engine, _mainView.View, _scene);

            // cameraCube.reset();
            // lightmapCube.reset();
            // window.reset();

            Ibl = null;

            _engine.Destroy(_depthMaterialInstance);
            _engine.Destroy(_depthMaterial);
            _engine.Destroy(_defaultMaterial);
            _engine.Destroy(_transparentMaterial);
            _engine.Destroy(_scene);
            _engine.Dispose();

            _engine = null;
        }

        private void InitCameraAndViews()
        {
            // create cameras
            _cameras[0] = _mainCamera = _engine.CreateCameraWithEntity(EntityManager.Create());
            _cameras[1] = _debugCamera = _engine.CreateCameraWithEntity(EntityManager.Create());
            _cameras[2] = _orthoCamera = _engine.CreateCameraWithEntity(EntityManager.Create());
            _cameras[3] = _uiCamera = _engine.CreateCameraWithEntity(EntityManager.Create());

            // set exposure
            foreach (var camera in _cameras) {
                camera.SetExposure(16.0f, 1f / 125.0f, 100.0f);
            }

            // create views
            _views.Add(_mainView = new CameraView(_renderer, "Main View"));

            if (_appConfig.EnableSplitView) {
                _views.AddRange(new CameraView[] {
                    _depthView = new CameraView(_renderer, "Depth View"),
                    _godView = new GodCameraView(_renderer, "God View"),
                    _orthoView = new CameraView(_renderer, "Ortho View")
                });
            }

            _views.Add(_uiView = new CameraView(_renderer, "UI View"));

            // set-up the camera manipulators
            _mainCameraManipulator = ManipulatorBuilder.Create()
                .WithTargetPosition(0, 0, -4f)
                .WithFlightMoveDamping(15.0f)
                .Build(_appConfig.CameraMode);

            _debugCameraManipulator = ManipulatorBuilder.Create()
                .WithTargetPosition(0, 0, -4)
                .Build(Mode.Orbit);

            _mainView.Camera = _mainCamera;
            _mainView.CameraManipulator = _mainCameraManipulator;

            _uiView.Camera = _uiCamera;

            if (_appConfig.EnableSplitView) {
                // Depth view always uses the main camera
                _depthView.Camera = _mainCamera;

                // The god view uses the main camera for culling, but the debug camera for viewing
                _godView.Camera = _mainCamera;
                _godView.GodCamera = _debugCamera;

                // Ortho view obviously uses an ortho camera
                _orthoView.Camera = _mainView.View.DirectionalLightCamera;

                _depthView.CameraManipulator = _mainCameraManipulator;
                _godView.CameraManipulator = _debugCameraManipulator;
            }

            // configure the cameras
            ConfigureCamerasForWindow();

            _mainCamera.LookAt(
                new Vector3(4, 0, -4),
                new Vector3(0, 0, -4),
                new Vector3(0, 1, 0)
            );
        }

        private void InitMaterials()
        {
            var sampleData = new SampleAppDataLoader();

            _depthMaterial = MaterialBuilder.Create()
                .WithPackage(sampleData.LoadDepthVisualizer())
                .Build(_engine);

            _depthMaterialInstance = _depthMaterial.CreateInstance();

            _defaultMaterial = MaterialBuilder.Create()
                .WithPackage(sampleData.LoadAiDefaultMat())
                .Build(_engine);

            _transparentMaterial = MaterialBuilder.Create()
                .WithPackage(sampleData.LoadTransparentColor())
                .Build(_engine);

            // std::unique_ptr < Cube > cameraCube(new Cube(*mEngine, mTransparentMaterial,  {1, 0, 0}));
            // we can't cull the light-frustum because it's not applied a rigid transform
            // and currently, filament assumes that for culling
            // std::unique_ptr < Cube > lightmapCube(new Cube(*mEngine, mTransparentMaterial,  {0, 1, 0}, false));

            _mainView.View.SetVisibleLayers(0x4, 0x4);

            if (_appConfig.EnableSplitView) {
                var rcm = _engine.RenderableManager;

                // rcm.SetLayerMask(rcm.GetInstance(cameraCube.SolidRenderable), 0x3, 0x2);
                // rcm.SetLayerMask(rcm.GetInstance(cameraCube.WireFrameRenderable), 0x3, 0x2);

                // rcm.SetLayerMask(rcm.GetInstance(lightmapCube.SolidRenderable), 0x3, 0x2);
                // rcm.SetLayerMask(rcm.GetInstance(lightmapCube.WireFrameRenderable), 0x3, 0x2);

                // Create the camera mesh
                // _scene.AddEntity(cameraCube.WireFrameRenderable);
                // _scene.AddEntity(cameraCube.SolidRenderable);

                // _scene.AddEntity(lightmapCube.WireFrameRenderable);
                // _scene.AddEntity(lightmapCube.SolidRenderable);

                _depthView.View.SetVisibleLayers(0x4, 0x4);
                _godView.View.SetVisibleLayers(0x6, 0x6);
                _orthoView.View.SetVisibleLayers(0x6, 0x6);

                // only preserve the color buffer for additional views; depth and stencil can be discarded.
                _depthView.View.ShadowingEnabled = false;
                _godView.View.ShadowingEnabled = false;
                _orthoView.View.ShadowingEnabled = false;
            }
        }

        private void ConfigureCamerasForWindow()
        {
            var dpiScaleX = 1.0f;
            var dpiScaleY = 1.0f;
            var width = _windowConfig.Width;
            var height = _windowConfig.Height;

            // If the app is not headless, query the window for its physical & virtual sizes.
            if (_window != null) {
                width = _window.Width;
                height = _window.Height;

                var scale = _window.ContentScale;
                dpiScaleX = scale.X;
                dpiScaleY = scale.Y;
            }

            var ratio = (float) height / (float) width;
            var sidebar = (int) (_sidebarWidth * dpiScaleX);

            // To trigger a floating-point exception, users could shrink the window to be smaller than
            // the sidebar. To prevent this we simply clamp the width of the main viewport.
            var mainWidth = Math.Max(1, width - sidebar);

            var near = 0.1f;
            var far = 100f;

            _mainCamera.SetLensProjection(_cameraFocalLength, (float) mainWidth / height, near, far);
            _debugCamera.SetProjection(45.0f, (float) width / height, 0.0625f, 4096, FieldOfView.Vertical);
            _uiCamera.SetProjection(Projection.Ortho, 0, (float) width / dpiScaleX, height / dpiScaleY, 0, 0, 1);

            _orthoCamera.SetProjection(Projection.Ortho, -3, 3, -3 * ratio, 3 * ratio, near, far);
            _orthoCamera.LookAt(
                new Vector3(0, 0, 0),
                new Vector3(0, 0, -4)
            );

            // We're in split view when there are more views than just the Main and UI views.
            if (_views.Count > 2) {
                int vpw = width / 2;
                int vph = height / 2;

                _mainView.Viewport = new Viewport(0, 0, vpw, vph);
                _depthView.Viewport = new Viewport(vpw, 0, width - vpw, vph);
                _godView.Viewport = new Viewport(vpw, vph, width - vpw, height - vph);
                _orthoView.Viewport = new Viewport(0, vph, vpw, height - vph);
            } else {
                _mainView.Viewport = new Viewport(sidebar, 0, mainWidth, height);
            }

            _uiView.Viewport = new Viewport(0, 0, width, height);
        }


        private void LoadDirt(ApplicationConfig config)
        {
            if (string.IsNullOrEmpty(config.DirtTexture)) {
                return;
            }

            if (!File.Exists(config.DirtTexture)) {
                Console.WriteLine("The specified dirt file does not exist: {0}", config.DirtTexture);
                return;
            }

            using (var stream = File.OpenRead(config.DirtTexture)) {
                var imageResult = ImageResult.FromStream(stream, ColorComponents.RedGreenBlue);

                _dirt = TextureBuilder.Create()
                    .WithWidth(imageResult.Width)
                    .WithHeight(imageResult.Height)
                    .WithFormat(TextureFormat.Rgb8)
                    .Build(_engine);

                var descriptor = new PixelBufferDescriptor(imageResult.Data, PixelDataFormat.Rgb, PixelDataType.UByte);

                _dirt.SetImage(_engine, 0, descriptor);
            }
        }

        private void LoadIbl(ApplicationConfig config)
        {
            if (string.IsNullOrEmpty(config.IblDirectory)) {
                return;
            }

            if (!Directory.Exists(config.IblDirectory)) {
                Console.WriteLine("The specified IBL path does not exist: {0}", config.IblDirectory);
                return;
            }

            Ibl = new Ibl(_engine);

            if (!Ibl.LoadFromDirectory(config.IblDirectory)) {
                Console.WriteLine("Could not load the specified IBL: {0}", config.IblDirectory);
                Ibl = null;
            }
        }

        #endregion

        public delegate void SetupCallback(Engine engine, View view, Scene scene);

        public delegate void CleanupCallback(Engine engine, View view, Scene scene);

        public delegate void PreRenderCallback(Engine engine, View view, Scene scene, Renderer renderer);

        public delegate void PostRenderCallback(Engine engine, View view, Scene scene, Renderer renderer);

        public delegate void AnimateCallback(Engine engine, View view, float now);
    }
}
