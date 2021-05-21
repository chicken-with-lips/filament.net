using System.IO;
using System.Numerics;
using System.Reflection;
using Filament;
using Filament.DemoApp;
using Filament.Image;
using Filament.MeshIO;
using Filament.SampleData;
using StbImageSharp;

namespace Suzanne
{
    class Program
    {
        private const string IBL_FOLDER = "default_env";

        static void Main(string[] args)
        {
            Material material = null;
            MaterialInstance materialInstance = null;
            Texture normal = null;
            Texture albedo = null;
            Texture ao = null;
            Texture metallic = null;
            Texture roughness = null;
            Mesh mesh = null;
            Matrix4x4 transform;

            var app = new Application(
                new WindowConfig() {
                    Title = "suzanne",
                },
                new ApplicationConfig() {
                    IblDirectory = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), IBL_FOLDER)
                }
            );

            app.Setup = (engine, view, scene) => {
                var tcm = engine.TransformManager;
                var rcm = engine.RenderableManager;

                var monkeyData = new MonkeyDataLoader();

                // Create textures. The KTX bundles are freed by KtxUtility.
                var albedoBundle = new KtxBundle(monkeyData.LoadAlbedoS3tc());
                var aoBundle = new KtxBundle(monkeyData.LoadAo());
                var metallicBundle = new KtxBundle(monkeyData.LoadMetallic());
                var roughnessBundle = new KtxBundle(monkeyData.LoadRougness());
                albedo = KtxUtility.CreateTexture(engine, albedoBundle, true);
                ao = KtxUtility.CreateTexture(engine, aoBundle, false);
                metallic = KtxUtility.CreateTexture(engine, metallicBundle, false);
                roughness = KtxUtility.CreateTexture(engine, roughnessBundle, false);
                normal = LoadNormalMap(engine, monkeyData.LoadNormal());

                var resourceData = new SampleDataLoader();
                var sampler = new TextureSampler(SamplerMinFilter.LinearMipmapLinear, SamplerMagFilter.Linear);

                // Instantiate material.
                material = MaterialBuilder.Create()
                    .WithPackage(resourceData.LoadTexturedLit())
                    .Build(engine);

                materialInstance = material.CreateInstance();
                materialInstance.SetParameter("albedo", albedo, sampler);
                materialInstance.SetParameter("ao", ao, sampler);
                materialInstance.SetParameter("metallic", metallic, sampler);
                materialInstance.SetParameter("normal", normal, sampler);
                materialInstance.SetParameter("roughness", roughness, sampler);

                var indirectLight = app.Ibl.IndirectLight;
                indirectLight.Intensity = 100000;
                indirectLight.Rotation = Matrix4x4.CreateFromAxisAngle(Vector3.UnitY, 0.5f);

                // Add geometry into the scene.
                mesh = MeshReader.LoadFromBuffer(engine, monkeyData.LoadSuzanne(), materialInstance);
                var ti = tcm.GetInstance(mesh.Renderable);
                transform = tcm.GetWorldTransform(ti) * Matrix4x4.CreateTranslation(0, 0, -4);

                rcm.SetCastShadows(rcm.GetInstance(mesh.Renderable), false);
                scene.AddEntity(mesh.Renderable);
                tcm.SetTransform(ti, transform);
            };

            app.Cleanup = (engine, view, scene) => {
                engine.Destroy(materialInstance);
                engine.Destroy(mesh.Renderable);
                engine.Destroy(material);
                engine.Destroy(albedo);
                engine.Destroy(normal);
                engine.Destroy(roughness);
                engine.Destroy(metallic);
                engine.Destroy(ao);
            };

            app.Run();
        }

        private static Texture LoadNormalMap(Engine engine, byte[] normals)
        {
            var imageResult = ImageResult.FromMemory(normals, ColorComponents.RedGreenBlue);

            var normalMap = TextureBuilder.Create()
                .WithWidth(imageResult.Width)
                .WithHeight(imageResult.Height)
                .WithLevels(0xFF)
                .WithFormat(TextureFormat.Rgb8)
                .Build(engine);

            var descriptor = new PixelBufferDescriptor(imageResult.Data, PixelDataFormat.Rgb, PixelDataType.UByte);

            normalMap.SetImage(engine, 0, descriptor);
            normalMap.GenerateMipmaps(engine);

            return normalMap;
        }
    }
}
