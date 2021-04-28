using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Filament.Image;
using OpenTK.Mathematics;
using StbImageSharp;

namespace Filament.DemoApp
{
    public class Ibl : IDisposable
    {
        readonly float Intensity = 30000f;
        readonly string[] FaceSuffix = new[] {"px", "nx", "py", "ny", "pz", "nz"};

        #region Properties

        public IndirectLight IndirectLight { get; private set; }
        public Skybox Skybox { get; private set; }
        public Vector3[] SphericalHarmonics => _bands;

        #endregion

        #region Members

        private readonly Engine _engine;

        private Texture _texture;
        private Texture _skyboxTexture;

        private Vector3[] _bands = new Vector3[9];

        #endregion

        #region Methods

        public Ibl(Engine engine)
        {
            _engine = engine;
        }

        public bool LoadFromKtx(string path)
        {
            string iblPath = path + "_ibl.ktx";

            if (!File.Exists(iblPath)) {
                return false;
            }

            string skyPath = path + "_skybox.ktx";

            if (!File.Exists(skyPath)) {
                return false;
            }

            var iblKtx = CreateKtx(iblPath);
            var skyKtx = CreateKtx(skyPath);

            _skyboxTexture = KtxUtility.CreateTexture(_engine, skyKtx, false);
            _texture = KtxUtility.CreateTexture(_engine, iblKtx, false);

            if (!iblKtx.GetSphericalHarmonics(out _bands)) {
                return false;
            }

            IndirectLight = IndirectLightBuilder.Create()
                .WithReflections(_texture)
                .WithIntensity(Intensity)
                .Build(_engine);

            Skybox = SkyboxBuilder.Create()
                .WithEnvironment(_skyboxTexture)
                .WithSun(true)
                .Build(_engine);

            return true;
        }

        private KtxBundle CreateKtx(string path)
        {
            return new KtxBundle(File.ReadAllBytes(path));
        }

        public bool LoadFromDirectory(string path)
        {
            // First check if KTX files are available.
            if (LoadFromKtx(Path.Combine(path, Path.GetFileName(path)))) {
                return true;
            }

            // Read spherical harmonics
            string sh = Path.Combine(path, "sh.txt");
            var pattern = new Regex(@"^\([\s]{0,}(.+),[\s]{0,}(.+),[\s]{0,}(.+)\)", RegexOptions.Compiled);

            if (File.Exists(sh)) {
                var lines = File.ReadAllLines(sh);

                if (lines.Length != _bands.Length) {
                    return false;
                }

                for (var i = 0; i < _bands.Length; i++) {
                    var matches = pattern.Matches(lines[i]);

                    if (!pattern.IsMatch(lines[0])) {
                        return false;
                    }

                    var parts = matches[0].Groups;

                    if (!float.TryParse(parts[1].Value, out var r)) {
                        return false;
                    }

                    if (!float.TryParse(parts[2].Value, out var g)) {
                        return false;
                    }

                    if (!float.TryParse(parts[3].Value, out var b)) {
                        return false;
                    }

                    _bands[i] = new Vector3(r, g, b);
                }
            } else {
                return false;
            }

            // Read mip-mapped cubemap
            string prefix = "m";

            if (!LoadCubemapLevel(ref _texture, path, 0, prefix + "0_")) {
                return false;
            }

            int numLevels = _texture.Levels;

            for (var i = 1; i < numLevels; i++) {
                LoadCubemapLevel(ref _texture, path, i, prefix + i + "_");
            }

            if (!LoadCubemapLevel(ref _skyboxTexture, path)) {
                return false;
            }

            IndirectLight = IndirectLightBuilder.Create()
                .WithReflections(_texture)
                .WithIrradiance(3, _bands)
                .WithIntensity(Intensity)
                .Build(_engine);

            Skybox = SkyboxBuilder.Create()
                .WithEnvironment(_skyboxTexture)
                .WithSun(true)
                .Build(_engine);

            return true;
        }

        private bool LoadCubemapLevel(ref Texture texture, string path, int level = 0, string levelPrefix = "")
        {
            if (LoadCubemapLevel(ref texture, out var buffer, out var offsets, path, level, levelPrefix)) {
                texture.SetImage(_engine, level, buffer, offsets);
                return true;
            }

            return false;
        }

        private bool LoadCubemapLevel(ref Texture texture, out PixelBufferDescriptor outBuffer, out FaceOffsets outOffsets, string path, int level, string levelPrefix)
        {
            int size = 0;
            int numLevels = 1;

            outBuffer = null;
            outOffsets = new FaceOffsets();

            {
                string faceName = levelPrefix + FaceSuffix[0] + ".rgb32f";
                string facePath = Path.Combine(path, faceName);

                if (!File.Exists(facePath)) {
                    Console.WriteLine("The face {0} does not exist", facePath);
                    return false;
                }

                var imageInfo = ImageInfo.FromStream(File.OpenRead(facePath));

                if (imageInfo.Value.Width != imageInfo.Value.Height) {
                    Console.WriteLine("width != height");
                    return false;
                }

                size = imageInfo.Value.Width;
                numLevels = 1;

                if (string.IsNullOrEmpty(levelPrefix)) {
                    numLevels = (int) MathHelper.Log2(size) + 1;
                }

                if (level == 0) {
                    texture = TextureBuilder.Create()
                        .WithWidth(size)
                        .WithHeight(size)
                        .WithLevels(numLevels)
                        .WithFormat(TextureFormat.R11F_G11F_B10F)
                        .WithSampler(TextureSamplerType.Cubemap)
                        .Build(_engine);
                }
            }

            // RGB_10_11_11_REV encoding: 4 bytes per pixel
            var faceSize = size * size * Marshal.SizeOf<uint>();

            FaceOffsets offsets = new FaceOffsets();
            byte[] pixelBuffer = new byte[faceSize * 6];
            var success = true;

            for (var j = 0; j < 6; j++) {
                outOffsets[j] = faceSize * j;

                string faceName = levelPrefix + FaceSuffix[j] + ".rgb32f";
                string facePath = Path.Combine(path, faceName);

                if (!File.Exists(facePath)) {
                    Console.WriteLine("The face {0} does not exist", facePath);
                    success = false;
                    break;
                }

                var imageResult = ImageResult.FromStream(File.OpenRead(facePath), ColorComponents.RedGreenBlueAlpha);

                if (imageResult.Width != imageResult.Height || imageResult.Width != size) {
                    Console.WriteLine("Face {0} has a wrong size {1}x{2}, instead of {3}x{3}", faceName, imageResult.Width, imageResult.Height, size);
                    success = false;
                    break;
                }

                if (imageResult.SourceComp != ColorComponents.RedGreenBlueAlpha) {
                    Console.WriteLine("Could not decode face {0}", faceName);
                    success = false;
                    break;
                }

                Array.Copy(imageResult.Data, 0, pixelBuffer, offsets[j], imageResult.Width * imageResult.Height * Marshal.SizeOf<uint>());
            }

            if (!success) {
                return false;
            }


            outBuffer = new PixelBufferDescriptor(pixelBuffer, PixelDataFormat.Rgb, PixelDataType.UInt_10F_11F_11F_Rev);

            return true;
        }

        #endregion

        #region IDisposable

        #region Properties

        public bool IsDisposed { get; private set; }

        #endregion

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed) {
                return;
            }

            _engine.Destroy(IndirectLight);
            _engine.Destroy(Skybox);
            _engine.Destroy(_texture);
            _engine.Destroy(_skyboxTexture);

            IsDisposed = true;
        }

        #endregion
    }
}
