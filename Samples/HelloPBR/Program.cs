using System;
using Filament;
using Filament.CameraUtilities;
using Filament.DemoApp;
using Filament.MeshIO;
using Filament.SampleData;
using OpenTK.Mathematics;

namespace HelloPBR
{
    class Program
    {
        const string IBL_FOLDER = "default_env";

        static void Main(string[] args)
        {
            int lightEntity = -1;
            Material material = null;
            MaterialInstance materialInstance = null;
            Mesh mesh = null;
            Matrix4 transform = Matrix4.Identity;

            var app = new Application(
                new WindowConfig() {
                    Title = "hellopbr",
                },
                new ApplicationConfig() {
                    IblDirectory = IBL_FOLDER
                }
            );

            app.Setup = (engine, view, scene) => {
                var tcm = engine.TransformManager;
                var rcm = engine.RenderableManager;

                var sampleData = new SampleDataLoader();
                var monkeyData = new MonkeyDataLoader();

                // Instantiate material.
                material = MaterialBuilder.Create()
                    .WithPackage(sampleData.LoadAiDefaultMat())
                    .Build(engine);

                materialInstance = material.CreateInstance();
                materialInstance.SetParameter("baseColor", RgbType.Linear, new Color(0.8f, 0.8f, 0.8f));
                materialInstance.SetParameter("metallic", 1.0f);
                materialInstance.SetParameter("roughness", 0.4f);
                materialInstance.SetParameter("reflectance", 0.5f);

                // Add geometry into the scene.
                mesh = MeshReader.LoadFromBuffer(engine, monkeyData.LoadSuzanne(), materialInstance);

                var ti = tcm.GetInstance(mesh.Renderable);

                transform = Matrix4.CreateTranslation(0, 0, -4) * tcm.GetWorldTransform(ti);
                rcm.SetCastShadows(rcm.GetInstance(mesh.Renderable), false);
                scene.AddEntity(mesh.Renderable);

                // Add light sources into the scene.
                lightEntity = EntityManager.Create();

                LightBuilder.Create(LightType.Sun)
                    .WithColor(Color.ToLinearAccurate(new sRGBColor(0.98f, 0.92f, 0.89f)))
                    .WithIntensity(110000)
                    .WithDirection(new Vector3(0.7f, -1, -0.8f))
                    .WithSunAngularRadius(1.9f)
                    .WithCastShadows(false)
                    .Build(engine, lightEntity);

                scene.AddEntity(lightEntity);
            };

            app.Cleanup = (engine, view, scene) => {
                engine.Destroy(lightEntity);
                engine.Destroy(materialInstance);
                engine.Destroy(mesh.Renderable);
                engine.Destroy(material);
            };

            app.Animate = (engine, view, now) => {
                var tcm = engine.TransformManager;
                var ti = tcm.GetInstance(mesh.Renderable);

                tcm.SetTransform(ti, Matrix4.CreateFromAxisAngle(Vector3.UnitY, now) * transform);
            };

            app.Run();
        }
    }
}
