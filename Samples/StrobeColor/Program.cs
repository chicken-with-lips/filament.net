using Filament;
using Filament.DemoApp;
using OpenTK.Mathematics;

namespace StrobeColor
{
    class Program
    {
        static void Main(string[] args)
        {
            Skybox skybox = null;

            var app = new Application(
                new WindowConfig() {
                    Title = "strobecolor",
                },
                new ApplicationConfig()
            );

            app.Setup += (engine, view, scene) => {
                skybox = SkyboxBuilder.Create()
                    .WithColor(new Color(0f, 0.25f, 0.5f, 1.0f))
                    .Build(engine);

                scene.Skybox = skybox;
                view.PostProcessingEnabled = false;
            };

            app.Animate += (engine, view, now) => {
                const float SPEED = 4;
                float r = 0.5f + 0.5f * (float) MathHelper.Sin(SPEED * now);
                float g = 0.5f + 0.5f * (float) MathHelper.Sin(SPEED * now + MathHelper.Pi * 2 / 3);
                float b = 0.5f + 0.5f * (float) MathHelper.Sin(SPEED * now + MathHelper.Pi * 4 / 3);

                skybox.Color = new Color(r, g, b, 1.0f);
            };

            app.Run();
        }
    }
}
